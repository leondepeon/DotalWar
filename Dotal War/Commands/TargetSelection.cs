using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotal_War.Commands
{
    public class TargetSelection
    {
        Game1 myGame;
        ButtonState RightPS;
        List<Vector2> TList;
        bool isSwiping = false;
        bool FirstTime = true;

        public TargetSelection (Game1 myGame)
        {
            this.myGame = myGame;
            TList = new List<Vector2>();
        }


        public void RunTarget()
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboad = Keyboard.GetState();

            if (mouse.RightButton == ButtonState.Released && RightPS == ButtonState.Pressed && !keyboad.IsKeyDown(Keys.LeftShift) && !isSwiping)
            {
                TList.Clear(); 
                TList.Add(new Vector2(mouse.X, mouse.Y));
                myGame.SystemManager.sMovement.SetTarget(TList, TargetType.Individual);
            }

            if (mouse.RightButton == ButtonState.Pressed && keyboad.IsKeyDown(Keys.LeftShift))
            {
                if (FirstTime)
                {
                    TList.Clear();
                    FirstTime = false;
                }
                TList.Add(new Vector2(mouse.X, mouse.Y));
                isSwiping = true;
            }

            if (mouse.RightButton == ButtonState.Released && RightPS == ButtonState.Pressed && isSwiping)
            {
                myGame.SystemManager.sMovement.SetTarget(TList, TargetType.Swipe);
                isSwiping = false;
                FirstTime = true;
            }
            RightPS = mouse.RightButton;
        }
    }
}
