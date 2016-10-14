namespace TheApplication.View
{
    partial class CreateIndexView
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
            this.SetSourceCollectionPathButton = new System.Windows.Forms.Button();
            this.SetIndexPathButton = new System.Windows.Forms.Button();
            this.CreateIndexButton = new System.Windows.Forms.Button();
            this.IndexTimerTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConfirmIndexButton = new System.Windows.Forms.Button();
            this.SourceCollectionPathTextBox = new TheApplication.View.WaterMarkTextBox();
            this.IndexPathTextBox = new TheApplication.View.WaterMarkTextBox();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // SetSourceCollectionPathButton
            // 
            this.SetSourceCollectionPathButton.Location = new System.Drawing.Point(629, 98);
            this.SetSourceCollectionPathButton.Name = "SetSourceCollectionPathButton";
            this.SetSourceCollectionPathButton.Size = new System.Drawing.Size(135, 41);
            this.SetSourceCollectionPathButton.TabIndex = 0;
            this.SetSourceCollectionPathButton.Text = "Find path";
            this.SetSourceCollectionPathButton.UseVisualStyleBackColor = true;
            this.SetSourceCollectionPathButton.Click += new System.EventHandler(this.SetSourceCollectionPathButton_Click);
            // 
            // SetIndexPathButton
            // 
            this.SetIndexPathButton.BackColor = System.Drawing.SystemColors.Control;
            this.SetIndexPathButton.Location = new System.Drawing.Point(629, 237);
            this.SetIndexPathButton.Name = "SetIndexPathButton";
            this.SetIndexPathButton.Size = new System.Drawing.Size(135, 41);
            this.SetIndexPathButton.TabIndex = 1;
            this.SetIndexPathButton.Text = "Find path";
            this.SetIndexPathButton.UseVisualStyleBackColor = true;
            this.SetIndexPathButton.Click += new System.EventHandler(this.SetIndexPathButton_Click);
            // 
            // CreateIndexButton
            // 
            this.CreateIndexButton.Location = new System.Drawing.Point(230, 429);
            this.CreateIndexButton.Name = "CreateIndexButton";
            this.CreateIndexButton.Size = new System.Drawing.Size(262, 41);
            this.CreateIndexButton.TabIndex = 6;
            this.CreateIndexButton.Text = "Build index";
            this.CreateIndexButton.UseVisualStyleBackColor = true;
            this.CreateIndexButton.Click += new System.EventHandler(this.CreateIndexButton_Click);
            // 
            // IndexTimerTextBox
            // 
            this.IndexTimerTextBox.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.IndexTimerTextBox.CausesValidation = false;
            this.IndexTimerTextBox.Location = new System.Drawing.Point(629, 440);
            this.IndexTimerTextBox.Name = "IndexTimerTextBox";
            this.IndexTimerTextBox.ReadOnly = true;
            this.IndexTimerTextBox.Size = new System.Drawing.Size(100, 20);
            this.IndexTimerTextBox.TabIndex = 7;
            this.IndexTimerTextBox.Text = "00:00";
            this.IndexTimerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(626, 406);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Index build time";
            // 
            // ConfirmIndexButton
            // 
            this.ConfirmIndexButton.Enabled = false;
            this.ConfirmIndexButton.Location = new System.Drawing.Point(315, 523);
            this.ConfirmIndexButton.Name = "ConfirmIndexButton";
            this.ConfirmIndexButton.Size = new System.Drawing.Size(262, 41);
            this.ConfirmIndexButton.TabIndex = 9;
            this.ConfirmIndexButton.Text = "Confirm index";
            this.ConfirmIndexButton.UseVisualStyleBackColor = true;
            this.ConfirmIndexButton.Click += new System.EventHandler(this.ConfirmIndexButton_Click);
            // 
            // SourceCollectionPathTextBox
            // 
            this.SourceCollectionPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SourceCollectionPathTextBox.Location = new System.Drawing.Point(65, 104);
            this.SourceCollectionPathTextBox.Name = "SourceCollectionPathTextBox";
            this.SourceCollectionPathTextBox.Size = new System.Drawing.Size(481, 26);
            this.SourceCollectionPathTextBox.TabIndex = 5;
            this.SourceCollectionPathTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.SourceCollectionPathTextBox.WaterMarkText = "Please provide the path to your source collection";
            // 
            // IndexPathTextBox
            // 
            this.IndexPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IndexPathTextBox.Location = new System.Drawing.Point(65, 243);
            this.IndexPathTextBox.Name = "IndexPathTextBox";
            this.IndexPathTextBox.Size = new System.Drawing.Size(481, 26);
            this.IndexPathTextBox.TabIndex = 4;
            this.IndexPathTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.IndexPathTextBox.WaterMarkText = "Please provide a path for where you want the index saved";
            // 
            // CreateIndexView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 622);
            this.Controls.Add(this.ConfirmIndexButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IndexTimerTextBox);
            this.Controls.Add(this.CreateIndexButton);
            this.Controls.Add(this.SourceCollectionPathTextBox);
            this.Controls.Add(this.IndexPathTextBox);
            this.Controls.Add(this.SetIndexPathButton);
            this.Controls.Add(this.SetSourceCollectionPathButton);
            this.Name = "CreateIndexView";
            this.Text = "Search Engine 1.0 Create Index";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetSourceCollectionPathButton;
        private System.Windows.Forms.Button SetIndexPathButton;
        private WaterMarkTextBox IndexPathTextBox;
        private WaterMarkTextBox SourceCollectionPathTextBox;
        private System.Windows.Forms.Button CreateIndexButton;
        private System.Windows.Forms.TextBox IndexTimerTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ConfirmIndexButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
    }
}