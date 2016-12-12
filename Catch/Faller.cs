using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catch
{
    class Faller
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle hitbox;
        Random random;

        public Faller(Texture2D texture)
        {
            random = new Random();
            this.texture = texture;
            position = new Vector2(random.Next(0, Game1.windowWidth-texture.Width+1), 0);
            hitbox = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
        }

        public void update()
        {
            position.Y += 3;
            hitbox.Location = new Point((int) position.X, (int) position.Y);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
