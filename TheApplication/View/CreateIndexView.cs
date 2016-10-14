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
        delegate bool CreateIndexDelegate(string SourceCollectionPath, string IndexPath);
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
            _CreateIndexController.SetSourceCollectionPath();
        }

        private void SetIndexPathButton_Click(object sender, EventArgs e)
        {
            _CreateIndexController.SetIndexPath();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime CurrentTime = System.DateTime.Now;

            int CurrentMinutes = CurrentTime.Minute - _StartTime.Minute;
            string CurrentMinutesString = CurrentMinutes.ToString();
            int CurrentSeconds = CurrentTime.Second - _StartTime.Second;
            string CurrentSecondsString = CurrentSeconds.ToString();

            if (CurrentMinutes < 10)
            {
                CurrentMinutesString = "0" + CurrentMinutes;
            }
            
            if (CurrentSeconds < 10)
            {
                CurrentSecondsString = "0" + CurrentSeconds;
            }


            IndexTimerTextBox.Text = string.Format("{0}:{1}", CurrentMinutesString, CurrentSecondsString);
        }


        private void CreateIndexButton_Click(object sender, EventArgs e)
        {
            CreateIndexButton.Enabled = false;
            _StartTime = System.DateTime.Now;
            _Timer.Start();

            _CreateIndexDelegate = new CreateIndexDelegate(_CreateIndexController.CreateIndex);
            _CreateIndexDelegate.BeginInvoke(string.Empty, string.Empty, this.IndexCreated, null);
        }

        private void IndexCreated(IAsyncResult result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { IndexCreated(result); }));
            }
            else
            {
                bool SuccessfullyCreatedIndex = _CreateIndexDelegate.EndInvoke(result);

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

        }
    }
}
