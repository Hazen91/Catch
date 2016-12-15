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

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, VectorToAngle(Velocity * Direction), new Vector2(Texture.Width, Texture.Height), 1f, SpriteEffects.None, 1);
            //spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
