﻿using System;

namespace Shield.Sandbox.MonoGame.Windows
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
            using (var app = new SandboxBootstrapper())
                app.Run();
        }
    }
}