using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NeuroxPC {
    static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Window w = new Window();
            
            Application.Run(w);
        }
    }
}
