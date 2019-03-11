using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    class Enemy : GameObject
    {
        Vector2 myStartPos = new Vector2(600, 300);

        public Enemy()
        {
            myPosition = myStartPos;
            myTexture = Game1.myEnemyTexture;
            myRectangle = myTexture.Bounds;
            myScale = 1;
            myColor = Color.Red;
        }

        public override void Update(GameTime someDeltaTime)
        {
            myRectangle = new Rectangle(myPosition.ToPoint(), myRectangle.Size);

            for (int i = 0; i < InGame.myGameObjects.Count; ++i)
            {
                if (InGame.myGameObjects[i] is Bullet && InGame.myGameObjects[i].myRectangle.Intersects(myRectangle))
                {
                    Destroy();   
                }
            }
        }
    }
}
