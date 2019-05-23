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
    class BattleCustomization : State
    {
        Texture2D
            myPlayerOneLeft,
            myPlayerOneRight,
            myPlayerOneConfirm;

        Texture2D
            myPlayerTwoLeft,
            myPlayerTwoRight,
            myPlayerTwoConfirm;

        List<Texture2D> mySkins;

        readonly Keys
            myPlayerOneLeftKey = Keys.A,
            myPlayerOneRightKey = Keys.D,
            myPlayerOneConfirmKey = Keys.J;

        readonly Keys
            myPlayerTwoLeftKey = Keys.Left,
            myPlayerTwoRightKey = Keys.Right,
            myPlayerTwoConfirmKey = Keys.Enter;

        KeyboardState 
            myKeyboardState,
            myPreviousKeyboardState;

        public static int
            myPlayerOneIndex = 0,
            myPlayerTwoIndex = 0;

        float myPreOrderScale;

        bool
            myPlayerOneReady,
            myPlayerTwoReady;

        public BattleCustomization(KeyboardState aPreviousKeyboardState)
        {
            myPreviousKeyboardState = aPreviousKeyboardState;
        }

        public override void Initialize()
        {
            myPlayerOneLeft = Game1.myA;
            myPlayerOneRight = Game1.myD;
            myPlayerOneConfirm = Game1.myJ;

            myPlayerTwoLeft = Game1.myLeft;
            myPlayerTwoRight = Game1.myRight;
            myPlayerTwoConfirm = Game1.myEnter;

            mySkins = Customization.mySkins;
            mySkins.Remove(Game1.myShipFire1);
            mySkins.Remove(Game1.myShipWater1);
        }

        public override void Update(GameTime someDeltaTime)
        {
            myKeyboardState = Keyboard.GetState();

            float tempDelta = (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (myPlayerOneReady && myPlayerTwoReady)
            {
                StartBattle();
            }

            if (myKeyboardState.IsKeyDown(Keys.Escape) && myPreviousKeyboardState.IsKeyUp(Keys.Escape))
            {
                Menu.ExitToMain();
            }

            if (myKeyboardState.IsKeyDown(myPlayerOneLeftKey) && myPreviousKeyboardState.IsKeyUp(myPlayerOneLeftKey))
            {
                if (myPlayerOneIndex == 0)
                {
                    myPlayerOneIndex = mySkins.Count - 1;
                }
                else
                {
                    myPlayerOneIndex--;
                }
            }
            if (myKeyboardState.IsKeyDown(myPlayerOneRightKey) && myPreviousKeyboardState.IsKeyUp(myPlayerOneRightKey))
            {
                if (myPlayerOneIndex == mySkins.Count - 1)
                {
                    myPlayerOneIndex = 0;
                }
                else
                {
                    myPlayerOneIndex++;
                }
            }
            if (myKeyboardState.IsKeyDown(myPlayerOneConfirmKey) && myPreviousKeyboardState.IsKeyUp(myPlayerOneConfirmKey))
            {
                myPlayerOneReady = myPlayerOneReady ? false : true;
            }

            if (myKeyboardState.IsKeyDown(myPlayerTwoLeftKey) && myPreviousKeyboardState.IsKeyUp(myPlayerTwoLeftKey))
            {
                if (myPlayerTwoIndex == 0)
                {
                    myPlayerTwoIndex = mySkins.Count - 1;
                }
                else
                {
                    myPlayerTwoIndex--;
                }
            }
            if (myKeyboardState.IsKeyDown(myPlayerTwoRightKey) && myPreviousKeyboardState.IsKeyUp(myPlayerTwoRightKey))
            {
                if (myPlayerTwoIndex == mySkins.Count - 1)
                {
                    myPlayerTwoIndex = 0;
                }
                else
                {
                    myPlayerTwoIndex++;
                }
            }
            if (myKeyboardState.IsKeyDown(myPlayerTwoConfirmKey) && myPreviousKeyboardState.IsKeyUp(myPlayerTwoConfirmKey))
            {
                myPlayerTwoReady = myPlayerTwoReady ? false : true;
            }

            myPreviousKeyboardState = myKeyboardState;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(mySkins[myPlayerOneIndex], new Vector2(250, 150), null, Color.White, 0, mySkins[myPlayerOneIndex].Bounds.Size.ToVector2() * 0.5f, 7, SpriteEffects.None, 0.5f);
            aSpriteBatch.Draw(mySkins[myPlayerTwoIndex], new Vector2(950, 150), null, Color.White, 0, mySkins[myPlayerTwoIndex].Bounds.Size.ToVector2() * 0.5f, 7, SpriteEffects.None, 0.5f);

            aSpriteBatch.Draw(myPlayerOneLeft, new Vector2(100, 150), null, Color.White, 0, myPlayerOneLeft.Bounds.Size.ToVector2() * 0.5f, 2, SpriteEffects.None, 0.5f);
            aSpriteBatch.Draw(myPlayerOneRight, new Vector2(400, 150), null, Color.White, 0, myPlayerOneRight.Bounds.Size.ToVector2() * 0.5f, 2, SpriteEffects.None, 0.5f);
            aSpriteBatch.Draw(myPlayerOneConfirm, new Vector2(160, 375), null, Color.White, 0, myPlayerOneConfirm.Bounds.Size.ToVector2() * 0.5f, 1.5f, SpriteEffects.None, 0.5f);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Confirm", new Vector2(215, 333), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);
            
            if (myPlayerOneReady)
            {
                aSpriteBatch.DrawString(Game1.mySpriteFont, "Ready", new Vector2(215, 533), Color.Green, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);
            }
            else
            {

                aSpriteBatch.DrawString(Game1.mySpriteFont, "Ready", new Vector2(215, 533), Color.Red, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);
            }

            aSpriteBatch.Draw(myPlayerTwoLeft, new Vector2(800, 150), null, Color.White, 0, myPlayerTwoLeft.Bounds.Size.ToVector2() * 0.5f, 2, SpriteEffects.None, 0.5f);
            aSpriteBatch.Draw(myPlayerTwoRight, new Vector2(1100, 150), null, Color.White, 0, myPlayerTwoRight.Bounds.Size.ToVector2() * 0.5f, 2, SpriteEffects.None, 0.5f);
            aSpriteBatch.Draw(myPlayerTwoConfirm, new Vector2(860, 375), null, Color.White, 0, myPlayerTwoConfirm.Bounds.Size.ToVector2() * 0.5f, 1.5f, SpriteEffects.None, 0.5f);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Confirm", new Vector2(940, 333), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);

            if (myPlayerTwoReady)
            {
                aSpriteBatch.DrawString(Game1.mySpriteFont, "Ready", new Vector2(940, 533), Color.Green, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);
            }
            else
            {
                aSpriteBatch.DrawString(Game1.mySpriteFont, "Ready", new Vector2(940, 533), Color.Red, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);
            }

            aSpriteBatch.Draw(Game1.myEsc, new Vector2(1000, 677), Color.White);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Main Menu", new Vector2(1055, 670), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0.5f);

            aSpriteBatch.Draw(Game1.myBackground, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
        }

        public void StartBattle()
        {
            Game1.AccessStateStack.Push(new Battle());
            Game1.GetCurrentState.Initialize();

            //Battle.myPlayers[0].myTexture = mySkins[myPlayerOneIndex];
            //Battle.myPlayers[1].myTexture = mySkins[myPlayerTwoIndex];
        }
    }
}
