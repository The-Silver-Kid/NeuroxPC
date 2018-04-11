using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace NeuroxPC {
    static class Program {

        //internal static bool elevated = false;
        internal static Window w;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            /*
            elevated = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            if (elevated)
                Console.WriteLine("\"Hmm, how interesting.\" The Shadow Mage thought.");*/
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            w = new Window();

            Application.Run(w);
        }
    }
}
