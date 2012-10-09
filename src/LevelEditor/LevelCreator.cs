using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using LevelDataTypes;

namespace LevelEditor
{
    class LevelCreator
    {
        public LevelData buildLevel(int level)
        {
            LevelData levelData = new LevelData();
            levelData.levelName = "level" + level;

            SpriteData newSprite = new SpriteData();
            newSprite.position = new Vector2(1000, 400);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            newSprite = new SpriteData();
            newSprite.position = new Vector2(1100, 320);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            newSprite = new SpriteData();
            newSprite.position = new Vector2(1250, 320);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            newSprite = new SpriteData();
            newSprite.obstacleType = collisionType.OBSTACLE;
            newSprite.textureName = "pin wheel";
            newSprite.scale = 0.4f;
            newSprite.position = new Vector2(1700, 350);
            newSprite.rotationSpeed = 0.02f;
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            newSprite = new SpriteData();
            newSprite.obstacleType = collisionType.OBSTACLE;
            newSprite.textureName = "pin";
            newSprite.scale = 0.4f;
            newSprite.position = new Vector2(1700, 350);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            return levelData;
        }
    }
}
