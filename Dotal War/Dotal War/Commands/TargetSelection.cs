using Dotal_War.Interfaces;
using Dotal_War.Systems;
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
        MovementSystem system;
        List<Vector2> TList;
        MouseAction rightAction;
        Vector2 mouseVector;
        ButtonState rightPrevious;

        bool reset = false;
        bool shiftPressed = false;

        public TargetSelection (Game1 myGame)
        {
            system = myGame.SystemManager.sMovement;
            TList = new List<Vector2>();
            mouseVector = new Vector2();
            
        }


        public void RunTarget()
        {
            MouseState mouse = Mouse.GetState();
            KeyboardState keyboad = Keyboard.GetState();
            rightAction = mouseActionState(mouse, keyboad);
            mouseVector.X = mouse.X;
            mouseVector.Y = mouse.Y;

            // Determain state of RightButton
            switch (rightAction)
            {
                case MouseAction.Release:
                    TList.Add(mouseVector);
                    system.SetTarget(TList);
                    reset = true;
                    break;
                case MouseAction.Hold:
                    TList.Add(mouseVector);
                    break;
                default:
                    break;
            }



            // Reset TargetList
            if (reset)
            {
                TList.Clear();
                reset = false;
            }
            rightPrevious = mouse.RightButton;

        }

        private MouseAction mouseActionState(MouseState mouse, KeyboardState keyboard)
        {
            if (mouse.RightButton == ButtonState.Pressed && keyboard.IsKeyDown(Keys.LeftShift))
            {
                return MouseAction.Hold;
            }

            else if (mouse.RightButton == ButtonState.Released && rightPrevious == ButtonState.Pressed)
            {
                return MouseAction.Release;
            }
            else
            {
                return MouseAction.Empty;
            }
        }
    }
}
