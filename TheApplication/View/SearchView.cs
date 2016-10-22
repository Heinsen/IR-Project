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
        public delegate List<RankedSEDocument> SearchCollectionDelegate(string QueryString, bool Preprocess, int pageNum);
        SearchCollectionDelegate _SearchCollectionDelegate;

        private SearchController _SearchController;
        private int _PageNum;

        public SearchView()
        {
            InitializeComponent();
        }

        public void SetSearchController(SearchController SearchController)
        {
            _SearchController = SearchController;
            _PageNum = 0;
        }

        private void SearchCollectionButton_Click(object sender, EventArgs e)
        {
            Search(0);

            //_SearchController.SearchIndex(QueryTextBox.Text, NoPreprocessingCheckBox.Checked);
        }

        private void Search(int pageNum)
        {
            SearchCollectionButton.Enabled = false;

            _SearchCollectionDelegate = new SearchCollectionDelegate(_SearchController.SearchIndex);
            _SearchCollectionDelegate.BeginInvoke((string)QueryTextBox.Text, NoPreprocessingCheckBox.Checked, _PageNum, this.SearchedCollection, null);
        }

        private void SearchedCollection(IAsyncResult result)
        {
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
                SearchCollectionButton.Enabled = true;

                List<RankedSEDocument> RankedSEDocuments;

                try
                {
                    RankedSEDocuments = _SearchCollectionDelegate.EndInvoke(result);
                    SetListViewData(RankedSEDocuments);
                }
                catch (Exception e)
                {
                    //TODO: Show error
                }                
            }
        }

        private void SetListViewData(List<RankedSEDocument> RankedSEDocuments)
        {
            RankedSEDocumentsListView.Items.Clear();
            RankedSEDocumentsListView.View = System.Windows.Forms.View.Details;
            RankedSEDocumentsListView.GridLines = true;
            RankedSEDocumentsListView.FullRowSelect = true;

            RankedSEDocumentsListView.Columns.Add("Rank", 35);
            RankedSEDocumentsListView.Columns.Add("Title", 300);
            RankedSEDocumentsListView.Columns.Add("Author", 150);
            RankedSEDocumentsListView.Columns.Add("Bibliograpic information", 100);
            RankedSEDocumentsListView.Columns.Add("Abstract", 400);

            foreach(RankedSEDocument Document in RankedSEDocuments)
            {
                string[] arr = new string[5];

                arr[0] = Document.Rank.ToString();
                arr[1] = Document.Title;
                arr[2] = Document.Author;
                arr[3] = Document.Bibliographic;
                arr[4] = Document.getAbstractFirstLine();
                ListViewItem ListViewItem = new ListViewItem(arr);
                RankedSEDocumentsListView.Items.Add(ListViewItem);
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

        private void SaveSearchResultButton_Click(object sender, EventArgs e)
        {
            _SearchController.SaveRankedDocuments();

        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            Search(_PageNum++);
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            if (_PageNum >= 1)
            {
                Search(_PageNum--);
            }
        }
    }
}
