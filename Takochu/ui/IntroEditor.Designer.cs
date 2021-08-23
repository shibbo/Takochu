
namespace Takochu.ui
{
    partial class IntroEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntroEditor));
            this.framesList = new System.Windows.Forms.ListBox();
            this.framesDataGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveBtn = new System.Windows.Forms.ToolStripButton();
            this.closeBtn = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.framesDataGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // framesList
            // 
            this.framesList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.framesList.FormattingEnabled = true;
            this.framesList.Items.AddRange(new object[] {
            "pos_x",
            "pos_y",
            "pos_z",
            "dir_x",
            "dir_y",
            "dir_z",
            "twist",
            "fovy"});
            this.framesList.Location = new System.Drawing.Point(0, 25);
            this.framesList.Name = "framesList";
            this.framesList.Size = new System.Drawing.Size(205, 459);
            this.framesList.TabIndex = 0;
            this.framesList.SelectedIndexChanged += new System.EventHandler(this.framesList_SelectedIndexChanged);
            // 
            // framesDataGrid
            // 
            this.framesDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.framesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.framesDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.framesDataGrid.Location = new System.Drawing.Point(211, 25);
            this.framesDataGrid.Name = "framesDataGrid";
            this.framesDataGrid.Size = new System.Drawing.Size(357, 458);
            this.framesDataGrid.TabIndex = 1;
            this.framesDataGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.framesDataGrid_RowsAdded);
            this.framesDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.framesDataGrid_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Time";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Value";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Velocity";
            this.Column3.Name = "Column3";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveBtn,
            this.closeBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(571, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveBtn
            // 
            this.saveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveBtn.Image")));
            this.saveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(35, 22);
            this.saveBtn.Text = "Save";
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.closeBtn.Image = ((System.Drawing.Image)(resources.GetObject("closeBtn.Image")));
            this.closeBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(40, 22);
            this.closeBtn.Text = "Close";
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // IntroEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 483);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.framesDataGrid);
            this.Controls.Add(this.framesList);
            this.Name = "IntroEditor";
            this.Text = "IntroEditor";
            ((System.ComponentModel.ISupportInitialize)(this.framesDataGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox framesList;
        private System.Windows.Forms.DataGridView framesDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private System.Windows.Forms.ToolStripButton closeBtn;
    }
}