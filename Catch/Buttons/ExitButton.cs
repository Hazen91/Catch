using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Catch.Buttons
{
    class ExitButton : Button
    {
        public ExitButton(ContentManager content, Texture2D texture, Vector2 position) : base(content, texture, position)
        {
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
        }

        public override void click()
        {
            throw new NotImplementedException();
        }

        public void click(Game1 game1)
        {
            game1.quit();
        }
    }
}
