using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    class Pirate : Enemy
    {
        public Pirate(Vector2 aPosition)
        {
            myTexture = Game1.myEnemyTexture;
            myPosition = aPosition;
            mySpeed = 75;
            myHealth = 2;
            myRectangle = myTexture.Bounds;
            myRectangle.Size = new Point((int)(myRectangle.Width * myScale), (int)(myRectangle.Height * myScale));
            myRectangle.Location = myPosition.ToPoint();
        }

        public override void Update(GameTime someDeltaTime)
        {
            myAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (myAttackTimer <= 0)
            {
                Shoot(new Vector2(1, 1));
                myAttackTimer = myAttackSpeed;
            }
        }
    }
}
