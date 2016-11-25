using DotalWar.Managers;
using Dotal_War.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Dotal_War.Managers
{
    public class EntityManager
    {
        #region Fields

        private int nextID = 1;
        public Dictionary<int, Entity> EntityList;
        Game1 myGame;

        #endregion

        public EntityManager(Game1 myGame)
        {
            this.myGame = myGame;
            EntityList = new Dictionary<int, Entity>();
        }

        #region AddEntity Overloads

        public Entity AddEntity()
        {
            Entity result;
            result = new Entity(nextID);
            EntityList.Add(nextID, result);
            result.cBag.Add(DataType.EntityState, EntityState.Alive);
            nextID++;
            return result;
        }
    
        public Entity AddEntity(Vector2 position)
        {
            Entity result;
            result = new Entity(nextID);
            EntityList.Add(nextID, result);
            result.cBag.Add(DataType.Position, position);
            result.cBag.Add(DataType.EntityState, EntityState.Alive);
            nextID++;
            return result;
        }

        #endregion

        #region Entity Methodes

        public void AddComponent(int entityID, IComponent component, object InitialValue)
        {
            component.AddComponent(GetEntity(entityID), InitialValue);

        }

        public void RemoveComponent(int entityID, IComponent component)
        {
            component.RemoveComponent(entityID);
        }

        public Entity GetEntity(int entityID)
        {
            return EntityList[entityID];
        }


        #endregion
    }
}
