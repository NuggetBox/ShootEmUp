using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShootEmUp
{
    public class Menu : State
    {
        List<Button> myButtons;
        Game1 myGame;

        int mySelectedIndex = 0;

        public Menu(Game1 aGame)
        {
            myGame = aGame;
            myButtons = new List<Button>();
            myButtons.Add(new Button("Start", Color.AliceBlue, Start, new Rectangle(100, 100, 40, 10)));
            // myButtons.Add(new Button("Stats", Color.AliceBlue, Stats));
            myButtons.Add(new Button("Quit", Color.AliceBlue, Quit, new Rectangle(500, 400, 40, 5)));
        }

        public override void Update(GameTime someDeltaTime)
        {
            //KeyboardState tempKeyboardState = Keyboard.GetState();
            MouseState tempMouseState = Mouse.GetState();

            int tempCount = 0;

            for (int i = 0; i < myButtons.Count; ++i)
            {
                if (tempMouseState.Position.X >= myButtons[i].myRectangle.Left && tempMouseState.Position.X <= myButtons[i].myRectangle.Right && tempMouseState.Position.Y <= myButtons[i].myRectangle.Top && tempMouseState.Y >= myButtons[i].myRectangle.Bottom)
                {
                    mySelectedIndex = i;
                    ++tempCount;
                }
            }

            if (tempCount > 0)
            {
                myButtons[mySelectedIndex].myColor = Color.Red;
                
                    myButtons[mySelectedIndex].Press();
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            //{
            //    myButtons[mySelectedIndex].Press();
            //}
            //else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
            //    if (mySelectedIndex == 0)
            //    {
            //        mySelectedIndex = myButtons.Count - 1;
            //    }
            //    else
            //    {
            //        --mySelectedIndex;
            //    }
            //}
            //else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //{
            //    if (mySelectedIndex == myButtons.Count - 1)
            //    {
            //        mySelectedIndex = 0;
            //    }
            //    else
            //    {
            //        ++mySelectedIndex;
            //    }
            //}
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            int tempSize = myButtons.Count;

            for (int i = 0; i < tempSize; ++i)
            {
                aSpriteBatch.DrawString(myGame.mySpriteFont, myButtons[i].myLabel, myButtons[i].myRectangle.Location.ToVector2(), myButtons[i].myColor);
            }
        }

        void Start()
        {
            Game1.AccessStateStack.Push(new InGame());
        }

        //void Stats()
        //{

        //}

        void Quit()
        {
            myGame.Exit();
        }
    }
}
