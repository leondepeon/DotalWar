using Dotal_War.Interfaces;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotal_War.Systems
{
    public class CollisionSystem:ISystem
    {
        #region Fields

        List<int> Subscribtions;
        EntityManager EntityManager;
        Entity updatingEntity;
        GlobalVariables GlobalVariables;

        Rectangle[] SectorRectangle;
        List<int>[] SectorSubs;


        #endregion

        #region Methodes

        public CollisionSystem(Game1 myGame)
        {
            EntityManager = myGame.EntityManager;
            GlobalVariables = myGame.globalVariables;
            Subscribtions = new List<int>();

            SectorRectangle = new Rectangle[4]
                {new Rectangle(0,0,GlobalVariables.WindowWidth/2,GlobalVariables.WindowHeight/2),
                new Rectangle(GlobalVariables.WindowWidth / 2,0, GlobalVariables.WindowWidth / 2, GlobalVariables.WindowHeight / 2),
                new Rectangle (0, GlobalVariables.WindowHeight / 2, GlobalVariables.WindowWidth / 2, GlobalVariables.WindowHeight / 2),
                new Rectangle(GlobalVariables.WindowWidth/2, GlobalVariables.WindowHeight / 2, GlobalVariables.WindowWidth / 2, GlobalVariables.WindowHeight / 2)};
            SectorSubs = new List<int>[4]
                { new List<int>(),new List<int>(),new List<int>(),new List<int>()};


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

        public void runSystem()
        {
            foreach (int subs in Subscribtions)
            {
                updatingEntity = EntityManager.GetEntity(subs);
                Rectangle CollisionRectangle = (Rectangle)(updatingEntity.cBag[DataType.DrawRectangle]);

                for (int i = 0; i < 4; i++)
                {
                    if (CollisionRectangle.Intersects(SectorRectangle[i]))
                    { SectorSubs[i].Add(subs); }

                    if (!CollisionRectangle.Intersects(SectorRectangle[i])&& SectorSubs[i].Contains(subs))
                    { SectorSubs[i].Remove(subs); }
                }

                updatingEntity = null;
            }

            foreach (List<int> sector in SectorSubs)
            {
                if (sector.Count > 1)
                {
                    for (int i = 0; i < sector.Count - 1; i++)
                    {
                        for (int j = i + 1; j < sector.Count; j++)
                        {
                            if (((Rectangle)(EntityManager.GetEntity(sector[i]).cBag[DataType.DrawRectangle])).Intersects(((Rectangle)(EntityManager.GetEntity(sector[j]).cBag[DataType.DrawRectangle]))))
                            {
                                GlobalVariables.universalScaleFactor = 3;
                            }

                        }
                    }
                }
            }


        }

        #endregion


    }
}
