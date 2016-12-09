using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Musoftware_XPath_Generator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frmmain());
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;       
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Frmmain.browse.Finalize();
        }


        public static IntPtr BrowserParentHandle { get; set; }

        public static int BrowserParentWidth { get; set; }

        public static int BrowserParentHeight { get; set; }
    }
}
