﻿using Dotal_War.Interfaces;
using Dotal_War.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Dotal_War.Components
{
    public class MovementComponent:IComponent
    {
        ISystem mySystem;

        #region Methodes

        public MovementComponent (ISystem mySystem)
        {
            this.mySystem = mySystem;
        }

        void IComponent.AddComponent(Entity target, object InitialValue)
        {
            if (!target.cBag.ContainsKey(DataType.Velocity))
            {
                target.cBag.Add(DataType.Velocity, new Vector2());
            }

            if (!target.cBag.ContainsKey(DataType.Speed))
            {
                target.cBag.Add(DataType.Speed, 0f);
            }

            if (!target.cBag.ContainsKey(DataType.IsMoveValid))
            {
                target.cBag.Add(DataType.IsMoveValid, false);
            }

            if (!target.cBag.ContainsKey(DataType.TargetList))
            {
                target.cBag.Add(DataType.TargetList, new List<Vector2>());
            }

            if (!target.cBag.ContainsKey(DataType.Rotation))
            {
                target.cBag.Add(DataType.Rotation, 0f);
            }

            if(!target.cBag.ContainsKey(DataType.Time))
            {
                target.cBag.Add(DataType.Time, 0f);
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
