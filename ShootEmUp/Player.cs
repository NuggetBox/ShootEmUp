﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ShootEmUp
{
    class Player : GameObject
    {
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
