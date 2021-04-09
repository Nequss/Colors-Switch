using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Colors_Switch.Logic;

namespace Colors_Switch.GameObjects
{
    public class Bullet
    {
        private readonly string[] BULLET_PATHS = { "./assets/red.png", "./assets/yellow.png", "./assets/green.png", "./assets/blue.png", };
        
        public Sprite bulletSprite;

        public Bullet(GameLoop gameLoop)
        {
            bulletSprite = new Sprite();
            bulletSprite.Texture = new Texture(BULLET_PATHS[new Random().Next(4)]);
            bulletSprite.Texture.Smooth = true;
            bulletSprite.Origin = new Vector2f(bulletSprite.Texture.Size.X / 2, bulletSprite.Texture.Size.Y / 2);

            float r = gameLoop.window.Size.X * (float)Math.Sqrt(2) / 2;
            float x = r * (float)Math.Cos(new Random().Next(361));
            float y = r * (float)Math.Sin(new Random().Next(361));

            bulletSprite.Position = new Vector2f(x, y);
        }

        public void Move(GameLoop gameLoop, float velocity)
        {
            float x = (float)Math.Cos(bulletSprite.Rotation) * gameLoop.gameTime.deltaTime * velocity;
            float y = (float)Math.Sin(bulletSprite.Rotation) * gameLoop.gameTime.deltaTime * velocity;

            Vector2f newPosition = new Vector2f(bulletSprite.Position.X + x, bulletSprite.Position.Y + y);
            bulletSprite.Position = newPosition;
        }
        public void Rotate(GameLoop gameLoop)
        {
            float dY = gameLoop.window.Size.Y / 2 - bulletSprite.Position.Y;
            float dX = gameLoop.window.Size.X / 2 - bulletSprite.Position.X;
            float angle = (float)(Math.Atan2(dY, dX) * 180 / Math.PI);

            bulletSprite.Rotation = angle;
        }

        public void Draw(GameLoop gameLoop)
        {
            if (bulletSprite.Texture == null)
                return;

            gameLoop.window.Draw(bulletSprite);
        }
    }
}
