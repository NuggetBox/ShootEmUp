using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    abstract class Enemy : GameObject
    {
        public float myAttackCooldown = 0.3f;
        public float myAttackTimer;
        public float myBulletSpeed;
        public float myBulletDamage;

        public void Initialize()
        {
            //myPosition = aStartPos;
            myRectangle = myTexture.Bounds;
            myRectangle.Size = new Point((int)(myRectangle.Width * myScale), (int)(myRectangle.Height * myScale));
            myRectangle.Location = myPosition.ToPoint();
        }

        //public override void Update(GameTime someDeltaTime)
        //{
        //    myRectangle = new Rectangle(myPosition.ToPoint(), myRectangle.Size);
        //}

        public void Shoot(Vector2 aDirection)
        {
            InGame.myGameObjects.Add(new Bullet(this, aDirection, myPosition, myBulletSpeed, myBulletDamage, Game1.myEnemyBullet));
        }
    }
}
