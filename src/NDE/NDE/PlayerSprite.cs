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
        private Vector2 JUMP_SPEED = new Vector2(1200, 1200);
        private Vector2 FALL_SPEED = new Vector2(200, 200);
        private const int MAX_FALL_SPEED = 1600;
        private PlayerIndex myPlayerIndex;

        private Vector2 myDirection = Vector2.Zero, mySpeed = Vector2.Zero, myStartingPosition = Vector2.Zero;
        private KeyboardState myPrevKeyboardState;
        private GamePadState myPrevGamePadState;

        enum state
        {
            standing,
            jumping
        }
        state myCurrentState = state.standing;

        public PlayerSprite(PlayerIndex index)
        {
            myPlayerIndex = index;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            scale = 0.15f;
            LoadContent(theContentManager, "little guy");

            // Set position after the size of the sprite has been made
            position = new Vector2(15, 200);
        }

        public void Update(GameTime theGameTime)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            GamePadState currentPadState = GamePad.GetState(myPlayerIndex);

            UpdateJump(currentKeyboardState, currentPadState);

            myPrevKeyboardState = currentKeyboardState;
            myPrevGamePadState = currentPadState;

            base.Update(theGameTime, mySpeed, myDirection);
        }

        private void UpdateJump(KeyboardState curKeyboardState, GamePadState curPadState)
        {
            if (myCurrentState == state.standing)
            {
                if ((curKeyboardState.IsKeyDown(Keys.Space) && !myPrevKeyboardState.IsKeyDown(Keys.Space)) ||
                    curPadState.IsButtonDown(Buttons.A) && !myPrevGamePadState.IsButtonDown(Buttons.A))
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

        protected override void detectCollision()
        {
            // Back to starting position. Stop jump
            float curFeetPosition = position.Y + mySize.Height;
            if(curFeetPosition > Game1.bottomPoint.Y)
            {
                position.Y = Game1.bottomPoint.Y - mySize.Height;
                myCurrentState = state.standing;
                myDirection = Vector2.Zero;
            }
        }
    }
}
