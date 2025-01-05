using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSpriteFix
{
    public class CustomGraphicsDeviceManager : GraphicsDeviceManager
    {
        public bool IsVerticalSync { get; set; }

        public CustomGraphicsDeviceManager(Game game)
            : base(game)
        {
            PreparingDeviceSettings += OnPreparingDeviceSettings;
        }

        protected void OnPreparingDeviceSettings(Object sender, PreparingDeviceSettingsEventArgs args)
        {
            if (args.GraphicsDeviceInformation.PresentationParameters.IsFullScreen)
            {
                args.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = IsVerticalSync
                    ? PresentInterval.Default
                    : PresentInterval.Immediate;
            }

            args.GraphicsDeviceInformation.GraphicsProfile = GraphicsProfile.HiDef;
        }

        public void SetDisplayMode(DisplaySettings displaySettings)
        {
            PreferredBackBufferWidth = displaySettings.DisplayDimension.Width;
            PreferredBackBufferHeight = displaySettings.DisplayDimension.Height;

            IsFullScreen = displaySettings.IsFullScreen;

            HardwareModeSwitch = displaySettings.IsFullScreen && !displaySettings.IsBorderlessWindowed;
            IsVerticalSync = displaySettings.IsVerticalSync;

            ApplyChanges();
        }
    }
}
