using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Zehkaa.SpriteClasses;
using Zehkaa.TerrainClasses;

namespace Zehkaa
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private ZehkaaSprite zehkaaHimself;
        private GroundSprite groundSprite;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D zehkaaTexture = Content.Load<Texture2D>("elephantGoodSize");
            Vector2 zehkaaStartPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                           _graphics.PreferredBackBufferHeight / 2);
            float zehkaaSpeed = 200f;

            zehkaaHimself = new ZehkaaSprite(zehkaaTexture, zehkaaStartPosition, zehkaaSpeed);

            groundSprite = new GroundSprite(Content.Load<Texture2D>("grassGoodSize"), _graphics);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            zehkaaHimself.Update(gameTime, groundSprite.GetBoundingBox());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            zehkaaHimself.Draw(_spriteBatch);
            groundSprite.Draw(_spriteBatch, _graphics);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
