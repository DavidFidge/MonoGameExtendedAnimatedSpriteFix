namespace AnimatedSpriteFix
{
    public class DisplaySettings
    {
        public DisplayDimension DisplayDimension { get; }
        public bool IsFullScreen { get; }
        public bool IsVerticalSync { get; }
        public bool IsBorderlessWindowed { get; }

        public DisplaySettings(DisplayDimension displayDimension, bool isFullScreen, bool isVerticalSync, bool isBorderlessWindowed)
        {
            DisplayDimension = displayDimension;
            IsFullScreen = isFullScreen;
            IsVerticalSync = isVerticalSync;
            IsBorderlessWindowed = isBorderlessWindowed;
        }
    }
}