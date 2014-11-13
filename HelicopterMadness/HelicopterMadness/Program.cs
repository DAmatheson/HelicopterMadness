namespace HelicopterMadness
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HelicopterGame game = new HelicopterGame())
            {
                game.Run();
            }
        }
    }
#endif
}

