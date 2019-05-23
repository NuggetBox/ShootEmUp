using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootEmUp
{
    class Player : GameObject
    {
        public static Random myRandom = new Random();

        readonly Keys
            myUp = Keys.W,
            myDown = Keys.S,
            myRight = Keys.D,
            myLeft = Keys.A,
            myClockwiseRotation = Keys.L,
            myCounterClockwiseRotation = Keys.J,
            myShoot = Keys.I;

        public List<PowerUp> myPowerUps = new List<PowerUp>();

        Vector2 myStartPos = new Vector2(640, 370);

        double myTriAngle = Math.PI * 0.1f;

        int
            myAnimationIndex,
            myBulletDamage = 1,
            myBulletSpeed = 200;

        public float
            myOriginalAttackCooldown = 0.5f,
            myAttackCooldown,
            myAttackTimer,
            myAnimationCooldown = 0.3f,
            myAnimationTimer,
            myRotationSpeed = 1.5f;

        public bool
            myIsFire,
            myIsPreOrderSkin,
            myTriShooting;

        public Player()
        {
            myPosition = myStartPos;
            myAnimationTimer = myAnimationCooldown;
            myTexture = Customization.GetSelectedTexture();
            myRectangle = CreateRectangle();
            myRectangle.Size = new Point((int)(Game1.myShip.Bounds.Size.X * myScale), (int)(Game1.myShip.Bounds.Size.Y * myScale));
            myAttackCooldown = myOriginalAttackCooldown;
            myLayer = 0.8f;
            myHealth = 100;
            mySpeed = 120;

            if ((Game1.myPlayerTexture == Game1.myShipFire1 || Game1.myPlayerTexture == Game1.myShipWater1) && Game1.myPreOrder)
            {
                myIsPreOrderSkin = true;
            }
        }

        public override void Update(GameTime someDeltaTime)
        {
            if (Game1.myPreOrder)
            {
                if (Game1.myPlayerTexture == Game1.myShipFire1)
                {
                    myIsFire = true;
                    myIsPreOrderSkin = true;
                }
                else if (Game1.myPlayerTexture == Game1.myShipWater1)
                {
                    myIsFire = false;
                    myIsPreOrderSkin = true;
                }
                else
                {
                    myIsPreOrderSkin = false;
                }
            }
            else
            {
                myIsPreOrderSkin = false;
            }

            float tempDelta = (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (myIsPreOrderSkin)
            {
                if (myAnimationCooldown <= 0)
                {
                    myAnimationIndex++;

                    if (myAnimationIndex == 4)
                    {
                        myAnimationIndex = 0;
                    }

                    if (myIsFire)
                    {
                        myTexture = Game1.myPreOrderSkinsFire[myAnimationIndex];
                    }
                    else
                    {
                        myTexture = Game1.myPreOrderSkinsWater[myAnimationIndex];
                    }

                    myAnimationCooldown = myAnimationTimer;
                }

                myAnimationCooldown -= tempDelta;
            }
            else
            {
                myTexture = Game1.myPlayerTexture;
            }

            CheckPlayerDeath();

            KeyboardState tempKeyboard = Keyboard.GetState();
            MouseState tempMouse = Mouse.GetState();

            myAttackTimer -= tempDelta;

            if (tempKeyboard.IsKeyDown(myUp))
            {
                myDirection += new Vector2(0, -1);
            }
            if (tempKeyboard.IsKeyDown(myRight))
            {
                myDirection += new Vector2(1, 0);
            }
            if (tempKeyboard.IsKeyDown(myDown))
            {
                myDirection += new Vector2(0, 1);
            }
            if (tempKeyboard.IsKeyDown(myLeft))
            {
                myDirection += new Vector2(-1, 0);
            }
            if (tempKeyboard.IsKeyDown(myShoot) && myAttackTimer <= 0)
            {
                Shoot(myTriShooting);
                myAttackTimer = myAttackCooldown;
            }
            if (tempKeyboard.IsKeyDown(myClockwiseRotation))
            {
                myRotation += tempDelta * myRotationSpeed;
            }
            if (tempKeyboard.IsKeyDown(myCounterClockwiseRotation))
            {
                myRotation -= tempDelta * myRotationSpeed;
            }

            if (myPosition.X + myDirection.X * mySpeed * tempDelta > Game1.myLeftBeach && myPosition.X + myDirection.X * mySpeed * tempDelta < Game1.myRightBeach)
            {
                Move(someDeltaTime);
            }

            for (int i = 0; i < myPowerUps.Count; ++i)
            {
                if (myPowerUps[i].myActiveTime <= 0)
                {
                    myPowerUps[i].Reset();
                    myPowerUps.RemoveAt(i);
                    i--;
                }
                else
                {
                    myPowerUps[i].Apply(tempDelta);
                }
            }
        }

        public void Shoot(bool aIsTriBool)
        {
            if (aIsTriBool)
                TriShoot();
            else
            {
                Vector2 tempRight = new Vector2((float)Math.Cos(myRotation), (float)Math.Sin(myRotation));
                Vector2 tempLeft = new Vector2((float)Math.Cos(myRotation + Math.PI), (float)Math.Sin(myRotation + Math.PI));

                InGame.myGameObjects.Add(new Bullet(this, tempRight, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
                InGame.myGameObjects.Add(new Bullet(this, tempLeft, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
            }
        }

        public void TriShoot()
        {
            Vector2 tempRight = new Vector2((float)Math.Cos(myRotation), (float)Math.Sin(myRotation));
            Vector2 tempTopRight = new Vector2((float)Math.Cos(myRotation - myTriAngle), (float)Math.Sin(myRotation - myTriAngle));
            Vector2 tempBottomRight = new Vector2((float)Math.Cos(myRotation + myTriAngle), (float)Math.Sin(myRotation + myTriAngle));

            Vector2 tempLeft = new Vector2((float)Math.Cos(myRotation + Math.PI), (float)Math.Sin(myRotation + Math.PI));
            Vector2 tempTopLeft = new Vector2((float)Math.Cos(myRotation + Math.PI + myTriAngle), (float)Math.Sin(myRotation + Math.PI + myTriAngle));
            Vector2 tempBottomLeft = new Vector2((float)Math.Cos(myRotation + Math.PI - myTriAngle), (float)Math.Sin(myRotation + Math.PI - myTriAngle));

            InGame.myGameObjects.Add(new Bullet(this, tempRight, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
            InGame.myGameObjects.Add(new Bullet(this, tempTopRight, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
            InGame.myGameObjects.Add(new Bullet(this, tempBottomRight, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
            InGame.myGameObjects.Add(new Bullet(this, tempLeft, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
            InGame.myGameObjects.Add(new Bullet(this, tempTopLeft, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
            InGame.myGameObjects.Add(new Bullet(this, tempBottomLeft, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
        }

        public void ResetAttackCooldown()
        {
            myAttackCooldown = myOriginalAttackCooldown;
        }
    }
}
