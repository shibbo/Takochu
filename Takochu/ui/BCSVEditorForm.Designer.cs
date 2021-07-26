namespace Takochu.ui
{
    partial class BCSVEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BCSVEditorForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.archiveTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openBCSVBtn = new System.Windows.Forms.ToolStripButton();
            this.openExternalBtn = new System.Windows.Forms.ToolStripButton();
            this.recentFilesListDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveBCSVBtn = new System.Windows.Forms.ToolStripButton();
            this.saveAll_Btn = new System.Windows.Forms.ToolStripButton();
            this.filesystemView = new System.Windows.Forms.TreeView();
            this.bcsvEditorsTabControl = new System.Windows.Forms.TabControl();
            this.RemoveEntryBtn = new System.Windows.Forms.ToolStripButton();
            this.HashGenBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.archiveTextBox,
            this.toolStripSeparator1,
            this.openBCSVBtn,
            this.openExternalBtn,
            this.recentFilesListDropDown,
            this.saveBCSVBtn,
            this.saveAll_Btn,
            this.RemoveEntryBtn,
            this.HashGenBtn});
            this.toolStrip1.Location = new System.Drawing.Point(4, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1077, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(50, 22);
            this.toolStripLabel1.Text = "Archive:";
            // 
            // archiveTextBox
            // 
            this.archiveTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.archiveTextBox.Name = "archiveTextBox";
            this.archiveTextBox.Size = new System.Drawing.Size(550, 25);
            this.archiveTextBox.Text = "/ObjectData/Kuribo.arc";
            this.archiveTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.archiveTextBox_KeyPress);
            this.archiveTextBox.TextChanged += new System.EventHandler(this.archiveTextBox_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // openBCSVBtn
            // 
            this.openBCSVBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openBCSVBtn.Image = ((System.Drawing.Image)(resources.GetObject("openBCSVBtn.Image")));
            this.openBCSVBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openBCSVBtn.Name = "openBCSVBtn";
            this.openBCSVBtn.Size = new System.Drawing.Size(40, 22);
            this.openBCSVBtn.Text = "Open";
            this.openBCSVBtn.Click += new System.EventHandler(this.openBCSVBtn_Click);
            // 
            // openExternalBtn
            // 
            this.openExternalBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openExternalBtn.Image = ((System.Drawing.Image)(resources.GetObject("openExternalBtn.Image")));
            this.openExternalBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openExternalBtn.Name = "openExternalBtn";
            this.openExternalBtn.Size = new System.Drawing.Size(49, 22);
            this.openExternalBtn.Text = "Open...";
            this.openExternalBtn.Click += new System.EventHandler(this.openExternalBtn_Click);
            // 
            // recentFilesListDropDown
            // 
            this.recentFilesListDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.recentFilesListDropDown.Image = ((System.Drawing.Image)(resources.GetObject("recentFilesListDropDown.Image")));
            this.recentFilesListDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recentFilesListDropDown.Name = "recentFilesListDropDown";
            this.recentFilesListDropDown.Size = new System.Drawing.Size(82, 22);
            this.recentFilesListDropDown.Text = "Recent Files";
            this.recentFilesListDropDown.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.recentFilesListDropDown_DropDownItemClicked);
            // 
            // saveBCSVBtn
            // 
            this.saveBCSVBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveBCSVBtn.Enabled = false;
            this.saveBCSVBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveBCSVBtn.Image")));
            this.saveBCSVBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveBCSVBtn.Name = "saveBCSVBtn";
            this.saveBCSVBtn.Size = new System.Drawing.Size(35, 22);
            this.saveBCSVBtn.Text = "Save";
            this.saveBCSVBtn.ToolTipText = "Save the currently viewed BCSV file.";
            this.saveBCSVBtn.Click += new System.EventHandler(this.saveBCSVBtn_Click);
            // 
            // saveAll_Btn
            // 
            this.saveAll_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveAll_Btn.Enabled = false;
            this.saveAll_Btn.Image = ((System.Drawing.Image)(resources.GetObject("saveAll_Btn.Image")));
            this.saveAll_Btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAll_Btn.Name = "saveAll_Btn";
            this.saveAll_Btn.Size = new System.Drawing.Size(52, 22);
            this.saveAll_Btn.Text = "Save All";
            this.saveAll_Btn.ToolTipText = "Save All of the currently opened BCSV files.";
            this.saveAll_Btn.Click += new System.EventHandler(this.saveAll_Btn_Click);
            // 
            // filesystemView
            // 
            this.filesystemView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesystemView.Location = new System.Drawing.Point(0, 28);
            this.filesystemView.Name = "filesystemView";
            this.filesystemView.Size = new System.Drawing.Size(1128, 451);
            this.filesystemView.TabIndex = 2;
            this.filesystemView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.filesystemView_NodeMouseDoubleClick);
            // 
            // bcsvEditorsTabControl
            // 
            this.bcsvEditorsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bcsvEditorsTabControl.Location = new System.Drawing.Point(0, 485);
            this.bcsvEditorsTabControl.Name = "bcsvEditorsTabControl";
            this.bcsvEditorsTabControl.SelectedIndex = 0;
            this.bcsvEditorsTabControl.Size = new System.Drawing.Size(1128, 123);
            this.bcsvEditorsTabControl.TabIndex = 3;
            // 
            // RemoveEntryBtn
            // 
            this.RemoveEntryBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RemoveEntryBtn.Image = ((System.Drawing.Image)(resources.GetObject("RemoveEntryBtn.Image")));
            this.RemoveEntryBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RemoveEntryBtn.Name = "RemoveEntryBtn";
            this.RemoveEntryBtn.Size = new System.Drawing.Size(84, 22);
            this.RemoveEntryBtn.Text = "Remove Entry";
            this.RemoveEntryBtn.Click += new System.EventHandler(this.RemoveEntryBtn_Click);
            // 
            // HashGenBtn
            // 
            this.HashGenBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.HashGenBtn.Image = ((System.Drawing.Image)(resources.GetObject("HashGenBtn.Image")));
            this.HashGenBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.HashGenBtn.Name = "HashGenBtn";
            this.HashGenBtn.Size = new System.Drawing.Size(93, 22);
            this.HashGenBtn.Text = "Hash Generator";
            this.HashGenBtn.Click += new System.EventHandler(this.HashGenBtn_Click);
            // 
            // BCSVEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 608);
            this.Controls.Add(this.bcsvEditorsTabControl);
            this.Controls.Add(this.filesystemView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BCSVEditorForm";
            this.Text = "BCSVEditorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BCSVEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.BCSVEditorForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox archiveTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton openBCSVBtn;
        private System.Windows.Forms.ToolStripButton saveBCSVBtn;
        private System.Windows.Forms.TreeView filesystemView;
        private System.Windows.Forms.TabControl bcsvEditorsTabControl;
        private System.Windows.Forms.ToolStripButton saveAll_Btn;
        private System.Windows.Forms.ToolStripButton openExternalBtn;
        private System.Windows.Forms.ToolStripDropDownButton recentFilesListDropDown;
        private System.Windows.Forms.ToolStripButton RemoveEntryBtn;
        private System.Windows.Forms.ToolStripButton HashGenBtn;
    }
}