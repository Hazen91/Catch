using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catch.Buttons
{
    class Button
    {
        public Texture2D texture;
        public Rectangle hitbox;
        Vector2 position;

        public Button(ContentManager content)
        {
            texture = content.Load<Texture2D>("startButton.png");
            position = new Vector2(0, 0);
            hitbox = new Rectangle(0,0,texture.Width,texture.Height);
        }

        public void click()
        {
            Game1.CurrentState = Game1.gameState.playing;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
