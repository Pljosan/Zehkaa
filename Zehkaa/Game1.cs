using System.Collections.Generic;
using System.Collections.Immutable;
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
        private PlatformSprite movingPlatform;

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
            Texture2D zehkaaTextureDown = Content.Load<Texture2D>("elephantDown");
            Vector2 zehkaaStartPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                           _graphics.PreferredBackBufferHeight / 2);
            float zehkaaSpeed = 200f;

            zehkaaHimself = new ZehkaaSprite(zehkaaTexture, zehkaaTextureDown, zehkaaStartPosition, zehkaaSpeed);

            groundSprite = new GroundSprite(Content.Load<Texture2D>("grassGoodSize"), _graphics);

            Vector2 platformPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                           groundSprite.GetBoundingBox().Top - 140f);
            movingPlatform = new PlatformSprite(Content.Load<Texture2D>("grassGoodSize"), platformPosition, _graphics);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            LinkedList<TerrainSprite> groundSprites = new LinkedList<TerrainSprite>();
            groundSprites.AddLast(groundSprite);

            LinkedList<TerrainSprite> platformSprites = new LinkedList<TerrainSprite>();
            platformSprites.AddLast(movingPlatform);

            Dictionary<TerrainType, LinkedList<TerrainSprite>> terrainSprites = new Dictionary<TerrainType, LinkedList<TerrainSprite>>();
            terrainSprites.Add(TerrainType.Ground, groundSprites);
            terrainSprites.Add(TerrainType.Platform, platformSprites);

            zehkaaHimself.Update(gameTime, terrainSprites);
            movingPlatform.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            zehkaaHimself.Draw(_spriteBatch);
            groundSprite.Draw(_spriteBatch, _graphics);
            movingPlatform.Draw(_spriteBatch, _graphics);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
