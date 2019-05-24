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

        enum TextRotating { Clockwise, CounterClockwise };
        TextRotating myTextRotating = TextRotating.Clockwise;

        public Texture2D myDisplayTexture;
        public Vector2 myWinTextPosition = new Vector2(800, 170);
        public Vector2 myDisplayPosition = new Vector2(800, 420);
        public string myWinText;
        public float myWinTextScale;
        public float myWinTextRotation;
        public float myDisplayRotation;
        Customization.TextScaling myTextScaling = Customization.TextScaling.Growing;

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
            }
            else
            {
                if (myKeyboardState.IsKeyDown(Keys.W) && myPreviousKeyboardState.IsKeyUp(Keys.W))
                {
                    if (mySelectedIndex != 0)
                    {
                        --mySelectedIndex;
                    }
                }
                else if (myKeyboardState.IsKeyDown(Keys.S) && myPreviousKeyboardState.IsKeyUp(Keys.S))
                {
                    if (mySelectedIndex != myButtons.Count - 1)
                    {
                        ++mySelectedIndex;
                    }
                }
            }

            myButtons[mySelectedIndex].myColor.B = 123;

            myPreviousKeyboardState = myKeyboardState;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            float tempDelta = (float)someDeltaTime.ElapsedGameTime.TotalSeconds;
            int tempSize = myButtons.Count;

            for (int i = 0; i < tempSize; ++i)
            {
                Button tempButton = myButtons[i];
                aSpriteBatch.DrawString(Game1.mySpriteFont, tempButton.myLabel, tempButton.myPosition, tempButton.myColor, 0, Vector2.Zero, myButtonScale, SpriteEffects.None, 0.5f);
            }

            // Solo end screen
            if (Game1.myFinalScore != null)
            {
                aSpriteBatch.DrawString(Game1.mySpriteFont, "Final score: " + Game1.myFinalScore, new Vector2(75, 13), Color.IndianRed, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0.5f);
            }

            // Battle end screen
            if (myDisplayTexture != null)
            {
                if (myWinTextScale >= 0.8f)
                {
                    myTextScaling = Customization.TextScaling.Shrinking;
                }
                else if (myWinTextScale <= 0.3f)
                {
                    myTextScaling = Customization.TextScaling.Growing;
                }

                switch (myTextScaling)
                {
                    case Customization.TextScaling.Growing:
                        myWinTextScale += tempDelta;
                        break;

                    case Customization.TextScaling.Shrinking:
                        myWinTextScale -= tempDelta;
                        break;
                } 

                if (myWinTextRotation >= 0.5f)
                {
                    myTextRotating = TextRotating.CounterClockwise;
                }
                else if (myWinTextRotation <= -0.5f)
                {
                    myTextRotating = TextRotating.Clockwise;
                }

                switch (myTextRotating)
                {
                    case TextRotating.Clockwise:
                        myWinTextRotation += tempDelta;
                        break;

                    case TextRotating.CounterClockwise:
                        myWinTextRotation -= tempDelta;
                        break;
                }

                myDisplayRotation += tempDelta;
                aSpriteBatch.DrawString(Game1.mySpriteFont, myWinText, myWinTextPosition, Color.Gold, myWinTextRotation, Game1.mySpriteFont.MeasureString(myWinText) * 0.5f, myWinTextScale, SpriteEffects.None, 0);
                aSpriteBatch.Draw(myDisplayTexture, myDisplayPosition, null, Color.White, myDisplayRotation, myDisplayTexture.Bounds.Size.ToVector2() * 0.5f, 10, SpriteEffects.None, 0.5f);
            }

            aSpriteBatch.Draw(Game1.myBackground, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
        }

        public static void Start()
        {
            Game1.myFinalScore = null;
            Game1.AccessStateStack.Push(new InGame());
            Game1.GetCurrentState.Initialize();
        }

        public static void Resume()
        {
            Game1.AccessStateStack.Pop();
        }

        public static void Battle()
        {
            Game1.AccessStateStack.Push(new BattleCustomization(myKeyboardState));
            Game1.GetCurrentState.Initialize();
        }

        public static void SkinCustomization()
        {
            Game1.AccessStateStack.Push(new Customization(myKeyboardState));
            Game1.GetCurrentState.Initialize();
        }

        public static void Restart()
        {
            Game1.myFinalScore = null;
            Game1.AccessStateStack.Push(new Battle());
            Game1.GetCurrentState.Initialize();
        }

        public static void Tutorial()
        {
            Game1.AccessStateStack.Push(new Tutorial());
            Game1.GetCurrentState.Initialize();
        }

        public static void ExitToMain()
        {
            Game1.myFinalScore = null;

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
