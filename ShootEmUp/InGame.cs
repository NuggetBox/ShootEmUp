using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace ShootEmUp
{
    class InGame : State
    {
        public static GameObject GetPlayer => myGameObjects[0];

        public Level myCurrentLevel { get { return myLevels[myLevelIndex]; } set { myCurrentLevel = value; } }

        public static List<GameObject> myGameObjects;

        public static List<Level> myLevels = new List<Level>
        {
            new Level(0, 10, 1, 3, 1, 2, 2, 5, 0, 1, false),
            
        };
    
        public int myLevelIndex; 

        public int x = 500, y = 100;

        public float
            myEnemyTimer,
            myWaveTimer,
            myLevelTimer;

        public static int myScore;

        public override void Initialize()
        {
            myEnemyTimer = myCurrentLevel.myEnemyDelay;

            myGameObjects = new List<GameObject>()
            {
                new Player(),

                //new Pirate(20, 510),

                //new Octopus(500, 50), 

                //new Clam(250, 250),

                //new Crab(20, 20),

                //new Crab(0, 0), new Crab(100, 50), new Crab(200, 0), new Crab(300, 0), new Crab(400, 0), new Crab(500, 0), new Crab(600, 0), new Crab(600, 0),
                //new Crab(25, 25), new Crab(100, 150), new Crab(200, 100), new Crab(300, 100), new Crab(400, 100), new Crab(500, 100), new Crab(500, 100), new Crab(500, 100),
                //new Crab(50, 50), new Crab(100, 250), new Crab(200, 200), new Crab(300, 200), new Crab(400, 200), new Crab(500, 200), new Crab(500, 200), new Crab(500, 200),
                //new Crab(75, 75), new Crab(100, 350), new Crab(200, 300), new Crab(300, 300), new Crab(400, 300), new Crab(500, 300), new Crab(500, 300), new Crab(500, 300),
                //new Crab(100, 600), new Crab(100, 450), new Crab(200, 400), new Crab(300, 400), new Crab(400, 400), new Crab(500, 400), new Crab(500, 400), new Crab(500, 400),
                //new Crab(100, 100), new Crab(100, 550), new Crab(200, 500), new Crab(300, 500), new Crab(400, 500), new Crab(500, 500), new Crab(500, 500), new Crab(500, 500),
            };
        }

        public override void Update(GameTime someDeltaTime)
        {
            float tempDelta = (float)someDeltaTime.ElapsedGameTime.TotalSeconds;
            KeyboardState tempKeyboardState = Keyboard.GetState();

            UpdateLevel(tempDelta);

            if (tempKeyboardState.IsKeyDown(Keys.Escape))
            {
                List<Button> tempButtons = new List<Button>
                {
                    new Button("Resume", Menu.Resume),
                    new Button("Exit To Main", Menu.ExitToMain),
                    new Button("Quit Game", Menu.Quit),
                };

                Game1.AccessStateStack.Push(new Menu(tempButtons));
            }

            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                myGameObjects[i].Update(someDeltaTime);
            }

            // Removes GameObjects that have "died" this update
            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                if (myGameObjects[i].myRemoved)
                {
                    if (myGameObjects[i] is Enemy)
                    {
                        ++myScore;
                    }

                    myGameObjects.RemoveAt(i);
                    --i;
                }
            }
        }

        public void UpdateLevel(float someDeltaTime)
        {
            if (myCurrentLevel.myComplete)
            {
                BetweenLevels(someDeltaTime);
            }

            if (myCurrentLevel.mySpawnedEnemies == myCurrentLevel.myNumEnemies)
            {
                myCurrentLevel.myComplete = true;
                myEnemyTimer = myCurrentLevel.myEnemyDelay;
            }
            else if (myEnemyTimer <= 0)
            {
                myCurrentLevel.mySpawnedEnemies++;
                myGameObjects.Add(new Crab(x, y));
                myEnemyTimer = myCurrentLevel.myEnemyDelay;
            }

            myEnemyTimer -= someDeltaTime;
        }

        bool BetweenLevels(float someDeltaTime)
        {
            if (myCurrentLevel.myLevelDelay <= 0)
            {
                myCurrentLevel = myLevels[myCurrentLevel.GetLevelNumber];
                return true;
            }

            myCurrentLevel.myLevelDelay -= someDeltaTime;
            return false;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game1.myBeach, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 20, SpriteEffects.None, 0);

            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                myGameObjects[i].Draw(aSpriteBatch);
            }

            aSpriteBatch.DrawString(Game1.mySpriteFont, "Score: " + myScore, new Vector2(10, 40), Color.White, 0, Vector2.Zero, 0.2f, SpriteEffects.None, 1);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Health: " + InGame.GetPlayer.myHealth, new Vector2(10, 10), Color.White, 0, Vector2.Zero, 0.2f, SpriteEffects.None, 1);
        }
    }
}
