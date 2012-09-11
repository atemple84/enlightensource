using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NDE
{
    class GhostSprite
    {
        // Sprite properties
        private Texture2D myTexture;
        public Vector2 position = new Vector2(0, 0);
        public float scale = 0.10f;
        public float rotation = 0f;
        public Color color = Color.White;

        public void LoadContent(ContentManager theContentManager, string spriteName)
        {
            myTexture = theContentManager.Load<Texture2D>(spriteName);
        }

        public Texture2D getTexture()
        {
            return myTexture;
        }
    }
}
