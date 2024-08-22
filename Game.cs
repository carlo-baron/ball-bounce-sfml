using SFML.Graphics;
using SFML.System;
using SFML.Window;
class Application : VectorCalc
{
    uint WindowHeight;
    uint WindowWidth;
    string WindowName;
    private RenderWindow window;
    List<Drawable> instantiatedShapes = new List<Drawable>();

    bool hasGravity = false;
    Clock clock = new Clock();
    // float moveSpeed = 300;
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
                CircleShape newCircle = new CircleShape(10)
                {
                    FillColor = Color.Green,
                };
                newCircle.Origin = new Vector2f(newCircle.Radius, newCircle.Radius);
                newCircle.Position = RepositionCircleToInBounds(newCircle);

                instantiatedShapes.Add(newCircle);
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
        Vector2f velocity = new Vector2f(100f,100f);

        while (window.IsOpen)
        {
            float deltaTime = clock.Restart().AsSeconds();

            window.DispatchEvents();
            window.Clear();

            foreach (CircleShape shape in instantiatedShapes)
            {
                (bool isColliding, Vector2f newDirection) = IsColliding(shape);

                if (isColliding)
                {
                    velocity = -velocity;
                }

                if (hasGravity)
                {
                    shape.Position += velocity * deltaTime;
                }

                window.Draw(shape);
            }

            window.Display();
        }

    }

    Vector2f RepositionCircleToInBounds(CircleShape circle)
    {
        Random random = new Random();

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


    (bool, Vector2f) IsColliding(CircleShape shape)
    {
        float[] windowBounds = WindowBounds();

        Vector2f[] circleBounds = BallBounds.Bounds(shape);

        // return circleBounds[2].Y > windowBounds[2] ||
        //        circleBounds[0].Y < windowBounds[0] ||
        //        circleBounds[3].X < windowBounds[3] ||
        //        circleBounds[1].X > windowBounds[1];

        if(circleBounds[2].Y > windowBounds[2]){
            return (true, new Vector2f(-1f, -1f));
        }else if(circleBounds[0].Y < windowBounds[0]){
            return(true, new Vector2f(-1f, -1f));
        }else if( circleBounds[3].X < windowBounds[3]){
            return(true, new Vector2f(-1f, 1f));
        }else if(circleBounds[1].X > windowBounds[1]){
            return(true, new Vector2f(-1f, 1f));
        }

        return (true, new Vector2f(1f, 1f));
    }

}