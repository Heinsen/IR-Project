using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.ComponentModel;
using System.Threading;
using TheApplication.Model;
using TheApplication.View;


namespace TheApplication.Controller
{
    public class CreateIndexController
    {
        LuceneHelper _LuceneHelper;
        List<SEDocument> _SourceCollection;
        
        public CreateIndexController()
        {
        }

        public bool CreateIndex()
        {
            //TODO In case no Source Collection was found
            _SourceCollection = FileManipulator.SearchDirectory((string)Properties.Settings.Default["SourceCollectionPath"]);
            _LuceneHelper = new LuceneHelper(_SourceCollection, (string)Properties.Settings.Default["IndexPath"]);

            return true;
        }

        public void SetSourceCollectionPath(string SourceCollectionPath)
        {
            Properties.Settings.Default["SourceCollectionPath"] = SourceCollectionPath;
            Properties.Settings.Default.Save();
        }

        public string GetSourceCollectionPath()
        {
            return (string)Properties.Settings.Default["SourceCollectionPath"];
        }

        public void SetIndexPath(string IndexPath)
        {
            Properties.Settings.Default["IndexPath"] = IndexPath;
            Properties.Settings.Default.Save();
        }

        public string GetIndexPath()
        {
            return (string)Properties.Settings.Default["IndexPath"];
        }

        public void ConfirmIndex()
        {
            SearchController SearchController = new SearchController(_LuceneHelper);
            SearchView SearchView = new SearchView(_LuceneHelper);
            SearchView.SetSearchController(SearchController);
            SearchView.Show();
        }
    }
}
