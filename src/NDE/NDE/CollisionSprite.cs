using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private ContentManager myContentManager;

        public CollisionSprite(int viewportWidth, collisionType theCollisionType)
        {
            myViewportWidth = viewportWidth + 150;
            myDirection = new Vector2(-1, 0);
            mySpeed = new Vector2(160, 0);
            myCollisionType = theCollisionType;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            myContentManager = theContentManager;
            switch (myCollisionType)
            {
                case collisionType.PLATFORM:
                    scale = 0.6f;
                    position = new Vector2(0, 265);
                    LoadContent(myContentManager, "platform_gold" + randomObjSelector.Next(1, 4).ToString());
                    break;
                case collisionType.OBSTACLE:
                    scale = 0.25f;
                    color = Color.Red;
                    position = new Vector2(175, 165);
                    LoadContent(myContentManager, "LOL");
                    break;
                default:
                    break;
            }
        }

        protected override void generateNewTexture()
        {
            switch (myCollisionType)
            {
                case collisionType.PLATFORM:
                    LoadContent(myContentManager, "platform_gold" + randomObjSelector.Next(1, 4).ToString());
                    break;
                case collisionType.OBSTACLE:
                    LoadContent(myContentManager, "LOL");
                    break;
                default:
                    break;
            }
        }
        protected override void detectCollision()
        {
        }
    }
}
