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
        public string AccessTag { get; private set; }

        public Bullet(string anOwner, Vector2 aDirection, float someSpeed, Texture2D aTexture, Color aColor)
        {
            AccessTag = anOwner;
            aDirection.Normalize();
            AccessVelocity = aDirection * someSpeed;
            myTexture = aTexture;
            myColor = aColor;
        }

        public override void Update(GameTime someDeltaTime)
        {
            AccessPosition += AccessVelocity;
            AccessRotation = (float)Math.Atan2(AccessVelocity.Y, AccessVelocity.X);
        }
    }
}
