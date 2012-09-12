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
        Vector2 JUMP_SPEED = new Vector2(1200, 1200);
        Vector2 FALL_SPEED = new Vector2(200, 200);
        const int MAX_FALL_SPEED = 1600;

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
            GamePadState currentPadState = GamePad.GetState(PlayerIndex.One);

            UpdateJump(currentKeyboardState, currentPadState);

            myPrevKeyboardState = currentKeyboardState;

            base.Update(theGameTime, mySpeed, myDirection);
        }

        private void UpdateJump(KeyboardState curKeyboardState, GamePadState curPadState)
        {
            if (myCurrentState == state.standing)
            {
                if ((curKeyboardState.IsKeyDown(Keys.Space) && !myPrevKeyboardState.IsKeyDown(Keys.Space)) ||
                    curPadState.IsButtonDown(Buttons.A) && !GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                    Jump();
            }

            if (myCurrentState == state.jumping)
            {
                // Top of jump. Change direction
                if (mySpeed.Y <= 0)
                    myDirection.Y = MOVE_DOWN;

                // Apply gravity on increments of 10
                int positionDiff = ((int)(myStartingPosition.Y + 0.5) - (int)(position.Y + 0.5)) % 10;
                if (positionDiff == 0 && mySpeed.Y < MAX_FALL_SPEED)
                {
                    // Apply gravity
                    Vector2 newtonSpeed = FALL_SPEED * myDirection;
                    newtonSpeed.X = newtonSpeed.Y;
                    mySpeed += newtonSpeed;
                }

                // Back to starting position. Stop jump
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
                // Apply speed and direction, and jumping state
                myCurrentState = state.jumping;
                myStartingPosition = position;
                myDirection.Y = MOVE_UP;
                mySpeed = JUMP_SPEED;
            }
        }
    }
}
