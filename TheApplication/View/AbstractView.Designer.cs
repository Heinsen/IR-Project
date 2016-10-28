namespace TheApplication.View
{
    partial class AbstractView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RankLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.AbstractLabel = new System.Windows.Forms.Label();
            this.AuthorLabel = new System.Windows.Forms.Label();
            this.BibliographicLabel = new System.Windows.Forms.Label();
            this.HeaderPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.HeaderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // RankLabel
            // 
            this.RankLabel.BackColor = System.Drawing.Color.White;
            this.RankLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RankLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RankLabel.Location = new System.Drawing.Point(38, 50);
            this.RankLabel.Name = "RankLabel";
            this.RankLabel.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.RankLabel.Size = new System.Drawing.Size(70, 70);
            this.RankLabel.TabIndex = 2;
            this.RankLabel.Text = "Rank";
            this.RankLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TitleLabel
            // 
            this.TitleLabel.BackColor = System.Drawing.Color.White;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(3, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(762, 44);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AbstractLabel
            // 
            this.AbstractLabel.BackColor = System.Drawing.Color.White;
            this.AbstractLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AbstractLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AbstractLabel.Location = new System.Drawing.Point(38, 197);
            this.AbstractLabel.Name = "AbstractLabel";
            this.AbstractLabel.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.AbstractLabel.Size = new System.Drawing.Size(857, 376);
            this.AbstractLabel.TabIndex = 4;
            this.AbstractLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AuthorLabel
            // 
            this.AuthorLabel.BackColor = System.Drawing.Color.White;
            this.AuthorLabel.Font = new System.Drawing.Font("Tahoma", 9F);
            this.AuthorLabel.Location = new System.Drawing.Point(3, 56);
            this.AuthorLabel.Name = "AuthorLabel";
            this.AuthorLabel.Size = new System.Drawing.Size(762, 24);
            this.AuthorLabel.TabIndex = 5;
            this.AuthorLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BibliographicLabel
            // 
            this.BibliographicLabel.BackColor = System.Drawing.Color.White;
            this.BibliographicLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BibliographicLabel.Location = new System.Drawing.Point(3, 92);
            this.BibliographicLabel.Name = "BibliographicLabel";
            this.BibliographicLabel.Size = new System.Drawing.Size(762, 24);
            this.BibliographicLabel.TabIndex = 6;
            this.BibliographicLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // HeaderPanel
            // 
            this.HeaderPanel.BackColor = System.Drawing.Color.White;
            this.HeaderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HeaderPanel.Controls.Add(this.BibliographicLabel);
            this.HeaderPanel.Controls.Add(this.TitleLabel);
            this.HeaderPanel.Controls.Add(this.AuthorLabel);
            this.HeaderPanel.Location = new System.Drawing.Point(125, 50);
            this.HeaderPanel.Name = "HeaderPanel";
            this.HeaderPanel.Size = new System.Drawing.Size(770, 128);
            this.HeaderPanel.TabIndex = 7;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(38, 588);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(857, 40);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // AbstractView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 640);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.HeaderPanel);
            this.Controls.Add(this.AbstractLabel);
            this.Controls.Add(this.RankLabel);
            this.Name = "AbstractView";
            this.Text = "AbstractView";
            this.HeaderPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label RankLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label AbstractLabel;
        private System.Windows.Forms.Label AuthorLabel;
        private System.Windows.Forms.Label BibliographicLabel;
        private System.Windows.Forms.Panel HeaderPanel;
        private System.Windows.Forms.Button CloseButton;
    }
}