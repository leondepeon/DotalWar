using Dotal_War;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Dotal_War
{
    class SelectionRectange
    {

        #region Fields

        Game1 myGame;
        GlobalVariables GlobalVariable;
        bool mSelect = false;
        bool allowVisuals = false;
        Point mInitial = new Point();
        Texture2D piksel;
        const int Thickness = 1;

        #endregion

        public SelectionRectange(Game1 myGame)
        {
            this.myGame = myGame;
            GlobalVariable = myGame.globalVariables;
            piksel = myGame.Content.Load<Texture2D>(@"Graphics\Piksel");
        }

        public void Run(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                GlobalVariable.LockSelection = false;
                allowVisuals = true;
                if (!mSelect)
                {
                    mInitial.X = mouse.X;
                    mInitial.Y = mouse.Y;
                    mSelect = true;

                }

                else
                {

                    if (mouse.X > mInitial.X)
                    {
                        GlobalVariable.mouseSelectionRectangle.X = mInitial.X;
                        GlobalVariable.mouseSelectionRectangle.Width = mouse.X - mInitial.X;
                    }
                    else if (mouse.X < mInitial.X)
                    {
                        GlobalVariable.mouseSelectionRectangle.X = mouse.X;
                        GlobalVariable.mouseSelectionRectangle.Width = mInitial.X - mouse.X;
                    }

                    if (mouse.Y > mInitial.Y)
                    {
                        GlobalVariable.mouseSelectionRectangle.Y = mInitial.Y;
                        GlobalVariable.mouseSelectionRectangle.Height = mouse.Y - mInitial.Y;
                    }

                    else if (mouse.Y < mInitial.Y)
                    {
                        GlobalVariable.mouseSelectionRectangle.Y = mouse.Y;
                        GlobalVariable.mouseSelectionRectangle.Height = mInitial.Y - mouse.Y;
                    }
                }
            }

            if (mSelect && mouse.LeftButton == ButtonState.Released)
            {
                mSelect = false;
                GlobalVariable.LockSelection = true;
                allowVisuals = false;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (allowVisuals)
            {
                //Top
                spriteBatch.Draw(piksel, new Rectangle(GlobalVariable.mouseSelectionRectangle.X, GlobalVariable.mouseSelectionRectangle.Y,
                    GlobalVariable.mouseSelectionRectangle.Width, Thickness), Color.BlueViolet);
                //Left
                spriteBatch.Draw(piksel, new Rectangle(GlobalVariable.mouseSelectionRectangle.X, GlobalVariable.mouseSelectionRectangle.Y,
                    Thickness, GlobalVariable.mouseSelectionRectangle.Height), Color.BlueViolet);
                //Bottom
                spriteBatch.Draw(piksel, new Rectangle(GlobalVariable.mouseSelectionRectangle.X, (GlobalVariable.mouseSelectionRectangle.Y + GlobalVariable.mouseSelectionRectangle.Height),
                    GlobalVariable.mouseSelectionRectangle.Width + 1, Thickness), Color.BlueViolet);
                //Right
                spriteBatch.Draw(piksel, new Rectangle((GlobalVariable.mouseSelectionRectangle.X + GlobalVariable.mouseSelectionRectangle.Width),
                    GlobalVariable.mouseSelectionRectangle.Y, Thickness, GlobalVariable.mouseSelectionRectangle.Height), Color.BlueViolet);
            }
        }
    }
}
