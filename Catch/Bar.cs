using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Catch
{
    class Bar
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        public Bar(Texture2D texture)
        {
            Texture = texture;
            Rows = 2;
            Columns = 5;
            currentFrame = 0;
            totalFrames = Rows * Columns -1;
        }

        public void Update(int velocity)
        {   
            currentFrame = (RoundOff( velocity / 12)/10) -1;
            if (currentFrame > totalFrames)
            {
                currentFrame = 9;
            }
            
        }

        public static int RoundOff(int i)
        {
            return ((int) Math.Round(i / 10.0)) * 10;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int) ((float) currentFrame / (float) Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int) location.X, (int) location.Y, width, height);

            
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            
        }
    }
}

