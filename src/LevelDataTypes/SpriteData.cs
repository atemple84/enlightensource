using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
