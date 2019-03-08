using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace ShootEmUp
{
    class InGame : State
    {
        public static List<GameObject> myGameObjects;

        public override void Initialize(ContentManager someContent)
        {
            myGameObjects = new List<GameObject>()
            {
                new Player(),
                new Enemy()
            };
        }

        public override void Update(GameTime someDeltaTime)
        {
            bool tempInput = false;

            KeyboardState tempKeyboardState = Keyboard.GetState();

            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                myGameObjects[i].Update(someDeltaTime);
            }

            if (tempKeyboardState.IsKeyDown(Keys.Escape))
            {
                ExitToMain();
            }
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                myGameObjects[i].Draw(aSpriteBatch);
            }
        }

        void ExitToMain()
        {
            while (true)
            {
                if (!(Game1.GetCurrentState is Menu))
                {
                    Game1.AccessStateStack.Pop();
                }
            }
        }
    }
}
