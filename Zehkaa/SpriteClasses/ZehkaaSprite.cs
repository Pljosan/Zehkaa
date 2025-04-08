using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;
using Zehkaa.Utils;

namespace Zehkaa.SpriteClasses
{
    internal class ZehkaaSprite
    {
        private Texture2D texture;
        private Texture2D textureDown;
        private Texture2D textureUp;
        private Vector2 position;
        private float speed;
        private SpriteEffects spriteEffect;
        private Vector2 velocity;

        private float gravity = 9.8f; //TODO: move this upwards so it's global

        private Rectangle boundingRectangle;

        public ZehkaaSprite(Texture2D textureUp, Texture2D textureDown, Vector2 startPosition, float movementSpeed)
        {
            this.textureUp = textureUp;
            this.textureDown = textureDown;
            this.position = startPosition;
            this.spriteEffect = SpriteEffects.None;
            this.speed = movementSpeed;
            this.velocity = Vector2.Zero;

            this.texture = textureUp;
        }

        public void Update(GameTime gameTime, Rectangle groundRectangle)
        {
            // time elapse since last Update call
            float updatedBallSpeed = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var kstate = Keyboard.GetState();

            if (ButtonPressUtils.IsMoveLeft(kstate))
            {
                position.X -= updatedBallSpeed;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            if (ButtonPressUtils.IsMoveRight(kstate))
            {
                position.X += updatedBallSpeed;
                spriteEffect = SpriteEffects.None;
            }

            if (ButtonPressUtils.IsJump(kstate))
            {
                position.Y -= 2 * updatedBallSpeed;
            }

            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += velocity.Y;

            if (boundingRectangle.Intersects(groundRectangle))
            {
                if (ButtonPressUtils.IsHoldLookDown(kstate))
                {
                    texture = textureDown;
                }
                else if (ButtonPressUtils.IsLetGoLookDown(kstate))
                {
                    texture = textureUp;
                }

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
                Vector2.Zero,
                Vector2.One,
                spriteEffect,
                0f
            );
        }

    }
}
