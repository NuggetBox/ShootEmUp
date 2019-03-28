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
        Keys 
            myUp = Keys.W,
            myDown = Keys.S,
            myRight = Keys.D, 
            myLeft = Keys.A,
            myClockwiseRotation = Keys.L,
            myCounterClockwiseRotation = Keys.J,
            myShoot = Keys.I;

        Vector2 myStartPos = new Vector2(500, 300);

        int 
            myBulletDamage = 1,
            myBulletSpeed = 300;

        float
            myAttackCooldown = 0.3f,
            myAttackTimer,
            myRotationSpeed = 1.5f;

        public Player()
        {
            myPosition = myStartPos;
            myTexture = Game1.myPlayer;
            myRectangle = CreateRectangle();
            myHealth = 300;
            mySpeed = 100;
        }

        public override void Update(GameTime someDeltaTime)
        {
            CheckPlayerDeath();

            KeyboardState tempKeyboard = Keyboard.GetState();
            MouseState tempMouse = Mouse.GetState();

            myAttackTimer -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            if (tempKeyboard.IsKeyDown(myUp))
            {
                myDirection += new Vector2(0, -1);
            }
            if (tempKeyboard.IsKeyDown(myRight))
            {
                myDirection += new Vector2(1, 0);
            }
            if (tempKeyboard.IsKeyDown(myDown))
            {
                myDirection += new Vector2(0, 1);
            }
            if (tempKeyboard.IsKeyDown(myLeft))
            {
                myDirection += new Vector2(-1, 0);
            }
            if (tempKeyboard.IsKeyDown(myShoot) && myAttackTimer <= 0)
            {
                Shoot();
                myAttackTimer = myAttackCooldown;
            }
            if (tempKeyboard.IsKeyDown(myClockwiseRotation))
            {
                myRotation += (float)someDeltaTime.ElapsedGameTime.TotalSeconds * myRotationSpeed;
            }
            if (tempKeyboard.IsKeyDown(myCounterClockwiseRotation))
            {
                myRotation -= (float)someDeltaTime.ElapsedGameTime.TotalSeconds * myRotationSpeed;
            }

            // Console.WriteLine(myPosition);
            // 420, 850

            if (myPosition.X + myDirection.X * mySpeed * someDeltaTime.ElapsedGameTime.TotalSeconds > 420 && myPosition.X + myDirection.X * mySpeed * someDeltaTime.ElapsedGameTime.TotalSeconds < 850)
            {
                Move(someDeltaTime);
            }
        }

        public void Shoot()
        {
            Vector2 tempRight = new Vector2((float)Math.Cos(myRotation), (float)Math.Sin(myRotation));
            Vector2 tempLeft = new Vector2((float)Math.Cos(myRotation + Math.PI), (float)Math.Sin(myRotation + Math.PI));

            InGame.myGameObjects.Add(new Bullet(this, tempRight, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
            InGame.myGameObjects.Add(new Bullet(this, tempLeft, myPosition, myBulletSpeed, myBulletDamage, Game1.myPlayerBullet));
        }
    }
}
