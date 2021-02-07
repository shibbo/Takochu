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
            this.galaxyViewControl = new GL_EditorFramework.GL_Core.GL_ControlModern();
            this.scenarioTreeView = new System.Windows.Forms.TreeView();
            this.sceneListView = new GL_EditorFramework.SceneListView();
            this.objUIControl = new GL_EditorFramework.ObjectUIControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cameraUIControl = new GL_EditorFramework.ObjectUIControl();
            this.cameraListView = new GL_EditorFramework.SceneListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lightsUIControl = new GL_EditorFramework.ObjectUIControl();
            this.lightsSceneListView = new GL_EditorFramework.SceneListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.layerViewerDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.openMsgEditorButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.galaxyNameTxtBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.scenarioNameTxtBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // galaxyViewControl
            // 
            this.galaxyViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.galaxyViewControl.BackColor = System.Drawing.Color.Black;
            this.galaxyViewControl.CameraDistance = 10F;
            this.galaxyViewControl.CameraTarget = ((OpenTK.Vector3)(resources.GetObject("galaxyViewControl.CameraTarget")));
            this.galaxyViewControl.CamRotX = 0F;
            this.galaxyViewControl.CamRotY = 0F;
            this.galaxyViewControl.CurrentShader = null;
            this.galaxyViewControl.Fov = 0.7853982F;
            this.galaxyViewControl.Location = new System.Drawing.Point(358, 29);
            this.galaxyViewControl.Name = "galaxyViewControl";
            this.galaxyViewControl.NormPickingDepth = 0F;
            this.galaxyViewControl.ShowOrientationCube = true;
            this.galaxyViewControl.Size = new System.Drawing.Size(1148, 798);
            this.galaxyViewControl.Stereoscopy = GL_EditorFramework.GL_Core.GL_ControlBase.StereoscopyType.DISABLED;
            this.galaxyViewControl.TabIndex = 0;
            this.galaxyViewControl.VSync = false;
            this.galaxyViewControl.ZFar = 32000F;
            this.galaxyViewControl.ZNear = 0.32F;
            this.galaxyViewControl.Load += new System.EventHandler(this.galaxyViewControl_Load);
            // 
            // scenarioTreeView
            // 
            this.scenarioTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scenarioTreeView.Location = new System.Drawing.Point(0, 0);
            this.scenarioTreeView.Name = "scenarioTreeView";
            this.scenarioTreeView.Size = new System.Drawing.Size(342, 776);
            this.scenarioTreeView.TabIndex = 2;
            this.scenarioTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.scenarioTreeView_AfterSelect);
            this.scenarioTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.scenarioTreeView_NodeMouseClick);
            // 
            // sceneListView
            // 
            this.sceneListView.Location = new System.Drawing.Point(0, 0);
            this.sceneListView.Name = "sceneListView";
            this.sceneListView.RootLists = ((System.Collections.Generic.Dictionary<string, System.Collections.IList>)(resources.GetObject("sceneListView.RootLists")));
            this.sceneListView.Size = new System.Drawing.Size(342, 629);
            this.sceneListView.TabIndex = 4;
            this.sceneListView.ItemClicked += new GL_EditorFramework.ItemClickedEventHandler(this.sceneListView_ItemClicked);
            // 
            // objUIControl
            // 
            this.objUIControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.objUIControl.BackColor = System.Drawing.SystemColors.Control;
            this.objUIControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.objUIControl.Location = new System.Drawing.Point(-3, 635);
            this.objUIControl.Name = "objUIControl";
            this.objUIControl.Size = new System.Drawing.Size(342, 169);
            this.objUIControl.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(2, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(350, 802);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.scenarioTreeView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(342, 776);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Scenario";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sceneListView);
            this.tabPage1.Controls.Add(this.objUIControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(342, 776);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Objects";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cameraUIControl);
            this.tabPage2.Controls.Add(this.cameraListView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(342, 776);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cameras";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cameraUIControl
            // 
            this.cameraUIControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraUIControl.BackColor = System.Drawing.SystemColors.Control;
            this.cameraUIControl.Location = new System.Drawing.Point(0, 533);
            this.cameraUIControl.Name = "cameraUIControl";
            this.cameraUIControl.Size = new System.Drawing.Size(342, 243);
            this.cameraUIControl.TabIndex = 5;
            // 
            // cameraListView
            // 
            this.cameraListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraListView.Location = new System.Drawing.Point(0, 0);
            this.cameraListView.Name = "cameraListView";
            this.cameraListView.RootLists = ((System.Collections.Generic.Dictionary<string, System.Collections.IList>)(resources.GetObject("cameraListView.RootLists")));
            this.cameraListView.Size = new System.Drawing.Size(342, 527);
            this.cameraListView.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lightsUIControl);
            this.tabPage3.Controls.Add(this.lightsSceneListView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(342, 776);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Light";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lightsUIControl
            // 
            this.lightsUIControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightsUIControl.BackColor = System.Drawing.SystemColors.Control;
            this.lightsUIControl.Location = new System.Drawing.Point(0, 517);
            this.lightsUIControl.Name = "lightsUIControl";
            this.lightsUIControl.Size = new System.Drawing.Size(342, 256);
            this.lightsUIControl.TabIndex = 1;
            // 
            // lightsSceneListView
            // 
            this.lightsSceneListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lightsSceneListView.Location = new System.Drawing.Point(0, 0);
            this.lightsSceneListView.Name = "lightsSceneListView";
            this.lightsSceneListView.RootLists = ((System.Collections.Generic.Dictionary<string, System.Collections.IList>)(resources.GetObject("lightsSceneListView.RootLists")));
            this.lightsSceneListView.Size = new System.Drawing.Size(342, 511);
            this.lightsSceneListView.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerViewerDropDown,
            this.openMsgEditorButton,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.galaxyNameTxtBox,
            this.toolStripLabel2,
            this.scenarioNameTxtBox,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(358, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1100, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
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
            // galaxyNameTxtBox
            // 
            this.galaxyNameTxtBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.galaxyNameTxtBox.Name = "galaxyNameTxtBox";
            this.galaxyNameTxtBox.Size = new System.Drawing.Size(300, 25);
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
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(42, 22);
            this.toolStripButton3.Text = "Apply";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip2.Location = new System.Drawing.Point(6, 1);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(109, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(35, 22);
            this.toolStripButton4.Text = "Save";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(40, 22);
            this.toolStripButton5.Text = "Close";
            // 
            // EditorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1518, 831);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.galaxyViewControl);
            this.Name = "EditorWindow";
            this.Text = "EditorWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorWindow_FormClosing);
            this.Load += new System.EventHandler(this.EditorWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GL_EditorFramework.GL_Core.GL_ControlModern galaxyViewControl;
        private System.Windows.Forms.TreeView scenarioTreeView;
        private GL_EditorFramework.SceneListView sceneListView;
        private GL_EditorFramework.ObjectUIControl objUIControl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private GL_EditorFramework.SceneListView cameraListView;
        private GL_EditorFramework.ObjectUIControl cameraUIControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton layerViewerDropDown;
        private System.Windows.Forms.ToolStripButton openMsgEditorButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox galaxyNameTxtBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox scenarioNameTxtBox;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private GL_EditorFramework.ObjectUIControl lightsUIControl;
        private GL_EditorFramework.SceneListView lightsSceneListView;
    }
}