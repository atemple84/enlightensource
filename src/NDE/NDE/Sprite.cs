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
        public Vector2 position;
        public float scale;
        public float rotation;
        public float rotationSpeed;
        public Color color;
        public bool repeat;
        protected Vector2 mySpeed;
        private string mySpriteName;
        protected Vector2 myCenter;

        // protected properties
        protected Texture2D myTexture;
        protected int myViewportWidth;
        protected Vector2 myDirection = Vector2.Zero;

        public Sprite(string spriteName)
        {
            mySpriteName = spriteName;
        }

        public virtual void LoadContent(ContentManager theContentManager)
        {
            myTexture = theContentManager.Load<Texture2D>(mySpriteName);
            int originX = myTexture.Width / 2;
            int originY = myTexture.Height / 2;
            myCenter = new Vector2(originX, originY);
            position = Vector2.Zero;
            rotation = 0f;
            rotationSpeed = 0f;
            scale = 1f;
            color = Color.White;
            repeat = true;
            mySpeed = Vector2.Zero;
        }

        public Rectangle boundingBox
        {
            get
            {
                return new Rectangle((int)(position.X - (myCenter.X * scale)), (int)(position.Y - (myCenter.Y * scale)), (int)(myTexture.Width * scale), (int)(myTexture.Height * scale));
            }
        }

        public Texture2D getTexture()
        {
            return myTexture;
        }

        public Vector2 center()
        {
            return myCenter;
        }

        public virtual void Update(GameTime gametime, ContentManager theContent)
        {
            position += myDirection * mySpeed * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (repeat && boundingBox.Right < 0)
                position.X = myViewportWidth + (myCenter.X * scale);

            rotation += rotationSpeed;
            rotation = rotation % (MathHelper.Pi * 2);
            detectCollision();
        }

        protected abstract void detectCollision();
    }
}
