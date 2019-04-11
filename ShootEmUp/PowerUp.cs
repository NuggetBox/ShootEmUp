using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    abstract class PowerUp : GameObject
    {
        public enum PowerUpType { FireRate }
        public static PowerUpType myPowerUpType = new PowerUpType();

        public int
            myLeftBounds = 420,
            myRightBounds = 850;

        public float myActiveTime;

        public abstract void Apply(float someDelta);
        public abstract void Reset();

        public void CollisionCheck()
        {
            if (myRectangle.Intersects(InGame.AccessPlayer.myRectangle))
            {
                InGame.AccessPlayer.myPowerUps.Add(this);
                myRemoved = true;
            }
        }
    }
}
