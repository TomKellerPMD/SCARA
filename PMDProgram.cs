﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SCARA_Example
{
    static class PMDProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
