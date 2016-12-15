using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Catch.Fallers
{
    abstract class Faller
    {
        Texture2D texture;
        Vector2 position;
        public Rectangle hitbox;
        static Random random;
        private Vector2 velocity;
        private Vector2 direction;

        public Texture2D Texture
        {
            get
            {
                return texture;
            }

            set
            {
                texture = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Vector2 Velocity
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

        public Vector2 Direction
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
        }

        public Faller(Texture2D texture)
        {
            random = new Random();
            this.texture = texture;
            position = new Vector2(random.Next(0, Game1.windowWidth - texture.Width + 1), 0);
            hitbox = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
            velocity = new Vector2(random.Next(200,500), random.Next(200, 500));
            direction = new Vector2(randomFloat(-1.5f, 1.5f, 2), 1f);
        }

        public virtual void update(GameTime gameTime)
        {
            
            position += direction * velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            direction.Normalize();
            hitbox.Location = new Point((int) position.X, (int) position.Y);
            if (position.X <= 0 || position.X >= Game1.windowWidth - texture.Width)
            {
                velocity.X *= -1;
                
            }
            
           
        }

        private float randomFloat(float min, float max, int floatingPoints)
        {
            random = new Random();
            return (float) Math.Round((random.NextDouble() * (max - min) + min), floatingPoints);
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, VectorToAngle(velocity*direction), new Vector2(texture.Width, texture.Height), 1f, SpriteEffects.FlipVertically, 1);
            //spriteBatch.Draw(texture, position, Color.White);
        }

        public float VectorToAngle(Vector2 vector)
        {
            return (float) Math.Atan2(vector.X, -vector.Y);
        }

    }
}
