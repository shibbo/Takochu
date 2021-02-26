namespace Takochu.util
{
    partial class MessageEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelsComboBox = new System.Windows.Forms.ComboBox();
            this.labelTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.zoneNamesComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowNamesList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.testMSBFBtn = new System.Windows.Forms.Button();
            this.flowNamesListBox = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Labels:";
            // 
            // labelsComboBox
            // 
            this.labelsComboBox.FormattingEnabled = true;
            this.labelsComboBox.Location = new System.Drawing.Point(47, 6);
            this.labelsComboBox.Name = "labelsComboBox";
            this.labelsComboBox.Size = new System.Drawing.Size(842, 21);
            this.labelsComboBox.TabIndex = 1;
            this.labelsComboBox.SelectedIndexChanged += new System.EventHandler(this.labelsComboBox_SelectedIndexChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTextBox.Location = new System.Drawing.Point(7, 30);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(878, 425);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zone:";
            // 
            // zoneNamesComboBox
            // 
            this.zoneNamesComboBox.FormattingEnabled = true;
            this.zoneNamesComboBox.Location = new System.Drawing.Point(49, 6);
            this.zoneNamesComboBox.Name = "zoneNamesComboBox";
            this.zoneNamesComboBox.Size = new System.Drawing.Size(862, 21);
            this.zoneNamesComboBox.TabIndex = 4;
            this.zoneNamesComboBox.SelectedIndexChanged += new System.EventHandler(this.zoneNamesComboBox_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(899, 487);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelsComboBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.labelTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(891, 461);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowNamesList);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.testMSBFBtn);
            this.tabPage2.Controls.Add(this.flowNamesListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(891, 461);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Flow";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowNamesList
            // 
            this.flowNamesList.FormattingEnabled = true;
            this.flowNamesList.Location = new System.Drawing.Point(49, 10);
            this.flowNamesList.Name = "flowNamesList";
            this.flowNamesList.Size = new System.Drawing.Size(836, 21);
            this.flowNamesList.TabIndex = 3;
            this.flowNamesList.SelectedIndexChanged += new System.EventHandler(this.flowNamesList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Flows:";
            // 
            // testMSBFBtn
            // 
            this.testMSBFBtn.Location = new System.Drawing.Point(6, 37);
            this.testMSBFBtn.Name = "testMSBFBtn";
            this.testMSBFBtn.Size = new System.Drawing.Size(75, 23);
            this.testMSBFBtn.TabIndex = 1;
            this.testMSBFBtn.Text = "Test Flow";
            this.testMSBFBtn.UseVisualStyleBackColor = true;
            this.testMSBFBtn.Click += new System.EventHandler(this.testMSBFBtn_Click);
            // 
            // flowNamesListBox
            // 
            this.flowNamesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowNamesListBox.FormattingEnabled = true;
            this.flowNamesListBox.Location = new System.Drawing.Point(6, 73);
            this.flowNamesListBox.Name = "flowNamesListBox";
            this.flowNamesListBox.Size = new System.Drawing.Size(176, 381);
            this.flowNamesListBox.TabIndex = 0;
            // 
            // MessageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 538);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.zoneNamesComboBox);
            this.Controls.Add(this.label2);
            this.Name = "MessageEditor";
            this.Text = "MessageEditor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox labelsComboBox;
        private System.Windows.Forms.RichTextBox labelTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox zoneNamesComboBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox flowNamesListBox;
        private System.Windows.Forms.Button testMSBFBtn;
        private System.Windows.Forms.ComboBox flowNamesList;
        private System.Windows.Forms.Label label3;
    }
}