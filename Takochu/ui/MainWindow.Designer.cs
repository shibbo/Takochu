namespace Takochu
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.selectGameFolderBtn = new System.Windows.Forms.ToolStripButton();
            this.bcsvEditorBtn = new System.Windows.Forms.ToolStripButton();
            this.rarcExplorer_Btn = new System.Windows.Forms.ToolStripButton();
            this.showMessageEditorBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.settingsBtn = new System.Windows.Forms.ToolStripButton();
            this.galaxyTreeView = new System.Windows.Forms.TreeView();
            this.glControl1 = new OpenTK.GLControl();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectGameFolderBtn,
            this.bcsvEditorBtn,
            this.rarcExplorer_Btn,
            this.showMessageEditorBtn,
            this.toolStripButton1,
            this.settingsBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(800, 25);
            this.mainToolStrip.Stretch = true;
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // selectGameFolderBtn
            // 
            this.selectGameFolderBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.selectGameFolderBtn.Image = ((System.Drawing.Image)(resources.GetObject("selectGameFolderBtn.Image")));
            this.selectGameFolderBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectGameFolderBtn.Name = "selectGameFolderBtn";
            this.selectGameFolderBtn.Size = new System.Drawing.Size(111, 22);
            this.selectGameFolderBtn.Text = "Select Game Folder";
            this.selectGameFolderBtn.Click += new System.EventHandler(this.selectGameFolderBtn_Click);
            // 
            // bcsvEditorBtn
            // 
            this.bcsvEditorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bcsvEditorBtn.Enabled = false;
            this.bcsvEditorBtn.Image = ((System.Drawing.Image)(resources.GetObject("bcsvEditorBtn.Image")));
            this.bcsvEditorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bcsvEditorBtn.Name = "bcsvEditorBtn";
            this.bcsvEditorBtn.Size = new System.Drawing.Size(72, 22);
            this.bcsvEditorBtn.Text = "BCSV Editor";
            this.bcsvEditorBtn.Click += new System.EventHandler(this.BcsvEditorBtn_Click);
            // 
            // rarcExplorer_Btn
            // 
            this.rarcExplorer_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.rarcExplorer_Btn.Image = ((System.Drawing.Image)(resources.GetObject("rarcExplorer_Btn.Image")));
            this.rarcExplorer_Btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rarcExplorer_Btn.Name = "rarcExplorer_Btn";
            this.rarcExplorer_Btn.Size = new System.Drawing.Size(86, 22);
            this.rarcExplorer_Btn.Text = "RARC Explorer";
            this.rarcExplorer_Btn.Click += new System.EventHandler(this.rarcExplorer_Btn_Click);
            // 
            // showMessageEditorBtn
            // 
            this.showMessageEditorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.showMessageEditorBtn.Image = ((System.Drawing.Image)(resources.GetObject("showMessageEditorBtn.Image")));
            this.showMessageEditorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.showMessageEditorBtn.Name = "showMessageEditorBtn";
            this.showMessageEditorBtn.Size = new System.Drawing.Size(91, 22);
            this.showMessageEditorBtn.Text = "Message Editor";
            this.showMessageEditorBtn.Click += new System.EventHandler(this.showMessageEditorBtn_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(78, 22);
            this.toolStripButton1.Text = "Model Thing";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("settingsBtn.Image")));
            this.settingsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(53, 22);
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // galaxyTreeView
            // 
            this.galaxyTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.galaxyTreeView.Location = new System.Drawing.Point(0, 25);
            this.galaxyTreeView.Name = "galaxyTreeView";
            this.galaxyTreeView.Size = new System.Drawing.Size(800, 390);
            this.galaxyTreeView.TabIndex = 1;
            this.galaxyTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.galaxyTreeView_NodeMouseDoubleClick);
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(0, 0);
            this.glControl1.TabIndex = 2;
            this.glControl1.VSync = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 415);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.galaxyTreeView);
            this.Controls.Add(this.mainToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Takochu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton selectGameFolderBtn;
        private System.Windows.Forms.ToolStripButton bcsvEditorBtn;
        private System.Windows.Forms.TreeView galaxyTreeView;
        private System.Windows.Forms.ToolStripButton rarcExplorer_Btn;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.ToolStripButton showMessageEditorBtn;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton settingsBtn;
    }
}

