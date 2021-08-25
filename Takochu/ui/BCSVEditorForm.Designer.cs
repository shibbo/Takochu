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
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.fieldTypesComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addFieldBtn = new System.Windows.Forms.ToolStripButton();
            this.newFieldNameTxt = new System.Windows.Forms.ToolStripTextBox();
            this.deleteFieldBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.deletionFieldTxt = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
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
            this.saveAll_Btn});
            this.toolStrip1.Location = new System.Drawing.Point(4, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1069, 25);
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
            this.archiveTextBox.Size = new System.Drawing.Size(750, 25);
            this.archiveTextBox.Text = "/ObjectData/Kuribo.arc";
            this.archiveTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.archiveTextBox_KeyPress);
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
            this.filesystemView.Location = new System.Drawing.Point(0, 53);
            this.filesystemView.Name = "filesystemView";
            this.filesystemView.Size = new System.Drawing.Size(1709, 426);
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
            this.bcsvEditorsTabControl.Size = new System.Drawing.Size(1709, 123);
            this.bcsvEditorsTabControl.TabIndex = 3;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.newFieldNameTxt,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.fieldTypesComboBox,
            this.toolStripSeparator3,
            this.addFieldBtn,
            this.toolStripSeparator4,
            this.toolStripLabel4,
            this.deletionFieldTxt,
            this.deleteFieldBtn});
            this.toolStrip2.Location = new System.Drawing.Point(4, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(907, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(97, 22);
            this.toolStripLabel2.Text = "New Field Name:";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabel3.Text = "Field Type:";
            // 
            // fieldTypesComboBox
            // 
            this.fieldTypesComboBox.Items.AddRange(new object[] {
            "Integer [0]",
            "Integer [3]",
            "Short",
            "Byte",
            "String",
            "Float"});
            this.fieldTypesComboBox.Name = "fieldTypesComboBox";
            this.fieldTypesComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // addFieldBtn
            // 
            this.addFieldBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addFieldBtn.Enabled = false;
            this.addFieldBtn.Image = ((System.Drawing.Image)(resources.GetObject("addFieldBtn.Image")));
            this.addFieldBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addFieldBtn.Name = "addFieldBtn";
            this.addFieldBtn.Size = new System.Drawing.Size(61, 22);
            this.addFieldBtn.Text = "Add Field";
            this.addFieldBtn.Click += new System.EventHandler(this.addFieldBtn_Click);
            // 
            // newFieldNameTxt
            // 
            this.newFieldNameTxt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.newFieldNameTxt.Name = "newFieldNameTxt";
            this.newFieldNameTxt.Size = new System.Drawing.Size(200, 25);
            // 
            // deleteFieldBtn
            // 
            this.deleteFieldBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.deleteFieldBtn.Image = ((System.Drawing.Image)(resources.GetObject("deleteFieldBtn.Image")));
            this.deleteFieldBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteFieldBtn.Name = "deleteFieldBtn";
            this.deleteFieldBtn.Size = new System.Drawing.Size(72, 22);
            this.deleteFieldBtn.Text = "Delete Field";
            this.deleteFieldBtn.Click += new System.EventHandler(this.deleteFieldBtn_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel4.Text = "Field To Delete:";
            // 
            // deletionFieldTxt
            // 
            this.deletionFieldTxt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.deletionFieldTxt.Name = "deletionFieldTxt";
            this.deletionFieldTxt.Size = new System.Drawing.Size(150, 25);
            // 
            // BCSVEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1709, 608);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.bcsvEditorsTabControl);
            this.Controls.Add(this.filesystemView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BCSVEditorForm";
            this.Text = "BCSVEditorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BCSVEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.BCSVEditorForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
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
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox newFieldNameTxt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox fieldTypesComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton addFieldBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton deleteFieldBtn;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox deletionFieldTxt;
    }
}