using Dotal_War.Interfaces;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotal_War.Systems
{
    public class CollisionSystem:ISystem
    {
        #region Fields

        List<int> Subscribtions;
        List<Entity> StaticSubs;
        List<Entity> DynamicSubs;

        EntityManager EntityManager;
        GlobalVariables GlobalVariables;

        #endregion

        #region Methodes

        public CollisionSystem(Game1 myGame)
        {
            EntityManager = myGame.EntityManager;
            GlobalVariables = myGame.globalVariables;
            Subscribtions = new List<int>();
            StaticSubs = new List<Entity>();
            DynamicSubs = new List<Entity>();
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

        public void runSystem()
        {
            foreach (int entity in Subscribtions)
            {
                if ((CollisionType)(EntityManager.GetEntity(entity).cBag[DataType.CollisionType]) == CollisionType.Static)
                {
                    StaticSubs.Add(EntityManager.GetEntity(entity));
                }

                else if ((CollisionType)(EntityManager.GetEntity(entity).cBag[DataType.CollisionType]) == CollisionType.Dynamic)
                {
                    DynamicSubs.Add(EntityManager.GetEntity(entity));
                }
            }

            foreach (Entity dynamic in DynamicSubs)
            {
                Vector2 pos0 = (Vector2)dynamic.cBag[DataType.Position];
                Rectangle rect0 = (Rectangle)dynamic.cBag[DataType.DrawRectangle];

                for (int i = 0; i < StaticSubs.Count; i++)
                {

                    Vector2 pos1 = (Vector2)StaticSubs[i].cBag[DataType.Position];
                    float dist = Vector2.Distance(pos0, pos1);

                    if (dist < 100)
                    {

                        Rectangle rect1 = (Rectangle)StaticSubs[i].cBag[DataType.DrawRectangle];

                        if (rect0.Intersects(rect1))
                        {
                            dynamic.cBag[DataType.IsMoveValid] = false;
                            dynamic.cBag[DataType.TargetIndex] = 0;
                            dynamic.cBag[DataType.Target] = null;
                            dynamic.cBag[DataType.TargetType] = TargetType.Empty;
                        }
                    }
                    
                }

                for (int j = 0; j < DynamicSubs.Count; j++)
                {
                    Vector2 newPos0 = new Vector2();
                    Vector2 newPos1 = new Vector2();


                    Vector2 pos1 = (Vector2)DynamicSubs[j].cBag[DataType.Position];
                    pos0 = (Vector2)dynamic.cBag[DataType.Position];
                    float dist = Vector2.Distance(pos0, pos1);


                    if (dist > 0 && dist < 25)
                    {
                        Rectangle rect1 = (Rectangle)DynamicSubs[j].cBag[DataType.DrawRectangle];

                        if (rect0.Intersects(rect1))
                        {
                            if (rect0.Left > rect1.Right)
                            {
                                newPos0.X = rect1.Right;
                                newPos1.X = pos1.X;
                            }

                            else if (rect0.Right < rect1.Left)
                            {
                                newPos0.X = rect1.Left;
                                newPos1.X = pos1.X;
                            }

                            else
                            {
                                newPos0.X = pos0.X;
                                newPos1.X = pos1.X;
                            }

                            if (rect0.Bottom > rect1.Top)
                            {
                                {
                                    newPos0.Y = rect1.Top -5;
                                    newPos1.Y = pos1.Y - -5;
                                }
                            }

                            else if (rect0.Top < rect1.Bottom)
                            {
                                {
                                    newPos0.Y = rect1.Bottom +5;
                                    newPos1.Y = pos1.Y + 5;
                                }
                            }

                            else
                            {
                                newPos0.Y = pos0.Y;
                                newPos1.Y = pos1.Y;
                            }

                            rect0.X = (int)newPos0.X - ((Texture2D)(dynamic.cBag[DataType.Sprite])).Width;
                            rect0.Y = (int)newPos0.Y - ((Texture2D)(dynamic.cBag[DataType.Sprite])).Height;
                            rect1.X = (int)newPos1.X - ((Texture2D)(DynamicSubs[j].cBag[DataType.Sprite])).Width;
                            rect1.Y = (int)newPos1.Y - ((Texture2D)(DynamicSubs[j].cBag[DataType.Sprite])).Height;
                            

                            dynamic.cBag[DataType.Position] = newPos0;
                            dynamic.cBag[DataType.DrawRectangle] = rect0;
                            DynamicSubs[j].cBag[DataType.Position] = newPos1;
                            DynamicSubs[j].cBag[DataType.DrawRectangle] = rect1;


                        }
                    }
                }

            }
            StaticSubs.Clear();
            DynamicSubs.Clear();
        }
        #endregion


    }
}
