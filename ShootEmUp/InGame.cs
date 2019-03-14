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

        public override void Initialize()
        {
            myGameObjects = new List<GameObject>()
            {
                new Player(),
                new Pirate(new Vector2(300, 400)),
            };
        }

        public override void Update(GameTime someDeltaTime)
        {
            KeyboardState tempKeyboardState = Keyboard.GetState();

            if (tempKeyboardState.IsKeyDown(Keys.Escape))
            {
                ExitToMain();
            }

            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                myGameObjects[i].Update(someDeltaTime);
            }

            // Removes GameObjects that have "died" this update
            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                if (myGameObjects[i].myRemoved)
                {
                    myGameObjects.RemoveAt(i);
                    --i;
                }
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
            while (!(Game1.GetCurrentState is Menu))
            {
                Game1.AccessStateStack.Pop();
            }
        }
    }
}
