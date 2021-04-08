using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Colors_Switch.Logic
{
    public abstract class GameLoop
    {
        public const int TARGET_FPS = 60;
        public const float TIME_UNTIL_UPDATE = 1f / TARGET_FPS;

        public RenderWindow window 
        { 
            get; 
            protected set; 
        }

        public GameTime gameTime 
        {
            get;
            protected set;
        }

        protected GameLoop(uint windowWidth, uint windowHeight, string windowTitle)
        {

            window = new RenderWindow(new VideoMode(windowWidth, windowHeight), windowTitle);
            gameTime = new GameTime();

            window.Closed += WindowClosed;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            window.Close();
        }

        public void Run()
        {
            LoadContent();
            Initialize();

            float totalTimeBeforeUpdate = 0f;
            float previousTimeElapsed = 0f;
            float deltaTime = 0f;
            float totalTimeElapsed = 0f;

            Clock clock = new Clock();

            while (window.IsOpen)
            {
                window.DispatchEvents();

                totalTimeElapsed = clock.ElapsedTime.AsSeconds();
                deltaTime = totalTimeElapsed - previousTimeElapsed;
                previousTimeElapsed = totalTimeElapsed;

                totalTimeBeforeUpdate += deltaTime;

                if(totalTimeBeforeUpdate >= TIME_UNTIL_UPDATE)
                {
                    gameTime.Update(totalTimeBeforeUpdate, clock.ElapsedTime.AsSeconds());
                    totalTimeBeforeUpdate = 0;

                    Update(gameTime);

                    window.Clear();
                    Draw(gameTime);
                    window.Display();
                }
            }
        }

        public abstract void LoadContent();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
