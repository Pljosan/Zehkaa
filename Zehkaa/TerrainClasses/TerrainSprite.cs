using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zehkaa.TerrainClasses
{
    internal class TerrainSprite
    {
        private Rectangle boundingRectangle;

        public TerrainSprite(Rectangle boundingRectangle)
        {
            this.boundingRectangle = boundingRectangle; 
        }

        public Rectangle GetBoundingBox()
        {
            return boundingRectangle;
        }
    }
}
