using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShootEmUp
{
    class InGame : State
    {
        List<GameObject> myGameObjects = new List<GameObject>();

        public override void Update(GameTime someDeltaTime)
        {
            for (int i = 0; i < myGameObjects.Count; ++i)
            {
                myGameObjects[i].Update(someDeltaTime);
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
