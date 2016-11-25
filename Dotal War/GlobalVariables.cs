using Microsoft.Xna.Framework;


namespace Dotal_War
{
    public class GlobalVariables
    {
        public string CurrentVersion = "0.16.09??.0"; // DotalWarVersion

        #region GameSettings

        public int WindowWidth;
        public int WindowHeight;

        #endregion

        #region Selection

        public Rectangle mouseSelectionRectangle = new Rectangle();
        public bool LockSelection;

        #endregion

        public int universalScaleFactor;

        public int CollisionOffset = 10; // offsets all collistions with 10 pixels
    }
}
