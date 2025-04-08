using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;

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

            if (IsMoveLeft(kstate))
            {
                position.X -= updatedBallSpeed;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            if (IsMoveRight(kstate))
            {
                position.X += updatedBallSpeed;
                spriteEffect = SpriteEffects.None;
            }

            if (IsJump(kstate))
            {
                position.Y -= 2 * updatedBallSpeed;
            }

            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += velocity.Y;

            if (boundingRectangle.Intersects(groundRectangle))
            {
                if (IsHoldLookDown(kstate))
                {
                    texture = textureDown;
                }
                else if (IsLetGoLookDown(kstate) || IsAnyOtherButtonPressed(kstate))
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

        private bool IsMoveLeft(KeyboardState kstate)
        {
            return kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.1f);
        }

        private bool IsMoveRight(KeyboardState kstate)
        {
            return kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.1f);
        }

        private bool IsLookUp(KeyboardState kstate)
        {
            //TODO: Use this as looking up
            return kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.1f);
        }

        private bool IsHoldLookDown(KeyboardState kstate)
        {
            return kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.1f);
        }

        private bool IsLetGoLookDown(KeyboardState kstate)
        {
            return kstate.IsKeyUp(Keys.Down) && kstate.IsKeyUp(Keys.S) && (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y >= -0.1f);
        }

        private bool IsJump(KeyboardState kstate)
        {
            return kstate.IsKeyDown(Keys.Space) || ButtonState.Pressed.Equals(GamePad.GetState(PlayerIndex.One).Buttons.A);
        }

        private bool IsAnyOtherButtonPressed(KeyboardState kstate)
        {
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            Keys pressedKey = Array.Find(keys, key => !key.Equals(Keys.Down) && !key.Equals(Keys.S));


            Debug.WriteLine("Pressed: " + pressedKey);

            return pressedKey != null;
        }

    }
}
