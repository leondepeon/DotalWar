using Dotal_War.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotal_War.Components
{
    public class SpawnComponent:IComponent
    {
        ISystem mySystem;
        Vector2 SpawnerLocation;

        #region Methodes

        public SpawnComponent(ISystem mySystem)
        {
            this.mySystem = mySystem;
        }

        void IComponent.AddComponent(Entity target, object InitialValue)
        {
            SpawnerLocation.X = ((Vector2)(target.cBag[DataType.Position])).X - ((Texture2D)(target.cBag[DataType.Sprite])).Width/2;
            SpawnerLocation.Y = ((Vector2)(target.cBag[DataType.Position])).Y - ((Texture2D)(target.cBag[DataType.Sprite])).Height/2;

            if (!target.cBag.ContainsKey(DataType.SpawnRadius))
            {
                target.cBag.Add(DataType.SpawnRadius, new Rectangle((int)(SpawnerLocation.X) -50 ,(int)(SpawnerLocation.Y) - 50,100,100));
            }

            if (!target.cBag.ContainsKey(DataType.SpawnLocation))
            {
                target.cBag.Add(DataType.SpawnLocation, new Vector2());
            }

            if (!target.cBag.ContainsKey(DataType.SpawnTime))
            {
                target.cBag.Add(DataType.SpawnTime, InitialValue);
            }

            if (!target.cBag.ContainsKey(DataType.CurrentTime))
            {
                target.cBag.Add(DataType.CurrentTime,0);
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
