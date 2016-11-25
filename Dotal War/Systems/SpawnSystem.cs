using Dotal_War.Components;
using Dotal_War.Interfaces;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotal_War.Systems
{
    public class SpawnSystem:ISystem
    {
        #region Fields

        List<int> Subscribtions;
        Game1 myGame;
        EntityManager EntityManager;
        ComponentManager componentManager;
        RenderComponent RenderComponent;
        Entity updatingEntity;
        ButtonState RightPS;
        Vector2 SpawnPoint;
        Vector2 Position;
        Rectangle SpawnRadius;
        Entity SpawnedEntity;
        List<Vector2> SpawnLocation;
        MouseState mouse;
        bool initiate = false;


        #endregion

        #region Methodes

        public SpawnSystem(Game1 myGame)
        {
            SpawnLocation = new List<Vector2>();
            EntityManager = myGame.EntityManager;
            RenderComponent = myGame.RenderComponent;
            mouse = Mouse.GetState();
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
            //if (initiate)
            //{   componentManager = myGame.ComponentManager; }

            foreach (int subs in Subscribtions)
            {
                updatingEntity = EntityManager.GetEntity(subs);
                SpawnPoint = (Vector2)(updatingEntity.cBag[DataType.SpawnLocation]);
                Position = (Vector2)(updatingEntity.cBag[DataType.Position]);
                SpawnRadius = (Rectangle)(updatingEntity.cBag[DataType.SpawnRadius]);

                if ((bool)(updatingEntity.cBag[DataType.IsSelected]) && mouse.RightButton == ButtonState.Released && RightPS == ButtonState.Pressed)
                {
                    if (SpawnRadius.Contains(mouse.Position))
                    {
                        SpawnLocation.Clear();
                        SpawnPoint.X = mouse.X;
                        SpawnPoint.Y = mouse.Y;
                        updatingEntity.cBag[DataType.SpawnLocation] = SpawnPoint;
                        SpawnLocation.Add(SpawnPoint);
                    }
                }

                if ((bool)(updatingEntity.cBag[DataType.IsSelected]) && Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    SpawnedEntity = EntityManager.AddEntity(Position);
                    SpawnedEntity.AddComponent(componentManager.cMovement, 3f);
                    SpawnedEntity.AddComponent(componentManager.cSelectionHandler, SelectionType.Units);
                    SpawnedEntity.AddComponent(RenderComponent, @"Graphics\Unit0");

                    SpawnedEntity.cBag[DataType.IsMoveValid] = true;
                    SpawnedEntity.cBag[DataType.TargetType] = TargetType.Individual;
                    SpawnedEntity.cBag[DataType.Target] = SpawnLocation;

                }


               updatingEntity = null;
            }
            initiate = true;
            RightPS = mouse.RightButton;
        }

        #endregion


    }
}
