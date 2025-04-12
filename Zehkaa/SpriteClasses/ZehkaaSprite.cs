using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zehkaa.TerrainClasses;
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
        private bool isOnGround = false;

        private float jumpStrength = 7f;

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

        //TODO: add this to array of world sprites?
        public void Update(GameTime gameTime, Rectangle groundRectangle, PlatformSprite platform)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float updatedSpeed = speed * delta;
            var kstate = Keyboard.GetState();

            velocity.X = 0;

            HandleMovement(kstate, updatedSpeed);

            HandleGravity(delta);

            position += velocity;

            HandleGroundTouch(kstate, groundRectangle);
            HandlePlatformTouch(kstate, platform);
        }

        private void HandlePlatformTouch(KeyboardState kstate, PlatformSprite platformSprite)
        {

            Rectangle platformRectangle = platformSprite.GetBoundingBox();
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            
            if (CollisionDetectionUtils.IsCollisionFromTop(boundingRectangle, platformRectangle)) {
                isOnGround = true;
                velocity.Y = 0;

                position.X += platformSprite.GetVelocity().X;
                position.Y = platformRectangle.Top - texture.Height;
            }

            if (CollisionDetectionUtils.IsCollisionFromBottom(boundingRectangle, platformRectangle))
            {
                position.Y = platformRectangle.Bottom + ConstantsUtil.DELTA;
                velocity.Y = 0;
            }

            if (CollisionDetectionUtils.IsCollisionFromLeft(boundingRectangle, platformRectangle))
            {
                velocity.X = 0;
                position.X = platformRectangle.Left - texture.Width - ConstantsUtil.DELTA;
            }

            if (CollisionDetectionUtils.IsCollisionFromRight(boundingRectangle, platformRectangle))
            {
                velocity.X = 0;
                position.X = platformRectangle.Right + ConstantsUtil.DELTA;
            }
        }

        private void HandleGravity(float delta)
        {
            if (!isOnGround)
                velocity.Y += ConstantsUtil.GRAVITY * delta;
        }

        private void HandleGroundTouch(KeyboardState kstate, Rectangle groundRectangle)
        {
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (boundingRectangle.Bottom >= groundRectangle.Top)
            {
                if (ButtonPressUtils.IsHoldLookDown(kstate))
                {
                    texture = textureDown;
                }
                else if (ButtonPressUtils.IsLetGoLookDown(kstate))
                {
                    texture = textureUp;
                }

                position.Y = groundRectangle.Top - texture.Height;
                velocity.Y = 0;
                isOnGround = true;
            }
            else
            {
                isOnGround = false;
            }
        }

        private void HandleMovement(KeyboardState kstate, float updatedSpeed)
        {
            if (ButtonPressUtils.IsMoveLeft(kstate))
            {
                velocity.X = -updatedSpeed;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            if (ButtonPressUtils.IsMoveRight(kstate))
            {
                velocity.X = updatedSpeed;
                spriteEffect = SpriteEffects.None;
            }

            if (ButtonPressUtils.IsJump(kstate) && isOnGround)
            {
                velocity.Y -= jumpStrength;
                isOnGround = false;
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
