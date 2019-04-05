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
        bool myFire;

        public Pirate(int x, int y)
        {
            myTexture = Game1.myPirate;
            myPosition = new Vector2(x, y);
            mySpeed = 30;
            myHealth = 3;
            myDamage = 3;
            myAttackRange = 200;
            myRectangle = CreateRectangle();
        }

        public override void Update(GameTime someDeltaTime)
        {
            myDirection = InGame.GetPlayer.myPosition - myPosition;
            myRotation = (float)Math.Atan2(myDirection.X, -myDirection.Y);
            myAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if ((InGame.GetPlayer.myPosition - myPosition).Length() <= myAttackRange)
            {
                myFire = true;
            }
            else
            {
                myDirection = InGame.GetPlayer.myPosition - myPosition;
                Move(someDeltaTime);
            }

            if (myAttackTimer <= 0 && myFire)
            {
                Shoot(InGame.GetPlayer.myPosition - myPosition, Game1.myEnemyBullet);
                myFire = false;
            }
        }
    }
}
