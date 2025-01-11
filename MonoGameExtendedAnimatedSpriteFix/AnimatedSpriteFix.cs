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

        private AnimatedSprite _purpleWormAnimatedSprite;
        private AnimatedSprite _greenTentacleAnimatedSprite;
        private KeyboardState _keyboardState;

        public AnimatedSpriteFixGame()
        {
            CustomGraphicsDeviceManager = new CustomGraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            
            Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            InitializeDisplaySettings();

            base.Initialize();
        }

        private void InitializeDisplaySettings()
        {
            var isFullScreen = false;
            var isVerticalSync = true;
            var isBorderlessWindowed = true;

            var displayDimensions = new DisplayDimension(
                500,
                500,
                0);

            var displaySettings = new DisplaySettings(
                displayDimensions,
                isFullScreen,
                isVerticalSync,
                isBorderlessWindowed
            );

            CustomGraphicsDeviceManager.SetDisplayMode(displaySettings);
        }

        protected override void LoadContent()
        {
            var spriteSheetTexture = Content.Load<Texture2D>("Animations/SpriteSheetTest");
            var spriteSheetAtlas = Texture2DAtlas.Create("SpriteSheetTestAtlas", spriteSheetTexture, 64, 64);
            var spriteSheet = new SpriteSheet("SpriteSheetTest", spriteSheetAtlas);
        
            spriteSheet.DefineAnimation("purpleworm", b =>
            {
                b.IsLooping(true);
                b.AddFrame(0, TimeSpan.FromSeconds(3));
                b.AddFrame(1, TimeSpan.FromSeconds(3));
            });
            
            spriteSheet.DefineAnimation("greententacle", b =>
            {
                b.IsLooping(true);
                b.AddFrame(2, TimeSpan.FromSeconds(3));
                b.AddFrame(3, TimeSpan.FromSeconds(3));
            });
            
            _purpleWormAnimatedSprite = new AnimatedSprite(spriteSheet);
            _purpleWormAnimatedSprite.SetAnimation("purpleworm");
            _purpleWormAnimatedSprite.Controller.Play();
            
            _greenTentacleAnimatedSprite = new AnimatedSprite(spriteSheet);
            _greenTentacleAnimatedSprite.SetAnimation("greententacle");
            _greenTentacleAnimatedSprite.Controller.Play();

            _keyboardState = new KeyboardState();
        }

        protected override void Update(GameTime gameTime)
        {
            _purpleWormAnimatedSprite.Update(gameTime); 
            _greenTentacleAnimatedSprite.Update(gameTime);
            
            if (_keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            _purpleWormAnimatedSprite.Draw(_spriteBatch, new Vector2(100f, 100f), 0f, Vector2.One);
            _greenTentacleAnimatedSprite.Draw(_spriteBatch, new Vector2(100f, 200f), 0f, Vector2.One);
            _spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
