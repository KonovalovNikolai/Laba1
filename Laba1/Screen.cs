using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Screen
{
    private RenderWindow _window;
    private FramerateCounter _fps;

    private Font _font;

    private Text _fpsText;
    private List<MovingShape> _shapes;


    public Color BackgroundColor { get; set; } = Color.Black;

    public Screen(uint fpsLimit, List<MovingShape> shapes)
    {
        _shapes = shapes;

        _InitWindow(fpsLimit);
        _fps = new FramerateCounter(1f);

        _font = new Font("ArialMT.ttf");
        _fpsText = new Text(_fps.ToString(), _font, 14);
        _fpsText.FillColor = Color.Red;
    }

    public void Run()
    {
        var clock = new Clock();
        while (_window.IsOpen)
        {            
            var elapsed = clock.Restart();
            var deltaTime = elapsed.AsSeconds();

            _window.DispatchEvents();

            _Clear();
            _Update(deltaTime);
            _Draw();
        }
    }

    private void _Draw()
    {
        foreach (var shape in _shapes)
        {
            _window.Draw(shape.Shape);
        }

        _window.Draw(_fpsText);
        _window.Display();
    }

    private void _Update(float deltaTime)
    {
        _fps.Update(deltaTime);
        _fpsText.DisplayedString = _fps.ToString();
        _UpdateShapes(deltaTime);
    }

    private void _Clear()
    {
        var rect = new RectangleShape();
        rect.FillColor = BackgroundColor;

        var boundsList = new List<FloatRect>(_shapes.Count + 1) {
            _fpsText.GetGlobalBounds()
        };

        boundsList.AddRange(
            _shapes.Select(s => s.Shape.GetGlobalBounds()).ToList()
        );

        foreach (var bound in boundsList)
        {
            var size = new Vector2f(bound.Width, bound.Height);
            var position = new Vector2f(bound.Left, bound.Top);

            rect.Position = position;
            rect.Size = size;

            _window.Draw(rect);
        }
    }

    private void _UpdateShapes(float deltaTime)
    {
        foreach (var shape in _shapes)
        {
            var bounds = shape.Shape.GetGlobalBounds();

            if (bounds.Left + bounds.Width > _window.Size.X || bounds.Left < 0)
            {
                var newVelocity = shape.Velocity;
                newVelocity.X *= -1;

                shape.Velocity = newVelocity;
            }

            if (bounds.Top + bounds.Height > _window.Size.Y || bounds.Top < 0)
            {
                var newVelocity = shape.Velocity;
                newVelocity.Y *= -1;

                shape.Velocity = newVelocity;
            }

            shape.Position += shape.Velocity * deltaTime;

            float xColorPos = 255 * shape.Position.X / _window.Size.X;
            float yColorPos = 255 * shape.Position.Y / _window.Size.Y;
            float xyColorPos = xColorPos + yColorPos / 2;
            shape.Color = new Color((byte)(xColorPos), (byte)(yColorPos), 99);
        }
    }

    private void _InitWindow(uint fpsLimit)
    {
        var settings = new ContextSettings();
        settings.AntialiasingLevel = 8;

        var mode = new VideoMode(1080, 720);

        _window = new RenderWindow(mode, "Laba 1", Styles.Fullscreen, settings);
        _window.SetFramerateLimit(fpsLimit);
        _window.SetVerticalSyncEnabled(false);

        _window.Closed += (_, _) => _window.Close();

        _window.KeyPressed += (_, key) =>
        {
            if (key.Code == Keyboard.Key.Q)
            {
                _window.Close();
            }
        };
    }
}