using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catch.Buttons
{
    abstract class Button
    {
        public Texture2D texture;
        public Rectangle hitbox;
        public Vector2 position;

        public Button(ContentManager content, Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public abstract void click();

    }
}
