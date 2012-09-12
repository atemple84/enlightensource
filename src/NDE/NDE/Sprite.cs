using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NDE
{
    class Sprite
    {
        protected const int MOVE_UP = -1;
        protected int MOVE_DOWN = 1;
        protected const int MOVE_LEFT = -1;
        protected const int MOVE_RIGHT = 1;

        // Sprite properties
        protected Texture2D myTexture;
        public Vector2 position = new Vector2(0, 0);
        public float scale = 1.0f;
        public float rotation = 0f;
        public Color color = Color.White;
        protected Rectangle mySize;

        public void LoadContent(ContentManager theContentManager, string spriteName)
        {
            myTexture = theContentManager.Load<Texture2D>(spriteName);
            mySize = new Rectangle(0, 0, (int)(myTexture.Width), (int)(myTexture.Height));
        }

        public Texture2D getTexture()
        {
            return myTexture;
        }

        public Rectangle getArea()
        {
            return mySize;
        }

        protected void Update(GameTime gametime, Vector2 speed, Vector2 direction)
        {
            position += direction * speed * (float)gametime.ElapsedGameTime.TotalSeconds;
        }
    }
}
