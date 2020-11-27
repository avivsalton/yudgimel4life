﻿using System;
using System.Threading;

namespace SkribblProject
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void runDrawer()
        {
            using (var drawer = new Drawer())
                drawer.Run();
        }

        static void runViewer()
        {
            using (var viewer = new Viewer())
                viewer.Run();
        }

        [STAThread]
        static void Main()
        {
            Thread t1 = new Thread(new ThreadStart(runDrawer));
            Thread t2 = new Thread(new ThreadStart(runViewer));

            t2.Start();
            t1.Start();
        }
    }
#endif
}
