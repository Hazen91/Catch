using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catch
{
    class Catcher
    {
        Vector2 position;
        Texture2D texture;
        public Rectangle hitbox;
        private float velocity = 500;
        private float maxVelocity = 1200;

        public float Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public Catcher(Texture2D texture)
        {
            this.texture = texture;
            position = new Vector2(0, Game1.windowHeight-texture.Height);
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (velocity >= maxVelocity)
            { velocity = maxVelocity; }

            if (keyState.IsKeyDown(Keys.Left) && position.X > 0)
            {
                position.X -= velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keyState.IsKeyDown(Keys.Right) && position.X <= Game1.windowWidth - texture.Width)
            {
                position.X += velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            hitbox.Location = new Point((int) position.X, (int) position.Y);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
