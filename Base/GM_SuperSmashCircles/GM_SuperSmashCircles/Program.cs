using System;
using System.Diagnostics;

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
            using (var game = new Game1())
                game.Run();
        }
    }
}
