using System;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Debug.WriteLine("test");
            using (var game = new SSCGame())
                game.Run();
        }
    }
}
