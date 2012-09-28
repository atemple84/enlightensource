using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework;

using LevelDataTypes;

namespace LevelEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            LevelData ExampleData = new LevelData();

            // Create background(s)
            SpriteData dummyBackground = new SpriteData();
            dummyBackground.obstacleType = collisionType.BACKGROUND;
            dummyBackground.textureName = "background_blue";
            dummyBackground.scale = 0.26666666f;
            dummyBackground.color = Color.White;
            dummyBackground.position = new Vector2(0, 0);
            dummyBackground.gameSpeed = Vector2.Zero;
            ExampleData.sprites.Add(dummyBackground);

            
            SpriteData dummySunGlow = new SpriteData();
            dummySunGlow.obstacleType = collisionType.BACKGROUND;
            dummySunGlow.textureName = "sun glow";
            dummySunGlow.scale = 0.2f;
            dummySunGlow.color = Color.White;
            dummySunGlow.position = new Vector2(600, 5);
            dummySunGlow.gameSpeed = new Vector2(80, 0);
            ExampleData.sprites.Add(dummySunGlow);
            

            SpriteData dummySun = new SpriteData();
            dummySun.obstacleType = collisionType.BACKGROUND;
            dummySun.textureName = "sun";
            dummySun.scale = 0.2f;
            dummySun.color = Color.White;
            dummySun.position = new Vector2(600, 5);
            dummySun.gameSpeed = new Vector2(80, 0);
            ExampleData.sprites.Add(dummySun);

            SpriteData dummyCloud = new SpriteData();
            dummyCloud.obstacleType = collisionType.BACKGROUND;
            dummyCloud.textureName = "cloud 3";
            dummyCloud.scale = 0.2f;
            dummyCloud.color = Color.White;
            dummyCloud.position = new Vector2(600, 60);
            dummyCloud.gameSpeed = new Vector2(100, 0);
            ExampleData.sprites.Add(dummyCloud);

            SpriteData dummyTopCloud1 = new SpriteData();
            dummyTopCloud1.obstacleType = collisionType.BACKGROUND;
            dummyTopCloud1.textureName = "top cloud";
            dummyTopCloud1.scale = 0.47f;
            dummyTopCloud1.color = Color.White;
            dummyTopCloud1.position = new Vector2(0, 415);
            dummyTopCloud1.gameSpeed = new Vector2(125, 0);
            ExampleData.sprites.Add(dummyTopCloud1);

            SpriteData dummyTopCloud2 = new SpriteData();
            dummyTopCloud2.obstacleType = collisionType.BACKGROUND;
            dummyTopCloud2.textureName = "top cloud";
            dummyTopCloud2.scale = 0.47f;
            dummyTopCloud2.color = Color.White;
            dummyTopCloud2.position = new Vector2(805, 415);
            dummyTopCloud2.gameSpeed = new Vector2(125, 0);
            ExampleData.sprites.Add(dummyTopCloud2);

            /*
            SpriteData backbackground = new SpriteData();
            backbackground.obstacleType = collisionType.BACKGROUND;
            backbackground.textureName = "backbackground";
            backbackground.scale = 0.4f;
            backbackground.color = Color.White;
            backbackground.position = new Vector2(0, 150);
            backbackground.gameSpeed = new Vector2(200, 0);
            ExampleData.sprites.Add(backbackground);

            SpriteData backbackground2 = new SpriteData();
            backbackground2.obstacleType = collisionType.BACKGROUND;
            backbackground2.textureName = "backbackground";
            backbackground2.scale = 0.4f;
            backbackground2.color = Color.White;
            backbackground2.position = new Vector2(1125, 150);
            backbackground2.gameSpeed = new Vector2(200, 0);
            ExampleData.sprites.Add(backbackground2);
            */

            // Create platform(s)
            SpriteData dummyPlatform = new SpriteData();
            dummyPlatform.scale = 0.3f;
            ExampleData.sprites.Add(dummyPlatform);

            // Create obstacle(s)
            SpriteData dummyObstacle = new SpriteData();
            dummyObstacle.obstacleType = collisionType.OBSTACLE;
            dummyObstacle.textureName = "LOL";
            dummyObstacle.scale = 0.2f;
            dummyObstacle.color = Color.Red;
            dummyObstacle.position = new Vector2(175, 165);
            ExampleData.sprites.Add(dummyObstacle);

            // Set up XML writer
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            // Export directly to NDEContent directory for immediate deserialization
            using (XmlWriter writer = XmlWriter.Create("../../../NDE/NDEContent/levels/dummyLevel.xml", settings))
                IntermediateSerializer.Serialize(writer, ExampleData, null);
        }
    }
}
