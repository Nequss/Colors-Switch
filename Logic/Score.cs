using System;
using System.Collections.Generic;
using System.Text;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.IO;

namespace Colors_Switch.Logic
{
    public class Score
    {
        const string SCORE_FONT_PATH = "./font.ttf";

        private Font scoreFont;
        public int score = 0;
        Text scoreText;

        public Score()
        {
            
        }

        public void LoadContent(GameLoop gameLoop)
        {
            scoreFont = new Font(SCORE_FONT_PATH);
            scoreText = new Text(score.ToString(), scoreFont, 80);
            scoreText.Origin = new Vector2f(scoreText.CharacterSize / 2, scoreText.CharacterSize / 2 + 7);
            scoreText.Position = new Vector2f(gameLoop.window.Size.X / 2, gameLoop.window.Size.Y / 2);
            scoreText.FillColor = Color.White;
        }

        public bool CheckColors(Color first, Color second)
        {
            int dR = Math.Abs(first.R - second.R);
            int dG = Math.Abs(first.G - second.G);
            int dB = Math.Abs(first.B - second.B);

            if ((dR + dG + dB) < 30)
            {
                score++;
                return true;
            }
            else
            {
                score = 0;
                return false;
            }
        }

        public void Rotate(Vector2i mousePosition, GameTime gameTime)
        {
            float dY = mousePosition.Y - scoreText.Position.Y;
            float dX = mousePosition.X - scoreText.Position.X;
            float angle = (float)(Math.Atan2(dY, dX) * 180 / Math.PI);

            scoreText.Rotation = angle + 90;
        }

        public void DrawScore(GameLoop gameLoop)
        {
            if (scoreFont == null)
                return;

            scoreText.DisplayedString = score.ToString();

            if(score >= 10)
            {
                scoreText.CharacterSize = 50;
                scoreText.Origin = new Vector2f(scoreText.CharacterSize / 2 + 25, scoreText.CharacterSize / 2 + 7);
            }

            gameLoop.window.Draw(scoreText);
        }
    }
}
