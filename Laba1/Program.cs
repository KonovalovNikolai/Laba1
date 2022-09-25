using SFML.Graphics;
using SFML.System;
using SFML.Window;

var shapes = new List<MovingShape> {
    new MovingShape(new CircleShape(200f, 8), new Vector2f(200f, 200f)),
    new MovingShape(new CircleShape(80f), new Vector2f(30f * 2, 100f * 2)),
    new MovingShape(new RectangleShape(new Vector2f(40f * 2, 60f * 2)), new Vector2f(80f * 2, 80f * 2)),
};

var screen = new Screen(144, shapes);
screen.Run();
