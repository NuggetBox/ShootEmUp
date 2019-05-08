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
        List<Texture2D> mySkins;

        Texture2D 
            myLeftArrowKey, 
            myRightArrowKey,
            myConfirmKey;

        KeyboardState myPreviousKeyboard;

        Keys
            myLeft = Keys.Left,
            myRight = Keys.Right,
            myConfirm = Keys.Enter;

        public static int index;

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
            };

            if (File.Exists(Game1.GetFullDirectory))
            {
                index = int.Parse(File.ReadAllText(Game1.GetFullDirectory));
            }
            else
            {
                index = 0;
            }

            myLeftArrowKey = Game1.myLeft;
            myRightArrowKey = Game1.myRight;
            myConfirmKey = Game1.myEnter;
        }

        public override void Update(GameTime someDeltaTime)
        {
            KeyboardState tempKeyboard = Keyboard.GetState();

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
                Game1.myPlayerTexture = mySkins[index];
                Game1.Save();
                Game1.AccessStateStack.Pop();
            }

            myPreviousKeyboard = tempKeyboard;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myLeftArrowKey, new Vector2(100, 150), null, Color.White, 0, myLeftArrowKey.Bounds.Size.ToVector2() * 0.5f, 2, SpriteEffects.None, 0);
            aSpriteBatch.Draw(myLeftArrowKey, new Vector2(400, 150), null, Color.White, 0, myLeftArrowKey.Bounds.Size.ToVector2() * 0.5f, 2, SpriteEffects.FlipHorizontally, 0);
            aSpriteBatch.Draw(myConfirmKey, new Vector2(160, 375), null, Color.White, 0, myConfirmKey.Bounds.Size.ToVector2() * 0.5f, 1.5f, SpriteEffects.None, 0);
            aSpriteBatch.Draw(mySkins[index], new Vector2(250, 150), null, Color.White, 0, mySkins[index].Bounds.Size.ToVector2() * 0.5f, 7, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Confirm", new Vector2(250, 340), Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
        }

        void Next()
        {
            if (index == mySkins.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }

        void Previous()
        {
            if (index == 0)
            {
                index = mySkins.Count - 1;
            }
            else
            {
                index--;
            }
        }
    }
}
