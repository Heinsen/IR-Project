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
using BrightIdeasSoftware;


namespace TheApplication.View
{
    public partial class SearchView : Form
    {
        public delegate SearchCollectionResult SearchCollectionDelegate(string QueryString, bool NoPreprocessing);
        SearchCollectionDelegate _SearchCollectionDelegate;

        private SearchController _SearchController;
        
        private List<RankedSEDocument> _RankedSEDocuments;

        private int _CurrentTopListViewItem = 0;
        private int _NNewListViewItems = 10;

        DateTime _StartTime;

        public SearchView()
        {
            InitializeComponent();

            SetupObjectListView();
        }

        private void SetupObjectListView()
        {
            this.Title.Renderer = CreateDescribedTaskRenderer();

            this.Title.AspectName = "Title";

            this.ObjectListView.CellPadding = new Rectangle(4, 1, 4, 0);


            TextOverlay textOverlay = this.ObjectListView.EmptyListMsgOverlay as TextOverlay;
            textOverlay.BackColor = Color.White;
            textOverlay.BorderWidth = 0f;
        }

        private DescribedTaskRenderer CreateDescribedTaskRenderer()
        {

            // Let's create an appropriately configured renderer.
            DescribedTaskRenderer renderer = new DescribedTaskRenderer();

            // Tell the renderer which property holds the text to be used as a description
            renderer.DescriptionAspectName = "Description";
            
            
            // Change the formatting slightly
            renderer.TitleFont = new Font("Tahoma", 10, FontStyle.Bold);
            renderer.DescriptionFont = new Font("Tahoma", 9);
            renderer.ImageTextSpace = 0;
            renderer.TitleDescriptionSpace = 1;

            // Use older Gdi renderering, since most people think the text looks clearer
            renderer.UseGdiTextRendering = true;
            
            return renderer;
        }

        public void SetSearchController(SearchController SearchController)
        {
            _SearchController = SearchController;
        }

        private void SearchCollectionButton_Click(object sender, EventArgs e)
        {
            //Clear local list and ObjectListView
            if(_RankedSEDocuments != null)
                _RankedSEDocuments.Clear();

            ObjectListView.ClearObjects();
            SaveSearchResultButton.Enabled = false;

            //Start search time
            _StartTime = DateTime.Now;

            SearchCollectionButton.Enabled = false;

            ProcessedQueryTextBox.Clear();

            //Start backgroundthread searching the collcation
            _SearchCollectionDelegate = new SearchCollectionDelegate(_SearchController.SearchIndex);
            _SearchCollectionDelegate.BeginInvoke(QueryTextBox.Text, !NoPreprocessingCheckBox.Checked, SearchedCollection, null);
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
                SaveSearchResultButton.Enabled = true;

                List<RankedSEDocument> RankedSEDocuments;

                try
                {
                    SearchCollectionResult SearchCollectionResult = _SearchCollectionDelegate.EndInvoke(result);

                    ProcessedQueryTextBox.Text = SearchCollectionResult.ProcessedQuery;

                    SetListViewData(SearchCollectionResult.RankedResults);

                    SetResultsStripStatusLabel(SearchCollectionResult.SearchEndDateTime, SearchCollectionResult.RankedResults.Count);
                }
                catch (Exception e)
                {
                    //TODO: Show error
                }                
            }
        }

        private void SetListViewData(List<RankedSEDocument> RankedSEDocuments)
        {
            this._RankedSEDocuments = RankedSEDocuments;
            ShowListViewItems(0);
        }
        
        private void PreviousButton_Click(object sender, EventArgs e)
        {
            _CurrentTopListViewItem -= _NNewListViewItems;

            ShowListViewItems(_CurrentTopListViewItem);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            _CurrentTopListViewItem += _NNewListViewItems;

            ShowListViewItems(_CurrentTopListViewItem);
        }

        //Navigate through the current search result 
        private void ShowListViewItems(int NewTopItem)
        {
            //If we have reached the start
            if (NewTopItem < 0)
            {
                NewTopItem = 0;
            }

            int NewLastItem = NewTopItem + _NNewListViewItems;

            //If we have reached the end
            if(NewLastItem >= _RankedSEDocuments.Count)
            {
                NextButton.Enabled = false;
                NewLastItem = _RankedSEDocuments.Count;
            }
            else
            {
                NextButton.Enabled = true;
            }

            this.ObjectListView.ClearObjects();

            for(int i = NewTopItem; i < NewLastItem; i++)
            {
                this.ObjectListView.AddObject(_RankedSEDocuments[i]);
            }

            if (NewTopItem <= 0)
            {
                PreviousButton.Enabled = false;
            }
            else
            {
                PreviousButton.Enabled = true;
            }

        }

        private void SaveSearchResultButton_Click(object sender, EventArgs e)
        {
            _SearchController.SaveRankedDocuments(QueryTextBox.Text);
        }

        private void SetResultsStripStatusLabel(DateTime SearchEndDateTime, int NRankedDocuments)
        {
            var TimeElapsed = SearchEndDateTime - _StartTime;
            StripStatusLabel.Text = string.Format("Found {0} relevant documents in {1} seconds and {2} milliseconds", NRankedDocuments, TimeElapsed.Seconds, TimeElapsed.Milliseconds);
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

        private void ObjectListView_ItemActivate(object sender, EventArgs e)
        {
            var item = ObjectListView.GetItem(ObjectListView.SelectedIndex).RowObject;

            AbstractView AbstractView = new AbstractView((RankedSEDocument) item);

            AbstractView.Show();

        }
    }
}
