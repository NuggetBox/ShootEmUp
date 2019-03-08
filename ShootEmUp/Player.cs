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
            AccessPosition = myStartPos;
            AccessTexture = Game1.myPlayerTexture;
            AccessRectangle = AccessTexture.Bounds;
            AccessSpeed = 5;
        }

        public override void Update(GameTime someDeltaTime)
        {
            KeyboardState tempKeyboard = Keyboard.GetState();
            MouseState tempMouse = Mouse.GetState();

            AccessRectangle = new Rectangle(AccessPosition.ToPoint(), AccessRectangle.Size);

            anAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (tempKeyboard.IsKeyDown(Keys.Up))
            {
                AccessVelocity += new Vector2(0, -1);
            }
            if (tempKeyboard.IsKeyDown(Keys.Right))
            {
                AccessVelocity += new Vector2(1, 0);
            }
            if (tempKeyboard.IsKeyDown(Keys.Down))
            {
                AccessVelocity += new Vector2(0, 1);
            }
            if (tempKeyboard.IsKeyDown(Keys.Left))
            {
                AccessVelocity += new Vector2(-1, 0);
            }
            if (tempKeyboard.IsKeyDown(Keys.Z) && anAttackTimer <= 0)
            {
                Shoot();
                anAttackTimer = anAttackCooldown;
            }

            Move();
            AccessVelocity = Vector2.Zero;
        }

        public void Shoot()
        {
            InGame.myGameObjects.Add(new Bullet("player", new Vector2(1, 0), AccessPosition, 40, Game1.myBulletTexture));
        }
    }
}
