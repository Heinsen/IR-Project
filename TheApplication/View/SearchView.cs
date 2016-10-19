using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheApplication.Controller;
using TheApplication.Model;

namespace TheApplication.View
{
    public partial class SearchView : Form
    {
        public delegate List<RankedSEDocument> SearchCollectionDelegate(string QueryString, bool Preprocess);
        SearchCollectionDelegate _SearchCollectionDelegate;

        private SearchController _SearchController;

        public SearchView(LuceneHelper LuceneHelper)
        {
            InitializeComponent();
        }

        public void SetSearchController(SearchController SearchController)
        {
            _SearchController = SearchController;
        }

        private void SearchCollectionButton_Click(object sender, EventArgs e)
        {
            SearchCollectionButton.Enabled = false;

            _SearchCollectionDelegate = new SearchCollectionDelegate(_SearchController.SearchIndex);
           // _SearchController.BeginInvoke()
                
                //({ QueryTextBox.Text, NoPreprocessingCheckBox.Checked }, this.SearchedCollection, null);
        }

        private void SearchedCollection(IAsyncResult result)
        {
            SearchCollectionButton.Enabled = true;

            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(
                        new MethodInvoker(
                        delegate () { SearchedCollection(result); }));
                }
                catch (Exception e)
                {
                }
            }
            else
            {
                List<RankedSEDocument> RankedSEDocuments;

                try
                {
                    RankedSEDocuments = _SearchCollectionDelegate.EndInvoke(result);
                    RankedSEDocumentsDataGridView.DataSource = RankedSEDocuments;
                }
                catch (Exception e)
                {
                    //TODO: Show error
                }                
            }
        }

        //Not used in current implementation
        private List<string> BindInformationNeedDropDown()
        {
            List<string> infList = new List<string>();
            infList.Add("what \"similarity laws\" must be obeyed when constructing aeroelastic models of heated high speed aircraft .");
            infList.Add("what are the structural and aeroelastic problems associated with flight of high speed aircraft .");
            infList.Add("how can the aerodynamic performance of channel flow ground effect machines be calculated .");
            infList.Add("in summarizing theoretical and experimental work on the behaviour of a typical aircraft structure in a noise environment is it possible to develop a design procedure .");
            infList.Add("has anyone developed an analysis which accurately establishes the large deflection behaviour of \"conical shells\" .");

            return infList;
        }


    }
}
