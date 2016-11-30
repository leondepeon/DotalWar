using DotalWar.Managers;
using Dotal_War;
using Dotal_War.Interfaces;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DotalWar
{
    public class HealthSystem:ISystem
    {

        List<int> Subscribtions;
        EntityManager EntityManager;
        Entity updatingEntity;
        GlobalVariables GlobalVariables;


        public HealthSystem(Game1 myGame)
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

        public void RunSystem()
        {
            foreach (int subs in Subscribtions)
            {
                updatingEntity = EntityManager.GetEntity(subs);

                int updatingHP = (int)(updatingEntity.cBag[DataType.HealthPoints]);
                Rectangle updatingBar = (Rectangle)(updatingEntity.cBag[DataType.HealthRectangle]);
                updatingBar.Width = updatingHP/3;

                if (updatingHP <= 0)
                {
                    updatingEntity.cBag[DataType.EntityState] = EntityState.Dead;
                }

                updatingEntity.cBag[DataType.HealthRectangle] = updatingBar;
                updatingEntity = null;

            }
        }
    }
}
