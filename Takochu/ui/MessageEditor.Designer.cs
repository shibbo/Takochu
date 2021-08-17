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
            this.dialogTypeList = new System.Windows.Forms.ComboBox();
            this.talkTypeList = new System.Windows.Forms.ComboBox();
            this.cameraTypeComboBox = new System.Windows.Forms.ComboBox();
            this.attribute6 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.attribute5 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.attribute4 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.attribute0 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowNamesList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.testMSBFBtn = new System.Windows.Forms.Button();
            this.flowNamesListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyMsgBtn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attribute6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attribute5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attribute4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attribute0)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.labelsComboBox.Location = new System.Drawing.Point(43, 6);
            this.labelsComboBox.Name = "labelsComboBox";
            this.labelsComboBox.Size = new System.Drawing.Size(842, 21);
            this.labelsComboBox.TabIndex = 1;
            this.labelsComboBox.SelectedIndexChanged += new System.EventHandler(this.labelsComboBox_SelectedIndexChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTextBox.Location = new System.Drawing.Point(7, 215);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(878, 514);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zone:";
            // 
            // zoneNamesComboBox
            // 
            this.zoneNamesComboBox.FormattingEnabled = true;
            this.zoneNamesComboBox.Location = new System.Drawing.Point(49, 27);
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
            this.tabControl1.Location = new System.Drawing.Point(12, 54);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(899, 784);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.applyMsgBtn);
            this.tabPage1.Controls.Add(this.dialogTypeList);
            this.tabPage1.Controls.Add(this.talkTypeList);
            this.tabPage1.Controls.Add(this.cameraTypeComboBox);
            this.tabPage1.Controls.Add(this.attribute6);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.attribute5);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.attribute4);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.attribute0);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.labelsComboBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.labelTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(891, 758);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Text";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dialogTypeList
            // 
            this.dialogTypeList.FormattingEnabled = true;
            this.dialogTypeList.Items.AddRange(new object[] {
            "Unknown 0",
            "Unknown 1",
            "Unknown 2",
            "Unknown 3",
            "Unknown 4",
            "Storyboard Text (Top)",
            "Storyboard Text (Center)",
            "Storyboard Text (Bottom)",
            "Unknown 8"});
            this.dialogTypeList.Location = new System.Drawing.Point(75, 113);
            this.dialogTypeList.Name = "dialogTypeList";
            this.dialogTypeList.Size = new System.Drawing.Size(261, 21);
            this.dialogTypeList.TabIndex = 19;
            // 
            // talkTypeList
            // 
            this.talkTypeList.FormattingEnabled = true;
            this.talkTypeList.Items.AddRange(new object[] {
            "Normal",
            "Shout",
            "Event",
            "Compose"});
            this.talkTypeList.Location = new System.Drawing.Point(75, 87);
            this.talkTypeList.Name = "talkTypeList";
            this.talkTypeList.Size = new System.Drawing.Size(261, 21);
            this.talkTypeList.TabIndex = 18;
            // 
            // cameraTypeComboBox
            // 
            this.cameraTypeComboBox.FormattingEnabled = true;
            this.cameraTypeComboBox.Items.AddRange(new object[] {
            "Normal",
            "Event",
            "Unknown"});
            this.cameraTypeComboBox.Location = new System.Drawing.Point(75, 58);
            this.cameraTypeComboBox.Name = "cameraTypeComboBox";
            this.cameraTypeComboBox.Size = new System.Drawing.Size(261, 21);
            this.cameraTypeComboBox.TabIndex = 17;
            // 
            // attribute6
            // 
            this.attribute6.Location = new System.Drawing.Point(75, 189);
            this.attribute6.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.attribute6.Name = "attribute6";
            this.attribute6.Size = new System.Drawing.Size(260, 20);
            this.attribute6.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Unknown 6";
            // 
            // attribute5
            // 
            this.attribute5.Location = new System.Drawing.Point(75, 163);
            this.attribute5.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.attribute5.Name = "attribute5";
            this.attribute5.Size = new System.Drawing.Size(260, 20);
            this.attribute5.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 165);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Unknown 5";
            // 
            // attribute4
            // 
            this.attribute4.Location = new System.Drawing.Point(75, 137);
            this.attribute4.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.attribute4.Name = "attribute4";
            this.attribute4.Size = new System.Drawing.Size(260, 20);
            this.attribute4.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Unknown 4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Talk Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 113);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Dialog Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Camera Type";
            // 
            // attribute0
            // 
            this.attribute0.Location = new System.Drawing.Point(75, 33);
            this.attribute0.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.attribute0.Name = "attribute0";
            this.attribute0.Size = new System.Drawing.Size(260, 20);
            this.attribute0.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sound ID";
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
            this.tabPage2.Size = new System.Drawing.Size(891, 758);
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
            this.testMSBFBtn.Enabled = false;
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
            this.flowNamesListBox.Size = new System.Drawing.Size(176, 667);
            this.flowNamesListBox.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(932, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // applyMsgBtn
            // 
            this.applyMsgBtn.Location = new System.Drawing.Point(7, 732);
            this.applyMsgBtn.Name = "applyMsgBtn";
            this.applyMsgBtn.Size = new System.Drawing.Size(878, 23);
            this.applyMsgBtn.TabIndex = 20;
            this.applyMsgBtn.Text = "Apply";
            this.applyMsgBtn.UseVisualStyleBackColor = true;
            this.applyMsgBtn.Click += new System.EventHandler(this.applyMsgBtn_Click);
            // 
            // MessageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 850);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.zoneNamesComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MessageEditor";
            this.Text = "MessageEditor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attribute6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attribute5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attribute4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attribute0)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.NumericUpDown attribute4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown attribute0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown attribute6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown attribute5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cameraTypeComboBox;
        private System.Windows.Forms.ComboBox talkTypeList;
        private System.Windows.Forms.ComboBox dialogTypeList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button applyMsgBtn;
    }
}