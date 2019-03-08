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

        Vector2 myStartPos = new Vector2(300, 300);

        public Player(ContentManager someContent)
        {
            myTexture = someContent.Load<Texture2D>("player");
            myColor = Color.White;
            AccessPosition = myStartPos;
            myLayer = 0.5f;
            myScale = 5;
            myRectangle = new Rectangle((int)AccessPosition.X, (int)AccessPosition.Y, (int)(myTexture.Width * myScale), (int)(myTexture.Height * myScale));
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
