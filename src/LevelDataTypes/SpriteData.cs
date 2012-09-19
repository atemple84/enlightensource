using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace LevelDataTypes
{
    public enum collisionType
    {
        PLATFORM,
        OBSTACLE
    }

    public class SpriteData
    {
        public string textureName = "platform_gold1";
        public collisionType obstacleType = collisionType.PLATFORM;
        public float scale = 0.6f;
        public Color color = Color.White;
        public Vector2 position = new Vector2(0, 265);
    }
}
