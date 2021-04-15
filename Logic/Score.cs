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
        const string SCORE_FONT_PATH = "./cour.ttf";

        private Font scoreFont;
        private int score;

        public Score()
        {
            
        }

        public void LoadContent()
        {
            scoreFont = new Font(SCORE_FONT_PATH);
        }

        public void CheckColors(Color first, Color second)
        {
            int dR = Math.Abs(first.R - second.R);
            int dG = Math.Abs(first.G - second.G);
            int dB = Math.Abs(first.B - second.B);

            if ((dR + dG + dB) < 30)
                score++;
        }

        public void DrawScore(GameLoop gameLoop)
        {
            if (scoreFont == null)
                return;

            Text scoreText = new Text(score.ToString(), scoreFont, 50);

            float x = gameLoop.window.Size.X - 100;
            float y = 100;

            scoreText.Position = new Vector2f(x, y);
            scoreText.FillColor = Color.White;

            gameLoop.window.Draw(scoreText);
        }
    }
}
