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
        OBSTACLE
    }

    class CollisionSprite : Sprite
    {
        public collisionType myCollisionType = collisionType.PLATFORM;
        Random randomObjSelector = new Random();

        public CollisionSprite(int viewportWidth, collisionType theCollisionType)
        {
            myViewportWidth = viewportWidth + 150;
            myDirection = new Vector2(-1, 0);
            myCollisionType = theCollisionType;
        }

        protected override void detectCollision()
        {
        }
    }
}
