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
        Vector2 myDashPos;

        int
            myChargeSpeed = 500,
            myRegularSpeed;

        bool
            myCharging,
            myDashing,
            myReturning;

        public Crab(int x, int y)
        {
            myPosition = new Vector2(x, y);
            myTexture = Game1.myCrab;
            myHealth = 1;
            mySpeed = 130;
            myRegularSpeed = mySpeed;
            myAttackSpeed = 1.5f;
            myAttackTimer = myAttackSpeed;
            myAnimSpeed = 0.2f;
            myAnimTimer = myAnimSpeed;
            myRectangle = CreateRectangle();
        }

        public override void Update(GameTime someDeltaTime)
        {
            float tempDelta = (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            myAnimTimer -= tempDelta;

            if (myAnimTimer <= 0)
            {
                myTexture = myTexture == Game1.myCrab ? Game1.myCrabPinch : Game1.myCrab;
                myAnimTimer = myAnimSpeed;
            }

            myRotation = (float)Math.Atan2(myDirection.X, -myDirection.Y);

            if ((InGame.GetPlayer.myPosition - myPosition).Length() <= myAttackRange || myCharging)
            {
                myColor = Color.DarkRed;

                if (myDashing)
                {
                    myColor = Color.White;
                    myDirection = InGame.GetPlayer.myPosition - myPosition;

                    if ((InGame.GetPlayer.myPosition - myPosition).Length() <= 5)
                    {
                        myReturning = true;
                        InGame.myGameObjects[0].myHealth -= myDamage;
                    }

                    if (myReturning)
                    {
                        if ((myPosition - myDashPos).Length() <= 5)
                        {
                            myDashing = false;
                            myCharging = false;
                            myReturning = false;
                            myAttackTimer = myAttackSpeed;
                        }

                        mySpeed = myRegularSpeed;
                        myDirection = myDashPos - myPosition;
                    }

                    Move(someDeltaTime);
                }
                else
                {
                    myCharging = true;

                    if (myAttackTimer <= 0)
                    {
                        myDashPos = myPosition;
                        myDirection = InGame.GetPlayer.myPosition - myPosition;
                        mySpeed = myChargeSpeed;
                        myDashing = true;
                    }

                    myAttackTimer -= tempDelta;
                }
            }
            else
            {
                myDirection = InGame.GetPlayer.myPosition - myPosition;
                Move(someDeltaTime);
            }
        }
    }
}
