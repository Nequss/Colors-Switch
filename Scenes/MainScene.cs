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
        public const uint   DEFAULT_WINDOW_WIDTH  = 800;
        public const uint   DEFAULT_WINDOW_HEIGHT = 800;
        public const string DEFAULT_WINDOW_TITLE  = "SCENE 1";
        public const string DEFAULT_WINDOW_BACKGROUND = "./assets/background.png";

        Player player;
        Score score;
        Sprite backgroundSprite;
        Clock spawnTime;

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
            score.LoadContent();
        }

        public override void Initialize()
        {
            spawnTime = new Clock();
            bullets.Add(new Bullet(this));
        }

        public override void Update(GameTime gameTime)
        {
            player.Rotate(Mouse.GetPosition(this.window));

            foreach (Bullet bullet in bullets)
            {
                bullet.Rotate(this);

                if (CollisionTester.PixelPerfectTest(player.playerSprite, bullet.bulletSprite, 200))
                {
                    score.CheckColors(CollisionTester.firstCollisionColor, CollisionTester.secondCollisionColor);

                    toRemove = bullet;
                }

                bullet.Move(this, 70);
            }

            if (toRemove != null)
            {
                bullets.Remove(toRemove);
                toRemove = null;
            }

            if (spawnTime.ElapsedTime.AsSeconds() > 2)
            {
                bullets.Add(new Bullet(this));
                spawnTime.Restart();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.window.Draw(backgroundSprite);
            player.Draw(this);

            foreach (Bullet bullet in bullets)
                bullet.Draw(this);

            score.DrawScore(this);

            DebugInfo.DrawPerformaceData(this, Color.White);
        }
    }
}
