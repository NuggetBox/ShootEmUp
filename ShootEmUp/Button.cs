using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShootEmUp
{
    class Button
    {
        public string myLabel;
        public Color myColor = Color.AliceBlue;
        public Action myAction;
        public Vector2 myPosition;
        public Texture2D myTexture;

        public Button(string aLabel, Action anAction)
        {
            myLabel = aLabel;
            myAction = anAction;
        }

        public Button(Texture2D aTexture, Action anAction, Vector2 aPosition)
        {
            myTexture = aTexture;
            myAction = anAction;
            myPosition = aPosition;
        }

        public void Press()
        {
            myAction.Invoke();
        }
    }
}
