using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace Zehkaa.TerrainClasses
{
    internal class PlatformSprite : TerrainSprite
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private float movementSpeed = 100f;
        private float movementI = 1;

        public PlatformSprite(Texture2D texture, Vector2 startPosition, GraphicsDeviceManager _graphics) : base(new Rectangle((int)startPosition.X, (int)startPosition.Y, texture.Width, texture.Height))
        {
            this.texture = texture;
            this.position = startPosition;
            this.velocity = Vector2.Zero;
        }

        public new Rectangle GetBoundingBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = movementSpeed * delta;

            velocity.X = 0;

            if (movementI > 0 && movementI < 100)
            {
                velocity.X += speed;
                movementI++;
            }

            if (movementI == 100)
            {
                movementI = -1;
            }

            if (movementI < 0 && movementI > -100)
            {
                velocity.X -= speed;
                movementI--;
            }

            if (movementI == -100)
            {
                movementI = 1;
            }

            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDeviceManager _graphics)
        {
            spriteBatch.Draw(
                texture,
                position,
                null,
                Color.White,
                0f,
                new Vector2(0, 0),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
        }

        public Vector2 GetVelocity()
        {
            return this.velocity;
        }
    }
}
