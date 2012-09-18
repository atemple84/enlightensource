using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

using LevelDataTypes;

namespace LevelEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            LevelData ExampleData = new LevelData();
            ExampleData.sprites.Add(new SpriteData());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create("dummyLevel.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, ExampleData, null);
            }
        }
    }
}
