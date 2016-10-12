namespace TheApplication
{
    partial class frmIR
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
            this.grpIndex = new System.Windows.Forms.GroupBox();
            this.lblIndexTime = new System.Windows.Forms.Label();
            this.lblIndexSummary = new System.Windows.Forms.Label();
            this.btnCreateIndex = new System.Windows.Forms.Button();
            this.btnIndexPath = new System.Windows.Forms.Button();
            this.btnBrowseDocumentPath = new System.Windows.Forms.Button();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.btnParse = new System.Windows.Forms.Button();
            this.cmbInformationNeed = new System.Windows.Forms.ComboBox();
            this.grdResult = new System.Windows.Forms.DataGridView();
            this.lblQuery = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkAsIs = new System.Windows.Forms.CheckBox();
            this.txtInformationNeed = new System.Windows.Forms.TextBox();
            this.lblInformationNeed = new System.Windows.Forms.Label();
            this.fbdDocuments = new System.Windows.Forms.FolderBrowserDialog();
            this.btnInfSearch = new System.Windows.Forms.Button();
            this.grpIndex.SuspendLayout();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.SuspendLayout();
            // 
            // grpIndex
            // 
            this.grpIndex.Controls.Add(this.lblIndexTime);
            this.grpIndex.Controls.Add(this.lblIndexSummary);
            this.grpIndex.Controls.Add(this.btnCreateIndex);
            this.grpIndex.Controls.Add(this.btnIndexPath);
            this.grpIndex.Controls.Add(this.btnBrowseDocumentPath);
            this.grpIndex.Location = new System.Drawing.Point(12, 12);
            this.grpIndex.Name = "grpIndex";
            this.grpIndex.Size = new System.Drawing.Size(1064, 63);
            this.grpIndex.TabIndex = 0;
            this.grpIndex.TabStop = false;
            this.grpIndex.Text = "Index";
            // 
            // lblIndexTime
            // 
            this.lblIndexTime.AutoSize = true;
            this.lblIndexTime.Location = new System.Drawing.Point(520, 24);
            this.lblIndexTime.Name = "lblIndexTime";
            this.lblIndexTime.Size = new System.Drawing.Size(0, 13);
            this.lblIndexTime.TabIndex = 4;
            // 
            // lblIndexSummary
            // 
            this.lblIndexSummary.AutoSize = true;
            this.lblIndexSummary.Location = new System.Drawing.Point(540, 29);
            this.lblIndexSummary.Name = "lblIndexSummary";
            this.lblIndexSummary.Size = new System.Drawing.Size(0, 13);
            this.lblIndexSummary.TabIndex = 3;
            // 
            // btnCreateIndex
            // 
            this.btnCreateIndex.Location = new System.Drawing.Point(423, 19);
            this.btnCreateIndex.Name = "btnCreateIndex";
            this.btnCreateIndex.Size = new System.Drawing.Size(75, 23);
            this.btnCreateIndex.TabIndex = 2;
            this.btnCreateIndex.Text = "Create Index";
            this.btnCreateIndex.UseVisualStyleBackColor = true;
            this.btnCreateIndex.Click += new System.EventHandler(this.btnCreateIndex_Click);
            // 
            // btnIndexPath
            // 
            this.btnIndexPath.Location = new System.Drawing.Point(202, 19);
            this.btnIndexPath.Name = "btnIndexPath";
            this.btnIndexPath.Size = new System.Drawing.Size(158, 23);
            this.btnIndexPath.TabIndex = 1;
            this.btnIndexPath.Text = "Browse Index Path";
            this.btnIndexPath.UseVisualStyleBackColor = true;
            this.btnIndexPath.Click += new System.EventHandler(this.btnIndexPath_Click);
            // 
            // btnBrowseDocumentPath
            // 
            this.btnBrowseDocumentPath.Location = new System.Drawing.Point(6, 19);
            this.btnBrowseDocumentPath.Name = "btnBrowseDocumentPath";
            this.btnBrowseDocumentPath.Size = new System.Drawing.Size(169, 23);
            this.btnBrowseDocumentPath.TabIndex = 0;
            this.btnBrowseDocumentPath.Text = "Browse Documetns Path";
            this.btnBrowseDocumentPath.UseVisualStyleBackColor = true;
            this.btnBrowseDocumentPath.Click += new System.EventHandler(this.btnBrowseDocmuentPath_Click);
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.btnInfSearch);
            this.grpSearch.Controls.Add(this.btnParse);
            this.grpSearch.Controls.Add(this.cmbInformationNeed);
            this.grpSearch.Controls.Add(this.grdResult);
            this.grpSearch.Controls.Add(this.lblQuery);
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.chkAsIs);
            this.grpSearch.Controls.Add(this.txtInformationNeed);
            this.grpSearch.Controls.Add(this.lblInformationNeed);
            this.grpSearch.Location = new System.Drawing.Point(12, 81);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(1064, 540);
            this.grpSearch.TabIndex = 1;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search";
            // 
            // btnParse
            // 
            this.btnParse.Location = new System.Drawing.Point(391, 39);
            this.btnParse.Name = "btnParse";
            this.btnParse.Size = new System.Drawing.Size(75, 23);
            this.btnParse.TabIndex = 7;
            this.btnParse.Text = "Parse";
            this.btnParse.UseVisualStyleBackColor = true;
            this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
            // 
            // cmbInformationNeed
            // 
            this.cmbInformationNeed.FormattingEnabled = true;
            this.cmbInformationNeed.Location = new System.Drawing.Point(97, 105);
            this.cmbInformationNeed.Name = "cmbInformationNeed";
            this.cmbInformationNeed.Size = new System.Drawing.Size(847, 21);
            this.cmbInformationNeed.TabIndex = 6;
            // 
            // grdResult
            // 
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdResult.Location = new System.Drawing.Point(9, 161);
            this.grdResult.Name = "grdResult";
            this.grdResult.Size = new System.Drawing.Size(1049, 363);
            this.grdResult.TabIndex = 5;
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(6, 73);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(35, 13);
            this.lblQuery.TabIndex = 4;
            this.lblQuery.Text = "Query";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(472, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkAsIs
            // 
            this.chkAsIs.AutoSize = true;
            this.chkAsIs.Location = new System.Drawing.Point(959, 15);
            this.chkAsIs.Name = "chkAsIs";
            this.chkAsIs.Size = new System.Drawing.Size(49, 17);
            this.chkAsIs.TabIndex = 2;
            this.chkAsIs.Text = "As Is";
            this.chkAsIs.UseVisualStyleBackColor = true;
            // 
            // txtInformationNeed
            // 
            this.txtInformationNeed.Location = new System.Drawing.Point(97, 13);
            this.txtInformationNeed.Name = "txtInformationNeed";
            this.txtInformationNeed.Size = new System.Drawing.Size(847, 20);
            this.txtInformationNeed.TabIndex = 1;
            // 
            // lblInformationNeed
            // 
            this.lblInformationNeed.AutoSize = true;
            this.lblInformationNeed.Location = new System.Drawing.Point(3, 16);
            this.lblInformationNeed.Name = "lblInformationNeed";
            this.lblInformationNeed.Size = new System.Drawing.Size(91, 13);
            this.lblInformationNeed.TabIndex = 0;
            this.lblInformationNeed.Text = "Information Need:";
            // 
            // btnInfSearch
            // 
            this.btnInfSearch.Location = new System.Drawing.Point(396, 132);
            this.btnInfSearch.Name = "btnInfSearch";
            this.btnInfSearch.Size = new System.Drawing.Size(151, 23);
            this.btnInfSearch.TabIndex = 8;
            this.btnInfSearch.Text = "Information Need Search";
            this.btnInfSearch.UseVisualStyleBackColor = true;
            this.btnInfSearch.Click += new System.EventHandler(this.btnInfSearch_Click);
            // 
            // frmIR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 633);
            this.Controls.Add(this.grpSearch);
            this.Controls.Add(this.grpIndex);
            this.Name = "frmIR";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmIR_Load);
            this.grpIndex.ResumeLayout(false);
            this.grpIndex.PerformLayout();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpIndex;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.FolderBrowserDialog fbdDocuments;
        private System.Windows.Forms.Button btnIndexPath;
        private System.Windows.Forms.Button btnBrowseDocumentPath;
        private System.Windows.Forms.Label lblIndexSummary;
        private System.Windows.Forms.Button btnCreateIndex;
        private System.Windows.Forms.TextBox txtInformationNeed;
        private System.Windows.Forms.Label lblInformationNeed;
        private System.Windows.Forms.DataGridView grdResult;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkAsIs;
        private System.Windows.Forms.ComboBox cmbInformationNeed;
        private System.Windows.Forms.Button btnParse;
        private System.Windows.Forms.Label lblIndexTime;
        private System.Windows.Forms.Button btnInfSearch;
    }
}

