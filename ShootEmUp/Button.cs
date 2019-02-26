using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootEmUp
{
    class Button
    {
        public string myLabel;
        public Color myColor;
        public Action myAction;
        public Vector2 myPosition;

        public Button(string aLabel, Color aColor, Action anAction, Vector2 aPosition)
        {
            myLabel = aLabel;
            myColor = aColor;
            myAction = anAction;
            myPosition = aPosition;
        }

        public void Press()
        {
            myAction.Invoke();
        }
    }
}
