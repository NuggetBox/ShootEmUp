using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShootEmUp
{
    class Menu : State
    {
        List<Button> myButtons;

        static KeyboardState 
            myPreviousKeyboardState,
            myKeyboardState;

        int mySelectedIndex = 0;
        int myButtonOffset = 75;

        float myButtonScale = 0.4f;

        public Menu(List<Button> someButtons)
        {
            myButtons = someButtons;

            for (int i = 0; i < myButtons.Count; ++i)
            {
                myButtons[i].myPosition = new Vector2(myButtonOffset, myButtonOffset * (i + 1));
            }
        }

        public override void Update(GameTime someDeltaTime)
        {
            for (int i = 0; i < myButtons.Count; ++i)
            {
                if (i != mySelectedIndex)
                {
                    myButtons[i].myColor.B = 255;
                }
            }

            myKeyboardState = Keyboard.GetState();

            if (myKeyboardState.IsKeyDown(Keys.Enter) && myPreviousKeyboardState.IsKeyUp(Keys.Enter))
            {
                myButtons[mySelectedIndex].Press();
                //Console.Beep();
            }
            else
            {
                if (myKeyboardState.IsKeyDown(Keys.Up) && myPreviousKeyboardState.IsKeyUp(Keys.Up))
                {
                    if (mySelectedIndex != 0)
                    {
                        --mySelectedIndex;
                    }
                    //Console.Beep();
                }
                else if (myKeyboardState.IsKeyDown(Keys.Down) && myPreviousKeyboardState.IsKeyUp(Keys.Down))
                {
                    if (mySelectedIndex != myButtons.Count - 1)
                    {
                        ++mySelectedIndex;
                    }
                    //Console.Beep();
                }
            }

            myButtons[mySelectedIndex].myColor.B = 123;

            myPreviousKeyboardState = myKeyboardState;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            int tempSize = myButtons.Count;

            for (int i = 0; i < tempSize; ++i)
            {
                Button tempButton = myButtons[i];
                aSpriteBatch.DrawString(Game1.mySpriteFont, tempButton.myLabel, tempButton.myPosition, tempButton.myColor, 0, Vector2.Zero, myButtonScale, SpriteEffects.None, 0);
            }

            if (Game1.myFinalScore > -1)
            {
                aSpriteBatch.DrawString(Game1.mySpriteFont, "Final score: " + Game1.myFinalScore, new Vector2(75, 13), Color.IndianRed, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
            }
        }

        public static void Start()
        {
            Game1.AccessStateStack.Push(new InGame());
            Game1.GetCurrentState.Initialize();
        }

        public static void Resume()
        {
            Game1.AccessStateStack.Pop();
        }

        public static void SkinCustomization()
        {
            Game1.AccessStateStack.Push(new Customization(myKeyboardState));
            Game1.GetCurrentState.Initialize();
        }

        public static void ExitToMain()
        {
            Game1.myFinalScore = -1;

            while (Game1.AccessStateStack.Count > 1)
            {
                Game1.AccessStateStack.Pop();
            }
        }

        public static void Quit()
        {
            Game1.myQuit = true;
        }
    }
}
