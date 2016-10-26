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
            this.ObjectListView = new BrightIdeasSoftware.ObjectListView();
            this.Rank = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Title = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Bibliographic = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.NextButton = new System.Windows.Forms.Button();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.SearchPanel.SuspendLayout();
            this.SearchResultPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectListView)).BeginInit();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchCollectionButton
            // 
            this.SearchCollectionButton.Location = new System.Drawing.Point(663, 13);
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
            this.NoPreprocessingCheckBox.Location = new System.Drawing.Point(117, 57);
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
            this.SearchPanel.Location = new System.Drawing.Point(-1, 12);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(1164, 106);
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
            this.QueryTextBox.Location = new System.Drawing.Point(117, 19);
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.Size = new System.Drawing.Size(483, 26);
            this.QueryTextBox.TabIndex = 0;
            this.QueryTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.QueryTextBox.WaterMarkText = "Search";
            // 
            // SaveSearchResultButton
            // 
            this.SaveSearchResultButton.Location = new System.Drawing.Point(321, 814);
            this.SaveSearchResultButton.Name = "SaveSearchResultButton";
            this.SaveSearchResultButton.Size = new System.Drawing.Size(512, 41);
            this.SaveSearchResultButton.TabIndex = 3;
            this.SaveSearchResultButton.Text = "Save result";
            this.SaveSearchResultButton.UseVisualStyleBackColor = true;
            this.SaveSearchResultButton.Click += new System.EventHandler(this.SaveSearchResultButton_Click);
            // 
            // SearchResultPanel
            // 
            this.SearchResultPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchResultPanel.Controls.Add(this.ObjectListView);
            this.SearchResultPanel.Controls.Add(this.NextButton);
            this.SearchResultPanel.Controls.Add(this.PreviousButton);
            this.SearchResultPanel.Location = new System.Drawing.Point(12, 124);
            this.SearchResultPanel.Name = "SearchResultPanel";
            this.SearchResultPanel.Size = new System.Drawing.Size(1132, 679);
            this.SearchResultPanel.TabIndex = 5;
            // 
            // ObjectListView
            // 
            this.ObjectListView.AllColumns.Add(this.Rank);
            this.ObjectListView.AllColumns.Add(this.Title);
            this.ObjectListView.AllColumns.Add(this.Bibliographic);
            this.ObjectListView.CellEditUseWholeCell = false;
            this.ObjectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Rank,
            this.Title,
            this.Bibliographic});
            this.ObjectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ObjectListView.EmptyListMsg = "Search the collection and the results will appear here";
            this.ObjectListView.EmptyListMsgFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ObjectListView.FullRowSelect = true;
            this.ObjectListView.HasCollapsibleGroups = false;
            this.ObjectListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ObjectListView.Location = new System.Drawing.Point(12, 3);
            this.ObjectListView.MultiSelect = false;
            this.ObjectListView.Name = "ObjectListView";
            this.ObjectListView.RowHeight = 60;
            this.ObjectListView.SelectColumnsOnRightClick = false;
            this.ObjectListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            this.ObjectListView.ShowGroups = false;
            this.ObjectListView.ShowHeaderInAllViews = false;
            this.ObjectListView.ShowSortIndicators = false;
            this.ObjectListView.Size = new System.Drawing.Size(1103, 624);
            this.ObjectListView.TabIndex = 9;
            this.ObjectListView.TabStop = false;
            this.ObjectListView.UseCompatibleStateImageBehavior = false;
            this.ObjectListView.View = System.Windows.Forms.View.Details;
            this.ObjectListView.ItemActivate += new System.EventHandler(this.ObjectListView_ItemActivate);
            // 
            // Rank
            // 
            this.Rank.AspectName = "Rank";
            this.Rank.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.FixedBounds;
            this.Rank.MaximumWidth = 42;
            this.Rank.MinimumWidth = 42;
            this.Rank.Text = "Rank";
            this.Rank.Width = 42;
            // 
            // Title
            // 
            this.Title.AspectName = "Title";
            this.Title.IsTileViewColumn = true;
            this.Title.MaximumWidth = 820;
            this.Title.MinimumWidth = 820;
            this.Title.Text = "Title";
            this.Title.Width = 820;
            // 
            // Bibliographic
            // 
            this.Bibliographic.AspectName = "Bibliographic";
            this.Bibliographic.MaximumWidth = 200;
            this.Bibliographic.MinimumWidth = 200;
            this.Bibliographic.Text = "Bibliographic";
            this.Bibliographic.Width = 200;
            this.Bibliographic.WordWrap = true;
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(567, 633);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(253, 41);
            this.NextButton.TabIndex = 6;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // PreviousButton
            // 
            this.PreviousButton.Location = new System.Drawing.Point(308, 633);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(253, 41);
            this.PreviousButton.TabIndex = 5;
            this.PreviousButton.Text = "Previous";
            this.PreviousButton.UseVisualStyleBackColor = true;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripStatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 858);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1156, 22);
            this.StatusStrip.TabIndex = 1;
            this.StatusStrip.Text = "Search";
            // 
            // StripStatusLabel
            // 
            this.StripStatusLabel.Name = "StripStatusLabel";
            this.StripStatusLabel.Size = new System.Drawing.Size(246, 17);
            this.StripStatusLabel.Text = "Search the collection through the search field";
            this.StripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SearchView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 880);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.SearchResultPanel);
            this.Controls.Add(this.SaveSearchResultButton);
            this.Controls.Add(this.SearchPanel);
            this.Name = "SearchView";
            this.Text = "Search Engine 1.0 Search collection";
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            this.SearchResultPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ObjectListView)).EndInit();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusLabel;
        private BrightIdeasSoftware.ObjectListView ObjectListView;
        private BrightIdeasSoftware.OLVColumn Rank;
        private BrightIdeasSoftware.OLVColumn Title;
        private BrightIdeasSoftware.OLVColumn Bibliographic;
    }
}