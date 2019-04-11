using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    class FireRate : PowerUp
    {
        Random myRandom = new Random();

        public float
            myNewAttackCooldown = 0.15f,
            myOriginalCooldown;

        public FireRate(float someActiveTime)
        {
            myPosition = new Vector2(myRandom.Next(Game1.myLeftBeach, Game1.myRightBeach), 0);
            mySpeed = 100;
            myTexture = Game1.myBarrel;
            myRectangle = CreateRectangle();
            myOriginalCooldown = InGame.AccessPlayer.myAttackCooldown;
            myActiveTime = someActiveTime;
            myDirection = new Vector2(0, 1);
        }

        public override void Apply(float someDelta)
        {
            InGame.AccessPlayer.myAttackCooldown = myNewAttackCooldown;
            myActiveTime -= someDelta;
        }

        public override void Reset()
        {
            InGame.AccessPlayer.myAttackCooldown = myOriginalCooldown;
            InGame.AccessPlayer.myPowerUps.Remove(this);
        }

        public override void Update(GameTime someDeltaTime)
        {
            Move(someDeltaTime);
            CollisionCheck();
        }
    }
}
