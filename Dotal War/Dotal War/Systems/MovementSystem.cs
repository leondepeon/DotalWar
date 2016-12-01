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
        //***NEW***
        List<Entity> entities;
        List<Vector2> targetList;
        Vector2 position;
        Vector2 velocity;
        Vector2 direction;
        Vector2 target;
        float acceleration = 5f;
        float speed;
        float speedLimit = 20f;
        float rotation;
        bool moveValid;
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


        public void RunSystem(GameTime gameTime)
        {
            foreach (int sub in Subscribtions)
            {
                entities.Add(EntityManager.GetEntity(sub));
            }

            foreach (Entity subject in entities)
            {
                #region Load
                position = (Vector2)subject.cBag[DataType.Position];
                velocity = (Vector2)subject.cBag[DataType.Velocity];
                direction = (Vector2)subject.cBag[DataType.Direction];
                targetList = ((List<Vector2>)(subject.cBag[DataType.TargetList]));
                target = targetList[0];
                speed = (float)subject.cBag[DataType.Speed];
                rotation = (float)subject.cBag[DataType.Rotation];
                moveValid = (bool)subject.cBag[DataType.IsMoveValid];
                #endregion

                #region Update
                if (moveValid)
                {
                    speed += acceleration * gameTime.ElapsedGameTime.Milliseconds / 1000;
                    speed = SpeedLimiter(speed, speedLimit);
                    velocity = direction * speed;
                    position += velocity * gameTime.ElapsedGameTime.Milliseconds / 1000;
                    rotation = (float)Math.Atan2(velocity.Y, velocity.X);

                    float d = (float)Vector2.Distance(target, position);
                    if (d <= 2)
                    {
                        speed = 0;
                        velocity = Vector2.Zero;
                        direction = Vector2.Zero;
                        moveValid = false;
                        targetList.Remove(target);
                    }
                }
                #endregion

                #region Unload
                subject.cBag[DataType.Position] = position;
                subject.cBag[DataType.Velocity] = velocity;
                subject.cBag[DataType.Direction] = direction;
                subject.cBag[DataType.TargetList] = targetList;
                subject.cBag[DataType.Speed] = speed;
                subject.cBag[DataType.Rotation] = rotation;
                subject.cBag[DataType.IsMoveValid] = moveValid;
                #endregion
            }

        }

        private float SpeedLimiter(float speed, float speedLimit)
        {
            if (speed > speedLimit)
            {
                return speedLimit;
            }
            else
            {
                return speed;
            }
        }

        #endregion
    }
}
