﻿using Dotal_War.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotal_War.Components
{
    public class CollisionComponent:IComponent
    {
        ISystem mySystem;

        #region Methodes

        public CollisionComponent(ISystem mySystem)
        {
            this.mySystem = mySystem;
        }

        void IComponent.AddComponent(Entity target, object InitialValue)
        {
            if (!target.cBag.ContainsKey(DataType.CollisionType))
            {
                target.cBag.Add(DataType.CollisionType, InitialValue);
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
