using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace ShootEmUp
{
    class TriShoot : PowerUp
    {
        public float myNewAttackCooldown = 0.15f;

        public TriShoot(float someActiveTime)
        {
            myPosition = new Vector2(myRandom.Next(Game1.myLeftBeach, Game1.myRightBeach), 0);
            mySpeed = 100;
            myColor = Color.Tomato;
            myTexture = Game1.myBarrel;
            myRectangle = CreateRectangle();
            myActiveTime = someActiveTime;
            myDirection = new Vector2(0, 1);
        }

        public override void Apply(float someDelta)
        {
            InGame.AccessPlayer.myTriShooting = true;
            myActiveTime -= someDelta;
        }

        public override void Reset()
        {
            InGame.AccessPlayer.myTriShooting = false;
        }

        public override void Update(GameTime someDeltaTime)
        {
            Move(someDeltaTime);
            CollisionCheck();
        }
    }
}
