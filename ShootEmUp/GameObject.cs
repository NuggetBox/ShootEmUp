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
        public Vector2 GetOrigin { get { return new Vector2(myRectangle.Width * 0.5f, myRectangle.Height * 0.5f); } }

        SpriteEffects mySpriteEffects = SpriteEffects.None;

        public Vector2 myPosition;
        public Vector2 myDirection = Vector2.Zero;
        public Rectangle myRectangle;

        public Texture2D myTexture;
        public Color myColor = Color.White;

        public int myScale = 1;

        public float myRotation;
        public float mySpeed;
        public float myHealth = 1;
        float myLayer;

        public bool myRemoved;
        public bool mySolid;

        public abstract void Update(GameTime someDeltaTime);

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, myPosition, null, myColor, myRotation, GetOrigin, myScale, mySpriteEffects, myLayer);
        }

        protected void Move(GameTime someDeltaTime)
        {
            if (myDirection != Vector2.Zero)
            {
                myDirection.Normalize();
            }

            Vector2 tempMove = myDirection * mySpeed * (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            Rectangle tempRectangle = new Rectangle(tempMove.ToPoint(), myRectangle.Size);

            if (CheckCollision())
            {
                myDirection = Vector2.Zero;
                return;
            }

            myPosition += tempMove;
            myRectangle.Location = new Point((int)(myPosition.X - myRectangle.Width * 0.5f), (int)(myPosition.Y - myRectangle.Height * 0.5f));
            myDirection = Vector2.Zero;
        }

        bool CheckCollision()
        {
            for (int i = 0; i < InGame.myGameObjects.Count; ++i)
            {
                if (myRectangle.Intersects(InGame.myGameObjects[i].myRectangle) && InGame.myGameObjects[i].mySolid && InGame.myGameObjects[i] != this)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
