using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootEmUp
{
    class Octopus : Enemy
    {
        static Random myRng = new Random();

        bool
            myCharging;

        int
            myInkCount = 5,
            myInkScale = 5;

        float
            myInkCooldown = 3,
            myInkTimer;

        public Octopus(int x, int y)
        {
            myPosition = new Vector2(x, y);
            myTexture = Game1.myOctopus;
            myHealth = 1;
            mySpeed = 70;
            myInkTimer = myInkCooldown;
            myAnimSpeed = 0.15f;
            myAnimTimer = myAnimSpeed;
            myRectangle = CreateRectangle();
        }

        public override void Update(GameTime someDeltaTime)
        {
            float tempDelta = (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            myAnimTimer -= tempDelta;
            
            if (myAnimTimer <= 0)
            {
                myTexture = myTexture == Game1.myOctopus ? Game1.myOctopusSlither : Game1.myOctopus;
                myAnimTimer = myAnimSpeed;
            }

            Vector2 tempPlayerDir = InGame.AccessPlayer.myPosition - myPosition;

            if (tempPlayerDir.Length() <= myAttackRange || myCharging)
            {
                myColor = Color.DarkRed;
                myCharging = true;

                if (myInkTimer <= 0)
                {
                    myCharging = false;
                    Ink();
                    myInkTimer = myInkCooldown;
                }

                myInkTimer -= tempDelta;
            }
            else
            {
                myColor = Color.White;
                myDirection = tempPlayerDir;
                Move(someDeltaTime);
            }

            myRotation = (float)Math.Atan2(tempPlayerDir.X, -tempPlayerDir.Y) + (float)Math.PI;
        }

        void Ink()
        {
            InGame.myGameObjects.Add(new Ink((int)(Game1.AccessWindowSize.X * 0.5f), (int)(Game1.AccessWindowSize.Y * 0.5f), myInkScale));

            for (int i = 0; i < myInkCount; ++i)
            {
                int tempX = myRng.Next(0, Game1.AccessWindowSize.X);
                int tempY = myRng.Next(0, Game1.AccessWindowSize.Y);

                InGame.myGameObjects.Add(new Ink(tempX, tempY, (int)(myInkScale * 0.75f)));
            }
        }
    }
}
