using DotalWar.Managers;
using Dotal_War.Components;
using Dotal_War.Managers;
using Dotal_War.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Dotal_War.Commands;

namespace Dotal_War
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GlobalVariables globalVariables = new GlobalVariables();
        public GameState CurrentGameState;

        Texture2D mouseClick;
        Rectangle mouseClickRectangle;


        #region EntityComponentSystem
        private DeadParrotCollector EntityCleaner;
        public EntityManager EntityManager;
        public SystemManager SystemManager;
        public ComponentManager ComponentManager;

        public RenderComponent RenderComponent;
        public RenderSystem RenderSystem;
        SelectionRectange TestSelect;
        TargetSelection targetSelection;
        #endregion

        StartMenu startMenu;
        TestWorld testWorld;

        PlayerInput firstPlayer;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            globalVariables.WindowWidth = 1280;
            globalVariables.WindowHeight = 720;

            globalVariables.universalScaleFactor = 1;

            graphics.PreferredBackBufferWidth = globalVariables.WindowWidth;
            graphics.PreferredBackBufferHeight = globalVariables.WindowHeight;

            CurrentGameState = GameState.Gameplay;
            IsMouseVisible = true;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            startMenu = new StartMenu(this);


            firstPlayer = new PlayerInput(this);
            firstPlayer.TESTBINDING();

            EntityManager = new EntityManager(this);
            SystemManager = new SystemManager(this);
            ComponentManager = new ComponentManager(this);

            TestSelect = new SelectionRectange(this);
            targetSelection = new TargetSelection(this);
            RenderSystem = new RenderSystem(this);
            RenderComponent = new RenderComponent(this);
            testWorld = new TestWorld(this);
            EntityCleaner = new DeadParrotCollector(this);
            
            //DEBUG
            mouseClick = Content.Load<Texture2D>(@"Graphics\Mouse0");
            mouseClickRectangle = new Rectangle((int)(Mouse.GetState().X) - mouseClick.Width / 2, (int)(Mouse.GetState().Y) - mouseClick.Height / 2, mouseClick.Width, mouseClick.Height);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            MouseState mouse = Mouse.GetState();

            //DEBUG
            if (mouse.RightButton == ButtonState.Pressed)
            {
                mouseClickRectangle.X = mouse.X - mouseClickRectangle.Width/2;
                mouseClickRectangle.Y = mouse.Y - mouseClickRectangle.Height / 2;
            }
            //

            switch (CurrentGameState)
            {
                case GameState.StartMenu:
                    startMenu.Update(mouse);
                    break;

                default:
                    firstPlayer.handleInput();
                    targetSelection.RunTarget();
                    TestSelect.Run(mouse);
                    SystemManager.Run(gameTime);
                    break;

            }

            EntityCleaner.Run();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.StartMenu:
                    startMenu.Draw(spriteBatch);
                    break;

                default:
                    RenderSystem.RunSystem(spriteBatch);
                    TestSelect.Draw(spriteBatch);
                    break;
            }
            //DEBUG
            spriteBatch.Draw(mouseClick, mouseClickRectangle, Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
