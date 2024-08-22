using SFML;
using SFML.System;
using System;
using System.Reflection.Metadata.Ecma335;

public abstract class VectorCalc{
    protected float Magnitude(Vector2f vector){
        return MathF.Sqrt(MathF.Pow(vector.X, 2) + MathF.Pow(vector.Y, 2));
    }

    protected Vector2f Normalize(Vector2f vector){
        float magnitude = Magnitude(vector);
        if(magnitude == 0){
            return new Vector2f(0,0);
        }

        return new Vector2f(vector.X / magnitude, vector.Y / magnitude);
    }

    protected Vector2f Normal(Vector2f vertex1, Vector2f vertex2){
        return new Vector2f(vertex2.Y - vertex1.Y, vertex2.X - vertex1.X);
    }
}