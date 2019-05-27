using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootEmUp
{
    class Highscore : State
    {
        public static string GetFullDirectory => Game1.GetDirectory + myFileName;

        public static int[] myHighscores = new int[10];

        static string myFileName = "highscore.bog";

        KeyboardState
            myKeyboardState,
            myPreviousKeyboardState;

        public Highscore(KeyboardState aKeyboardState)
        {
            myPreviousKeyboardState = aKeyboardState;
        }

        public override void Update(GameTime someDeltaTime)
        {
            myKeyboardState = Keyboard.GetState();

            if (myKeyboardState.IsKeyDown(Keys.Escape) && myPreviousKeyboardState.IsKeyUp(Keys.Escape))
            {
                Game1.AccessStateStack.Pop();
            }

            myPreviousKeyboardState = myKeyboardState;
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(Game1.myBackground, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
            aSpriteBatch.Draw(Game1.myEsc, new Vector2(1000, 677), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Main Menu", new Vector2(1055, 670), Color.Red, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0.5f);

            for (int i = 0; i < myHighscores.Length; ++i)
            {
                aSpriteBatch.DrawString(Game1.mySpriteFont, (i + 1).ToString() + ": " + myHighscores[i], new Vector2(50, 60 * (i + 1)), Color.White, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
            }
        }

        public static void AddScore(int aScore)
        {
            for (int i = 0; i < myHighscores.Length; ++i)
            {
                if (aScore > myHighscores[i])
                {
                    for (int j = myHighscores.Length - 1; j > i; --j)
                    {
                        myHighscores[j] = myHighscores[j - 1];
                    }

                    myHighscores[i] = aScore;
                    break;
                }
            }
        }

        public static void Save()
        {
            string[] tempData = new string[10];

            for (int i = 0; i < tempData.Length; ++i)
            {
                tempData[i] = myHighscores[i].ToString();
            }

            File.WriteAllLines(GetFullDirectory, tempData);
        }

        public static void Load()
        {
            if (File.Exists(GetFullDirectory))
            {
                string[] tempData = File.ReadAllLines(GetFullDirectory);

                for (int i = 0; i < tempData.Length; ++i)
                {
                    myHighscores[i] = int.Parse(tempData[i]);
                }
            }
        }
    }
}
