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
        public int myBulletDamage = 1;

        public float myAttackSpeed = 0.3f;
        public float myAttackTimer;
        public float myBulletSpeed = 200;

        public void Shoot(Vector2 aDirection)
        {
            InGame.myGameObjects.Add(new Bullet(this, aDirection, myPosition, myBulletSpeed, myBulletDamage, Game1.myEnemyBullet));
        }
    }
}
