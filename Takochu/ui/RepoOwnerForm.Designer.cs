
namespace Takochu.ui
{
    partial class RepoOwnerForm
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
            this.UserTreeView = new System.Windows.Forms.TreeView();
            this.UserLabel = new System.Windows.Forms.Label();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserTreeView
            // 
            this.UserTreeView.Location = new System.Drawing.Point(12, 25);
            this.UserTreeView.Name = "UserTreeView";
            this.UserTreeView.Size = new System.Drawing.Size(121, 115);
            this.UserTreeView.TabIndex = 0;
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(42, 9);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(48, 13);
            this.UserLabel.TabIndex = 1;
            this.UserLabel.Text = "User List";
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Location = new System.Drawing.Point(139, 25);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(110, 52);
            this.InfoLabel.TabIndex = 2;
            this.InfoLabel.Text = "Select one \r\nof the usernames in\r\nthe list and then press\r\nthe OK button.";
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(139, 80);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(96, 60);
            this.ConfirmButton.TabIndex = 3;
            this.ConfirmButton.Text = "OK";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // RepoOwnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 148);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.UserTreeView);
            this.Name = "RepoOwnerForm";
            this.Text = "Updater";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView UserTreeView;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.Button ConfirmButton;
    }
}