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
            mySpeed = 50;
            myHealth = 2;
            myRectangle = new Rectangle((int)(myPosition.X - myTexture.Width * myScale * 0.5f), (int)(myPosition.Y - myTexture.Height * myScale * 0.5f), myTexture.Width * myScale, myTexture.Height * myScale);
        }

        public override void Update(GameTime someDeltaTime)
        {
            myAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (myAttackTimer <= 0)
            {
                Shoot(new Vector2(1, 1));
                myAttackTimer = myAttackSpeed;
            }

            myDirection = InGame.myGameObjects[0].myPosition - myPosition;
            Move(someDeltaTime);
        }
    }
}
