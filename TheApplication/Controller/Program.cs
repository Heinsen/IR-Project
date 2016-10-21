using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheApplication.Controller;
using TheApplication.View;
using TheApplication.Model;

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

            //Giving CreateIndexController the chosen ILuceneHelperImplementation and IQueryParserImplemenation
            LuceneHelper LuceneHelper = new LuceneHelper();
            QueryParser QueryParser = new QueryParser();
            
            CreateIndexController CreateIndexController = new CreateIndexController(LuceneHelper, QueryParser);
            CreateIndexView CreateIndexView = new CreateIndexView();
            CreateIndexView.SetCreateIndexController(CreateIndexController);
            Application.Run(CreateIndexView);
        }
    }
}
