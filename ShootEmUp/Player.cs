using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ShootEmUp
{
    class Player : GameObject
    {
        Texture2D myBulletTexture;

        public Player(ContentManager content)
        {
            myTexture = content.Load<Texture2D>("player");
            //myRectangle = new Rectangle()
            myBulletTexture = Game1.myBulletTexture;
        }

        public override void Update(GameTime someDeltaTime)
        {
            KeyboardState tempKeyboard = Keyboard.GetState();
            MouseState tempMouse = Mouse.GetState();


        }

        public void Shoot(MouseState aMouse)
        {

        }
    }
}
