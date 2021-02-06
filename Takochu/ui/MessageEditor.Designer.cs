namespace Takochu.util
{
    partial class MessageEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelsComboBox = new System.Windows.Forms.ComboBox();
            this.labelTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.zoneNamesComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Labels:";
            // 
            // labelsComboBox
            // 
            this.labelsComboBox.FormattingEnabled = true;
            this.labelsComboBox.Location = new System.Drawing.Point(56, 37);
            this.labelsComboBox.Name = "labelsComboBox";
            this.labelsComboBox.Size = new System.Drawing.Size(574, 21);
            this.labelsComboBox.TabIndex = 1;
            this.labelsComboBox.SelectedIndexChanged += new System.EventHandler(this.labelsComboBox_SelectedIndexChanged);
            // 
            // labelTextBox
            // 
            this.labelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTextBox.Location = new System.Drawing.Point(12, 64);
            this.labelTextBox.Name = "labelTextBox";
            this.labelTextBox.Size = new System.Drawing.Size(618, 374);
            this.labelTextBox.TabIndex = 2;
            this.labelTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Zone:";
            // 
            // zoneNamesComboBox
            // 
            this.zoneNamesComboBox.FormattingEnabled = true;
            this.zoneNamesComboBox.Location = new System.Drawing.Point(56, 10);
            this.zoneNamesComboBox.Name = "zoneNamesComboBox";
            this.zoneNamesComboBox.Size = new System.Drawing.Size(574, 21);
            this.zoneNamesComboBox.TabIndex = 4;
            this.zoneNamesComboBox.SelectedIndexChanged += new System.EventHandler(this.zoneNamesComboBox_SelectedIndexChanged);
            // 
            // MessageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 450);
            this.Controls.Add(this.zoneNamesComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTextBox);
            this.Controls.Add(this.labelsComboBox);
            this.Controls.Add(this.label1);
            this.Name = "MessageEditor";
            this.Text = "MessageEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox labelsComboBox;
        private System.Windows.Forms.RichTextBox labelTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox zoneNamesComboBox;
    }
}