using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace NDE
{
    class PlayerSprite : Sprite
    {
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;

        private Vector2 myDirection = Vector2.Zero, mySpeed = Vector2.Zero, myStartingPosition = Vector2.Zero;
        private KeyboardState myPrevKeyboardState;

        enum state
        {
            standing,
            jumping
        }
        state myCurrentState = state.standing;

        public void LoadContent(ContentManager theContentManager)
        {
            scale = 0.15f;
            position = new Vector2(0, 200);
            LoadContent(theContentManager, "little guy");
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            UpdateJump(currentKeyboardState);

            myPrevKeyboardState = currentKeyboardState;

            base.Update(theGameTime, mySpeed, myDirection);
        }

        private void UpdateJump(KeyboardState curKeyboardState)
        {
            if (myCurrentState == state.standing)
            {
                if (curKeyboardState.IsKeyDown(Keys.Space) == true && myPrevKeyboardState.IsKeyDown(Keys.Space) == false)
                {
                    Jump();
                }
            }

            if (myCurrentState == state.jumping)
            {
                if (myStartingPosition.Y - position.Y > 150)
                {
                    myDirection.Y = MOVE_DOWN;
                }

                if (position.Y > myStartingPosition.Y)
                {
                    position.Y = myStartingPosition.Y;
                    myCurrentState = state.standing;
                    myDirection = Vector2.Zero;
                }
            }
        }

        private void Jump()
        {
            if (myCurrentState != state.jumping)
            {
                myCurrentState = state.jumping;
                myStartingPosition = position;
                myDirection.Y = MOVE_UP;
                mySpeed = new Vector2(120, 120);
            }
        }
    }
}
