using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ShootEmUp
{
    public abstract class State
    {
        public abstract void Update(GameTime someDeltaTime);
        public abstract void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch);
    }
}
