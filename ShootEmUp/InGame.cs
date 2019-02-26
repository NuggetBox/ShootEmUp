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

        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {

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
