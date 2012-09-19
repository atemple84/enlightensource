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
            ExampleData.sprites.Add(new SpriteData());
            SpriteData dummyObstacle = new SpriteData();
            dummyObstacle.obstacleType = collisionType.OBSTACLE;
            dummyObstacle.textureName = "LOL";
            dummyObstacle.scale = 0.25f;
            dummyObstacle.color = Color.Red;
            dummyObstacle.position = new Vector2(175, 165);
            ExampleData.sprites.Add(dummyObstacle);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("../../../NDE/NDEContent/levels/dummyLevel.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, ExampleData, null);
            }
        }
    }
}
