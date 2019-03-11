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
    class Player : GameObject
    {
        Vector2 myStartPos = new Vector2(300, 300);

        protected float anAttackCooldown = 0.3f;
        protected float anAttackTimer;

        public Player()
        {
            myPosition = myStartPos;
            myTexture = Game1.myPlayerTexture;
            myRectangle = myTexture.Bounds;
            mySpeed = 5;
        }

        public override void Update(GameTime someDeltaTime)
        {
            KeyboardState tempKeyboard = Keyboard.GetState();
            MouseState tempMouse = Mouse.GetState();

            myRectangle = new Rectangle(myPosition.ToPoint(), myRectangle.Size);

            anAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (tempKeyboard.IsKeyDown(Keys.Up))
            {
                myDirection += new Vector2(0, -1);
            }
            if (tempKeyboard.IsKeyDown(Keys.Right))
            {
                myDirection += new Vector2(1, 0);
            }
            if (tempKeyboard.IsKeyDown(Keys.Down))
            {
                myDirection += new Vector2(0, 1);
            }
            if (tempKeyboard.IsKeyDown(Keys.Left))
            {
                myDirection += new Vector2(-1, 0);
            }
            if (tempKeyboard.IsKeyDown(Keys.Z) && anAttackTimer <= 0)
            {
                Shoot();
                anAttackTimer = anAttackCooldown;
            }

            Move();
            myDirection = Vector2.Zero;
        }

        public void Shoot()
        {
            InGame.myGameObjects.Add(new Bullet("player", new Vector2(1, 0), myPosition, 40, Game1.myBulletTexture));
        }
    }
}
