using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catch.Buttons
{
    class StartButton : Button
    {

        public StartButton(ContentManager content, Texture2D texture, Vector2 position) : base(content, texture, position)
        {
            this.texture = texture;
            this.position = position;
            hitbox = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
        }

        /*
        public StartButton(ContentManager content)
        {
            texture = content.Load<Texture2D>("startButton.png");
            position = new Vector2(0, 0);
            hitbox = new Rectangle(0,0,texture.Width,texture.Height);
        }
        */

        public override void click()
        {
            Game1.CurrentState = Game1.gameState.playing;
            Game1.Score = 0;
            Game1.Lifes = 3;
        }

    }
}
