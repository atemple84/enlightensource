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

            // 1 background hill
            SpriteData newSprite = new SpriteData();
            newSprite.textureName = "hill back";
            newSprite.position = new Vector2(100, 500);
            newSprite.scale = 0.5f;
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.gameSpeed = new Vector2(50, 0);
            levelData.sprites.Add(newSprite);

            // 2 background hill
            newSprite = new SpriteData();
            newSprite.textureName = "hill back";
            newSprite.position = new Vector2(250, 400);
            newSprite.scale = 0.5f;
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.gameSpeed = new Vector2(50, 0);
            levelData.sprites.Add(newSprite);

            // 3 background hill
            newSprite = new SpriteData();
            newSprite.textureName = "hill back";
            newSprite.position = new Vector2(400, 550);
            newSprite.scale = 0.5f;
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.gameSpeed = new Vector2(50, 0);
            levelData.sprites.Add(newSprite);

            // 4 background hill
            newSprite = new SpriteData();
            newSprite.textureName = "hill back";
            newSprite.position = new Vector2(550, 300);
            newSprite.scale = 0.5f;
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.gameSpeed = new Vector2(50, 0);
            levelData.sprites.Add(newSprite);

            // 5 background hill
            newSprite = new SpriteData();
            newSprite.textureName = "hill back";
            newSprite.position = new Vector2(700, 450);
            newSprite.scale = 0.5f;
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.gameSpeed = new Vector2(50, 0);
            levelData.sprites.Add(newSprite);

            // 6 background hill
            newSprite = new SpriteData();
            newSprite.textureName = "hill back";
            newSprite.position = new Vector2(850, 400);
            newSprite.scale = 0.5f;
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.gameSpeed = new Vector2(50, 0);
            levelData.sprites.Add(newSprite);

            // 7 background hill
            newSprite = new SpriteData();
            newSprite.textureName = "hill back";
            newSprite.position = new Vector2(1000, 550);
            newSprite.scale = 0.5f;
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.gameSpeed = new Vector2(50, 0);
            levelData.sprites.Add(newSprite);

            // Sun
            newSprite = new SpriteData();
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.textureName = "sun";
            newSprite.scale = 0.35f;
            newSprite.color = Color.White;
            newSprite.position = new Vector2(800, 85);
            newSprite.gameSpeed = new Vector2(35, 0);
            levelData.sprites.Add(newSprite);

            // Sun glow
            newSprite = new SpriteData();
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.textureName = "sun glow";
            newSprite.scale = 0.35f;
            newSprite.color = Color.White;
            newSprite.position = new Vector2(800, 85);
            newSprite.gameSpeed = new Vector2(35, 0);
            levelData.sprites.Add(newSprite);

            // 1 rainbow
            newSprite = new SpriteData();
            newSprite.textureName = "rainbow";
            newSprite.position = new Vector2(400, 200);
            newSprite.scale = 0.65f;
            newSprite.obstacleType = collisionType.BACKGROUND;
            newSprite.gameSpeed = new Vector2(0, 0);
            levelData.sprites.Add(newSprite);

            // 1 platform
            newSprite = new SpriteData();
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
