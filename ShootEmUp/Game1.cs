﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.IO;
using System;

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

        public static string GetFullDirectory => GetDirectory + myFileName;
        public static string GetDirectory => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BoatGame\boatgamedata\";
        static string myFileName = "data.bog";

        public static int myFinalScore = -1;

        GraphicsDeviceManager myGraphics;
        SpriteBatch mySpriteBatch;

        public static SpriteFont mySpriteFont;

        Keys
            myPitchUp = Keys.PageUp,
            myPitchDown = Keys.PageDown,
            myPitchReset = Keys.Q,
            myPitchRandom = Keys.E;

        public static Texture2D myPlayerTexture;

        public static Texture2D
            myShip,
            myShipGreen,
            myShipRed,
            myShipSteam,
            myShipThicc;

        public static SoundEffectInstance mySong;

        public static Texture2D
            myPlayerBullet,
            myLeft, 
            myRight,
            myEnter,
            myBarrel,
            myCrab,
            myCrabPinch,
            myOctopus,
            myOctopusSlither,
            myInk,
            myBeach,
            myWater,
            myClamClosed,
            myClamOpening,
            myClamOpen,
            myPearl;

        public static int
            myLeftBeach = 300,
            myRightBeach = 980;

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
                new Button("Skins", Menu.SkinCustomization),
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
            // CRAB RAVE
            mySong = Content.Load<SoundEffect>("crabrave").CreateInstance();
            mySong.IsLooped = true;
            mySong.Play();

            // Create a new SpriteBatch, which can be used to draw textures.
            mySpriteBatch = new SpriteBatch(GraphicsDevice);

            myShip = Content.Load<Texture2D>("ship");
            myShipGreen = Content.Load<Texture2D>("shipGreen");
            myShipRed = Content.Load<Texture2D>("shipRed");
            myShipSteam = Content.Load<Texture2D>("shipSteam");
            myShipThicc = Content.Load<Texture2D>("shipThicc");

            myPlayerTexture = myShip;

            mySpriteFont = Content.Load<SpriteFont>("Standard");

            myBarrel = Content.Load<Texture2D>("barrel");
            myPlayerBullet = Content.Load<Texture2D>("ball");

            myLeft = Content.Load<Texture2D>("left");
            myRight = Content.Load<Texture2D>("right");
            myEnter = Content.Load<Texture2D>("enter");

            myCrab = Content.Load<Texture2D>("crab");
            myCrabPinch = Content.Load<Texture2D>("crabpinch");

            myOctopus = Content.Load<Texture2D>("octopus");
            myOctopusSlither = Content.Load<Texture2D>("octopusslither");
            myInk = Content.Load<Texture2D>("ink");

            myBeach = Content.Load<Texture2D>("beach");
            myWater = Content.Load<Texture2D>("water");

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
            KeyboardState tempKeyboard = Keyboard.GetState();
            float tempDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (tempKeyboard.IsKeyDown(myPitchUp))
            {
                float tempPitch = mySong.Pitch + 0.1f * tempDelta;

                if (tempPitch > 1)
                {
                    mySong.Pitch = 1;
                }
                else
                {
                    mySong.Pitch = tempPitch;
                }
            }
            if (tempKeyboard.IsKeyDown(myPitchDown))
            {
                float tempPitch = mySong.Pitch - 0.1f * tempDelta;

                if (tempPitch < -1)
                {
                    mySong.Pitch = -1;
                }
                else
                {
                    mySong.Pitch = tempPitch;
                }
            }
            if (tempKeyboard.IsKeyDown(myPitchRandom))
            {
                int tempPitch = Player.myRandom.Next(-100, 101);
                mySong.Pitch = tempPitch * 0.01f;
            }
            if (tempKeyboard.IsKeyDown(myPitchReset))
            {
                mySong.Pitch = 0;
            }

            // TODO: Add your update logic here
            if (myQuit)
                QuitGame();

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

        void QuitGame()
        {
            Save();
            Exit();
        }

        public static void Save()
        {
            if (Directory.Exists(GetDirectory))
            {
                File.WriteAllText(GetFullDirectory, Customization.index.ToString());
            }
            else
            {
                Directory.CreateDirectory(GetDirectory);
                File.WriteAllText(GetFullDirectory, Customization.index.ToString());
            }
        }
    }
}
