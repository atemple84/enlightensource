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
            platform,
            obstacle
        }
        collosionType myCollisionType = collosionType.platform;
        Random randomObjSelector = new Random();
        private int myViewportWidth;

        public CollisionSprite(int viewportWidth)
        {
            myViewportWidth = viewportWidth + 150;
        }

        public void LoadContent(ContentManager theContentManager)
        {
            scale = 0.6f;
            position = new Vector2(0, 265);
            if (myCollisionType == collosionType.platform)
            {
                LoadContent(theContentManager, "platform_gold" + randomObjSelector.Next(1, 4).ToString());
            }
        }

        public void Update(GameTime theGameTime)
        {
            Vector2 aDirection = new Vector2(-1, 0);
            Vector2 aSpeed = new Vector2(160, 0);
            if (position.X < -150)
                position.X = myViewportWidth;

            base.Update(theGameTime, aSpeed, aDirection);
        }

        protected override void detectCollision()
        {
        }
    }
}
