namespace TheApplication.View
{
    partial class SearchView
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
            this.SearchCollectionButton = new System.Windows.Forms.Button();
            this.NoPreprocessingCheckBox = new System.Windows.Forms.CheckBox();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.QueryTextBox = new TheApplication.View.WaterMarkTextBox();
            this.SaveSearchResultButton = new System.Windows.Forms.Button();
            this.SearchResultPanel = new System.Windows.Forms.Panel();
            this.RankedSEDocumentsListView = new System.Windows.Forms.ListView();
            this.NextButton = new System.Windows.Forms.Button();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.SearchPanel.SuspendLayout();
            this.SearchResultPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchCollectionButton
            // 
            this.SearchCollectionButton.Location = new System.Drawing.Point(663, 30);
            this.SearchCollectionButton.Name = "SearchCollectionButton";
            this.SearchCollectionButton.Size = new System.Drawing.Size(135, 41);
            this.SearchCollectionButton.TabIndex = 1;
            this.SearchCollectionButton.Text = "Search";
            this.SearchCollectionButton.UseVisualStyleBackColor = true;
            this.SearchCollectionButton.Click += new System.EventHandler(this.SearchCollectionButton_Click);
            // 
            // NoPreprocessingCheckBox
            // 
            this.NoPreprocessingCheckBox.AutoSize = true;
            this.NoPreprocessingCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoPreprocessingCheckBox.Location = new System.Drawing.Point(117, 83);
            this.NoPreprocessingCheckBox.Name = "NoPreprocessingCheckBox";
            this.NoPreprocessingCheckBox.Size = new System.Drawing.Size(196, 20);
            this.NoPreprocessingCheckBox.TabIndex = 2;
            this.NoPreprocessingCheckBox.TabStop = false;
            this.NoPreprocessingCheckBox.Text = "Do not preprocess my query";
            this.NoPreprocessingCheckBox.UseVisualStyleBackColor = true;
            // 
            // SearchPanel
            // 
            this.SearchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchPanel.Controls.Add(this.QueryTextBox);
            this.SearchPanel.Controls.Add(this.NoPreprocessingCheckBox);
            this.SearchPanel.Controls.Add(this.SearchCollectionButton);
            this.SearchPanel.Location = new System.Drawing.Point(-1, 35);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(950, 150);
            this.SearchPanel.TabIndex = 3;
            // 
            // QueryTextBox
            // 
            this.QueryTextBox.AutoCompleteCustomSource.AddRange(new string[] {
            "what \"similarity laws\" must be obeyed when constructing aeroelastic models of hea" +
                "ted high speed aircraft",
            "what are the structural and aeroelastic problems associated with flight of high s" +
                "peed aircraft",
            "how can the aerodynamic performance of channel flow ground effect machines be cal" +
                "culated",
            "in summarizing theoretical and experimental work on the behaviour of a typical ai" +
                "rcraft structure in a noise environment is it possible to develop a design proce" +
                "dure",
            "has anyone developed an analysis which accurately establishes the large deflectio" +
                "n behaviour of \"conical shells\""});
            this.QueryTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.QueryTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.QueryTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.QueryTextBox.Location = new System.Drawing.Point(117, 36);
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.Size = new System.Drawing.Size(483, 26);
            this.QueryTextBox.TabIndex = 0;
            this.QueryTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.QueryTextBox.WaterMarkText = "Put in your query";
            // 
            // SaveSearchResultButton
            // 
            this.SaveSearchResultButton.Location = new System.Drawing.Point(325, 809);
            this.SaveSearchResultButton.Name = "SaveSearchResultButton";
            this.SaveSearchResultButton.Size = new System.Drawing.Size(288, 41);
            this.SaveSearchResultButton.TabIndex = 3;
            this.SaveSearchResultButton.Text = "Save result";
            this.SaveSearchResultButton.UseVisualStyleBackColor = true;
            this.SaveSearchResultButton.Click += new System.EventHandler(this.SaveSearchResultButton_Click);
            // 
            // SearchResultPanel
            // 
            this.SearchResultPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchResultPanel.Controls.Add(this.RankedSEDocumentsListView);
            this.SearchResultPanel.Controls.Add(this.NextButton);
            this.SearchResultPanel.Controls.Add(this.PreviousButton);
            this.SearchResultPanel.Location = new System.Drawing.Point(12, 184);
            this.SearchResultPanel.Name = "SearchResultPanel";
            this.SearchResultPanel.Size = new System.Drawing.Size(910, 619);
            this.SearchResultPanel.TabIndex = 5;
            // 
            // RankedSEDocumentsListView
            // 
            this.RankedSEDocumentsListView.Location = new System.Drawing.Point(3, 6);
            this.RankedSEDocumentsListView.Name = "RankedSEDocumentsListView";
            this.RankedSEDocumentsListView.Size = new System.Drawing.Size(902, 531);
            this.RankedSEDocumentsListView.TabIndex = 8;
            this.RankedSEDocumentsListView.UseCompatibleStateImageBehavior = false;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(425, 556);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(135, 41);
            this.NextButton.TabIndex = 6;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            // 
            // PreviousButton
            // 
            this.PreviousButton.Location = new System.Drawing.Point(142, 556);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(135, 41);
            this.PreviousButton.TabIndex = 5;
            this.PreviousButton.Text = "Previous";
            this.PreviousButton.UseVisualStyleBackColor = true;
            // 
            // SearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 862);
            this.Controls.Add(this.SearchResultPanel);
            this.Controls.Add(this.SaveSearchResultButton);
            this.Controls.Add(this.SearchPanel);
            this.Name = "SearchView";
            this.Text = "Search Engine 1.0 Search collection";
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            this.SearchResultPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private WaterMarkTextBox QueryTextBox;
        private System.Windows.Forms.Button SearchCollectionButton;
        private System.Windows.Forms.CheckBox NoPreprocessingCheckBox;
        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.Button SaveSearchResultButton;
        private System.Windows.Forms.Panel SearchResultPanel;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.ListView RankedSEDocumentsListView;
    }
}