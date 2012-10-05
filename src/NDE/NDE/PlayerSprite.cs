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

        private Vector2 myStartingPosition = Vector2.Zero;
        private KeyboardState myPrevKeyboardState;
        private GamePadState myPrevGamePadState;

        public event ChangedEventHandler Changed;

        enum state
        {
            STANDING,
            JUMPING
        }
        state myCurrentState = state.STANDING;

        public PlayerSprite(PlayerIndex index)
        {
            myPlayerIndex = index;
            scale = 0.088f;
            spriteName = "little guy";
            position = new Vector2(50, 200);
        }

        public override void Update(GameTime theGameTime, ContentManager theContent)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            GamePadState currentPadState = GamePad.GetState(myPlayerIndex);

            UpdateJump(currentKeyboardState, currentPadState);

            myPrevKeyboardState = currentKeyboardState;
            myPrevGamePadState = currentPadState;

            base.Update(theGameTime, theContent);
        }

        private void UpdateJump(KeyboardState curKeyboardState, GamePadState curPadState)
        {
            if (myCurrentState == state.STANDING)
            {
                if ((curKeyboardState.IsKeyDown(Keys.Space) && !myPrevKeyboardState.IsKeyDown(Keys.Space)) ||
                    curPadState.IsButtonDown(Buttons.A) && !myPrevGamePadState.IsButtonDown(Buttons.A))
                    Jump();
            }

            if (myCurrentState == state.JUMPING)
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

            // TEMPORARY death key
            if (curKeyboardState.IsKeyDown(Keys.D) || curPadState.IsButtonDown(Buttons.RightStick))
            {
                if (Changed != null)
                    Changed(this, false);
            }
        }

        private void Jump()
        {
            if (myCurrentState != state.JUMPING)
            {
                // Apply speed and direction, and jumping state
                myCurrentState = state.JUMPING;
                myStartingPosition = position;
                myDirection.Y = MOVE_UP;
                mySpeed = JUMP_SPEED;
            }
        }

        protected override void detectCollision()
        {
            // Back to starting position. Stop jump
            // **** THIS IS A TEMPORARY GROUND!!! UNTIL PROPER COLLISION DETECTION IS ENABLED ****
            float curFeetPosition = boundingBox.Bottom;
            if(curFeetPosition > Game1.bottomPoint.Y)
            {
                position.Y = Game1.bottomPoint.Y - boundingBox.Height;
                myCurrentState = state.STANDING;
                myDirection = Vector2.Zero;
            }
        }
    }
}
