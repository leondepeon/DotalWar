﻿using DotalWar;
using Dotal_War.Systems;
using Microsoft.Xna.Framework;

namespace Dotal_War.Managers
{
    public class SystemManager
    {
        Game1 myGame;
        #region System Instances

        // Declare Instances
        public SelectionHandlerSystem sSelectionHandler;
        public MovementSystem sMovement;
        public HealthSystem sHealth;
        public SpawnSystem sSpawn;
        public CollisionSystem sCollision;

        #endregion

        public SystemManager(Game1 myGame)
        {
            this.myGame = myGame;
            // Initiate Instances
            sSelectionHandler = new SelectionHandlerSystem(myGame);
            sHealth = new HealthSystem(myGame);
            sMovement = new MovementSystem(myGame);
            sSpawn = new SpawnSystem(myGame);
            sCollision = new CollisionSystem(myGame);
        }

        public void Run(GameTime gameTime)
        {
            //Run SystemInstances
            sSelectionHandler.RunSystem();
            sHealth.RunSystem();
            sMovement.RunSystem(gameTime);
            sSpawn.RunSystem();
            sCollision.runSystem();
        }

    }
}
