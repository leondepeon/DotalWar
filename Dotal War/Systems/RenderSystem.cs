using Dotal_War.Interfaces;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace Dotal_War.Systems
{
    public class RenderSystem:ISystem
    {
        #region Fields

        List<int> Subscribtions;
        EntityManager EntityManager;
        Entity updatingEntity;
        Texture2D Piksel;
        GlobalVariables globalVariables;
        Color RenderColor;
        Vector2 Renderposition;
        Texture2D RenderSprite;
        float RenderRotation;
        Rectangle SpawnRadius;
        Vector2 SpawnPoint;

        #endregion

        #region Methodes

        public RenderSystem(Game1 myGame)
        {
            globalVariables = myGame.globalVariables;
            EntityManager = myGame.EntityManager;
            Subscribtions = new List<int>();
            Piksel = myGame.Content.Load<Texture2D>(@"Graphics\Piksel");
        }

        #region Subscriber Management

        void ISystem.Subscribe(int entityID)
        {
            Subscribtions.Add(entityID);
        }

        void ISystem.UnSubscribe(int entityID)
        {
            if (Subscribtions.Contains(entityID))
            { Subscribtions.Remove(entityID); }
        }

        #endregion

        public void RunSystem(SpriteBatch spriteBatch)
        {
            foreach (int Subs in Subscribtions)
            {
                updatingEntity = EntityManager.GetEntity(Subs);

                RenderSprite = (Texture2D)(updatingEntity.cBag[DataType.Sprite]);
                Renderposition = new Vector2(((Vector2)(updatingEntity.cBag[DataType.Position])).X - (RenderSprite.Width/2),
                    ((Vector2)(updatingEntity.cBag[DataType.Position])).Y - (RenderSprite.Height / 2));



                if (updatingEntity.cBag.ContainsKey(DataType.Rotation))
                {
                    RenderRotation = (float)(updatingEntity.cBag[DataType.Rotation]);
                }
                else
                {RenderRotation = 0f;}

                if ((bool)(updatingEntity.cBag[DataType.IsSelected]))
                {

                    switch ((SelectionType)(updatingEntity.cBag[DataType.SelectionType]))
                    {
                        case SelectionType.Units:
                            RenderColor = Color.Red;
                            break;

                        case SelectionType.Buildings:
                            if (updatingEntity.cBag.ContainsKey(DataType.SpawnRadius))
                            {
                                SpawnRadius = (Rectangle)(updatingEntity.cBag[DataType.SpawnRadius]);
                                SpawnPoint = (Vector2)(updatingEntity.cBag[DataType.SpawnLocation]);
                                //Top
                                spriteBatch.Draw(Piksel, new Rectangle(SpawnRadius.X, SpawnRadius.Y,
                                    SpawnRadius.Width, 2), Color.Black);
                                //Left
                                spriteBatch.Draw(Piksel, new Rectangle(SpawnRadius.X, SpawnRadius.Y,
                                    2, SpawnRadius.Height), Color.Black);
                                //Bottom
                                spriteBatch.Draw(Piksel, new Rectangle(SpawnRadius.X, (SpawnRadius.Y + SpawnRadius.Height),
                                    SpawnRadius.Width + 2, 2), Color.Black);
                                //Right
                                spriteBatch.Draw(Piksel, new Rectangle((SpawnRadius.X + SpawnRadius.Width),
                                    SpawnRadius.Y, 2, SpawnRadius.Height), Color.Black);

                                spriteBatch.Draw(Piksel, SpawnPoint, Color.Black);

                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                { RenderColor = (Color)(updatingEntity.cBag[DataType.defaultColor]); }

                if (updatingEntity.cBag.ContainsKey(DataType.HealthRectangle) && updatingEntity.cBag.ContainsKey(DataType.IsHoverOver))
                {
                    if ((bool)(updatingEntity.cBag[DataType.IsHoverOver])||(bool)(updatingEntity.cBag[DataType.IsSelected]))
                    {  spriteBatch.Draw(Piksel, (Rectangle)(updatingEntity.cBag[DataType.HealthRectangle]), Color.Green); }
                }
                spriteBatch.Draw(RenderSprite, Renderposition, null, RenderColor, RenderRotation, new Vector2(RenderSprite.Width/2,RenderSprite.Height/2),
                   globalVariables.universalScaleFactor , SpriteEffects.None, 0);

                updatingEntity = null;
            }
        }
        #endregion
    }
}
