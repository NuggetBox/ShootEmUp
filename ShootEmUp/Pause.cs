using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class Pause : State
    {
        List<Button> myButtons;
        public readonly Game1 myGame;

        int mySelectedIndex = 0;
        int myButtonOffset = 75;

        float myButtonScale = 0.4f;
        float myTimer;
        float myDelay = 100;

        public Pause()
        {
            myButtons = new List<Button>
            {
                 new Button("Resume", Resume),
                 //new Button("Settings", Stats),
                 new Button("Exit to main menu", ExitToMain)
            };

            for (int i = 0; i < myButtons.Count; ++i)
            {
                myButtons[i].myPosition = new Vector2(myButtonOffset, myButtonOffset * (i + 1));
            }
        }

        public override void Update(GameTime someDeltaTime)
        {
            bool tempInput = false;

            for (int i = 0; i < myButtons.Count; ++i)
            {
                if (i != mySelectedIndex)
                {
                    myButtons[i].myColor.B = 255;
                }
            }

            KeyboardState tempKeyboardState = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                myButtons[mySelectedIndex].Press();
            }
            else if (myTimer >= myDelay)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    //if (mySelectedIndex == 0)
                    //{
                    //    mySelectedIndex = myButtons.Count - 1;
                    //}
                    if (mySelectedIndex != 0)
                    {
                        --mySelectedIndex;
                    }

                    tempInput = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    //if (mySelectedIndex == myButtons.Count - 1)
                    //{
                    //    mySelectedIndex = 0;
                    //}
                    if (mySelectedIndex != myButtons.Count - 1)
                    {
                        ++mySelectedIndex;
                    }

                    tempInput = true;
                }
            }

            if (tempInput)
            {
                myTimer = 0;
            }
            else
            {
                myTimer += someDeltaTime.ElapsedGameTime.Milliseconds;
            }

            myButtons[mySelectedIndex].myColor.B = 123;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            int tempSize = myButtons.Count;

            for (int i = 0; i < tempSize; ++i)
            {
                Button tempButton = myButtons[i];
                aSpriteBatch.DrawString(Game1.mySpriteFont, tempButton.myLabel, tempButton.myPosition, tempButton.myColor, 0, Vector2.Zero, myButtonScale, SpriteEffects.None, 0);
            }
        }

        void Resume()
        {
            Game1.AccessStateStack.Pop();
        }

        // TODO: MAKE EXIT TO MAIN WORK
        void ExitToMain()
        {
            while (!(Game1.GetCurrentState is Menu))
            {
                Game1.AccessStateStack.Pop();
            }
        }
    }
}
