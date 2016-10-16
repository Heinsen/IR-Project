using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheApplication.Controller;
using TheApplication.View;

namespace TheApplication
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

            CreateIndexController CreateIndexController = new CreateIndexController();
            CreateIndexView CreateIndexView = new CreateIndexView();
            CreateIndexView.SetCreateIndexController(CreateIndexController);
            Application.Run(CreateIndexView);
        }
    }
}
