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
        public int 
            myDamage = 1,
            myBulletSpeed = 200;

        public float 
            myAttackSpeed = 1f,
            myAttackTimer;

        public void Shoot(Vector2 aDirection)
        {
            aDirection.Normalize();
            InGame.myGameObjects.Add(new Bullet(this, aDirection, myPosition, myBulletSpeed, myDamage, Game1.myEnemyBullet));
        }
    }
}
