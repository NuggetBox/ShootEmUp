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
            myTexture = Game1.myPirate;
            myPosition = aPosition;
            mySpeed = 30;
            myHealth = 3;
            myRectangle = new Rectangle((int)myPosition.X, (int)myPosition.Y, myTexture.Width * myScale, myTexture.Height * myScale);
        }

        public override void Update(GameTime someDeltaTime)
        {
            myAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (myAttackTimer <= 0)
            {
                Shoot(InGame.myGameObjects[0].myPosition - myPosition);
                myAttackTimer = myAttackSpeed;
            }

            myDirection = InGame.myGameObjects[0].myPosition - myPosition;
            myRotation = (float)Math.Atan2(myDirection.X, -myDirection.Y);
            Move(someDeltaTime);
        }
    }
}
