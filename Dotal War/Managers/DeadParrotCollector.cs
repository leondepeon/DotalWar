using Dotal_War;
using Dotal_War.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotalWar.Managers
{
    public class DeadParrotCollector
    {
        EntityManager entityManager;
        ComponentManager componentManager;
        Entity updatingEntity;

        public DeadParrotCollector(Game1 mygame)
        {
            entityManager = mygame.EntityManager;
            componentManager = mygame.ComponentManager;
        }

        public void Run()
        {
            if (entityManager.EntityList != null)
            {
                bool allowLoop = true;
                while (allowLoop)
                {
                    foreach (KeyValuePair<int, Entity> entity in entityManager.EntityList)
                    {
                        allowLoop = false;
                        updatingEntity = entity.Value;
                        if ((EntityState)(updatingEntity.cBag[DataType.EntityState]) == EntityState.Dead)
                        {
                            componentManager.RemoveAllComponents(entity.Key);
                            entityManager.EntityList.Remove(entity.Key);
                            allowLoop = true;
                            break;
                        }
                        updatingEntity = null;
                    }
                }
            }
        }
    }
}
