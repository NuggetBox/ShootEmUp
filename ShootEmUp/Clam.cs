using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootEmUp
{
    class Clam : Enemy
    {
        Texture2D
            myClosed,
            myOpening,
            myOpen;

        //float myAnimationTimer = 0.1f;

        public Clam(int x, int y)
        {
            myClosed = Game1.myClamClosed;
            myOpening = Game1.myClamOpening;
            myOpen = Game1.myClamOpen;
            myTexture = myClosed;

            myPosition = new Vector2(x, y);
            myHealth = 3;
            mySpeed = 50;
            myAttackSpeed = 0.8f;
            myAttackTimer = myAttackSpeed;
            myDamage = 3;
            myRectangle = CreateRectangle();
        }

        public override void Update(GameTime someDeltaTime)
        {
            myRotation = (float)Math.Atan2(myDirection.X, -myDirection.Y);

            myDirection = InGame.GetPlayer.myPosition - myPosition;

            if (myAttackTimer > 0.66 * myAttackSpeed)
            {
                myTexture = myClosed;
            }
            if (myAttackTimer < 0.66 * myAttackSpeed)
            {
                myTexture = myOpening;
            }
            if (myAttackTimer < 0.33 * myAttackSpeed)
            {
                myTexture = myOpen;
            }

            if (myDirection.Length() <= myAttackRange)
            {
                if (myAttackTimer <= 0)
                {
                    Shoot(myDirection, Game1.myPearl);
                }
            }
            else
            {
                Move(someDeltaTime);

                if (myDirection.Length() < myAttackRange + 10)
                {
                    myTexture = myClosed;
                }
            }

            myAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
