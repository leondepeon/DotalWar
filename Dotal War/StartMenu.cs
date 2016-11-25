using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Dotal_War
{
    public class StartMenu
    {
        Game1 myGame;
        GlobalVariables globalVariables;

        Texture2D PlayButton;
        Texture2D QuitButton;
        Texture2D OptionButton;

        Rectangle PlayRect;
        Rectangle QuitRect;
        Rectangle OptionRect;
        Rectangle ReleaseNotes;

        SpriteFont VersionFont;
        string DisplayText;
        Vector2 VersionPosition;

        ButtonState LeftPrevious;

        public StartMenu(Game1 myGame)
        {
            this.myGame = myGame;
            globalVariables = myGame.globalVariables;
            VersionFont = myGame.Content.Load<SpriteFont>(@"Fonts\VersionFont");
            VersionPosition = new Vector2(globalVariables.WindowWidth * 3/ 4, globalVariables.WindowHeight* 9 / 10);
            DisplayText = "Current Version: " + globalVariables.CurrentVersion;
            ReleaseNotes = new Rectangle((int)(VersionPosition.X), (int)(VersionPosition.Y), 250, 10);

            PlayButton = myGame.Content.Load<Texture2D>(@"Buttons\PlayB");
            QuitButton = myGame.Content.Load<Texture2D>(@"Buttons\QuitB");
            OptionButton = myGame.Content.Load<Texture2D>(@"Buttons\OptB");

            PlayRect = new Rectangle(globalVariables.WindowWidth / 4 - PlayButton.Width / 2, globalVariables.WindowHeight*3 / 4 - PlayButton.Height / 2, 
                PlayButton.Width, PlayButton.Height);

            QuitRect = new Rectangle(globalVariables.WindowWidth * 3 / 4 - QuitButton.Width / 2, globalVariables.WindowHeight*3 / 4 - QuitButton.Height / 2,
    QuitButton.Width, QuitButton.Height);

            OptionRect = new Rectangle(globalVariables.WindowWidth * 2 / 4 - OptionButton.Width / 2, globalVariables.WindowHeight*3 / 4 - OptionButton.Height / 2,
    OptionButton.Width, OptionButton.Height);

        }


        public void Update(MouseState mouse)
        {
            if (PlayRect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Released && LeftPrevious == ButtonState.Pressed)
            {
                myGame.CurrentGameState = GameState.Gameplay;
            }

            if (QuitRect.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Released && LeftPrevious == ButtonState.Pressed)
            {
                myGame.Exit();
            }

            if (ReleaseNotes.Contains(mouse.Position) && mouse.LeftButton == ButtonState.Released && LeftPrevious == ButtonState.Pressed)
            {
                Process.Start("notepad.exe", "Release Notes.txt");
            }

            LeftPrevious = mouse.LeftButton;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayButton, PlayRect, Color.White);
            spriteBatch.Draw(QuitButton, QuitRect, Color.White);
            spriteBatch.Draw(OptionButton, OptionRect, Color.White);
            spriteBatch.DrawString(VersionFont, DisplayText, VersionPosition, Color.Black);

        }
    }
}
