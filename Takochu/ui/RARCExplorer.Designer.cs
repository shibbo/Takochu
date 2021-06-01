namespace Takochu.ui
{
    partial class RARCExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RARCExplorer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.rarcName_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.openRARC_Btn = new System.Windows.Forms.ToolStripButton();
            this.export_Btn = new System.Windows.Forms.ToolStripButton();
            this.rarc_TreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openExternal = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.rarcName_TextBox,
            this.openRARC_Btn,
            this.openExternal,
            this.export_Btn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1033, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(28, 22);
            this.toolStripLabel1.Text = "File:";
            // 
            // rarcName_TextBox
            // 
            this.rarcName_TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rarcName_TextBox.Name = "rarcName_TextBox";
            this.rarcName_TextBox.Size = new System.Drawing.Size(700, 25);
            this.rarcName_TextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rarcName_TextBox_KeyPress);
            this.rarcName_TextBox.TextChanged += new System.EventHandler(this.rarcName_TextBox_TextChanged);
            // 
            // openRARC_Btn
            // 
            this.openRARC_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openRARC_Btn.Image = ((System.Drawing.Image)(resources.GetObject("openRARC_Btn.Image")));
            this.openRARC_Btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openRARC_Btn.Name = "openRARC_Btn";
            this.openRARC_Btn.Size = new System.Drawing.Size(40, 22);
            this.openRARC_Btn.Text = "Open";
            this.openRARC_Btn.Click += new System.EventHandler(this.openRARC_Btn_Click);
            // 
            // export_Btn
            // 
            this.export_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.export_Btn.Enabled = false;
            this.export_Btn.Image = ((System.Drawing.Image)(resources.GetObject("export_Btn.Image")));
            this.export_Btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.export_Btn.Name = "export_Btn";
            this.export_Btn.Size = new System.Drawing.Size(45, 22);
            this.export_Btn.Text = "Export";
            this.export_Btn.Click += new System.EventHandler(this.export_Btn_Click);
            // 
            // rarc_TreeView
            // 
            this.rarc_TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rarc_TreeView.Location = new System.Drawing.Point(0, 25);
            this.rarc_TreeView.Name = "rarc_TreeView";
            this.rarc_TreeView.Size = new System.Drawing.Size(1033, 425);
            this.rarc_TreeView.TabIndex = 1;
            this.rarc_TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.rarc_TreeView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // openExternal
            // 
            this.openExternal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.openExternal.Image = ((System.Drawing.Image)(resources.GetObject("openExternal.Image")));
            this.openExternal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openExternal.Name = "openExternal";
            this.openExternal.Size = new System.Drawing.Size(49, 22);
            this.openExternal.Text = "Open...";
            this.openExternal.Click += new System.EventHandler(this.openExternal_Click);
            // 
            // RARCExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 450);
            this.Controls.Add(this.rarc_TreeView);
            this.Controls.Add(this.toolStrip1);
            this.Name = "RARCExplorer";
            this.Text = "RARCExplorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RARCExplorer_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox rarcName_TextBox;
        private System.Windows.Forms.ToolStripButton openRARC_Btn;
        private System.Windows.Forms.TreeView rarc_TreeView;
        private System.Windows.Forms.ToolStripButton export_Btn;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton openExternal;
    }
}