using SFML.Graphics;
using SFML.System;
public abstract class BallBounds{
    public static Vector2f[] Bounds(CircleShape shape)
    {
        Vector2f left = new Vector2f(shape.Position.X - shape.Radius, shape.Position.Y);
        Vector2f top = new Vector2f(shape.Position.X, shape.Position.Y - shape.Radius);
        Vector2f right = new Vector2f(shape.Position.X + shape.Radius, shape.Position.Y);
        Vector2f bottom = new Vector2f(shape.Position.X, shape.Position.Y + shape.Radius);

        return [top, right, bottom, left];
    }
}