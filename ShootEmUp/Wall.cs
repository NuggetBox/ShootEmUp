using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShootEmUp
{
    class Wall : GameObject
    {
        public Wall()
        {
            mySolid = true;
        }

        public override void Update(GameTime someDeltaTime) { }
    }
}
