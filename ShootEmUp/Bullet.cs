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
            myDirection = aDirection * someSpeed;
            myPosition = anOrigin;
            myTexture = aTexture;
            myRectangle = myTexture.Bounds;
        }

        public override void Update(GameTime someDeltaTime)
        {
            myRectangle = new Rectangle(myPosition.ToPoint(), myRectangle.Size);
            myPosition += myDirection;
            myRotation = (float)Math.Atan2(myDirection.Y, myDirection.X);

            if (myPosition.X < -100 || myPosition.X > Game1.AccessWindowSize.X + 100 || myPosition.Y < -100 || myPosition.Y > Game1.AccessWindowSize.Y + 100)
            {
                Destroy();
            }
        }
    }
}
