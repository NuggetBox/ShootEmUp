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
        Random myRng = new Random();

        bool
            myCharging;

        int
            myAttackRange = 150,
            myInkCount = 5,
            myInkScale = 6;

        float
            myInkCooldown = 5,
            myInkTimer;

        public Octopus(int x, int y)
        {
            myPosition = new Vector2(x, y);
            myTexture = Game1.myOctopus;
            myHealth = 3;
            mySpeed = 70;
            myInkTimer = myInkCooldown;
            myRectangle = CreateRectangle();
        }

        public override void Update(GameTime someDeltaTime)
        {
            Vector2 tempPlayerDir = InGame.GetPlayer.myPosition - myPosition;

            if (tempPlayerDir.Length() <= myAttackRange || myCharging)
            {
                myCharging = true;

                if (myInkTimer <= 0)
                {
                    myCharging = false;
                    Ink();
                    myInkTimer = myInkCooldown;
                }

                myInkTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                myDirection = tempPlayerDir;
                Move(someDeltaTime);
            }
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
