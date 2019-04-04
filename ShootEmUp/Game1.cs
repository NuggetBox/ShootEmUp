using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShootEmUp
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static Stack<State> AccessStateStack { get; set; }
        public static State GetCurrentState => AccessStateStack.Peek();
        public static Point AccessWindowSize { get; private set; }

        GraphicsDeviceManager myGraphics;
        SpriteBatch mySpriteBatch;

        public static SpriteFont mySpriteFont;

        public static Texture2D 
            myPlayer,
            myPlayerBullet;

        //public static Texture2D hej;
        public static Texture2D
            myCrab,
            myCrabPinch,
            myOctopus,
            myOctopusSlither,
            myInk,
            myPirate,
            myEnemyBullet,
            myBeach,
            myClamClosed,
            myClamOpening,
            myClamOpen,
            myPearl;

        public static bool myQuit;

        public Game1()
        {
            AccessWindowSize = new Point(1280, 720);

            myGraphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = AccessWindowSize.X,
                PreferredBackBufferHeight = AccessWindowSize.Y
            };

            IsMouseVisible = true;
            myGraphics.ApplyChanges();
            Content.RootDirectory = "Content";
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
            AccessStateStack = new Stack<State>();

            List<Button> tempButtons = new List<Button>
            {
                new Button("Start", Menu.Start),
                new Button("Quit", Menu.Quit),
            };

            Menu menu = new Menu(tempButtons);

            AccessStateStack.Push(menu);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            mySpriteBatch = new SpriteBatch(GraphicsDevice);

            mySpriteFont = Content.Load<SpriteFont>("Standard");

            myPlayer = Content.Load<Texture2D>("ship");
            myPlayerBullet = Content.Load<Texture2D>("ball");

            myCrab = Content.Load<Texture2D>("crab");
            myCrabPinch = Content.Load<Texture2D>("crabpinch");

            myOctopus = Content.Load<Texture2D>("octopus");
            myOctopusSlither = Content.Load<Texture2D>("octopusslither");
            myInk = Content.Load<Texture2D>("ink");

            myPirate = Content.Load<Texture2D>("pirate");
            myEnemyBullet = Content.Load<Texture2D>("ball");

            myBeach = Content.Load<Texture2D>("beach");

            myClamClosed = Content.Load<Texture2D>("clamclosed");
            myClamOpening = Content.Load<Texture2D>("clamopening");
            myClamOpen = Content.Load<Texture2D>("clamopen");
            myPearl = Content.Load<Texture2D>("pearl");
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
            // TODO: Add your update logic here
            if (myQuit)
                Exit();

            GetCurrentState.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DodgerBlue);

            // TODO: Add your drawing code here
            mySpriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, null);

            base.Draw(gameTime);

            GetCurrentState.Draw(gameTime, mySpriteBatch);

            mySpriteBatch.End();
        }
    }
}
