using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace NDE
{
    class PlayerSprite : Sprite
    {
        public void LoadContent(ContentManager theContentManager)
        {
            scale = 0.15f;
            LoadContent(theContentManager, "little guy");
        }
    }
}
