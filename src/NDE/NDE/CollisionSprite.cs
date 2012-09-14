using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NDE
{
    class CollisionSprite : Sprite
    {
        enum collosionType
        {
            PLATFORM,
            OBSTACLE
        }
        collosionType myCollisionType = collosionType.PLATFORM;
        Random randomObjSelector = new Random();
        private ContentManager myContentManager;

        public CollisionSprite(int viewportWidth)
        {
            myViewportWidth = viewportWidth + 150;
            myDirection = new Vector2(-1, 0);
            mySpeed = new Vector2(160, 0);
        }

        public void LoadContent(ContentManager theContentManager)
        {
            myContentManager = theContentManager;
            scale = 0.6f;
            position = new Vector2(0, 265);
            if (myCollisionType == collosionType.PLATFORM)
            {
                LoadContent(myContentManager, "platform_gold" + randomObjSelector.Next(1, 4).ToString());
            }
        }

        protected override void generateNewTexture()
        {
            LoadContent(myContentManager, "platform_gold" + randomObjSelector.Next(1, 4).ToString());
        }
        protected override void detectCollision()
        {
        }
    }
}
