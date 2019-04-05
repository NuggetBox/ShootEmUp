using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShootEmUp
{
    abstract class Enemy : GameObject
    {
        public int 
            myDamage = 1,
            myBulletSpeed = 200;

        public float 
            myAttackSpeed = 1f,
            myAttackTimer,
            myAttackRange = 150;

        public void Shoot(Vector2 aDirection, Texture2D aTexture)
        {
            aDirection.Normalize();
            InGame.myGameObjects.Add(new Bullet(this, aDirection, myPosition, myBulletSpeed, myDamage, aTexture));
            myAttackTimer = myAttackSpeed;
        }
    }
}
