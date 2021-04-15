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
    public static class DebugInfo
    {
        public const string DEBUG_FONT_PATH = "./cour.ttf";

        public static Font consoleFont;
        
        public static void LoadContent()
        {
            consoleFont = new Font(DEBUG_FONT_PATH);
        }

        public static void DrawPerformaceData(GameLoop gameLoop, Color fontColor, float delay)
        {
            if (consoleFont == null)
                return;

            string totalTimeElapsed = gameLoop.gameTime.totalTimeElapsed.ToString("0.000");
            string deltaTime = gameLoop.gameTime.deltaTime.ToString("0.00000");
            float fps = 1f / gameLoop.gameTime.deltaTime;
            string fprStr = fps.ToString("0.00");

            Text text1 = new Text(totalTimeElapsed, consoleFont, 14);
            text1.Position = new Vector2f(4f, 8f);
            text1.FillColor = fontColor;

            Text text2 = new Text(deltaTime, consoleFont, 14);
            text2.Position = new Vector2f(4f, 28f);
            text2.FillColor = fontColor;

            Text text3 = new Text(fprStr, consoleFont, 14);
            text3.Position = new Vector2f(4f, 48f);
            text3.FillColor = fontColor;

            Text text4 = new Text(delay.ToString(), consoleFont, 14);
            text4.Position = new Vector2f(4f, 68f);
            text4.FillColor = fontColor;

            gameLoop.window.Draw(text1);
            gameLoop.window.Draw(text2);
            gameLoop.window.Draw(text3);
            gameLoop.window.Draw(text3);
        }
    }
}
