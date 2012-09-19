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
            dummyBackground.textureName = "background";
            dummyBackground.scale = 1f;
            dummyBackground.color = Color.White;
            dummyBackground.position = new Vector2(0, 0);
            dummyBackground.gameSpeed = Vector2.Zero;
            ExampleData.sprites.Add(dummyBackground);

            // Create platform(s)
            ExampleData.sprites.Add(new SpriteData());

            // Create obstacle(s)
            SpriteData dummyObstacle = new SpriteData();
            dummyObstacle.obstacleType = collisionType.OBSTACLE;
            dummyObstacle.textureName = "LOL";
            dummyObstacle.scale = 0.25f;
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
