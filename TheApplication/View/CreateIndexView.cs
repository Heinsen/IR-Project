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

namespace TheApplication.View
{
    public partial class CreateIndexView : Form
    {
        CreateIndexController _CreateIndexController;
        public delegate bool CreateIndexDelegate();
        CreateIndexDelegate _CreateIndexDelegate;
        Timer _Timer = new Timer();
        DateTime _StartTime;

        public CreateIndexView()
        {
            InitializeComponent();
            _Timer.Tick += new EventHandler(Timer_Tick);
        }

        public void SetCreateIndexController(CreateIndexController CreateIndexController)
        {
            //Setting callback class
            _CreateIndexController = CreateIndexController;
            //Loading earlier set paths
            SourceCollectionPathTextBox.Text = _CreateIndexController.GetSourceCollectionPath();
            IndexPathTextBox.Text = _CreateIndexController.GetIndexPath();
        }

        private void SetSourceCollectionPathButton_Click(object sender, EventArgs e)
        {
            string SourceNewCollectionPath = ShowFolderBrowser();
            if (SourceNewCollectionPath != string.Empty)
            {
                SourceCollectionPathTextBox.Text = SourceNewCollectionPath;
                _CreateIndexController.SetSourceCollectionPath(SourceNewCollectionPath);
            }
        }

        private void SetIndexPathButton_Click(object sender, EventArgs e)
        {
            string NewIndexPath = ShowFolderBrowser();
            if (NewIndexPath != string.Empty)
            {
                IndexPathTextBox.Text = NewIndexPath;
                _CreateIndexController.SetIndexPath(NewIndexPath);
            }
        }

        private string ShowFolderBrowser()
        {
            this.FolderBrowser.ShowNewFolderButton = false;
            this.FolderBrowser.RootFolder = System.Environment.SpecialFolder.UserProfile;

            DialogResult result = this.FolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                return this.FolderBrowser.SelectedPath;
            }

            return string.Empty;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            var TimeElapsed = DateTime.Now - _StartTime;

            IndexTimerTextBox.Text = string.Format("{0:D2}:{1:D2}", TimeElapsed.Seconds, TimeElapsed.Milliseconds);
        }


        private void CreateIndexButton_Click(object sender, EventArgs e)
        {
            CreateIndexButton.Enabled = false;
            _StartTime = System.DateTime.Now;
            _Timer.Start();

            //_CreateIndexController.CreateIndex();

            _CreateIndexDelegate = new CreateIndexDelegate(_CreateIndexController.CreateIndex);
            _CreateIndexDelegate.BeginInvoke(this.IndexCreated, null);
        }

        private void IndexCreated(IAsyncResult result)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(
                        new MethodInvoker(
                        delegate () { IndexCreated(result); }));
                }
                catch (Exception e)
                {
                }
            }
            else
            {

                bool SuccessfullyCreatedIndex = false;

                try
                {
                    SuccessfullyCreatedIndex = _CreateIndexDelegate.EndInvoke(result);
                }
                catch (Exception e)
                {

                }
                _Timer.Stop();
                CreateIndexButton.Enabled = true;

                if (SuccessfullyCreatedIndex)
                {
                    ConfirmIndexButton.Enabled = true;
                }
                else
                {
                    //TODO: Show error message
                }
            }
        }

        private void ConfirmIndexButton_Click(object sender, EventArgs e)
        {
            _CreateIndexController.ConfirmIndex();
            this.Hide();
        }
    }
}
