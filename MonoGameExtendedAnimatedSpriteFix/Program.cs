namespace AnimatedSpriteFix
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using var game = new AnimatedSpriteFixGame();

            game.Run();
        }
    }
}