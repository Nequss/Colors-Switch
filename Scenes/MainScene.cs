using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Colors_Switch.Logic;
using Colors_Switch.GameObjects;

namespace Colors_Switch.Scenes
{
    public class MainScene : GameLoop
    {
        public const uint   DEFAULT_WINDOW_WIDTH  = 1000;
        public const uint   DEFAULT_WINDOW_HEIGHT = 1000;
        public const string DEFAULT_WINDOW_TITLE  = "Colors Switch";
        public const string DEFAULT_WINDOW_BACKGROUND = "./assets/background.png";

        Player player;
        Score score;
        Sprite backgroundSprite;
        Clock spawnTime;
        Menu menu;
        
        float delay = 3.5f;
        int bulletVelocity = 90;

        List<Bullet> bullets = new List<Bullet>();
        Bullet? toRemove = null;

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

                        delay -= 0.025f;
                        bulletVelocity += 2;
                    }
                    else
                    {
                        delay = 5.0f;
                        bulletVelocity = 70;
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
    }
}
