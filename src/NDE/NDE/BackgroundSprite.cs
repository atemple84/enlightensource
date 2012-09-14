using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDE
{
    class BackgroundSprite : Sprite
    {
        public BackgroundSprite(int viewportWidth)
        {
            myViewportWidth = viewportWidth + 150;
            //myDirection = new Vector2(-1, 0);
            //mySpeed = new Vector2(160, 0);
        }

        // Must put in due to pure virtual function. Backgrounds don't collide
        protected override void detectCollision() {}
    }
}
