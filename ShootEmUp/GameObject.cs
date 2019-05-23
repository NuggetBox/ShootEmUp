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
    abstract class GameObject
    {
        public Vector2 GetOrigin => new Vector2(myTexture.Bounds.Width * 0.5f, myTexture.Bounds.Height * 0.5f);

        SpriteEffects mySpriteEffects = SpriteEffects.None;

        public Vector2 
            myPosition,
            myDirection = Vector2.Zero;

        public Rectangle myRectangle;

        public Texture2D myTexture;

        public Color myColor = Color.White;

        public int 
            myHealth = 1,
            mySpeed;

        public float
            myScale = 4,
            myAnimSpeed,
            myAnimTimer,
            myRotation;

        public float myLayer = 0.5f;

        public bool 
            myRemoved,
            mySolid;

        public abstract void Update(GameTime someDeltaTime);

        public void Draw(SpriteBatch aSpriteBatch)
        {
            aSpriteBatch.Draw(myTexture, myPosition, null, myColor, myRotation, GetOrigin, myScale, mySpriteEffects, myLayer);
        }

        protected void Move(GameTime someDeltaTime)
        {
            if (myDirection != Vector2.Zero)
            {
                myDirection.Normalize();
            }

            Vector2 tempMove = myDirection * mySpeed * (float)someDeltaTime.ElapsedGameTime.TotalSeconds;

            //Rectangle tempRectangle = new Rectangle(tempMove.ToPoint(), myRectangle.Size);

            //if (CheckCollision(tempRectangle))
            //{
            //    myDirection = Vector2.Zero;
            //    return;
            //}

            myPosition += tempMove;
            myRectangle.Location = ((myPosition - GetOrigin * myScale)).ToPoint();

            if (this is Player)
                myDirection = Vector2.Zero;
        }

        bool CheckCollision(Rectangle aRectangle)
        {
            for (int i = 0; i < InGame.myGameObjects.Count; ++i)
            {
                if (myRectangle.Intersects(InGame.myGameObjects[i].myRectangle) && InGame.myGameObjects[i].mySolid && InGame.myGameObjects[i] != this)
                {
                    return true;
                }
            }

            return false;
        }

        protected Rectangle CreateRectangle()
        {
            return new Rectangle((int)(myPosition.X - GetOrigin.X * myScale), (int)(myPosition.Y - GetOrigin.Y * myScale), (int)(myTexture.Width * myScale), (int)(myTexture.Height * myScale));
        }

        protected void CheckPlayerDeath()
        {
            if (Game1.GetCurrentState is InGame)
            {
                if (InGame.AccessPlayer.myHealth <= 0)
                {
                    Game1.myFinalScore = InGame.myScore;
                    InGame.myScore = 0;

                    List<Button> tempButtons = new List<Button>()
                    {
                        new Button("Restart", Menu.Start),
                        new Button("Main Menu", Menu.ExitToMain),
                        new Button("Quit", Menu.Quit),
                    };

                    Game1.AccessStateStack.Push(new Menu(tempButtons));
                }
            }
            else if (Game1.GetCurrentState is Battle)
            {
                if (this is Player)
                {
                    if (myHealth <= 0)
                    {
                        List<Button> tempButtons = new List<Button>()
                        {
                            new Button("Restart", Menu.Restart),
                            new Button("Main Menu", Menu.ExitToMain),
                            new Button("Quit Game", Menu.Quit)
                        };

                        Texture2D tempDisplay;
                        string tempWinText;

                        if ((this as Player).myPlayerOne)
                        {
                            tempDisplay = Battle.myPlayers[1].myTexture;
                            tempWinText = "Player 2 wins!";
                        }
                        else
                        {
                            tempDisplay = Battle.myPlayers[0].myTexture;
                            tempWinText = "Player 1 wins!";
                        }

                        Game1.AccessStateStack.Push(new Menu(tempButtons) { myDisplayTexture = tempDisplay, myWinText = tempWinText } );
                        Game1.GetCurrentState.Initialize();
                    }
                }
            }
        }
    }
}
