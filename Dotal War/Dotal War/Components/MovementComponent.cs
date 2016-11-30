using Dotal_War.Interfaces;
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
            if (!target.cBag.ContainsKey(DataType.Speed))
            {
                target.cBag.Add(DataType.Speed, InitialValue);
            }

            if (!target.cBag.ContainsKey(DataType.TargetIndex))
            {
                target.cBag.Add(DataType.TargetIndex, 0);
            }

            if (!target.cBag.ContainsKey(DataType.IsMoveValid))
            {
                target.cBag.Add(DataType.IsMoveValid, false);
            }

            if (!target.cBag.ContainsKey(DataType.TargetList))
            {
                target.cBag.Add(DataType.TargetList, new List<Vector2>());
            }

            if (!target.cBag.ContainsKey(DataType.TargetType))
            {
                target.cBag.Add(DataType.TargetType, TargetType.Empty);
            }

            if (!target.cBag.ContainsKey(DataType.Rotation))
            {
                target.cBag.Add(DataType.Rotation, 0f);
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
