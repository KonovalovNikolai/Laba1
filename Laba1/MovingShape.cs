using SFML.Graphics;
using SFML.System;

class MovingShape
{
    public Shape Shape { get; init; }

    public MovingShape(Shape shape, Vector2f velocity)
    {
        Shape = shape;
        Velocity = velocity;
    }

    public Vector2f Velocity { get; set; }

    public Vector2f Position
    {
        get => Shape.Position;
        set { Shape.Position = value; }
    }

    public Color Color
    {
        get => Shape.FillColor;
        set { Shape.FillColor = value; }
    }
}