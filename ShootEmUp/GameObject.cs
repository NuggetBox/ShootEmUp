using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootEmUp
{
    abstract class GameObject
    {
        public Vector2 AccessPosition { get; set; }
        public Vector2 GetOrigin { get { return new Vector2(AccessTexture.Width / 2, AccessTexture.Height / 2); } }
        public Vector2 AccessVelocity { get; set; } = Vector2.Zero;

        public float AccessRotation { get; protected set; }
        public float AccessSpeed { get; set; }

        public Texture2D AccessTexture { get; protected set; }
        public Rectangle AccessRectangle { get; protected set; }

        protected Color myColor = Color.White;
        protected float myScale = 3;
        protected SpriteEffects mySpriteEffects = SpriteEffects.None;
        protected float myLayer;

        public abstract void Update(GameTime someDeltaTime);

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(AccessTexture, AccessPosition, null, myColor, AccessRotation, GetOrigin, myScale, mySpriteEffects, myLayer);
        }

        protected void Move()
        {
            AccessVelocity.Normalize();
            AccessVelocity *= AccessSpeed;
            AccessPosition += AccessVelocity;
            //myRectangle.Location = AccessPosition.ToPoint();
            AccessVelocity = Vector2.Zero;
        }

        protected void Destroy()
        {
            InGame.myGameObjects.Remove(this);
        }

        //public void Rotate(float rotation)
        //{
        //    AccessRotation += rotation;
        //}
    }
}
