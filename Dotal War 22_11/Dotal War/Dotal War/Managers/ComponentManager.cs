using Dotal_War.Components;
using Dotal_War.Interfaces;
using System.Collections.Generic;

namespace Dotal_War.Managers
{
    public class ComponentManager
    {
        #region Fields

        SystemManager systemsManager;
        Game1 myGame;
        public IComponent RemoveRender;
        List<IComponent> components;

        #endregion

        #region ComponentInstances

        //Declare ComponentInstance
        
        public SelectionHandlerComponent cSelectionHandler;
        public MovementComponent cMovement;
        public HealthComponent cHealth;
        public SpawnComponent cSpawn;
        //public CollisionComponent cCollision;


        #endregion

        #region ManagerConstructor

        public ComponentManager(Game1 mygame)
        {
            myGame = mygame;
            systemsManager = myGame.SystemManager;
            components = new List<IComponent>();

            // Initiate ComponentInstance
            components.Add(cSelectionHandler = new SelectionHandlerComponent(systemsManager.sSelectionHandler));
            components.Add(cMovement = new MovementComponent(systemsManager.sMovement));
            components.Add(cHealth = new HealthComponent(systemsManager.sHealth));
            components.Add(cSpawn = new SpawnComponent(systemsManager.sSpawn));
            //components.Add(cCollision = new CollisionComponent(systemsManager.sCollision));

        }

        public void RemoveAllComponents(int entityID)
        {
            foreach (IComponent component in components)
            {
                component.RemoveComponent(entityID);
            }
            RemoveRender.RemoveComponent(entityID);
        }

        #endregion
    }
}
