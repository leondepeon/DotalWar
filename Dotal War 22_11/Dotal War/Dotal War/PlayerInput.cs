using Dotal_War.Commands;
using Microsoft.Xna.Framework.Input;

namespace Dotal_War
{
    public class PlayerInput
    {
        #region Fields

        ButtonState RightMousePS;
        MouseState mouse;
        KeyboardState keyboard;

        #endregion

        #region Methodes

        public PlayerInput(Game1 myGame)
        {
            // initiate here
        }

        public void TESTBINDING()
        {
            //Bind Here
        }


        public void handleInput()
        {
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            // add key functionality here
            if (mouse.RightButton == ButtonState.Released && RightMousePS == ButtonState.Pressed)
            {
                if (RightClick != null) { RightClick.execute(); }
            }

            if (keyboard.IsKeyDown(Keys.Escape))
            { if (EscapeKey != null) { EscapeKey.execute(); } }


            RightMousePS = mouse.RightButton;
        }

        

        #endregion

        #region Commands

        ICommand RightClick;
        ICommand EscapeKey;

        #endregion
    }
}
