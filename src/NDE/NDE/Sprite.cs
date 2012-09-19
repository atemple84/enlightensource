using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NDE
{
    abstract class Sprite
    {
        protected const int MOVE_UP = -1, MOVE_DOWN = 1, MOVE_LEFT = -1, MOVE_RIGHT = 1;

        // Sprite properties
        public Vector2 position = new Vector2(0, 0);
        public float scale = 1.0f;
        public float rotation = 0f;
        public Color color = Color.White;
        public bool repeat = true;
        public Vector2 speed = Vector2.Zero;
        public string spriteName;

        // protected properties
        protected Texture2D myTexture;
        protected int myViewportWidth;
        protected Vector2 myDirection = Vector2.Zero;
        protected Rectangle mySize;

        public void LoadContent(ContentManager theContentManager)
        {
            myTexture = theContentManager.Load<Texture2D>(spriteName);
            mySize = new Rectangle(0, 0, (int)(myTexture.Width * scale), (int)(myTexture.Height * scale));
        }

        public Texture2D getTexture()
        {
            return myTexture;
        }

        public Rectangle getSize()
        {
            return mySize;
        }

        public virtual void Update(GameTime gametime, ContentManager theContent)
        {
            position += myDirection * speed * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (repeat && position.X < -150)
            {
                position.X = myViewportWidth;
                LoadContent(theContent);
            }
            detectCollision();
        }

        protected abstract void detectCollision();
    }
}
