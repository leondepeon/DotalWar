using Dotal_War.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Dotal_War.Components
{
    public class RenderComponent : IComponent
    {
        ISystem mySystem;

        #region Fields

        Texture2D sprite;
        Rectangle drawRectangle;
        Vector2 Position;
        ContentManager contentManager;
        Game1 myGame;

        #endregion

        #region Methodes

        public RenderComponent(Game1 myGame)
        {
            contentManager = myGame.Content;
            mySystem = myGame.RenderSystem;
            myGame.ComponentManager.RemoveRender = this;
        }

        void IComponent.AddComponent(Entity target, object InitialValue)
        {
            Position = (Vector2)(target.cBag[DataType.Position]);

            if (!target.cBag.ContainsKey(DataType.Sprite))
            {
            sprite = contentManager.Load<Texture2D>((string)(InitialValue));
            target.cBag.Add(DataType.Sprite, sprite);
            }

            if (!target.cBag.ContainsKey(DataType.DrawRectangle))
            {
            drawRectangle = new Rectangle((int)(Position.X) - sprite.Width, (int)(Position.Y) - sprite.Height, sprite.Width, sprite.Height);
            target.cBag.Add(DataType.DrawRectangle, drawRectangle);
            }

            if (!target.cBag.ContainsKey(DataType.defaultColor))
            {
            target.cBag.Add(DataType.defaultColor, Color.White);
            }

            mySystem.Subscribe(target.entityID);

        }

        void IComponent.RemoveComponent(int entityID)
        {
            mySystem.UnSubscribe(entityID);
        }

        #endregion
    }
}
