﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using System;

namespace ShootEmUp
{
    class InGame : State
    {
        public static Player AccessPlayer { get { return myGameObjects[0] as Player; } set { myGameObjects[0] = value; } }

        public Level AccessLevel { get { return myLevels[myLevelIndex]; } set { AccessLevel = value; } }

        public static Random myRandom = new Random();

        enum LevelState { Spawning, BetweenWave, BetweenLevel, Boss }
        LevelState myState = new LevelState();

        public static List<GameObject> myGameObjects;

        public static List<Level> myLevels = new List<Level>();

        Point
            myLeftSpawn = new Point(0, 0),
            myRightSpawn = new Point(1280, 720),
            myLeftSpawn2 = new Point(0, 720),
            myRightSpawn2 = new Point(1280, 720);

        bool mySpawnSwap = true;
    
        public int myLevelIndex;

        public float
            myPowerUpTimer,
            myEnemyTimer,
            myWaveTimer,
            myLevelTimer;

        public static int myScore;

        public override void Initialize()
        {
            myScore = 0;

            myLevels = new List<Level>
            {
                new Level(1, 1, 1, 20, 5, 3, 0.96f, 0.75f, 2, 0.2f), // Default

                //new Level(1, 0, 0, 5, 0, 1, 1000, 0, 0, 0), //Crab art
                //new Level(0, 0, 1, 5, 0, 1, 1000, 0, 0, 0), //Clam art
                //new Level(0, 1, 0, 5, 0, 1, 1000, 0, 0, 0), //Octopus art
                //new Level(0, 0, 0, 5, 0, 1000, 1, 0, 0, 0), //Barrel art
            };

            myPowerUpTimer = AccessLevel.myPowerUpDelay;
            myEnemyTimer = AccessLevel.myEnemyDelay;
            myWaveTimer = AccessLevel.myWaveDelay;
            myLevelTimer = AccessLevel.myLevelDelay;

            myGameObjects = new List<GameObject>()
            {
                new Player(true),

                new Water(Game1.AccessWindowSize.X * 0.5f, Game1.AccessWindowSize.Y * 0.5f),
                new Water(Game1.AccessWindowSize.X * 0.5f, - Game1.AccessWindowSize.Y * 0.5f),
            };

            AccessPlayer.myTexture = Game1.myPlayerTexture;
            myGameObjects.Add(new FireRate(3));
        }

        public void UpdateLevel(float someDeltaTime)
        {
            switch (myState)
            {
                case LevelState.Spawning:
                    myEnemyTimer -= someDeltaTime;
                    myPowerUpTimer -= someDeltaTime;

                    if (myPowerUpTimer <= 0)
                    {
                        myGameObjects.Add(AccessLevel.GetNextPowerUp(3));
                        myPowerUpTimer = AccessLevel.myPowerUpDelay;
                    }

                    if (myEnemyTimer <= 0)
                    {
                        if (myRightSpawn.Y < 0 || myRightSpawn.Y > 720)
                        {
                            mySpawnSwap = mySpawnSwap ? false : true;
                        }

                        myGameObjects.Add(AccessLevel.GetNextEnemy(myLeftSpawn.X, myLeftSpawn.Y));
                        myGameObjects.Add(AccessLevel.GetNextEnemy(myRightSpawn.X, myRightSpawn.Y));
                        myGameObjects.Add(AccessLevel.GetNextEnemy(myLeftSpawn.X, myLeftSpawn2.Y - myLeftSpawn.Y));
                        myGameObjects.Add(AccessLevel.GetNextEnemy(myRightSpawn.X, myRightSpawn2.Y - myRightSpawn.Y));
                        AccessLevel.mySpawnedEnemies += 4;
                        AccessLevel.myEnemyDelay *= AccessLevel.myEnemyDelayFactor;

                        // ENEMY DELAY
                        if (AccessLevel.myEnemyDelay <= AccessLevel.myMinEnemyDelay)
                        {
                            AccessLevel.myEnemyDelay = AccessLevel.myMinEnemyDelay;
                        }

                        myEnemyTimer = AccessLevel.myEnemyDelay;

                        if (mySpawnSwap)
                        {
                            myLeftSpawn = new Point(myLeftSpawn.X, myLeftSpawn.Y + AccessLevel.mySpawnChange);
                            myRightSpawn = new Point(myRightSpawn.X, myRightSpawn.Y - AccessLevel.mySpawnChange);
                        }
                        else
                        {
                            myLeftSpawn = new Point(myLeftSpawn.X, myLeftSpawn.Y - AccessLevel.mySpawnChange);
                            myRightSpawn = new Point(myRightSpawn.X, myRightSpawn.Y + AccessLevel.mySpawnChange);
                        }
                    }
                    
                    if (AccessLevel.mySpawnedEnemies >= AccessLevel.myNumEnemies && !AccessLevel.myIsEndless)
                    {
                        myState = LevelState.BetweenWave;
                    }

                    break;
                case LevelState.BetweenWave:
                    if (myWaveTimer <= 0)
                    {
                        if (AccessLevel.myCompletedWaves == AccessLevel.myNumWaves && !AccessLevel.myIsEndless)
                        {
                            if (AccessLevel.myIsBoss)
                            {
                                myState = LevelState.Boss;
                            }
                            else if (IsEveryoneDead())
                            {
                                myState = LevelState.BetweenLevel;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            AccessLevel.mySpawnedEnemies = 0;
                            AccessLevel.myCompletedWaves++;
                            AccessLevel.myNumEnemies = (int)(AccessLevel.myIncreaseEnemyFactor * AccessLevel.myNumEnemies);
                            myState = LevelState.Spawning;
                        }

                        myWaveTimer = AccessLevel.myWaveDelay;
                    }

                    myWaveTimer -= someDeltaTime;

                    break;
                case LevelState.BetweenLevel:

                    if (myLevelTimer <= 0)
                    {
                        if (myLevels.Count == myLevelIndex + 1)
                        {
                            throw new Exception();
                            // OUT OF LEVELS
                        }
                        else
                        {
                            myLevelIndex++;
                            myState = LevelState.Spawning;
                        }
                    }

                    myLevelTimer -= someDeltaTime;

                    break;
                case LevelState.Boss:
                    UpdateBoss(someDeltaTime);

                    break;
            }
        }

        bool IsEveryoneDead()
        {
            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                if (myGameObjects[i] is Enemy)
                {
                    return false;
                }
            }

            return true;
        }

        bool BetweenLevels(float someDeltaTime)
        {
            if (AccessLevel.myLevelDelay <= 0)
            {
                if (myLevels.Count == AccessLevel.GetLevelNumber)
                {
                    //Menu.ExitToMain();
                }
                else
                {
                    AccessLevel = myLevels[AccessLevel.GetLevelNumber];
                    return true;
                }
            }

            AccessLevel.myLevelDelay -= someDeltaTime;
            return false;
        }

        void UpdateBoss(float someDeltaTime)
        {

        }

        public override void Update(GameTime someDeltaTime)
        {
            float tempDelta = (float)someDeltaTime.ElapsedGameTime.TotalSeconds;
            KeyboardState tempKeyboardState = Keyboard.GetState();

            UpdateLevel(tempDelta);

            // TODO: Pause
            if (tempKeyboardState.IsKeyDown(Keys.Escape))
            {
                List<Button> tempButtons = new List<Button>
                {
                    new Button("Resume", Menu.Resume),
                    new Button("Skins", Menu.SkinCustomization),
                    new Button("Main Menu", Menu.ExitToMain),
                    new Button("Quit", Menu.Quit),
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

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game1.myBeach, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 5 + 1f/3, SpriteEffects.None, 0.11f);

            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                myGameObjects[i].Draw(aSpriteBatch);
            }

            aSpriteBatch.DrawString(Game1.mySpriteFont, "Score: " + myScore, new Vector2(10, 40), Color.Blue, 0, Vector2.Zero, 0.2f, SpriteEffects.None, 1);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Health: " + InGame.AccessPlayer.myHealth, new Vector2(10, 10), Color.Blue, 0, Vector2.Zero, 0.2f, SpriteEffects.None, 1);
        }
    }
}
