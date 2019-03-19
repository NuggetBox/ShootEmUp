using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootEmUp
{
    class Bullet : GameObject
    {
        public bool GetIsPlayerBullet => myOwner is Player;

        public GameObject myOwner;

        public float myDamage;

        public Bullet(GameObject anOwner, Vector2 aDirection, Vector2 aPosition, int someSpeed, float someDamage, Texture2D aTexture)
        {
            myOwner = anOwner;
            myDirection = aDirection;
            aDirection.Normalize();
            mySpeed = someSpeed;
            myDamage = someDamage;
            myTexture = aTexture;
            myPosition = aPosition;
            myRectangle = CreateRectangle();
        }

        public override void Update(GameTime someDeltaTime)
        {
            myPosition += myDirection * mySpeed * (float)someDeltaTime.ElapsedGameTime.TotalSeconds;
            myRectangle.Location = (myPosition - GetOrigin * myScale).ToPoint();
            myRotation = (float)Math.Atan2(myDirection.X, -myDirection.Y);

            if (myPosition.X + 2 * myRectangle.Width < 0 || myPosition.X > Game1.AccessWindowSize.X + myRectangle.Width || myPosition.Y + 2 * myRectangle.Height < 0 || myPosition.Y > Game1.AccessWindowSize.Y + myRectangle.Height)
            {
                myRemoved = true;
            }

            CheckCollision();
        }

        void CheckCollision()
        {
            for (int i = 0; i < InGame.myGameObjects.Count; ++i)
            {
                if (myRectangle.Intersects(InGame.myGameObjects[i].myRectangle) && !(InGame.myGameObjects[i] is Bullet) && !myRemoved)
                {
                    if (InGame.myGameObjects[i] is Player)
                    {
                        if (!GetIsPlayerBullet)
                        {
                            InGame.myGameObjects[i].myHealth -= myDamage;

                            // TODO: IF PLAYER DIE
                            CheckPlayerDeath();

                            myRemoved = true;
                            return;
                        }
                    }
                    else if (InGame.myGameObjects[i] is Enemy && GetIsPlayerBullet)
                    {
                        InGame.myGameObjects[i].myHealth -= myDamage;

                        // TODO: IF ENEMY DIE
                        if (InGame.myGameObjects[i].myHealth <= 0)
                        {
                            InGame.myGameObjects[i].myRemoved = true;
                        }

                        myRemoved = true;
                    }
                }
            }
        }
    }
}
