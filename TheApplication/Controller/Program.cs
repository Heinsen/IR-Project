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
            LuceneClass LuceneClass = new LuceneClass();

            BooleanQueryParser BooleanQueryParser = new BooleanQueryParser();
            CreateIndexController CreateIndexController = new CreateIndexController(LuceneClass, BooleanQueryParser);

            //BooleanLexicalQueryParser BooleanLexicalQueryParser = new BooleanLexicalQueryParser();
            //CreateIndexController CreateIndexController = new CreateIndexController(LuceneClass, BooleanLexicalQueryParser);

            CreateIndexView CreateIndexView = new CreateIndexView();
            CreateIndexView.SetCreateIndexController(CreateIndexController);
            Application.Run(CreateIndexView);
        }
    }
}
