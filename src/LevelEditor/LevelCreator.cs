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

            // 1 platform
            SpriteData newSprite = new SpriteData();
            newSprite.position = new Vector2(1000, 400);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            // 2 platform
            newSprite = new SpriteData();
            newSprite.position = new Vector2(1100, 320);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            // 3 platform
            newSprite = new SpriteData();
            newSprite.position = new Vector2(1250, 320);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            // 4 platform
            newSprite = new SpriteData();
            newSprite.position = new Vector2(1350, 240);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            // 5 platform
            newSprite = new SpriteData();
            newSprite.position = new Vector2(1450, 160);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            // 6 platform
            newSprite = new SpriteData();
            newSprite.position = new Vector2(1600, 220);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            // 7 platform
            newSprite = new SpriteData();
            newSprite.position = new Vector2(1750, 280);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            // 8 platform
            newSprite = new SpriteData();
            newSprite.position = new Vector2(1900, 340);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            // 1 pinwheel
            newSprite = new SpriteData();
            newSprite.obstacleType = collisionType.OBSTACLE;
            newSprite.textureName = "pin wheel";
            newSprite.scale = 0.4f;
            newSprite.position = new Vector2(2080, 350);
            newSprite.rotationSpeed = 0.02f;
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            newSprite = new SpriteData();
            newSprite.obstacleType = collisionType.OBSTACLE;
            newSprite.textureName = "pin";
            newSprite.scale = 0.4f;
            newSprite.position = new Vector2(2080, 350);
            newSprite.repeat = false;
            levelData.sprites.Add(newSprite);

            return levelData;
        }
    }
}
