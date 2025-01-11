using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSpriteFix
{
    public class CustomGraphicsDeviceManager : GraphicsDeviceManager
    {
        public CustomGraphicsDeviceManager(Game game)
            : base(game)
        {
            PreparingDeviceSettings += OnPreparingDeviceSettings;
        }

        protected void OnPreparingDeviceSettings(Object sender, PreparingDeviceSettingsEventArgs args)
        {
            args.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.Reach;
        }

        public void SetDisplayMode(DisplaySettings displaySettings)
        {
            PreferredBackBufferWidth = displaySettings.DisplayDimension.Width;
            PreferredBackBufferHeight = displaySettings.DisplayDimension.Height;

            IsFullScreen = displaySettings.IsFullScreen;

            HardwareModeSwitch = displaySettings.IsFullScreen && !displaySettings.IsBorderlessWindowed;

            ApplyChanges();
        }
    }
}
