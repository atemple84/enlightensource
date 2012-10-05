using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NDE
{
    enum collisionType
    {
        PLATFORM,
        OBSTACLE,
        BACKGROUND
    }

    class MovingSprite : Sprite
    {
        public event ChangedEventHandler Changed;
        public collisionType myCollisionType = collisionType.PLATFORM;
        private bool inCollisionDetection = false;

        public MovingSprite(int viewportWidth, collisionType theCollisionType)
        {
            myViewportWidth = viewportWidth;
            myCollisionType = theCollisionType;
        }

        public void setSpeed(Vector2 speed)
        {
            mySpeed = speed;
            if (speed != Vector2.Zero)
                myDirection = new Vector2(-1, 0);
        }

        protected override void detectCollision()
        {
            if (myCollisionType == collisionType.BACKGROUND)
                return;
            else
            {
                if (inCollisionDetection)
                {
                    if (boundingBox.Right < 0 || boundingBox.Left > 300)
                        changeCollisionDetection(false);
                }
                else
                {
                    if (boundingBox.Left < 300 && boundingBox.Right > 0)
                        changeCollisionDetection(true);
                }
            }
        }

        private void changeCollisionDetection(bool collisionFlag)
        {
            inCollisionDetection = collisionFlag;
            if (Changed != null)
                Changed(this, collisionFlag);
        }
    }
}
