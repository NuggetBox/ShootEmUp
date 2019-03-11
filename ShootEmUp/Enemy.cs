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

        float anAttackCooldown = 0.3f;
        float anAttackTimer;

        public Enemy()
        {
            myPosition = myStartPos;
            myScale = 1;
            myTexture = Game1.myEnemyTexture;
            myRectangle = myTexture.Bounds;
            myRectangle.Size = new Point((int)(myRectangle.Width * myScale), (int)(myRectangle.Height * myScale));
            myRectangle.Location = myPosition.ToPoint();
            myColor = Color.Red;
        }

        public override void Update(GameTime someDeltaTime)
        {
            myRectangle = new Rectangle(myPosition.ToPoint(), myRectangle.Size);
        }
    }
}
