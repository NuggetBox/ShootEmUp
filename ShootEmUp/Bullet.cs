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

        public Bullet(string anOwner, Vector2 aDirection, Vector2 anOrigin, float someSpeed, Texture2D aTexture)
        {
            AccessTag = anOwner;
            aDirection.Normalize();
            AccessVelocity = aDirection * someSpeed;
            AccessPosition = anOrigin;
            AccessTexture = aTexture;
            AccessRectangle = AccessTexture.Bounds;
        }

        public override void Update(GameTime someDeltaTime)
        {
            AccessRectangle = new Rectangle(AccessPosition.ToPoint(), AccessRectangle.Size);
            AccessPosition += AccessVelocity;
            AccessRotation = (float)Math.Atan2(AccessVelocity.Y, AccessVelocity.X);

            if (AccessPosition.X < -100 || AccessPosition.X > Game1.AccessWindowSize.X + 100 || AccessPosition.Y < -100 || AccessPosition.Y > Game1.AccessWindowSize.Y + 100)
            {
                Destroy();
            }
        }
    }
}
