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
        public Vector2 myPosition;
        public Vector2 GetOrigin { get { return new Vector2(myTexture.Width / 2, myTexture.Height / 2); } }
        public Vector2 myDirection = Vector2.Zero;

        public float myRotation;
        public float mySpeed;

        public Texture2D myTexture;
        public Rectangle myRectangle;

        public Color myColor = Color.White;
        protected float myScale = 1;
        SpriteEffects mySpriteEffects = SpriteEffects.None;
        float myLayer;

        public abstract void Update(GameTime someDeltaTime);

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, myPosition, null, myColor, myRotation, GetOrigin, myScale, mySpriteEffects, myLayer);
        }

        protected void Move()
        {
            myPosition += myDirection * mySpeed;
            myRectangle.Location = new Point((int)myPosition.X, (int)myPosition.Y);
        }

        protected void Destroy()
        {
            InGame.myGameObjects.Remove(this);
        }
    }
}
