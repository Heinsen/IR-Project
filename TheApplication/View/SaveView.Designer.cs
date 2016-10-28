namespace TheApplication.View
{
    partial class SaveView
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
            this.DocumentPathTextBox = new TheApplication.View.WaterMarkTextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SaveFileNameTextBox = new TheApplication.View.WaterMarkTextBox();
            this.FindSavePathButton = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DocumentPathTextBox
            // 
            this.DocumentPathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.DocumentPathTextBox.Location = new System.Drawing.Point(90, 105);
            this.DocumentPathTextBox.Name = "DocumentPathTextBox";
            this.DocumentPathTextBox.Size = new System.Drawing.Size(518, 26);
            this.DocumentPathTextBox.TabIndex = 6;
            this.DocumentPathTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.DocumentPathTextBox.WaterMarkText = "Please provide the path where you want to save you documentresults";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(374, 258);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(234, 55);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(90, 258);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(234, 55);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // SaveFileNameTextBox
            // 
            this.SaveFileNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SaveFileNameTextBox.Location = new System.Drawing.Point(90, 172);
            this.SaveFileNameTextBox.Name = "SaveFileNameTextBox";
            this.SaveFileNameTextBox.Size = new System.Drawing.Size(518, 26);
            this.SaveFileNameTextBox.TabIndex = 9;
            this.SaveFileNameTextBox.WaterMarkColor = System.Drawing.Color.Gray;
            this.SaveFileNameTextBox.WaterMarkText = "Please provide the file name";
            this.SaveFileNameTextBox.TextChanged += new System.EventHandler(this.SaveFileNameTextBox_TextChanged);
            // 
            // FindSavePathButton
            // 
            this.FindSavePathButton.Location = new System.Drawing.Point(641, 105);
            this.FindSavePathButton.Name = "FindSavePathButton";
            this.FindSavePathButton.Size = new System.Drawing.Size(138, 46);
            this.FindSavePathButton.TabIndex = 11;
            this.FindSavePathButton.Text = "Find path";
            this.FindSavePathButton.UseVisualStyleBackColor = true;
            this.FindSavePathButton.Click += new System.EventHandler(this.FindSavePathButton_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.AutoSize = false;
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 350);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(934, 22);
            this.StatusStrip.TabIndex = 12;
            this.StatusStrip.Text = "Please provide a path and a filename";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(200, 17);
            this.StatusLabel.Text = "Please provide a path and a filename";
            // 
            // SaveView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 372);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.FindSavePathButton);
            this.Controls.Add(this.SaveFileNameTextBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.DocumentPathTextBox);
            this.Name = "SaveView";
            this.Text = "Search Engine 1.0 Save query result";
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WaterMarkTextBox DocumentPathTextBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.Button CloseButton;
        private WaterMarkTextBox SaveFileNameTextBox;
        private System.Windows.Forms.Button FindSavePathButton;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    }
}