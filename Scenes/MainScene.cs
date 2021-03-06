using Colors_Switch.GameObjects;
using Colors_Switch.Logic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Colors_Switch.Scenes
{
    public class MainScene : GameLoop
    {
        public const uint DEFAULT_WINDOW_WIDTH = 1000;
        public const uint DEFAULT_WINDOW_HEIGHT = 1000;
        public const string DEFAULT_WINDOW_TITLE = "Colors Switch";
        public const string DEFAULT_WINDOW_BACKGROUND = "./assets/background.png";

        private Player player;
        private Score score;
        private Sprite backgroundSprite;
        private Clock spawnTime;
        private Menu menu;

        private float delay = 3.5f;
        private int bulletVelocity = 90;

        private List<Bullet> bullets = new List<Bullet>();
        private Bullet? toRemove = null;

        public MainScene() : base(DEFAULT_WINDOW_WIDTH, DEFAULT_WINDOW_HEIGHT, DEFAULT_WINDOW_TITLE)
        {
        }

        public override void LoadContent()
        {
            backgroundSprite = new Sprite();
            backgroundSprite.Texture = new Texture(DEFAULT_WINDOW_BACKGROUND);

            DebugInfo.LoadContent();

            player = new Player();
            player.LoadContent(this);

            score = new Score();
            score.LoadContent(this);

            menu = new Menu();
            menu.LoadContent(this);
        }

        public override void Initialize()
        {
            spawnTime = new Clock();
        }

        public override void Update(GameTime gameTime)
        {
            player.Rotate(Mouse.GetPosition(this.window), gameTime);
            score.Rotate(Mouse.GetPosition(this.window), gameTime);

            foreach (Bullet bullet in bullets)
            {
                bullet.Rotate(this);

                if (CollisionTester.PixelPerfectTest(player.playerSprite, bullet.bulletSprite, 200))
                {
                    if (score.CheckColors(CollisionTester.firstCollisionColor, CollisionTester.secondCollisionColor))
                    {
                        menu.UpdateScoreText(score.score);

                        if (delay >= 0.8f)
                            delay -= 0.05f;

                        bulletVelocity += 3;
                    }
                    else
                    {
                        delay = 3.5f;
                        bulletVelocity = 90;
                    }

                    toRemove = bullet;
                }

                bullet.Move(this, bulletVelocity);
            }

            if (toRemove != null)
            {
                bullets.Remove(toRemove);
                toRemove = null;
            }

            if (gameTime.timeScale == 1)
            {
                if (spawnTime.ElapsedTime.AsSeconds() > delay)
                {
                    bullets.Add(new Bullet(this));
                    spawnTime.Restart();
                }
            }
            else
            {
                menu.CheckHovers(Mouse.GetPosition(this.window));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.window.Draw(backgroundSprite);

            if (gameTime.timeScale == 1)
            {
                score.DrawScore(this);
                player.Draw(this);

                foreach (Bullet bullet in bullets)
                    bullet.Draw(this);
            }
            else
            {
                menu.Draw(this);
            }

            DebugInfo.DrawPerformaceData(this, Color.White, delay, bulletVelocity);
        }

        public override void MenuClick(Vector2i mousePosition)
        {
            menu.MenuClick(this, mousePosition);
        }
    }
}