namespace Takochu.ui
{
    partial class StageObjectAttrFinder
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
            this.objectsDataGrid = new System.Windows.Forms.DataGridView();
            this.ObjectID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ObjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZoneName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.objectsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // objectsDataGrid
            // 
            this.objectsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.objectsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ObjectID,
            this.ObjectName,
            this.ZoneName,
            this.AttrName});
            this.objectsDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectsDataGrid.Location = new System.Drawing.Point(0, 0);
            this.objectsDataGrid.Name = "objectsDataGrid";
            this.objectsDataGrid.ReadOnly = true;
            this.objectsDataGrid.Size = new System.Drawing.Size(456, 450);
            this.objectsDataGrid.TabIndex = 0;
            // 
            // ObjectID
            // 
            this.ObjectID.HeaderText = "Object ID";
            this.ObjectID.Name = "ObjectID";
            this.ObjectID.ReadOnly = true;
            // 
            // ObjectName
            // 
            this.ObjectName.HeaderText = "Object Name";
            this.ObjectName.Name = "ObjectName";
            this.ObjectName.ReadOnly = true;
            // 
            // ZoneName
            // 
            this.ZoneName.HeaderText = "Zone Name";
            this.ZoneName.Name = "ZoneName";
            this.ZoneName.ReadOnly = true;
            // 
            // AttrName
            // 
            this.AttrName.HeaderText = "AttrName";
            this.AttrName.Name = "AttrName";
            this.AttrName.ReadOnly = true;
            // 
            // StageObjectAttrFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 450);
            this.Controls.Add(this.objectsDataGrid);
            this.Name = "StageObjectAttrFinder";
            this.Text = "StageObjectAttrFinder";
            ((System.ComponentModel.ISupportInitialize)(this.objectsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView objectsDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZoneName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttrName;
    }
}