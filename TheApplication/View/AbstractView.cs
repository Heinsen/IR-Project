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

namespace TheApplication.View
{
    public partial class AbstractView : Form
    {
        public AbstractView(RankedSEDocument RankedSEDocument)
        {
            InitializeComponent();

            RankLabel.Text += Environment.NewLine + RankedSEDocument.Rank.ToString();

            TitleLabel.Text = RankedSEDocument.Title;

            AuthorLabel.Text = RankedSEDocument.Author;

            BibliographicLabel.Text = RankedSEDocument.Bibliographic;

            AbstractLabel.Text = RankedSEDocument.Abstract;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
