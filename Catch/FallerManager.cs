using Catch.Fallers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Catch
{
    class FallerManager
    {
        ContentManager content;
        public List<Faller> fallerList;
        private float delay = 1;
        private float remainingDelay = 3;
        Texture2D starTexture;
        Texture2D speedPowerUpTexture;
        Texture2D cometTexture;
        static Random random;
        private Texture2D speedPowerDownTexture;

        public FallerManager(ContentManager content)
        {
            this.content = content;
            fallerList = new List<Faller>();
            starTexture = content.Load<Texture2D>("starHitbox.png");
            speedPowerUpTexture = content.Load<Texture2D>("SpeedPowerUp.png");
            speedPowerDownTexture = content.Load<Texture2D>("SpeedPowerDown.png");
            cometTexture = content.Load<Texture2D>("star.png");
        }

        public void update(GameTime gameTime)
        {
            var timer = (float) gameTime.ElapsedGameTime.TotalSeconds;

            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                random = new Random();
                int decider = random.Next(1,13);
                if (decider == 1)
                {
                    fallerList.Add(new SpeedPowerUp(speedPowerUpTexture));
                }
                else if (decider == 2)
                {
                    fallerList.Add(new SpeedPowerDown(speedPowerDownTexture));
                }
                else
                {
                    fallerList.Add(new Star(starTexture, cometTexture));
                }
                random = new Random();
                delay = randomFloat(1.5f,4f,1);
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

        private float randomFloat(float min, float max, int floatingPoints)
        {
            random = new Random();
            return (float) Math.Round((random.NextDouble() * (max - min) + min), floatingPoints);
        }
    }
}
