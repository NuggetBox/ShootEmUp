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
        public SpriteFont mySpriteFont;

        public static Texture2D myBulletTexture;

        public Game1()
        {
            AccessWindowSize = new Point(1280, 720);

            myGraphics = new GraphicsDeviceManager(this);
            myGraphics.PreferredBackBufferWidth = AccessWindowSize.X;
            myGraphics.PreferredBackBufferHeight = AccessWindowSize.Y;
            //if (!myGraphics.IsFullScreen)
            //{
            //    myGraphics.ToggleFullScreen();
            //}
            IsMouseVisible = false;
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

            Menu menu = new Menu(this);
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

            // TODO: use this.Content to load your game content here
            mySpriteFont = Content.Load<SpriteFont>("Standard");
            myBulletTexture = Content.Load<Texture2D>("bullet");
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            if (GetCurrentState is Menu)
            {
                GraphicsDevice.Clear(Color.Azure);
            }

            // TODO: Add your update logic here
            GetCurrentState.Update(gameTime);
            GetCurrentState.Initialize(Content);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            mySpriteBatch.Begin();

            base.Draw(gameTime);

            GetCurrentState.Draw(gameTime, mySpriteBatch);

            mySpriteBatch.End();
        }
    }
}
