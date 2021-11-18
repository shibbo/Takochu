namespace Takochu.ui
{
    partial class EditorWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorWindow));
            this.scenarioTreeView = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.objectsListTreeView = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stageInformationBtn = new System.Windows.Forms.ToolStripButton();
            this.introCameraEditorBtn = new System.Windows.Forms.ToolStripButton();
            this.layerViewerDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.openMsgEditorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.galaxyNameTxtBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.scenarioNameTxtBox = new System.Windows.Forms.ToolStripTextBox();
            this.applyGalaxyNameBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.saveGalaxyBtn = new System.Windows.Forms.ToolStripButton();
            this.closeEditorBtn = new System.Windows.Forms.ToolStripButton();
            this.glLevelView = new OpenTK.GLControl();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // scenarioTreeView
            // 
            this.scenarioTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scenarioTreeView.Location = new System.Drawing.Point(0, 0);
            this.scenarioTreeView.Name = "scenarioTreeView";
            this.scenarioTreeView.Size = new System.Drawing.Size(342, 714);
            this.scenarioTreeView.TabIndex = 2;
            this.scenarioTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.scenarioTreeView_AfterSelect);
            this.scenarioTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.scenarioTreeView_NodeMouseClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(2, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(350, 740);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.scenarioTreeView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(342, 714);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Scenario";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(342, 714);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Zones";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toolStrip3);
            this.tabPage1.Controls.Add(this.objectsListTreeView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(342, 714);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Objects";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripButton1});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(336, 25);
            this.toolStrip3.TabIndex = 9;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(80, 22);
            this.toolStripDropDownButton1.Text = "Add Object";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(82, 22);
            this.toolStripButton1.Text = "Delete Object";
            // 
            // objectsListTreeView
            // 
            this.objectsListTreeView.Location = new System.Drawing.Point(6, 29);
            this.objectsListTreeView.Name = "objectsListTreeView";
            this.objectsListTreeView.Size = new System.Drawing.Size(330, 424);
            this.objectsListTreeView.TabIndex = 8;
            this.objectsListTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectsListTreeView_NodeMouseClick);
            this.objectsListTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectsListTreeView_NodeMouseDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(342, 714);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cameras";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(342, 714);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Light";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stageInformationBtn,
            this.introCameraEditorBtn,
            this.layerViewerDropDown,
            this.openMsgEditorButton,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.galaxyNameTxtBox,
            this.toolStripLabel2,
            this.scenarioNameTxtBox,
            this.applyGalaxyNameBtn});
            this.toolStrip1.Location = new System.Drawing.Point(358, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1272, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // stageInformationBtn
            // 
            this.stageInformationBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stageInformationBtn.Image = ((System.Drawing.Image)(resources.GetObject("stageInformationBtn.Image")));
            this.stageInformationBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stageInformationBtn.Name = "stageInformationBtn";
            this.stageInformationBtn.Size = new System.Drawing.Size(105, 22);
            this.stageInformationBtn.Text = "Stage Information";
            this.stageInformationBtn.Click += new System.EventHandler(this.stageInformationBtn_Click);
            // 
            // introCameraEditorBtn
            // 
            this.introCameraEditorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.introCameraEditorBtn.Enabled = false;
            this.introCameraEditorBtn.Image = ((System.Drawing.Image)(resources.GetObject("introCameraEditorBtn.Image")));
            this.introCameraEditorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.introCameraEditorBtn.Name = "introCameraEditorBtn";
            this.introCameraEditorBtn.Size = new System.Drawing.Size(70, 22);
            this.introCameraEditorBtn.Text = "Intro Editor";
            this.introCameraEditorBtn.Click += new System.EventHandler(this.introCameraEditorBtn_Click);
            // 
            // layerViewerDropDown
            // 
            this.layerViewerDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.layerViewerDropDown.Image = ((System.Drawing.Image)(resources.GetObject("layerViewerDropDown.Image")));
            this.layerViewerDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.layerViewerDropDown.Name = "layerViewerDropDown";
            this.layerViewerDropDown.Size = new System.Drawing.Size(76, 22);
            this.layerViewerDropDown.Text = "View Layer";
            // 
            // openMsgEditorButton
            // 
            this.openMsgEditorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openMsgEditorButton.Image = ((System.Drawing.Image)(resources.GetObject("openMsgEditorButton.Image")));
            this.openMsgEditorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openMsgEditorButton.Name = "openMsgEditorButton";
            this.openMsgEditorButton.Size = new System.Drawing.Size(91, 22);
            this.openMsgEditorButton.Text = "Message Editor";
            this.openMsgEditorButton.Click += new System.EventHandler(this.openMsgEditorButton_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(111, 22);
            this.toolStripButton2.Text = "Import New Object";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(79, 22);
            this.toolStripLabel1.Text = "Galaxy Name:";
            // 
            // galaxyNameTxtBox
            // 
            this.galaxyNameTxtBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.galaxyNameTxtBox.Name = "galaxyNameTxtBox";
            this.galaxyNameTxtBox.Size = new System.Drawing.Size(300, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(85, 22);
            this.toolStripLabel2.Text = "Mission Name:";
            // 
            // scenarioNameTxtBox
            // 
            this.scenarioNameTxtBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.scenarioNameTxtBox.Name = "scenarioNameTxtBox";
            this.scenarioNameTxtBox.Size = new System.Drawing.Size(300, 25);
            // 
            // applyGalaxyNameBtn
            // 
            this.applyGalaxyNameBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.applyGalaxyNameBtn.Enabled = false;
            this.applyGalaxyNameBtn.Image = ((System.Drawing.Image)(resources.GetObject("applyGalaxyNameBtn.Image")));
            this.applyGalaxyNameBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.applyGalaxyNameBtn.Name = "applyGalaxyNameBtn";
            this.applyGalaxyNameBtn.Size = new System.Drawing.Size(42, 22);
            this.applyGalaxyNameBtn.Text = "Apply";
            this.applyGalaxyNameBtn.Click += new System.EventHandler(this.applyGalaxyNameBtn_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveGalaxyBtn,
            this.closeEditorBtn});
            this.toolStrip2.Location = new System.Drawing.Point(6, 1);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(77, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // saveGalaxyBtn
            // 
            this.saveGalaxyBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveGalaxyBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveGalaxyBtn.Image")));
            this.saveGalaxyBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveGalaxyBtn.Name = "saveGalaxyBtn";
            this.saveGalaxyBtn.Size = new System.Drawing.Size(35, 22);
            this.saveGalaxyBtn.Text = "Save";
            this.saveGalaxyBtn.Click += new System.EventHandler(this.saveGalaxyBtn_Click);
            // 
            // closeEditorBtn
            // 
            this.closeEditorBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.closeEditorBtn.Image = ((System.Drawing.Image)(resources.GetObject("closeEditorBtn.Image")));
            this.closeEditorBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeEditorBtn.Name = "closeEditorBtn";
            this.closeEditorBtn.Size = new System.Drawing.Size(39, 22);
            this.closeEditorBtn.Text = "Close";
            this.closeEditorBtn.Click += new System.EventHandler(this.closeEditorBtn_Click);
            // 
            // glLevelView
            // 
            this.glLevelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glLevelView.BackColor = System.Drawing.Color.Black;
            this.glLevelView.Location = new System.Drawing.Point(358, 27);
            this.glLevelView.Name = "glLevelView";
            this.glLevelView.Size = new System.Drawing.Size(1472, 737);
            this.glLevelView.TabIndex = 9;
            this.glLevelView.VSync = false;
            this.glLevelView.Load += new System.EventHandler(this.glLevelView_Load);
            this.glLevelView.Paint += new System.Windows.Forms.PaintEventHandler(this.glLevelView_Paint);
            this.glLevelView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseDown);
            this.glLevelView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseMove);
            this.glLevelView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseUp);
            this.glLevelView.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseWheel);
            this.glLevelView.Resize += new System.EventHandler(this.glLevelView_Resize);
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1842, 767);
            this.Controls.Add(this.glLevelView);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "EditorWindow";
            this.Text = "EditorWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorWindow_FormClosing);
            this.Load += new System.EventHandler(this.EditorWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView scenarioTreeView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton layerViewerDropDown;
        private System.Windows.Forms.ToolStripButton openMsgEditorButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox galaxyNameTxtBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox scenarioNameTxtBox;
        private System.Windows.Forms.ToolStripButton applyGalaxyNameBtn;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton saveGalaxyBtn;
        private System.Windows.Forms.ToolStripButton closeEditorBtn;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripButton stageInformationBtn;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ToolStripButton introCameraEditorBtn;
        private System.Windows.Forms.TreeView objectsListTreeView;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private OpenTK.GLControl glLevelView;
    }
}