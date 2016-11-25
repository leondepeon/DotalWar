using Dotal_War.Interfaces;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace Dotal_War.Systems
{
    public class SelectionHandlerSystem:ISystem
    {
        #region Fields

        List<int> Subscribtions;
        EntityManager EntityManager;
        Entity updatingEntity;
        GlobalVariables GlobalVariables;


        #endregion

        #region Methodes

        public SelectionHandlerSystem (Game1 myGame)
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
            foreach (int Subs in Subscribtions)
            {
                updatingEntity = EntityManager.GetEntity(Subs);

                if (GlobalVariables.mouseSelectionRectangle.Contains((Rectangle)(updatingEntity.cBag[DataType.DrawRectangle])))
                {
                    updatingEntity.cBag[DataType.IsSelected] = true;                  
                }

                else if (!GlobalVariables.mouseSelectionRectangle.Contains((Rectangle)(updatingEntity.cBag[DataType.DrawRectangle])))
                {
                    if (!GlobalVariables.LockSelection)
                    {
                        updatingEntity.cBag[DataType.IsSelected] = false;
                    }

                }

                if (((Rectangle)(updatingEntity.cBag[DataType.DrawRectangle])).Contains(Mouse.GetState().Position))
                {
                    updatingEntity.cBag[DataType.IsHoverOver] = true;
                }

                else if (!((Rectangle)(updatingEntity.cBag[DataType.DrawRectangle])).Contains(Mouse.GetState().Position))
                {
                    updatingEntity.cBag[DataType.IsHoverOver] = false;
                }
                updatingEntity = null;

            }
        }

        #endregion
    }
}
