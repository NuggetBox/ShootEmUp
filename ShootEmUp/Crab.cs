using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    class Crab : Enemy
    {
        public int myDashDistance = 40;

        int mySpeedMult = 5;

        bool myCharging;

        public Crab(Vector2 aPosition)
        {
            myPosition = aPosition;
            myTexture = Game1.myCrab;
            myHealth = 3;
            mySpeed = 130;
            myAttackSpeed = 1.5f;
            myAttackTimer = myAttackSpeed;
            myRectangle = CreateRectangle();
        }

        public override void Update(GameTime someDeltaTime)
        {
            myRotation = (float)Math.Atan2(myDirection.X, -myDirection.Y);

            if ((InGame.myGameObjects[0].myPosition - myPosition).Length() <= myDashDistance || myCharging)
            {
                CrabAttack(someDeltaTime);
            }
            else
            {
                myDirection = InGame.myGameObjects[0].myPosition - myPosition;
                Move(someDeltaTime);
            }
        }

        void CrabAttack(GameTime someDeltaTime)
        {
            myCharging = true;

            if (myAttackTimer <= 0)
            {
                Vector2 tempDashPos = myPosition;
                myDirection = InGame.myGameObjects[0].myPosition - myPosition;
                mySpeed *= mySpeedMult;

            }

            myAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
