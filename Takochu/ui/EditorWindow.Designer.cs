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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorWindow));
            this.scenarioTreeView = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.zonesDataGridView = new System.Windows.Forms.DataGridView();
            this.zonesListTreeView = new System.Windows.Forms.TreeView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.deleteObjButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.objectsListTreeView = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.camerasDataGridView = new System.Windows.Forms.DataGridView();
            this.cameraListTreeView = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lightsDataGridView = new System.Windows.Forms.DataGridView();
            this.lightsTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stageInformationBtn = new System.Windows.Forms.ToolStripButton();
            this.introCameraEditorBtn = new System.Windows.Forms.ToolStripButton();
            this.layerViewerDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.openMsgEditorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.GalaxyNameTxtBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.scenarioNameTxtBox = new System.Windows.Forms.ToolStripTextBox();
            this.applyGalaxyNameBtn = new System.Windows.Forms.ToolStripButton();
            this.glLevelView = new OpenTK.GLControl();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attrFinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditorWindowStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.OpenSaveStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zonesDataGridView)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.camerasDataGridView)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lightsDataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            this.EditorWindowStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // scenarioTreeView
            // 
            this.scenarioTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scenarioTreeView.Location = new System.Drawing.Point(0, 0);
            this.scenarioTreeView.Name = "scenarioTreeView";
            this.scenarioTreeView.Size = new System.Drawing.Size(342, 749);
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
            this.tabControl1.Location = new System.Drawing.Point(2, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(350, 775);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.scenarioTreeView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(342, 749);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Scenario";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.zonesDataGridView);
            this.tabPage5.Controls.Add(this.zonesListTreeView);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(342, 749);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Zone Attributes";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.tabPage5.SizeChanged += new System.EventHandler(this.TabPage_SizeChanged);
            // 
            // zonesDataGridView
            // 
            this.zonesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zonesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.zonesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.zonesDataGridView.Location = new System.Drawing.Point(6, 462);
            this.zonesDataGridView.Name = "zonesDataGridView";
            this.zonesDataGridView.RowHeadersVisible = false;
            this.zonesDataGridView.Size = new System.Drawing.Size(333, 282);
            this.zonesDataGridView.TabIndex = 1;
            // 
            // zonesListTreeView
            // 
            this.zonesListTreeView.Location = new System.Drawing.Point(3, 3);
            this.zonesListTreeView.Name = "zonesListTreeView";
            this.zonesListTreeView.Size = new System.Drawing.Size(336, 453);
            this.zonesListTreeView.TabIndex = 0;
            this.zonesListTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.zonesListTreeView_NodeMouseClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.toolStrip3);
            this.tabPage1.Controls.Add(this.objectsListTreeView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(342, 749);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Objects";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.SizeChanged += new System.EventHandler(this.TabPage_SizeChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 432);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(330, 308);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView1_CurrentCellDirtyStateChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.deleteObjButton,
            this.toolStripLabel3});
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
            // deleteObjButton
            // 
            this.deleteObjButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteObjButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteObjButton.Image")));
            this.deleteObjButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteObjButton.Name = "deleteObjButton";
            this.deleteObjButton.Size = new System.Drawing.Size(82, 22);
            this.deleteObjButton.Text = "Delete Object";
            this.deleteObjButton.Click += new System.EventHandler(this.deleteObjButton_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel3.Text = "Debug";
            this.toolStripLabel3.Click += new System.EventHandler(this.toolStripLabel3_Click);
            // 
            // objectsListTreeView
            // 
            this.objectsListTreeView.Location = new System.Drawing.Point(6, 31);
            this.objectsListTreeView.Name = "objectsListTreeView";
            this.objectsListTreeView.Size = new System.Drawing.Size(330, 395);
            this.objectsListTreeView.TabIndex = 8;
            this.objectsListTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectsListTreeView_NodeMouseClick);
            this.objectsListTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.objectsListTreeView_NodeMouseDoubleClick);
            this.objectsListTreeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.objectsListTreeView_KeyUp);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.camerasDataGridView);
            this.tabPage2.Controls.Add(this.cameraListTreeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(342, 749);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cameras";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.SizeChanged += new System.EventHandler(this.TabPage_SizeChanged);
            // 
            // camerasDataGridView
            // 
            this.camerasDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.camerasDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.camerasDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.camerasDataGridView.Location = new System.Drawing.Point(6, 378);
            this.camerasDataGridView.Name = "camerasDataGridView";
            this.camerasDataGridView.RowHeadersVisible = false;
            this.camerasDataGridView.Size = new System.Drawing.Size(330, 362);
            this.camerasDataGridView.TabIndex = 1;
            // 
            // cameraListTreeView
            // 
            this.cameraListTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraListTreeView.Location = new System.Drawing.Point(6, 3);
            this.cameraListTreeView.Name = "cameraListTreeView";
            this.cameraListTreeView.Size = new System.Drawing.Size(330, 369);
            this.cameraListTreeView.TabIndex = 0;
            this.cameraListTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.cameraListTreeView_NodeMouseClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lightsDataGridView);
            this.tabPage3.Controls.Add(this.lightsTreeView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(342, 749);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Light";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.SizeChanged += new System.EventHandler(this.TabPage_SizeChanged);
            // 
            // lightsDataGridView
            // 
            this.lightsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lightsDataGridView.Location = new System.Drawing.Point(6, 347);
            this.lightsDataGridView.Name = "lightsDataGridView";
            this.lightsDataGridView.Size = new System.Drawing.Size(333, 399);
            this.lightsDataGridView.TabIndex = 1;
            // 
            // lightsTreeView
            // 
            this.lightsTreeView.Location = new System.Drawing.Point(6, 3);
            this.lightsTreeView.Name = "lightsTreeView";
            this.lightsTreeView.Size = new System.Drawing.Size(333, 338);
            this.lightsTreeView.TabIndex = 0;
            this.lightsTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.lightsTreeView_NodeMouseClick);
            this.lightsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.lightsTreeView_NodeMouseDoubleClick);
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
            this.GalaxyNameTxtBox,
            this.toolStripLabel2,
            this.scenarioNameTxtBox,
            this.applyGalaxyNameBtn});
            this.toolStrip1.Location = new System.Drawing.Point(358, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1276, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // stageInformationBtn
            // 
            this.stageInformationBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stageInformationBtn.Image = ((System.Drawing.Image)(resources.GetObject("stageInformationBtn.Image")));
            this.stageInformationBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stageInformationBtn.Name = "stageInformationBtn";
            this.stageInformationBtn.Size = new System.Drawing.Size(106, 22);
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
            this.toolStripButton2.Size = new System.Drawing.Size(112, 22);
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
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "Galaxy Name:";
            // 
            // GalaxyNameTxtBox
            // 
            this.GalaxyNameTxtBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.GalaxyNameTxtBox.Name = "GalaxyNameTxtBox";
            this.GalaxyNameTxtBox.Size = new System.Drawing.Size(300, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(86, 22);
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
            // glLevelView
            // 
            this.glLevelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glLevelView.BackColor = System.Drawing.Color.Black;
            this.glLevelView.Location = new System.Drawing.Point(358, 29);
            this.glLevelView.Name = "glLevelView";
            this.glLevelView.Size = new System.Drawing.Size(1472, 775);
            this.glLevelView.TabIndex = 9;
            this.glLevelView.VSync = false;
            this.glLevelView.Load += new System.EventHandler(this.glLevelView_Load);
            this.glLevelView.SizeChanged += new System.EventHandler(this.glLevelView_Resize);
            this.glLevelView.Paint += new System.Windows.Forms.PaintEventHandler(this.glLevelView_Paint);
            this.glLevelView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseClick);
            this.glLevelView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseDown);
            this.glLevelView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseMove);
            this.glLevelView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseUp);
            this.glLevelView.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseWheel);
            this.glLevelView.Resize += new System.EventHandler(this.glLevelView_Resize);
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.DisplayToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(1842, 24);
            this.MainMenuStrip.TabIndex = 11;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileToolStripMenuItem.Text = "File";
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.SaveToolStripMenuItem.Text = "Save";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // DisplayToolStripMenuItem
            // 
            this.DisplayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AreaToolStripMenuItem,
            this.pathsToolStripMenuItem});
            this.DisplayToolStripMenuItem.Name = "DisplayToolStripMenuItem";
            this.DisplayToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.DisplayToolStripMenuItem.Text = "Display";
            // 
            // AreaToolStripMenuItem
            // 
            this.AreaToolStripMenuItem.Checked = true;
            this.AreaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AreaToolStripMenuItem.Name = "AreaToolStripMenuItem";
            this.AreaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.AreaToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.AreaToolStripMenuItem.Text = "Area";
            this.AreaToolStripMenuItem.Click += new System.EventHandler(this.AreaToolStripMenuItem_Click);
            // 
            // pathsToolStripMenuItem
            // 
            this.pathsToolStripMenuItem.Checked = true;
            this.pathsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pathsToolStripMenuItem.Name = "pathsToolStripMenuItem";
            this.pathsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.pathsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.pathsToolStripMenuItem.Text = "Paths";
            this.pathsToolStripMenuItem.Click += new System.EventHandler(this.pathsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attrFinderToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // attrFinderToolStripMenuItem
            // 
            this.attrFinderToolStripMenuItem.Enabled = false;
            this.attrFinderToolStripMenuItem.Name = "attrFinderToolStripMenuItem";
            this.attrFinderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.attrFinderToolStripMenuItem.Text = "Attribute Finder";
            this.attrFinderToolStripMenuItem.Click += new System.EventHandler(this.attrFinderToolStripMenuItem_Click);
            // 
            // EditorWindowStatusStrip
            // 
            this.EditorWindowStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.OpenSaveStatusLabel,
            this.toolStripStatusLabel3});
            this.EditorWindowStatusStrip.Location = new System.Drawing.Point(0, 809);
            this.EditorWindowStatusStrip.Name = "EditorWindowStatusStrip";
            this.EditorWindowStatusStrip.Size = new System.Drawing.Size(1842, 22);
            this.EditorWindowStatusStrip.TabIndex = 12;
            this.EditorWindowStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "State：";
            // 
            // OpenSaveStatusLabel
            // 
            this.OpenSaveStatusLabel.Name = "OpenSaveStatusLabel";
            this.OpenSaveStatusLabel.Size = new System.Drawing.Size(71, 17);
            this.OpenSaveStatusLabel.Text = "OpenGalaxy";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(26, 17);
            this.toolStripStatusLabel3.Text = "test";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1842, 831);
            this.Controls.Add(this.glLevelView);
            this.Controls.Add(this.EditorWindowStatusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.MainMenuStrip);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorWindow";
            this.Text = "EditorWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorWindow_FormClosing);
            this.Load += new System.EventHandler(this.EditorWindow_Load);
            this.Resize += new System.EventHandler(this.glLevelView_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.zonesDataGridView)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.camerasDataGridView)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lightsDataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.EditorWindowStatusStrip.ResumeLayout(false);
            this.EditorWindowStatusStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStripTextBox GalaxyNameTxtBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox scenarioNameTxtBox;
        private System.Windows.Forms.ToolStripButton applyGalaxyNameBtn;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripButton stageInformationBtn;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ToolStripButton introCameraEditorBtn;
        private System.Windows.Forms.TreeView objectsListTreeView;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton deleteObjButton;
        private OpenTK.GLControl glLevelView;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip EditorWindowStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel OpenSaveStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.DataGridView camerasDataGridView;
        private System.Windows.Forms.TreeView cameraListTreeView;
        private System.Windows.Forms.DataGridView zonesDataGridView;
        private System.Windows.Forms.TreeView zonesListTreeView;
        private System.Windows.Forms.DataGridView lightsDataGridView;
        private System.Windows.Forms.TreeView lightsTreeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pathsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attrFinderToolStripMenuItem;
    }
}