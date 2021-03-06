﻿using Catch.Fallers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Catch.Fallers
{
    class Star : Faller
    {
        Texture2D comet;
        public Star(Texture2D texture, Texture2D comet) : base(texture)
        {
            this.comet = comet;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), null, Color.White * 0.25f, VectorToAngle(Velocity * Direction), new Vector2(Texture.Width, Texture.Height), 1f, SpriteEffects.FlipVertically, 1);
            spriteBatch.Draw(comet, new Vector2(Position.X, Position.Y), null, Color.White*0.95f, VectorToAngle(Velocity * Direction), new Vector2(Texture.Width, Texture.Height), 1f, SpriteEffects.FlipVertically, 1);
         
        }
    }
}
