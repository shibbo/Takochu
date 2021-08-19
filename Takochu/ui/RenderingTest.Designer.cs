namespace Takochu.ui
{
    partial class RenderingTest
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.glLevelView = new OpenTK.GLControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // glLevelView
            // 
            this.glLevelView.BackColor = System.Drawing.Color.Black;
            this.glLevelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glLevelView.Location = new System.Drawing.Point(0, 0);
            this.glLevelView.Name = "glLevelView";
            this.glLevelView.Size = new System.Drawing.Size(800, 450);
            this.glLevelView.TabIndex = 0;
            this.glLevelView.VSync = false;
            this.glLevelView.Load += new System.EventHandler(this.glLevelView_Load);
            this.glLevelView.Paint += new System.Windows.Forms.PaintEventHandler(this.glLevelView_Paint);
            this.glLevelView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseDown);
            this.glLevelView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseMove);
            this.glLevelView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseUp);
            this.glLevelView.Resize += new System.EventHandler(this.glLevelView_Resize);
            this.glLevelView.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glLevelView_MouseWheel);
            // 
            // RenderingTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.glLevelView);
            this.Name = "RenderingTest";
            this.Text = "RenderingTest";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private OpenTK.GLControl glLevelView;
    }
}