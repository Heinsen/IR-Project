using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheApplication.Model;

namespace TheApplication
{
    public partial class frmIR : Form
    {
        LuceneAdvancedSearch myLuceneApp = new LuceneAdvancedSearch();
        QueryParser myParser = new QueryParser();

        public frmIR()
        {
            InitializeComponent();
        }

        string documentFolderPath = @"C:\Users\nahide\Documents\DataScience\IR\Assignment\Project\crandocs";
        string indexFolderPath = @"C:\Users\nahide\Documents\DataScience\IR\Assignment\Project\Index";

        private void btnBrowseDocmuentPath_Click(object sender, EventArgs e)
        {
            this.fbdDocuments.ShowNewFolderButton = false;
            this.fbdDocuments.RootFolder = System.Environment.SpecialFolder.UserProfile;

            DialogResult result = this.fbdDocuments.ShowDialog();
            if (result == DialogResult.OK)
            {
                documentFolderPath = this.fbdDocuments.SelectedPath;
            }
        }

        private void btnIndexPath_Click(object sender, EventArgs e)
        {
            this.fbdDocuments.ShowNewFolderButton = false;
            this.fbdDocuments.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult result = this.fbdDocuments.ShowDialog();
            if (result == DialogResult.OK)
            {
                indexFolderPath = this.fbdDocuments.SelectedPath;
            }
        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            DateTime start = System.DateTime.Now;
            //read all the files and load them into memory
            List<SEDocument> AllDocs = LuceneAdvancedSearch.GetAllDocuments(documentFolderPath);

            myLuceneApp.CreateIndex(indexFolderPath);
            foreach (SEDocument doc in AllDocs)
                myLuceneApp.IndexDocument(doc);
            myLuceneApp.CleanUpIndexer();
            DateTime end = System.DateTime.Now;
            lblIndexTime.Text = string.Format("Total Index Time: {0}", (end - start));

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            grdResult.DataSource = Search(0);
        }

        private List<RankedSEDocument> Search(int pageNum)
        {
            List<RankedSEDocument> result = new List<RankedSEDocument>();
            myLuceneApp.CreateSearcher();
            string toSearch = string.Empty;
            if (chkAsIs.Checked)
            {
                toSearch = txtInformationNeed.Text;
            }
            else
            {
                //toSearch = txtInformationNeed.Text;
                toSearch = ParseInformationNeed(txtInformationNeed.Text);
            }

            result = myLuceneApp.SearchText(toSearch, myParser.FindPhrases(txtInformationNeed.Text), chkAsIs.Checked, pageNum);
            
            myLuceneApp.CleanUpSearcher();
            return result;
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            lblQuery.Text = ParseInformationNeed(txtInformationNeed.Text);
        }

        private string ParseInformationNeed(string ifn)
        {
            return myParser.InformationNeedParser(ifn);
        }

        private void frmIR_Load(object sender, EventArgs e)
        {
            cmbInformationNeed.DataSource = myLuceneApp.BindInformationNeedDropDown();
            myParser.LoadPOSTagger();
            QueryParser.BindPOSDictionary();
        }

        private void btnInfSearch_Click(object sender, EventArgs e)
        {
            myLuceneApp.CreateSearcher();
            string toSearch = string.Empty;
            toSearch = ParseInformationNeed(cmbInformationNeed.SelectedValue.ToString());

            List<RankedSEDocument> result = myLuceneApp.SearchText(toSearch, myParser.FindPhrases(cmbInformationNeed.SelectedValue.ToString()), false, 0);
            grdResult.DataSource = result;

            myLuceneApp.CleanUpSearcher();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            grdResult.DataSource = Search(0);
        }

        private void btnSecond_Click(object sender, EventArgs e)
        {
            grdResult.DataSource = Search(1);
        }

    }
}
