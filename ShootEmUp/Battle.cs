using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootEmUp
{
    class Battle : State
    {
        public static Player[] myPlayers;

        public static List<Bullet> myBullets;

        List<Water> mySea;

        KeyboardState myKeyboardState;

        public override void Initialize()
        {
            myPlayers = new Player[]
            {
                new Player() { myPlayerOne = true, myStartPos = new Vector2(320, 360), mySpeed = 360, myRotationSpeed = 3.5f, myOriginalAttackCooldown = 0.15f, myHealth = 25, myBulletSpeed = 450 },

                new Player() { myUp = Keys.Up, myDown = Keys.Down, myRight = Keys.Right, myLeft = Keys.Left,
                    myClockwiseRotation = Keys.NumPad6, myCounterClockwiseRotation = Keys.NumPad4, myShoot = Keys.NumPad8,
                    myStartPos = new Vector2(960, 360), myPlayerOne = false, mySpeed = 360, myRotationSpeed = 3.5f, myOriginalAttackCooldown = 0.15f, myHealth = 25, myBulletSpeed = 450 }
            };

            myPlayers[0].myPosition = myPlayers[0].myStartPos;
            myPlayers[1].myPosition = myPlayers[1].myStartPos;

            myBullets = new List<Bullet>();

            mySea = new List<Water>()
            {
                new Water(Game1.AccessWindowSize.X * 0.25f, Game1.AccessWindowSize.Y * 0.5f),
                new Water(Game1.AccessWindowSize.X * 0.75f, Game1.AccessWindowSize.Y * 0.5f),
                new Water(Game1.AccessWindowSize.X * 0.25f, -Game1.AccessWindowSize.Y * 0.5f),
                new Water(Game1.AccessWindowSize.X * 0.75f, -Game1.AccessWindowSize.Y * 0.5f),
            };
        }

        public override void Update(GameTime someDeltaTime)
        {
            myKeyboardState = Keyboard.GetState();

            if (myKeyboardState.IsKeyDown(Keys.Escape))
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

            for (int i = 0; i < mySea.Count; ++i)
            {
                mySea[i].Update(someDeltaTime);

                if (mySea[i].myPosition.Y >= Game1.AccessWindowSize.Y * 1.5f)
                {
                    if (mySea[i].myPosition.X == Game1.AccessWindowSize.X * 0.25f)
                    {
                        mySea.Add(new Water(Game1.AccessWindowSize.X * 0.25f, -Game1.AccessWindowSize.Y * 0.5f));
                    }
                    else
                    {
                        mySea.Add(new Water(Game1.AccessWindowSize.X * 0.75f, -Game1.AccessWindowSize.Y * 0.5f));
                    }

                    mySea[i].myRemoved = true;
                }
            }

            for (int i = 0; i < myPlayers.Length; ++i)
            {
                myPlayers[i].Update(someDeltaTime);
            }

            for (int i = 0; i < myBullets.Count; ++i)
            {
                myBullets[i].Update(someDeltaTime);
            }

            for (int i = 0; i < myBullets.Count; ++i)
            {
                if (myBullets[i].myRemoved)
                {
                    myBullets.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < mySea.Count; ++i)
            {
                if (mySea[i].myRemoved)
                {
                    mySea.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            for (int i = 0; i < mySea.Count; ++i)
            {
                aSpriteBatch.Draw(mySea[i].myTexture, mySea[i].myPosition, null, Color.White, 0, mySea[i].GetOrigin, 5 + 1/3f, SpriteEffects.None, 0);
            }

            aSpriteBatch.Draw(Game1.myBattleBeach, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 5 + 1/3f, SpriteEffects.None, 0.1f);

            for (int i = 0; i < myPlayers.Length; ++i)
            {
                myPlayers[i].Draw(aSpriteBatch);
                aSpriteBatch.DrawString(Game1.mySpriteFont, myPlayers[i].myHealth.ToString(), new Vector2(1000 * i, 0), Color.White);
            }

            for (int i = 0; i < myBullets.Count; ++i)
            {
                myBullets[i].Draw(aSpriteBatch);
            }
        }
    }
}
