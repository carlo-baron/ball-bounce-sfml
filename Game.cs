using SFML.Graphics;
using SFML.System;
using SFML.Window;
// using System.Drawing;
class Application : VectorCalc
{
    uint WindowHeight;
    uint WindowWidth;
    string WindowName;
    RenderWindow window;
    
    List<Drawable> instantiatedShapes = new List<Drawable>();

    bool hasGravity = false;
    Clock clock = new Clock();

    Random random = new Random();
    public Application(uint windowHeight, uint windowWidth, string windowName)
    {
        WindowHeight = windowHeight;
        WindowWidth = windowWidth;
        WindowName = windowName;

        VideoMode mode = new VideoMode(WindowWidth, WindowHeight);
        window = new RenderWindow(mode, WindowName, Styles.Close);

        #region General Setting
        window.SetVerticalSyncEnabled(true);
        window.SetKeyRepeatEnabled(false);
        window.SetFramerateLimit(60);
        #endregion

        #region events
        window.Closed += (sender, args) => window.Close();
        window.MouseButtonPressed += (sender, args) =>
        {
            if (args.Button == Mouse.Button.Right)
            {
                hasGravity = !hasGravity;
            }
        };
        window.MouseButtonPressed += (sender, args) =>
        {
            if (args.Button == Mouse.Button.Left)
            {
                Ball ball = new Ball(random.Next(10, 25))
                {
                    FillColor = new Color((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)),
                    Velocity = new Vector2f(100f, 100f),
                };
                ball.Origin = new Vector2f(ball.Radius, ball.Radius);
                ball.Position = RepositionCircleToInBounds(ball);

                instantiatedShapes.Add(ball);
            }
        };
        #endregion

        #region Shapes
        CircleShape dot = new CircleShape(5)
        {
            FillColor = Color.Blue,
            Origin = new Vector2f(5, 5)
        };
        #endregion

        MainLoop(dot);
    }

    void MainLoop(CircleShape dot)
    {
        while (window.IsOpen)
        {
            float deltaTime = clock.Restart().AsSeconds();

            window.DispatchEvents();
            window.Clear();

            foreach (Ball shape in instantiatedShapes)
            {
                (bool isColliding, Vector2f newDirection) = IsColliding(shape);

                if (isColliding)
                {
                    shape.Velocity = new Vector2f(shape.Velocity.X * newDirection.X, shape.Velocity.Y * newDirection.Y);
                }

                if (hasGravity)
                {
                    shape.Position += shape.Velocity * deltaTime;
                }

                window.Draw(shape);
            }

            window.Display();
        }

    }

    Vector2f RepositionCircleToInBounds(CircleShape circle)
    {
        int randomX = random.Next((int)circle.Radius, (int)window.Size.X - (int)circle.Radius);
        int randomY = random.Next((int)circle.Radius, (int)WindowHeight - (int)circle.Radius);

        return new Vector2f(randomX, randomY);
    }

    float[] WindowBounds()
    {
        float top = 0;
        float left = 0;
        float bottom = window.Size.Y;
        float right = window.Size.X;

        return new float[] { top, right, bottom, left };
    }


    (bool, Vector2f) IsColliding(Ball shape)
    {
        float[] windowBounds = WindowBounds();

        Vector2f[] circleBounds = BallBounds.Bounds(shape);

        if (circleBounds[2].Y > windowBounds[2] || circleBounds[0].Y < windowBounds[0])
        {
            return (true, new Vector2f(1f, -1f));
        }
        else if (circleBounds[3].X < windowBounds[3] || circleBounds[1].X > windowBounds[1])
        {
            return (true, new Vector2f(-1f, 1f));
        }


        return (false, new Vector2f(1f, 1f));
    }

}