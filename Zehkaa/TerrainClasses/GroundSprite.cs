using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zehkaa.TerrainClasses
{ 
    internal class GroundSprite : TerrainSprite
    {
        private Texture2D texture;

        public GroundSprite(Texture2D texture, GraphicsDeviceManager _graphics) : base(new Rectangle(0, _graphics.PreferredBackBufferHeight - texture.Height, _graphics.PreferredBackBufferWidth, texture.Height))
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager _graphics)
        {
            int numberOfTiles = (int)Math.Ceiling((double)_graphics.PreferredBackBufferWidth / texture.Width);

            for (int i = 0; i < numberOfTiles; i++)
            {

                spriteBatch.Draw(
                    texture,
                    new Vector2(i * texture.Width, _graphics.PreferredBackBufferHeight - texture.Height),
                    null,
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
