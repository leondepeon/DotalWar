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


        public void SetTarget(List<Vector2> targetList)
        {
            List<Vector2> TargetList;
            Entity checkEntity;
            foreach (int sub in Subscribtions)
            {
                checkEntity = EntityManager.GetEntity(sub);
                // determines if subscribed entity is: 1)selected 2) is infact a unit and not something else
                if ((bool)(checkEntity.cBag[DataType.IsSelected]) && (SelectionType)(checkEntity.cBag[DataType.SelectionType]) == SelectionType.Units)
                {
                    TargetList = (List<Vector2>)checkEntity.cBag[DataType.TargetList];
                    TargetList.AddRange(targetList);
                    checkEntity.cBag[DataType.TargetList] = TargetList;
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
                List<Vector2> EntityTargetList = (List<Vector2>)(updatingEntity.cBag[DataType.TargetList]);
                TargetType EntityTType = (TargetType)(updatingEntity.cBag[DataType.TargetType]);
                int currentIndex = (int)(updatingEntity.cBag[DataType.TargetIndex]);
                float EntitySpeed = (float)(updatingEntity.cBag[DataType.Speed]);
                bool IsMoveValid = (bool)(updatingEntity.cBag[DataType.IsMoveValid]);

                #endregion

                #region Update movement and Rotation

                Vector2 EntityDirection = new Vector2();
                float rotation = 0f;

                if (EntityTargetList.Count != 0)
                {
                EntityTarget = EntityTargetList[0];
                    // calculates direction and rotation of entity
                EntityDirection = new Vector2(EntityTarget.X - position.X, EntityTarget.Y - position.Y);
                rotation = (float)Math.Atan2(EntityDirection.Y, EntityDirection.X);
                EntityDirection.Normalize();
                    IsMoveValid = true;
                }





                // updates movement
                if (IsMoveValid)
                {
                    position += (EntityDirection * EntitySpeed);
                    EntityRectangle.X = (int)(position.X - ((Texture2D)(updatingEntity.cBag[DataType.Sprite])).Width);
                    EntityRectangle.Y = (int)(position.Y - ((Texture2D)(updatingEntity.cBag[DataType.Sprite])).Height);
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
                    EntityTargetList.Remove(EntityTarget);


                    if (EntityTargetList.Count == 0)
                    {
                        updatingEntity.cBag[DataType.IsMoveValid] = false;
                    }


                }
                #endregion

                #region Update Data

                updatingEntity.cBag[DataType.Position] = position;
                updatingEntity.cBag[DataType.Rotation] = rotation;
                updatingEntity.cBag[DataType.DrawRectangle] = EntityRectangle;
                updatingEntity.cBag[DataType.TargetList] = EntityTargetList;

                #endregion
            }
        }

        #endregion
    }
}
