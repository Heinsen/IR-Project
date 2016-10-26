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
    public partial class SaveView : Form
    {
        public delegate void SaveFileDelegate();
        SaveFileDelegate _SaveFileDelegate;

        SaveController _SaveController;

        public SaveView()
        {
            InitializeComponent();
        }

        public void SetSaveController(SaveController SaveController)
        {
            _SaveController = SaveController;

            //Loading earlier set paths
            DocumentPathTextBox.Text = _SaveController.GetSavePath();
            SaveFileNameTextBox.Text = _SaveController.GetFileName();
        }
    
        private void FindSavePathButton_Click(object sender, EventArgs e)
        {
            string SavePath = ShowFolderBrowser();
            if (SavePath != string.Empty)
            {
                DocumentPathTextBox.Text = SavePath;
                _SaveController.SetSavePath(SavePath);
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

        private void SaveFileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _SaveController.SetFileName(SaveFileNameTextBox.Text);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if((DocumentPathTextBox.Text != string.Empty) && (SaveFileNameTextBox.Text != string.Empty))
            {

                SaveButton.Enabled = false;

                _SaveFileDelegate = new SaveFileDelegate(_SaveController.SaveDocument);
                _SaveFileDelegate.BeginInvoke(this.FileSaved, null);
            }
        }

        private void FileSaved(IAsyncResult result)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(
                        new MethodInvoker(
                        delegate () { FileSaved(result); }));
                }
                catch (Exception e)
                {
                }
            }
            else
            {
                //TODO: Indicate wether it was a success

                SaveButton.Enabled = true;
            }
        }
    }
}
