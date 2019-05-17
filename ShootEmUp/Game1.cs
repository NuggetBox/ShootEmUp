using Microsoft.Xna.Framework;
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
        public static readonly bool myPreOrder = true;

        public static Stack<State> AccessStateStack { get; set; }
        public static State GetCurrentState => AccessStateStack.Peek();
        public static Point AccessWindowSize { get; private set; }

        public static string GetFullDirectory => GetDirectory + @myFileName;
        public static string GetDirectory => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BoatGame\boatgamedata\";

        static string myFileName = "data.bog";

        public static int? myFinalScore = null;

        GraphicsDeviceManager myGraphics;
        SpriteBatch mySpriteBatch;

        KeyboardState myPreviousKeyboard;

        public static SpriteFont mySpriteFont;

        public static SoundEffectInstance mySong;

        Keys
            myPitchUp = Keys.E,
            myPitchDown = Keys.Q,
            myPitchReset = Keys.R,
            myPitchRandom = Keys.F,
            myMute = Keys.M;

        public static Texture2D myPlayerTexture;

        public static Texture2D[] myPreOrderSkinsFire;
        public static Texture2D[] myPreOrderSkinsWater;

        public static Texture2D
            myShip,
            myShipFire1,
            myShipFire2,
            myShipFire3,
            myShipFire4,
            myShipWater1,
            myShipWater2,
            myShipWater3,
            myShipWater4,
            myShipPurple,
            myShipBee,
            myShipRed,
            myShipColor,
            myShipTriple,
            myShipSteam,
            myShipThicc;

        public static Texture2D
            myEnter,
            myEsc,
            myRight,
            myLeft,
            myUp,
            myDown,
            myLock,
            myW,
            myA,
            myS,
            myD,
            myQ,
            myE,
            myR,
            myF,
            myI,
            myJ,
            myL,
            myM;

        public static Texture2D
            myPlayerBullet,
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
                new Button("Tutorial", Menu.Tutorial),
                new Button("Quit", Menu.Quit),
            };

            Menu menu = new Menu(tempButtons);

            AccessStateStack.Push(menu); 

            base.Initialize();

            if (!File.Exists(GetFullDirectory))
            {
                AccessStateStack.Push(new Tutorial());
                GetCurrentState.Initialize();
            }
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

            mySpriteFont = Content.Load<SpriteFont>("Standard");

            myShip = GetContent("ship");
            myShipFire1 = GetContent("ShipFire");
            myShipFire2 = GetContent("ShipFire2");
            myShipFire3 = GetContent("ShipFire3");
            myShipFire4 = GetContent("ShipFire4");
            myShipWater1 = GetContent("shipWater");
            myShipWater2 = GetContent("shipWater2");
            myShipWater3 = GetContent("shipWater3");
            myShipWater4 = GetContent("shipWater4");
            myShipPurple = GetContent("shipPurple");
            myShipBee = GetContent("shipBee");
            myShipRed = GetContent("shipRed");
            myShipColor = GetContent("shipColor");
            myShipTriple = GetContent("shipTriple");
            myShipSteam = GetContent("shipSteam");
            myShipThicc = GetContent("shipThicc");

            myW = GetContent("w");
            myA = GetContent("a");
            myS = GetContent("s");
            myD = GetContent("d");
            myQ = GetContent("q");
            myE = GetContent("e");
            myR = GetContent("r");
            myF = GetContent("f");
            myJ = GetContent("j");
            myI = GetContent("i");
            myL = GetContent("l");
            myM = GetContent("m");
            myLock = GetContent("lock");
            myEnter = GetContent("enter");
            myRight = GetContent("right");
            myLeft = GetContent("left");
            myUp = GetContent("up");
            myDown = GetContent("down");
            myEsc = GetContent("esc");

            myBarrel = GetContent("barrel");
            myPlayerBullet = GetContent("ball");

            myCrab = GetContent("crab");
            myCrabPinch = GetContent("crabpinch");

            myOctopus = GetContent("octopus");
            myOctopusSlither = GetContent("octopusslither");
            myInk = GetContent("ink");

            myBeach = GetContent("beach");
            myWater = GetContent("water");

            myClamClosed = GetContent("clamclosed");
            myClamOpening = GetContent("clamopening");
            myClamOpen = GetContent("clamopen");
            myPearl = GetContent("pearl");

            myPreOrderSkinsFire = new Texture2D[4]
            {
                myShipFire1, myShipFire2, myShipFire3, myShipFire4,
            };

            myPreOrderSkinsWater = new Texture2D[4]
            {
                myShipWater1, myShipWater2, myShipWater3, myShipWater4,
            };

            myPlayerTexture = myShip;
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
            if (tempKeyboard.IsKeyDown(myMute) && myPreviousKeyboard.IsKeyUp(myMute))
            {
                mySong.Volume = mySong.Volume == 1 ? 0 : 1;
            }

            // TODO: Add your update logic here
            if (myQuit)
                QuitGame();

            GetCurrentState.Update(gameTime);

            myPreviousKeyboard = tempKeyboard;

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

        Texture2D GetContent(string aName)
        {
            return Content.Load<Texture2D>(aName);
        }

        public static void Save()
        {
            if (!Directory.Exists(GetDirectory))
            {
                Directory.CreateDirectory(GetDirectory);
            }

            File.WriteAllText(GetFullDirectory, Customization.myIndex.ToString());
        }
    }
}
