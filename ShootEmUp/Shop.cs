using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace ShootEmUp
{
    class Shop : State
    {
        KeyboardState
            myPreviousKeyboardState,
            myKeyboardState;

        List<Button> myButtons;

        int mySelectedIndex = 0;

        public Shop(KeyboardState aPreviousKeyboardState)
        {
            myPreviousKeyboardState = aPreviousKeyboardState;
        }

        public override void Initialize()
        {
            if (File.Exists(Game1.GetFullDirectory))
            {
                string[] tempData = File.ReadAllLines(Game1.GetFullDirectory);

                Player.myCrabScore = int.Parse(tempData[1]);
                Player.myClamScore = int.Parse(tempData[2]);
                Player.myOctopusScore = int.Parse(tempData[3]);
            }

            myButtons = new List<Button>()
            {
                new Button(),
                new Button(),
            };

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
            else if (myKeyboardState.IsKeyDown(Keys.Up) && myPreviousKeyboardState.IsKeyUp(Keys.Up))
            {
                if (mySelectedIndex != 0)
                {
                    mySelectedIndex--;
                }
            }
            else if (myKeyboardState.IsKeyDown(Keys.Down) && myPreviousKeyboardState.IsKeyUp(Keys.Down))
            {
                if (mySelectedIndex != myButtons.Count - 1)
                {
                    mySelectedIndex++;
                }
            }

            myButtons[mySelectedIndex].myColor.B = 123;
            myKeyboardState = myPreviousKeyboardState;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {

        }
    }
}
