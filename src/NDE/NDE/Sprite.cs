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
        public float rotationSpeed = 0f;
        public Color color = Color.White;
        public bool repeat = true;
        protected Vector2 mySpeed = Vector2.Zero;
        public string spriteName;
        private Vector2 myCenter;

        // protected properties
        protected Texture2D myTexture;
        protected int myViewportWidth;
        protected Vector2 myDirection = Vector2.Zero;
        protected Rectangle mySize;

        public void LoadContent(ContentManager theContentManager)
        {
            myTexture = theContentManager.Load<Texture2D>(spriteName);
            mySize = new Rectangle(0, 0, (int)(myTexture.Width * scale), (int)(myTexture.Height * scale));
            int originX = myTexture.Width / 2;
            int originY = myTexture.Height / 2;
            myCenter = new Vector2(originX, originY);
        }

        public Texture2D getTexture()
        {
            return myTexture;
        }

        public Rectangle getSize()
        {
            return mySize;
        }

        public Vector2 center()
        {
            return myCenter;
        }

        public virtual void Update(GameTime gametime, ContentManager theContent)
        {
            position += myDirection * mySpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (repeat && (position.X + (mySize.Width/2)) < 0)
                position.X = myViewportWidth + (mySize.Width/2);

            rotation += rotationSpeed;
            rotation = rotation % (MathHelper.Pi * 2);
            detectCollision();
        }

        protected abstract void detectCollision();
    }
}
