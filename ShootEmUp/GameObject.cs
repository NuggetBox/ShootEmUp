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
        public Vector2 GetPosition { get; private set; }

        public Vector2 GetOrigin { get { return new Vector2(myTexture.Width / 2, myTexture.Height / 2); } }

        public float AccessRotation { get; protected set; }

        public string AccessTag { get; protected set; }

        protected Texture2D myTexture;
        protected Rectangle myRectangle;
        protected Color myColor;
        protected Vector2 myScale;
        protected SpriteEffects mySpriteEffects = SpriteEffects.None;
        protected float myLayer;

        public abstract void Update(GameTime someDeltaTime);

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, GetPosition, myRectangle, myColor, AccessRotation, GetOrigin, myScale, mySpriteEffects, myLayer);
        }

        public void Move(Vector2 vector)
        {
            GetPosition += vector;
        }

        public void Rotate(float rotation)
        {
            AccessRotation += rotation;
        }
    }
}
