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
    public class Player
    {
        public const string PLAYER_SQUARE_PATH = "./assets/square.png";

        public Sprite playerSprite;
        public Player()
        {

        }

        public void LoadContent(GameLoop gameLoop)
        {
            playerSprite = new Sprite();
            playerSprite.Texture = new Texture(PLAYER_SQUARE_PATH);
            playerSprite.Texture.Smooth = true;
            playerSprite.Origin = new Vector2f(playerSprite.Texture.Size.X / 2, playerSprite.Texture.Size.Y / 2);
            playerSprite.Position = new Vector2f(gameLoop.window.Size.X / 2, gameLoop.window.Size.Y / 2);
        }

        public void Rotate(Vector2i mousePosition, GameTime gameTime)
        {
            float dY = mousePosition.Y - playerSprite.Position.Y;
            float dX = mousePosition.X - playerSprite.Position.X;
            float angle = (float)(Math.Atan2(dY, dX) * 180 / Math.PI);

            playerSprite.Rotation = angle + 90;
        }

        public void Draw(GameLoop gameLoop)
        {
            if (playerSprite.Texture == null)
                return;

            gameLoop.window.Draw(playerSprite);
        }
    }
}
