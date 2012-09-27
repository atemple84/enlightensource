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
        public collisionType myCollisionType = collisionType.PLATFORM;

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
        }
    }
}
