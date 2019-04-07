using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    class Ink : GameObject
    {
        float
            myInkDuration = 4,
            myInkTimer;

        public Ink(int x, int y, int aScale)
        {
            myPosition = new Vector2(x, y);
            myTexture = Game1.myInk;
            myLayer = 0.99f;
            myScale = aScale;
            myInkTimer = myInkDuration;
        }

        public override void Update(GameTime someDeltaTime)
        {
            if (myInkTimer <= 0)
            {
                myRemoved = true;
                myInkTimer = myInkDuration;
            }

            myInkTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
