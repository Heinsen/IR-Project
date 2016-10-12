using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        string documentFolderPath = string.Empty;
        string indexFolderPath = string.Empty;

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
            List<Doc> AllDocs = LuceneAdvancedSearch.GetAllDocuments(documentFolderPath);

            myLuceneApp.CreateIndex(indexFolderPath);
            foreach (Doc doc in AllDocs)
                myLuceneApp.IndexDocument(doc);
            myLuceneApp.CleanUpIndexer();
            DateTime end = System.DateTime.Now;
            lblIndexTime.Text = string.Format("Total Index Time: {0}", (end - start));

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            myLuceneApp.CreateSearcher();
            string toSearch = string.Empty;
            if (chkAsIs.Checked)
            {
                toSearch = txtInformationNeed.Text;
            }
            else
            {
                toSearch = ParseInformationNeed(txtInformationNeed.Text);
            }

            List<Doc> result = myLuceneApp.SearchText(toSearch);
            if (result != null)
            {
                grdResult.DataSource = result;
            }

            myLuceneApp.CleanUpSearcher();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            lblQuery.Text = ParseInformationNeed(txtInformationNeed.Text);
        }

        private string ParseInformationNeed(string ifn)
        {
            return myParser.InformationNeedParser(ifn) + string.Join(" ", myParser.FindPhrases(ifn));
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

            List<Doc> result = myLuceneApp.SearchText(toSearch);
            if (result != null)
            {
                grdResult.DataSource = result;
            }

            myLuceneApp.CleanUpSearcher();
        }

    }
}
