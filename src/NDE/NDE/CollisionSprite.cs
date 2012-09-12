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

        public void LoadContent(ContentManager theContentManager)
        {
            scale = 0.6f;
            position = new Vector2(0, 265);
            if (myCollisionType == collosionType.platform)
            {
                LoadContent(theContentManager, "platform_gold" + randomObjSelector.Next(1, 4).ToString());
            }
        }
    }
}
