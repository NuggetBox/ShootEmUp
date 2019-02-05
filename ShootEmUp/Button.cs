using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootEmUp
{
    class Button
    {
        public string myLabel;
        public SpriteFont mySpriteFont;
        public Color myColor;
        public Action myAction;
        public Rectangle myRectangle;

        public Button(string aLabel, Color aColor, Action anAction, Rectangle aRectangle)
        {
            myLabel = aLabel;
            myColor = aColor;
            myAction = anAction;
            myRectangle = aRectangle;
        }

        public void Press()
        {
            myAction.Invoke();
        }
    }
}
