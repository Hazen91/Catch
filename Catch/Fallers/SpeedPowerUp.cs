using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Catch.Fallers
{
    class SpeedPowerUp : Faller
    {
        public SpeedPowerUp(Texture2D texture) : base(texture)
        { 
            
        }

        public override void update(GameTime gameTime)
        {
            base.update(gameTime);
        }
    }
}
