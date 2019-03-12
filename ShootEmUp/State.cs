using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace ShootEmUp
{
    public abstract class State
    {
        public virtual void Initialize() { }
        public abstract void Update(GameTime someDeltaTime);
        public abstract void Draw(GameTime someDeltaTime, SpriteBatch aSpriteBatch);
    }
}
