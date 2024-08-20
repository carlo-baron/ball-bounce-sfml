using System.Globalization;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Application{
    uint WindowHeight;
    uint WindowWidth;
    string WindowName;
    List<Drawable> instantiatedShapes = new List<Drawable>();
    // Clock clock = new Clock();
    // float moveSpeed = 300;
    public Application(uint windowHeight, uint windowWidth, string windowName){
        WindowHeight = windowHeight;
        WindowWidth = windowWidth;
        WindowName = windowName;

        VideoMode mode = new VideoMode(WindowWidth, WindowHeight);
        RenderWindow window = new RenderWindow(mode, WindowName, Styles.Close);

        #region General Setting
        window.SetVerticalSyncEnabled(true);
        window.SetKeyRepeatEnabled(false);
        window.SetFramerateLimit(60);
        #endregion

        #region events
        window.Closed += (sender, args) => window.Close();
        window.MouseButtonPressed += (sender, args) => {if(args.Button == Mouse.Button.Left){
                Random random = new Random();
                CircleShape newCircle = new CircleShape(10){
                    FillColor = Color.Green,
                    Position = new Vector2f(random.Next((int)WindowHeight), random.Next((int)WindowWidth)),
                };
                newCircle.Origin = new Vector2f(newCircle.Radius, newCircle.Radius);
                instantiatedShapes.Add(newCircle);

                window.Draw(newCircle);
            }
        };
        #endregion

        #region Shapes
        
        #endregion

        MainLoop(window);
    }

    void MainLoop(RenderWindow window){
        while(window.IsOpen){
            window.DispatchEvents();
            window.Clear();

            foreach(Drawable shape in instantiatedShapes){
                window.Draw(shape);
                Console.WriteLine(shape);
            }

            window.Display();
        }
    }
}