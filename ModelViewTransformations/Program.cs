#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace ModelViewTransformations
{
    static class Program
    {
        private static GameApplication game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            game = new GameApplication();
            game.Run();
        }
    }
}
