using Dotal_War.Interfaces;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Dotal_War.Systems
{
    public class MovementSystem:ISystem
    {
        #region Fields

        List<int> Subscribtions;
        EntityManager EntityManager;
        Entity updatingEntity;
        GlobalVariables GlobalVariables;
        

        #endregion

        #region Methodes

        public MovementSystem (Game1 myGame)
        {
            EntityManager = myGame.EntityManager;
            GlobalVariables = myGame.globalVariables;
            Subscribtions = new List<int>();
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


        public void SetTarget(List<Vector2> TargetList, TargetType TType)
        {
            foreach (int sub in Subscribtions)
            {
                // determines if subscribed entity is: 1)selected 2) is infact a unit and not something else
                if ((bool)(EntityManager.GetEntity(sub).cBag[DataType.IsSelected]) && (SelectionType)(EntityManager.GetEntity(sub).cBag[DataType.SelectionType]) == SelectionType.Units)
                {
                    EntityManager.GetEntity(sub).cBag[DataType.Target] = TargetList;
                    EntityManager.GetEntity(sub).cBag[DataType.IsMoveValid] = true;
                    EntityManager.GetEntity(sub).cBag[DataType.TargetType] = TType;
                }
            }

        }


        public void RunSystem()
        {
            foreach (int subs in Subscribtions)
            {
                #region Load Data
                updatingEntity = EntityManager.GetEntity(subs);

                Vector2 position = (Vector2)(updatingEntity.cBag[DataType.Position]);
                Vector2 EntityTarget = new Vector2();
                Rectangle EntityRectangle = (Rectangle)(updatingEntity.cBag[DataType.DrawRectangle]);
                List<Vector2> EntityTargetList = (List<Vector2>)(updatingEntity.cBag[DataType.Target]);
                TargetType EntityTType = (TargetType)(updatingEntity.cBag[DataType.TargetType]);
                int currentIndex = (int)(updatingEntity.cBag[DataType.TargetIndex]);
                float EntitySpeed = (float)(updatingEntity.cBag[DataType.Speed]);
                bool IsMoveValid = (bool)(updatingEntity.cBag[DataType.IsMoveValid]);
                #endregion

                #region Update movement and Rotation

                // checks whether Movement is alowed on this entity, then determains its current target location
                if (IsMoveValid)
                {
                    switch (EntityTType)
                    {
                        case TargetType.Individual:// P2P: only one target in list
                            EntityTarget = EntityTargetList[currentIndex];
                            break;
                        case TargetType.Swipe:// Swipe: multiple targets in list, currentindex decides which one to pick
                            EntityTarget = EntityTargetList[currentIndex];
                            break;
                        default:
                            break;
                    }
                }


                // calculates direction and rotation of entity
                Vector2 EntityDirection = new Vector2(EntityTarget.X - position.X, EntityTarget.Y - position.Y);
                float rotation = (float)Math.Atan2(EntityDirection.Y, EntityDirection.X);
                EntityDirection.Normalize();

                // updates movement
                if (IsMoveValid)
                {
                    position += (EntityDirection * EntitySpeed);
                    EntityRectangle.X = (int)(position.X - ((Texture2D)(updatingEntity.cBag[DataType.Sprite])).Width / 2);
                    EntityRectangle.Y = (int)(position.Y - ((Texture2D)(updatingEntity.cBag[DataType.Sprite])).Height / 2);
                }

                // checks whether entity has a healthbar and updates its position
                if (updatingEntity.cBag.ContainsKey(DataType.HealthRectangle))
                {
                    Rectangle EntityHPRectangle = (Rectangle)(updatingEntity.cBag[DataType.HealthRectangle]);
                    EntityHPRectangle.X = (int)(position.X - EntityHPRectangle.Width / 2);
                    EntityHPRectangle.Y = (int)(position.Y - 20);
                    updatingEntity.cBag[DataType.HealthRectangle] = EntityHPRectangle;
                }

                // case the entity is close to its target it acts acording to its target type
                if ((EntityTarget - position).Length() <= 2)
                {
                    switch (EntityTType)
                    {
                        case TargetType.Individual: 
                            updatingEntity.cBag[DataType.IsMoveValid] = false;
                            updatingEntity.cBag[DataType.TargetIndex] = 0;// resets target index
                            updatingEntity.cBag[DataType.Target] = null;// empties targetlist
                            updatingEntity.cBag[DataType.TargetType] = TargetType.Empty; // resets TargetType
                            break;

                        case TargetType.Swipe:// first decides where in the target list the entity currently is
                            if (currentIndex < EntityTargetList.Count - 1)
                            {
                                currentIndex += 1;// moves to the next target
                                updatingEntity.cBag[DataType.TargetIndex] = currentIndex;
                            }
                            else // same as Individual
                            {
                                updatingEntity.cBag[DataType.IsMoveValid] = false;
                                updatingEntity.cBag[DataType.TargetIndex] = 0;
                                updatingEntity.cBag[DataType.Target] = null;
                                updatingEntity.cBag[DataType.TargetType] = TargetType.Empty;
                            }
                            break;

                        default:
                            break;
                    }
                }
                #endregion

                #region Update Data

                updatingEntity.cBag[DataType.Position] = position;
                updatingEntity.cBag[DataType.Rotation] = rotation;
                updatingEntity.cBag[DataType.DrawRectangle] = EntityRectangle;

                #endregion
            }
        }

        #endregion
    }
}
