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
            AccessPosition = myStartPos;
            AccessTexture = Game1.myEnemyTexture;
            AccessRectangle = AccessTexture.Bounds;
            myScale = 1;
            myColor = Color.Red;
        }

        public override void Update(GameTime someDeltaTime)
        {
            AccessRectangle = new Rectangle(AccessPosition.ToPoint(), AccessRectangle.Size);

            for (int i = 0; i < InGame.myGameObjects.Count; ++i)
            {
                if (InGame.myGameObjects[i] is Bullet && InGame.myGameObjects[i].AccessRectangle.Intersects(AccessRectangle))
                {
                    Destroy();   
                }
            }
        }
    }
}
