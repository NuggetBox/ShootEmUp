using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace ShootEmUp
{
    class Customization : State
    {
        enum TextScaling { Shrinking, Growing };

        TextScaling myTextScaling = TextScaling.Growing;

        public static List<Texture2D> mySkins;

        Texture2D 
            myLeftArrowKey, 
            myRightArrowKey,
            myConfirmKey;

        KeyboardState myPreviousKeyboard;

        readonly Keys
            myLeft = Keys.Left,
            myRight = Keys.Right,
            myConfirm = Keys.Enter;

        public static int myIndex;

        float myPreOrderScale;

        public Customization(KeyboardState aKeyboardState)
        {
            myPreviousKeyboard = aKeyboardState;
        }

        public override void Initialize()
        {
            mySkins = new List<Texture2D>()
            {
                Game1.myShip,
                Game1.myShipPurple,
                Game1.myShipBee,
                Game1.myShipRed,
                Game1.myShipColor,
                Game1.myShipTriple,
                Game1.myShipSteam,
                Game1.myShipThicc,
                Game1.myShipFire1,
                Game1.myShipWater1,
            };

            if (File.Exists(Game1.GetFullDirectory))
            {
                myIndex = int.Parse(File.ReadAllText(Game1.GetFullDirectory));
            }
            else
            {
                myIndex = 0;
            }

            myLeftArrowKey = Game1.myLeft;
            myRightArrowKey = Game1.myRight;
            myConfirmKey = Game1.myEnter;
        }

        public override void Update(GameTime someDeltaTime)
        {
            KeyboardState tempKeyboard = Keyboard.GetState();

            float tempDelta = (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (myPreOrderScale <= 0.2f)
            {
                myTextScaling = TextScaling.Growing;
            }
            else if (myPreOrderScale >= 0.45f)
            {
                myTextScaling = TextScaling.Shrinking;
            }

            switch (myTextScaling)
            {
                case TextScaling.Growing:
                    myPreOrderScale += tempDelta * 0.5f;
                    break;

                case TextScaling.Shrinking:
                    myPreOrderScale -= tempDelta * 0.5f;
                    break;
            }

            if (tempKeyboard.IsKeyDown(myLeft) && myPreviousKeyboard.IsKeyUp(myLeft))
            {
                Previous();
            }
            else if (tempKeyboard.IsKeyDown(myRight) && myPreviousKeyboard.IsKeyUp(myRight))
            {
                Next();
            }
            else if (tempKeyboard.IsKeyDown(myConfirm) && myPreviousKeyboard.IsKeyUp(myConfirm))
            {
                if (Game1.myPreOrder || (!Game1.myPreOrder && (mySkins[myIndex] != Game1.myShipFire1 && mySkins[myIndex] != Game1.myShipWater1)))
                {
                    Game1.myPlayerTexture = mySkins[myIndex];
                    Game1.Save();
                    Game1.AccessStateStack.Pop();
                }
            }

            myPreviousKeyboard = tempKeyboard;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            if (mySkins[myIndex] == Game1.myShipFire1 || mySkins[myIndex] == Game1.myShipWater1)
            {
                aSpriteBatch.DrawString(Game1.mySpriteFont, "Pre-Order Exclusive!", new Vector2(850, 150), Color.Yellow, 0, Game1.mySpriteFont.MeasureString("Pre-Order Exclusive!") * 0.5f, myPreOrderScale, SpriteEffects.None, 0);
            }

            if (!Game1.myPreOrder)
            {
                if (mySkins[myIndex] == Game1.myShipFire1 || mySkins[myIndex] == Game1.myShipWater1)
                {
                    aSpriteBatch.Draw(mySkins[myIndex], new Vector2(250, 150), null, Color.DarkGray, 0, mySkins[myIndex].Bounds.Size.ToVector2() * 0.5f, 7, SpriteEffects.None, 0);
                    aSpriteBatch.Draw(Game1.myLock, new Vector2(250, 150), null, Color.White, -0.35f, Game1.myLock.Bounds.Size.ToVector2() * 0.5f, 10, SpriteEffects.None, 0.5f);
                }
                else
                {
                    aSpriteBatch.Draw(mySkins[myIndex], new Vector2(250, 150), null, Color.White, 0, mySkins[myIndex].Bounds.Size.ToVector2() * 0.5f, 7, SpriteEffects.None, 0);
                }
            }
            else
            {
                aSpriteBatch.Draw(mySkins[myIndex], new Vector2(250, 150), null, Color.White, 0, mySkins[myIndex].Bounds.Size.ToVector2() * 0.5f, 7, SpriteEffects.None, 0);
            }

            aSpriteBatch.Draw(myLeftArrowKey, new Vector2(100, 150), null, Color.White, 0, myLeftArrowKey.Bounds.Size.ToVector2() * 0.5f, 2, SpriteEffects.None, 0);
            aSpriteBatch.Draw(myLeftArrowKey, new Vector2(400, 150), null, Color.White, 0, myLeftArrowKey.Bounds.Size.ToVector2() * 0.5f, 2, SpriteEffects.FlipHorizontally, 0);
            aSpriteBatch.Draw(myConfirmKey, new Vector2(160, 375), null, Color.White, 0, myConfirmKey.Bounds.Size.ToVector2() * 0.5f, 1.5f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Confirm", new Vector2(250, 340), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
        }

        public static Texture2D GetSelectedTexture()
        {
            if (File.Exists(Game1.GetFullDirectory))
            {
                myIndex = int.Parse(File.ReadAllText(Game1.GetFullDirectory));
            }
            else
            {
                myIndex = 0;
            }

            Game1.myPlayerTexture = mySkins[myIndex];
            return mySkins[myIndex];
        }

        void Next()
        {
            if (myIndex == mySkins.Count - 1)
            {
                myIndex = 0;
            }
            else
            {
                myIndex++;
            }
        }

        void Previous()
        {
            if (myIndex == 0)
            {
                myIndex = mySkins.Count - 1;
            }
            else
            {
                myIndex--;
            }
        }
    }
}
