using System.Threading;
using FrigidRogue.MonoGame.Core.Components;
using FrigidRogue.MonoGame.Core.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace AnimatedSpriteFix
{
    public class AnimatedSpriteFixGame : Game
    {
        private SpriteBatch _spriteBatch;
        public CustomGraphicsDeviceManager CustomGraphicsDeviceManager { get; }

        private GameTimeService _gameTimeService;
        private AnimatedSprite _purpleWormAnimatedSprite;
        private AnimatedSprite _greenTentacleAnimatedSprite;

        public AnimatedSpriteFixGame()
        {
            CustomGraphicsDeviceManager = new CustomGraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            
            Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            InitializeDisplaySettings();

            _gameTimeService = new GameTimeService(new StopwatchProvider());

            base.Initialize();
        }

        private void InitializeDisplaySettings()
        {
            var isFullScreen = false;
            var isVerticalSync = true;
            var isBorderlessWindowed = true;

            var displayDimensions = new DisplayDimension(
                1024,
                768,
                0);

            var displaySettings = new DisplaySettings(
                displayDimensions,
                isFullScreen,
                isVerticalSync,
                isBorderlessWindowed
            );

            CustomGraphicsDeviceManager.SetDisplayMode(displaySettings);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            var spriteSheetTexture = Content.Load<Texture2D>("Animations/SpriteSheetTest");
            var spriteSheetAtlas = Texture2DAtlas.Create("SpriteSheetTestAtlas", spriteSheetTexture, 32, 32);
            var spriteSheet = new SpriteSheet("SpriteSheetTest", spriteSheetAtlas);
        
            spriteSheet.DefineAnimation("purpleworm", b =>
            {
                b.IsLooping(true);
                b.AddFrame(0, TimeSpan.FromSeconds(5));
                b.AddFrame(1, TimeSpan.FromSeconds(5));
            });
            
            spriteSheet.DefineAnimation("greententacle", b =>
            {
                b.IsLooping(true);
                b.AddFrame(0, TimeSpan.FromSeconds(5));
                b.AddFrame(1, TimeSpan.FromSeconds(5));
            });
            
            _purpleWormAnimatedSprite = new AnimatedSprite(spriteSheet);
            _purpleWormAnimatedSprite.SetAnimation("purpleworm");
            _purpleWormAnimatedSprite.Controller.Play();
            
            _greenTentacleAnimatedSprite = new AnimatedSprite(spriteSheet);
            _greenTentacleAnimatedSprite.SetAnimation("greententacle");
            _greenTentacleAnimatedSprite.Controller.Play();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _gameTimeService.Update(gameTime);
            
            _purpleWormAnimatedSprite.Update(gameTime); 
            _greenTentacleAnimatedSprite.Update(gameTime);

            var keyboardState = new KeyboardState();
            
            keyboardState.IsKeyDown(Keys.Escape);
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!IsActive)
                return;
            
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            _purpleWormAnimatedSprite.Draw(_spriteBatch, new Vector2(0f, 0f), 0f, Vector2.One);
            _greenTentacleAnimatedSprite.Draw(_spriteBatch, new Vector2(0f, 32f), 0f, Vector2.One);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
