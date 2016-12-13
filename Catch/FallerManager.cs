using Catch.Fallers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catch
{
    class FallerManager
    {
        ContentManager content;
        public List<Faller> fallerList;
        private const float delay = 3;
        private float remainingDelay = delay;
        Texture2D starTexture;
        Texture2D speedPowerUpTexture;
        Random random;

        public FallerManager(ContentManager content)
        {
            this.content = content;
            fallerList = new List<Faller>();
            starTexture = content.Load<Texture2D>("star.png");
            speedPowerUpTexture = content.Load<Texture2D>("SpeedPowerUp.png");
        }

        public void update(GameTime gameTime)
        {
            var timer = (float) gameTime.ElapsedGameTime.TotalSeconds;

            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                random = new Random();
                int decider = random.Next(1,5);
                if (decider != 1)
                { fallerList.Add(new Star(starTexture)); }
                else
                { fallerList.Add(new SpeedPowerUp(speedPowerUpTexture)); }
                remainingDelay = delay;
            }

            foreach (Faller faller in fallerList)
            {
                faller.update(gameTime);
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Faller faller in fallerList)
            {
                faller.draw(spriteBatch);
            }
        }
    }
}
