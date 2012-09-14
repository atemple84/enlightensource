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
        protected Texture2D myTexture;
        public Vector2 position = new Vector2(0, 0);
        public float scale = 1.0f;
        public float rotation = 0f;
        public Color color = Color.White;
        protected Rectangle mySize;
        public bool repeat = true;

        // Movement properties
        protected int myViewportWidth;
        protected Vector2 mySpeed = Vector2.Zero;
        protected Vector2 myDirection = Vector2.Zero;

        public void LoadContent(ContentManager theContentManager, string spriteName)
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

        public virtual void Update(GameTime gametime)
        {
            position += myDirection * mySpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (repeat && position.X < -150)
            {
                position.X = myViewportWidth;
                generateNewTexture();
            }
            detectCollision();
        }

        protected virtual void generateNewTexture() {}
        protected abstract void detectCollision();
    }
}
