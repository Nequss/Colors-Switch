using Colors_Switch.Logic;
using SFML.Graphics;
using SFML.System;
using System;

namespace Colors_Switch.GameObjects
{
    public class Menu
    {
        public const string MENU_PATH = "./assets/box.png";
        private const string MENU_FONT_PATH = "./font.ttf";

        private Font menuFont;

        public Text bestScoreText;
        public Text scoreText;
        public Text playText;
        public Text exitText;

        public Sprite menu;

        private Color red = new Color(232, 76, 61);
        private Color blue = new Color(53, 152, 219);
        private Color yellow = new Color(241, 196, 15);
        private Color green = new Color(45, 204, 112);

        public Menu()
        {

        }

        public void LoadContent(GameLoop gameLoop)
        {
            menu = new Sprite();
            menu.Texture = new Texture(MENU_PATH);
            menu.Texture.Smooth = true;
            menu.Origin = new Vector2f(menu.Texture.Size.X / 2, menu.Texture.Size.Y / 2);
            menu.Position = new Vector2f(gameLoop.window.Size.X / 2, gameLoop.window.Size.Y / 2);

            menuFont = new Font(MENU_FONT_PATH);

            bestScoreText = new Text("Best score", menuFont, 50);
            bestScoreText.Origin = new Vector2f(bestScoreText.CharacterSize * bestScoreText.DisplayedString.Length / 2, bestScoreText.CharacterSize / 2 + 7);
            bestScoreText.Position = new Vector2f(gameLoop.window.Size.X / 2, gameLoop.window.Size.Y / 2 - 150);
            bestScoreText.FillColor = Color.White;

            scoreText = new Text("0", menuFont, 50);
            scoreText.Origin = new Vector2f(scoreText.CharacterSize * scoreText.DisplayedString.Length / 2, scoreText.CharacterSize / 2 + 7);
            scoreText.Position = new Vector2f(gameLoop.window.Size.X / 2, gameLoop.window.Size.Y / 2 - 50);
            scoreText.FillColor = Color.White;

            playText = new Text("Play", menuFont, 50);
            playText.Origin = new Vector2f(playText.CharacterSize * playText.DisplayedString.Length / 2, playText.CharacterSize / 2 + 7);
            playText.Position = new Vector2f(gameLoop.window.Size.X / 2, gameLoop.window.Size.Y / 2 + 50);
            playText.FillColor = Color.White;

            exitText = new Text("Exit", menuFont, 50);
            exitText.Origin = new Vector2f(exitText.CharacterSize * exitText.DisplayedString.Length / 2, exitText.CharacterSize / 2 + 7);
            exitText.Position = new Vector2f(gameLoop.window.Size.X / 2, gameLoop.window.Size.Y / 2 + 150);
            exitText.FillColor = Color.White;
        }

        public void MenuClick(GameLoop gameLoop, Vector2i mousePosition)
        {
            if (playText.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
                gameLoop.gameTime.timeScale = 1;

            if (exitText.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
                gameLoop.window.Close();
        }

        public void CheckHovers(Vector2i mousePosition)
        {
            if (bestScoreText.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
                bestScoreText.FillColor = red;
            else
                bestScoreText.FillColor = Color.White;

            if (scoreText.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
                scoreText.FillColor = blue;
            else
                scoreText.FillColor = Color.White;

            if (playText.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
                playText.FillColor = yellow;
            else
                playText.FillColor = Color.White;

            if (exitText.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
                exitText.FillColor = green;
            else
                exitText.FillColor = Color.White;
        }

        public void UpdateScoreText(int score)
        {
            scoreText.Origin = new Vector2f(scoreText.CharacterSize * scoreText.DisplayedString.Length / 2, scoreText.CharacterSize / 2 + 7);

            int s = Int32.Parse(scoreText.DisplayedString);

            scoreText.DisplayedString = s > score ? s.ToString() : score.ToString();
        }

        public void Draw(GameLoop gameLoop)
        {
            if (menu.Texture == null)
                return;

            gameLoop.window.Draw(menu);
            gameLoop.window.Draw(bestScoreText);
            gameLoop.window.Draw(scoreText);
            gameLoop.window.Draw(playText);
            gameLoop.window.Draw(exitText);
        }
    }
}