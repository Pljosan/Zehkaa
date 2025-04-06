using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zehkaa.SpriteClasses
{
    internal class ZehkaaSprite
    {
        private Texture2D texture;
        private Vector2 position;
        private float speed;
        private SpriteEffects spriteEffect;
        private Vector2 velocity;

        private float gravity = 9.8f; //TODO: move this upwards so it's global

        private Rectangle boundingRectangle;

        public ZehkaaSprite(Texture2D texture, Vector2 startPosition, float movementSpeed)
        {
            this.texture = texture;
            this.position = startPosition;
            this.spriteEffect = SpriteEffects.None;
            this.speed = movementSpeed;
            this.velocity = Vector2.Zero;
        }

        public void Update(GameTime gameTime, Rectangle groundRectangle)
        {
            // time elapse since last Update call
            float updatedBallSpeed = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Left))
            {
                position.X -= updatedBallSpeed;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                position.X += updatedBallSpeed;
                spriteEffect = SpriteEffects.None;
            }

            if (kstate.IsKeyDown(Keys.Up))
            {
                position.Y -= updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                position.Y += updatedBallSpeed;
            }
            
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += velocity.Y;

            if (boundingRectangle.Intersects(groundRectangle))
            {
                position.Y = groundRectangle.Top - texture.Height + 1; // + 1 is a hack to stop gravity from bouncing all the time
                velocity.Y = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture,
                position,
                null,
                Color.White,
                0f,
                //new Vector2(texture.Width / 2, texture.Height / 2),
                Vector2.Zero,
                Vector2.One,
                spriteEffect,
                0f
            );
        }
    }
}
