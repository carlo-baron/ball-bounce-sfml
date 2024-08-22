using SFML.Graphics;
using SFML.System;
using System;

class Ball : CircleShape{
    public Vector2f Velocity { get; set; }
    public Ball(float radius) : base(radius){

    }
}