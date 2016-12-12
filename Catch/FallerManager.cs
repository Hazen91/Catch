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
        Texture2D fallerTexture;

        public FallerManager(ContentManager content)
        {
            this.content = content;
            fallerList = new List<Faller>();
            fallerTexture = content.Load<Texture2D>("star.png");
        }

        public void update(GameTime gameTime)
        {
            var timer = (float) gameTime.ElapsedGameTime.TotalSeconds;

            remainingDelay -= timer;

            if (remainingDelay <= 0)
            {
                fallerList.Add(new Faller(fallerTexture));
                
                remainingDelay = delay;
            }

            foreach (Faller faller in fallerList)
            {
                faller.update();
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
