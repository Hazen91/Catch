﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private float speed;
        private float speedX;

        public Faller(Texture2D texture)
        {
            random = new Random();
            this.texture = texture;
            position = new Vector2(random.Next(0, Game1.windowWidth-texture.Width+1), 0);
            hitbox = new Rectangle((int) position.X, (int) position.Y, texture.Width, texture.Height);
            random = new Random();
            speed = random.Next(100, 300);
            random = new Random();
            speedX = random.Next(-300, 301);
            Debug.WriteLine(speed);
        }

        public void update(GameTime gameTime)
        {
            position.Y += speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            position.X += speedX * (float) gameTime.ElapsedGameTime.TotalSeconds;
            hitbox.Location = new Point((int) position.X, (int) position.Y);
            if (position.X <= 0)
            {
                speedX *= -1;
            }
            if (position.X >= Game1.windowWidth - texture.Width)
            {
                speedX *= -1;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
