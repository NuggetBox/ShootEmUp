using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    class Water : GameObject
    {
        public Water(float x, float y)
        {
            myPosition = new Vector2(x, y);
            myDirection = new Vector2(0, 1);
            mySpeed = 100;
            myTexture = Game1.myWater;
            myLayer = 0f;
            myScale = 5 + 1f/3;
        }

        public override void Update(GameTime someDeltaTime)
        {
            if (myPosition.Y >= Game1.AccessWindowSize.Y * 1.5f)
            {
                InGame.myGameObjects.Add(new Water(Game1.AccessWindowSize.X * 0.5f, -Game1.AccessWindowSize.Y * 0.5f));
                myRemoved = true;
            }

            Move(someDeltaTime);
        }
    }
}
