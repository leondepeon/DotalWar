using Dotal_War.Interfaces;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        Rectangle drawRectangle;
        Rectangle healthBar;
        Vector2 position;
        Vector2 velocity;
        Vector2 direction;
        Vector2 target;
        float acceleration = 50f;
        float speed;
        float speedLimit = 200f;
        float rotation;
        bool moveValid;
        #endregion

        #region Methodes

        public MovementSystem (Game1 myGame)
        {
            EntityManager = myGame.EntityManager;
            GlobalVariables = myGame.globalVariables;
            Subscribtions = new List<int>();
            entities = new List<Entity>();
            direction = new Vector2();

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
                    checkEntity.cBag[DataType.IsMoveValid] = true;
                }
            }

        }


        public void RunSystem(GameTime gameTime)
        {
            bool updated = false;

            foreach (int sub in Subscribtions)
            {
                entities.Add(EntityManager.GetEntity(sub));
            }

            foreach (Entity subject in entities)
            {
                moveValid = (bool)subject.cBag[DataType.IsMoveValid];
                

                #region Load
                if (moveValid)
                {
                    position = (Vector2)subject.cBag[DataType.Position];
                    velocity = (Vector2)subject.cBag[DataType.Velocity];
                    targetList = ((List<Vector2>)(subject.cBag[DataType.TargetList]));
                    target = targetList[0];
                    speed = (float)subject.cBag[DataType.Speed];
                    rotation = (float)subject.cBag[DataType.Rotation];
                    drawRectangle = (Rectangle)subject.cBag[DataType.DrawRectangle];
                    if(subject.cBag.ContainsKey(DataType.HealthRectangle))
                    {
                        healthBar = (Rectangle)subject.cBag[DataType.HealthRectangle];
                    }
                }
                #endregion

                #region Update
                if (moveValid)
                {
                    direction = target - position;
                    direction.Normalize();
                    speed += acceleration * gameTime.ElapsedGameTime.Milliseconds /1000;
                    speed = SpeedLimiter(speed, speedLimit);
                    velocity = direction * speed;
                    position += velocity * gameTime.ElapsedGameTime.Milliseconds / 1000;
                    rotation = (float)Math.Atan2(velocity.Y, velocity.X);
                    drawRectangle.X = (int)position.X - drawRectangle.Width;
                    drawRectangle.Y = (int)position.Y - drawRectangle.Height;
                    healthBar.X = (int)position.X - healthBar.Width/2;
                    healthBar.Y = (int)position.Y - healthBar.Height - 15;
                    float d = (float)Vector2.Distance(target, position);
                    if (d <= 2)
                    {
                        targetList.Remove(target);
                        if (targetList.Count == 0)
                        {
                            speed = 0;
                            velocity = new Vector2();
                            direction = new Vector2();
                            moveValid = false;
                        }

                    }

                    updated = true;
                }
                #endregion

                #region Unload
                if (updated)
                {
                    subject.cBag[DataType.Position] = position;
                    subject.cBag[DataType.Velocity] = velocity;
                    subject.cBag[DataType.TargetList] = targetList;
                    subject.cBag[DataType.Speed] = speed;
                    subject.cBag[DataType.Rotation] = rotation;
                    subject.cBag[DataType.IsMoveValid] = moveValid;
                    subject.cBag[DataType.DrawRectangle] = drawRectangle;
                    subject.cBag[DataType.HealthRectangle] = healthBar;
                }
                #endregion
            }
            entities.Clear();
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
