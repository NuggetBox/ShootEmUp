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
    class Tutorial : State
    {
        List<Key> myKeys;

        public override void Initialize()
        {
            myKeys = new List<Key>()
            {
                new Key(Game1.myW, new Vector2(100, 71)),
                new Key(Game1.myA, new Vector2(46, 125)),
                new Key(Game1.myS, new Vector2(100, 125)),
                new Key(Game1.myD, new Vector2(154, 125)),

                new Key(Game1.myI, new Vector2(100, 185)),
                new Key(Game1.myJ, new Vector2(70, 245)),
                new Key(Game1.myL, new Vector2(135, 245)),

                new Key(Game1.myEsc, new Vector2(104, 315)),

                new Key(Game1.myE, new Vector2(100, 414)),
                new Key(Game1.myQ, new Vector2(100, 474)),
                new Key(Game1.myR, new Vector2(100, 534)),
                new Key(Game1.myF, new Vector2(100, 594)),
                new Key(Game1.myM, new Vector2(100, 654)),

                new Key(Game1.myUp, new Vector2(650, 71)),
                new Key(Game1.myLeft, new Vector2(596, 125)),
                new Key(Game1.myDown, new Vector2(650, 125)),
                new Key(Game1.myRight, new Vector2(704, 125)),
                new Key(Game1.myEnter, new Vector2(630, 195)),

                //new Key(Game1.myEsc, new Vector2(50, 0)),

                new Key(Game1.myEsc, new Vector2(1000, 677)),
            };
        }

        public override void Update(GameTime someDeltaTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Menu.ExitToMain();
            }
        }

        public override void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch)
        {
            for (int i = 0; i < myKeys.Count; ++i)
            {
                Key tempKey = myKeys[i];
                aSpriteBatch.Draw(tempKey.myTexture, tempKey.myPosition, Color.White);
            }

            aSpriteBatch.DrawString(Game1.mySpriteFont, "Gameplay", new Vector2(85, 0), Color.LightGoldenrodYellow, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Movement", new Vector2(225, 100), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Shoot", new Vector2(225, 185), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Rotate", new Vector2(225, 245), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Pause", new Vector2(225, 305), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Sound", new Vector2(115, 350), Color.LightGoldenrodYellow, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);

            aSpriteBatch.DrawString(Game1.mySpriteFont, "Pitch Up", new Vector2(175, 414), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Pitch Down", new Vector2(175, 474), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Reset Pitch", new Vector2(175, 534), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Random Pitch", new Vector2(175, 594), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Mute Sound", new Vector2(175, 654), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);

            aSpriteBatch.DrawString(Game1.mySpriteFont, "Menu Navigation", new Vector2(620, 0), Color.LightGoldenrodYellow, 0, Vector2.Zero, 0.4f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Change Selection", new Vector2(775, 100), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
            aSpriteBatch.DrawString(Game1.mySpriteFont, "Confirm Selection", new Vector2(775, 220), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);

            aSpriteBatch.DrawString(Game1.mySpriteFont, "Main Menu", new Vector2(1055, 670), Color.White, 0, Vector2.Zero, 0.28f, SpriteEffects.None, 0);
        }
    }

    class Key
    {
        public Texture2D myTexture;
        public Vector2 myPosition;

        public Key(Texture2D aTexture, Vector2 aPosition)
        {
            myTexture = aTexture;
            myPosition = aPosition;
        }
    }
}
