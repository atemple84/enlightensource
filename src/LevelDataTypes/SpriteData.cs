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
        OBSTACLE,
        BACKGROUND
    }

    public class SpriteData
    {
        public string textureName = "platform";
        public collisionType obstacleType = collisionType.PLATFORM;
        public float scale = 1f;
        public Color color = Color.White;
        public Vector2 position = new Vector2(0, 265);
        public Vector2 gameSpeed = new Vector2(LevelData.gameSpeed, LevelData.gameSpeed);
        public bool repeat = true;
        public float rotation = 0f;
        public float rotationSpeed = 0f;
    }
}
