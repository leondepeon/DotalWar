using Dotal_War.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dotal_War.Components
{
    public class HealthComponent:IComponent
    {
        ISystem mySystem;

        int Thickness = 2;
        int CenterDist = 15;

        Rectangle TempBarRect;
        Vector2 TempEntityCenter;

        #region Methodes

        public HealthComponent(ISystem mySystem)
        {
            this.mySystem = mySystem;
        }

        void IComponent.AddComponent(Entity target, object InitialValue)
        {
            if (!target.cBag.ContainsKey(DataType.HealthPoints))
            {
                target.cBag.Add(DataType.HealthPoints, InitialValue);
            }

            if (!target.cBag.ContainsKey(DataType.HealthRectangle))
            {
                TempEntityCenter = new Vector2(((Rectangle)(target.cBag[DataType.DrawRectangle])).Center.X, ((Rectangle)(target.cBag[DataType.DrawRectangle])).Center.Y);
                TempBarRect = new Rectangle((int)(TempEntityCenter.X) - ((int)(InitialValue) / 3) / 2, (int)(TempEntityCenter.Y) - (CenterDist + Thickness / 2), (int)(InitialValue) / 3, Thickness);
                target.cBag.Add(DataType.HealthRectangle, TempBarRect);
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
