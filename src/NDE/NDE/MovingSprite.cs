using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace NDE
{
    enum collisionTypes
    {
        PLATFORM,
        OBSTACLE,
        BACKGROUND
    }

    class MovingSprite : Sprite
    {
        public event ChangedEventHandler Changed;
        private collisionTypes myCollisionType;
        private bool inCollisionDetection;

        public MovingSprite(string spriteName, int viewportWidth, collisionTypes theCollisionType)
            : base(spriteName)
        {
            myViewportWidth = viewportWidth;
            myCollisionType = theCollisionType;
        }

        public collisionTypes getCollisionType()
        {
            return myCollisionType;
        }

        public override void LoadContent(ContentManager theContentManager)
        {
            base.LoadContent(theContentManager);
            Changed = null;
            inCollisionDetection = false;
        }
        public void setSpeed(Vector2 speed)
        {
            mySpeed = speed;
            if (speed != Vector2.Zero)
                myDirection = new Vector2(-1, 0);
        }

        protected override void detectCollision()
        {
            if (myCollisionType == collisionTypes.BACKGROUND)
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
