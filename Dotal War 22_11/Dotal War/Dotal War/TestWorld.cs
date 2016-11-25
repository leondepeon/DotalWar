using Dotal_War.Components;
using Dotal_War.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dotal_War
{
    public class TestWorld
    {
        Game1 myGame;
        GlobalVariables globalVariables;
        ComponentManager componentManager;

        Entity Unit;
        Entity Unit2;
        Entity Alter;


        public TestWorld(Game1 mygame)
        {
            myGame = mygame;
            globalVariables = mygame.globalVariables;
            componentManager = mygame.ComponentManager;


            Unit = myGame.EntityManager.AddEntity(new Vector2(globalVariables.WindowWidth / 2, globalVariables.WindowHeight / 2));
            Unit.AddComponent(componentManager.cMovement, 3f);
            Unit.AddComponent(componentManager.cSelectionHandler, SelectionType.Units);
            Unit.AddComponent(mygame.RenderComponent, @"Graphics\Unit0");
            Unit.AddComponent(componentManager.cHealth, 100);
            ////Unit.AddComponent(componentManager.cCollision, true);

            //Unit2 = myGame.EntityManager.AddEntity(new Vector2(globalVariables.WindowWidth / 2, globalVariables.WindowHeight / 3));
            //Unit2.AddComponent(componentManager.cMovement, 3f);
            //Unit2.AddComponent(componentManager.cSelectionHandler, false);
            //Unit2.AddComponent(mygame.RenderComponent, @"Graphics\Unit0");
            //Unit2.AddComponent(componentManager.cHealth, 100);

            Alter = myGame.EntityManager.AddEntity(new Vector2(globalVariables.WindowWidth / 4, globalVariables.WindowHeight / 2));
            Alter.AddComponent(mygame.RenderComponent, @"Graphics\StaticBlock");
            Alter.AddComponent(componentManager.cSelectionHandler, SelectionType.Buildings);
            Alter.AddComponent(componentManager.cSpawn, 20);
           // Alter.AddComponent(componentManager.cCollision, true);

        }
    }
}
