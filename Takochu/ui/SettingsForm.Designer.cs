
namespace Takochu.ui
{
    partial class SettingsForm
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
            this.GamePathTextBox = new System.Windows.Forms.TextBox();
            this.updateGamePathBtn = new System.Windows.Forms.Button();
            this.DBGroupBox = new System.Windows.Forms.GroupBox();
            this.DbInfoLbl = new System.Windows.Forms.Label();
            this.RegenDBBtn = new System.Windows.Forms.Button();
            this.editorGroupBox = new System.Windows.Forms.GroupBox();
            this.ShowArgs = new System.Windows.Forms.CheckBox();
            this.miscGroupBox = new System.Windows.Forms.GroupBox();
            this.useDevCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LanguageComboBox = new System.Windows.Forms.ComboBox();
            this.tryUpdateBtn = new System.Windows.Forms.Button();
            this.useInternalNames = new System.Windows.Forms.CheckBox();
            this.DBGroupBox.SuspendLayout();
            this.editorGroupBox.SuspendLayout();
            this.miscGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Game Path:";
            // 
            // GamePathTextBox
            // 
            this.GamePathTextBox.Location = new System.Drawing.Point(102, 17);
            this.GamePathTextBox.Name = "GamePathTextBox";
            this.GamePathTextBox.ReadOnly = true;
            this.GamePathTextBox.Size = new System.Drawing.Size(372, 20);
            this.GamePathTextBox.TabIndex = 1;
            // 
            // updateGamePathBtn
            // 
            this.updateGamePathBtn.Location = new System.Drawing.Point(480, 17);
            this.updateGamePathBtn.Name = "updateGamePathBtn";
            this.updateGamePathBtn.Size = new System.Drawing.Size(31, 20);
            this.updateGamePathBtn.TabIndex = 2;
            this.updateGamePathBtn.Text = ". . .";
            this.updateGamePathBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.updateGamePathBtn.UseVisualStyleBackColor = true;
            this.updateGamePathBtn.Click += new System.EventHandler(this.updateGamePathBtn_Click);
            // 
            // DBGroupBox
            // 
            this.DBGroupBox.Controls.Add(this.DbInfoLbl);
            this.DBGroupBox.Controls.Add(this.RegenDBBtn);
            this.DBGroupBox.Location = new System.Drawing.Point(15, 46);
            this.DBGroupBox.Name = "DBGroupBox";
            this.DBGroupBox.Size = new System.Drawing.Size(498, 49);
            this.DBGroupBox.TabIndex = 3;
            this.DBGroupBox.TabStop = false;
            this.DBGroupBox.Text = "Object Database";
            // 
            // DbInfoLbl
            // 
            this.DbInfoLbl.AutoSize = true;
            this.DbInfoLbl.Location = new System.Drawing.Point(98, 24);
            this.DbInfoLbl.Name = "DbInfoLbl";
            this.DbInfoLbl.Size = new System.Drawing.Size(85, 13);
            this.DbInfoLbl.TabIndex = 1;
            this.DbInfoLbl.Text = "DatabaseInfoLbl";
            // 
            // RegenDBBtn
            // 
            this.RegenDBBtn.Location = new System.Drawing.Point(16, 19);
            this.RegenDBBtn.Name = "RegenDBBtn";
            this.RegenDBBtn.Size = new System.Drawing.Size(75, 23);
            this.RegenDBBtn.TabIndex = 0;
            this.RegenDBBtn.Text = "Regenerate";
            this.RegenDBBtn.UseVisualStyleBackColor = true;
            this.RegenDBBtn.Click += new System.EventHandler(this.RegenDBBtn_Click);
            // 
            // editorGroupBox
            // 
            this.editorGroupBox.Controls.Add(this.ShowArgs);
            this.editorGroupBox.Location = new System.Drawing.Point(15, 101);
            this.editorGroupBox.Name = "editorGroupBox";
            this.editorGroupBox.Size = new System.Drawing.Size(498, 43);
            this.editorGroupBox.TabIndex = 4;
            this.editorGroupBox.TabStop = false;
            this.editorGroupBox.Text = "Editor";
            // 
            // ShowArgs
            // 
            this.ShowArgs.AutoSize = true;
            this.ShowArgs.Location = new System.Drawing.Point(16, 19);
            this.ShowArgs.Name = "ShowArgs";
            this.ShowArgs.Size = new System.Drawing.Size(172, 17);
            this.ShowArgs.TabIndex = 0;
            this.ShowArgs.Text = "Show Undocumented Obj Args";
            this.ShowArgs.UseVisualStyleBackColor = true;
            this.ShowArgs.CheckedChanged += new System.EventHandler(this.ShowArgs_CheckedChanged);
            // 
            // miscGroupBox
            // 
            this.miscGroupBox.Controls.Add(this.useInternalNames);
            this.miscGroupBox.Controls.Add(this.useDevCheckBox);
            this.miscGroupBox.Controls.Add(this.label2);
            this.miscGroupBox.Controls.Add(this.LanguageComboBox);
            this.miscGroupBox.Location = new System.Drawing.Point(15, 150);
            this.miscGroupBox.Name = "miscGroupBox";
            this.miscGroupBox.Size = new System.Drawing.Size(498, 57);
            this.miscGroupBox.TabIndex = 5;
            this.miscGroupBox.TabStop = false;
            this.miscGroupBox.Text = "Miscellaneous";
            // 
            // useDevCheckBox
            // 
            this.useDevCheckBox.AutoSize = true;
            this.useDevCheckBox.Location = new System.Drawing.Point(362, 27);
            this.useDevCheckBox.Name = "useDevCheckBox";
            this.useDevCheckBox.Size = new System.Drawing.Size(134, 17);
            this.useDevCheckBox.TabIndex = 2;
            this.useDevCheckBox.Text = "Download pre-releases";
            this.useDevCheckBox.UseVisualStyleBackColor = true;
            this.useDevCheckBox.CheckedChanged += new System.EventHandler(this.useDevCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Language:";
            // 
            // LanguageComboBox
            // 
            this.LanguageComboBox.FormattingEnabled = true;
            this.LanguageComboBox.Location = new System.Drawing.Point(85, 25);
            this.LanguageComboBox.Name = "LanguageComboBox";
            this.LanguageComboBox.Size = new System.Drawing.Size(94, 21);
            this.LanguageComboBox.TabIndex = 0;
            this.LanguageComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguageComboBox_SelectedIndexChanged);
            // 
            // tryUpdateBtn
            // 
            this.tryUpdateBtn.Location = new System.Drawing.Point(15, 218);
            this.tryUpdateBtn.Name = "tryUpdateBtn";
            this.tryUpdateBtn.Size = new System.Drawing.Size(108, 23);
            this.tryUpdateBtn.TabIndex = 2;
            this.tryUpdateBtn.Text = "Check for updates";
            this.tryUpdateBtn.UseVisualStyleBackColor = true;
            this.tryUpdateBtn.Click += new System.EventHandler(this.tryUpdateBtn_Click);
            // 
            // useInternalNames
            // 
            this.useInternalNames.AutoSize = true;
            this.useInternalNames.Location = new System.Drawing.Point(202, 27);
            this.useInternalNames.Name = "useInternalNames";
            this.useInternalNames.Size = new System.Drawing.Size(154, 17);
            this.useInternalNames.TabIndex = 3;
            this.useInternalNames.Text = "Use Internal Galaxy Names";
            this.useInternalNames.UseVisualStyleBackColor = true;
            this.useInternalNames.CheckedChanged += new System.EventHandler(this.useInternalNames_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 253);
            this.Controls.Add(this.tryUpdateBtn);
            this.Controls.Add(this.miscGroupBox);
            this.Controls.Add(this.editorGroupBox);
            this.Controls.Add(this.DBGroupBox);
            this.Controls.Add(this.updateGamePathBtn);
            this.Controls.Add(this.GamePathTextBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.DBGroupBox.ResumeLayout(false);
            this.DBGroupBox.PerformLayout();
            this.editorGroupBox.ResumeLayout(false);
            this.editorGroupBox.PerformLayout();
            this.miscGroupBox.ResumeLayout(false);
            this.miscGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox GamePathTextBox;
        private System.Windows.Forms.Button updateGamePathBtn;
        private System.Windows.Forms.GroupBox DBGroupBox;
        private System.Windows.Forms.Label DbInfoLbl;
        private System.Windows.Forms.Button RegenDBBtn;
        private System.Windows.Forms.GroupBox editorGroupBox;
        private System.Windows.Forms.CheckBox ShowArgs;
        private System.Windows.Forms.GroupBox miscGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox LanguageComboBox;
        private System.Windows.Forms.Button tryUpdateBtn;
        private System.Windows.Forms.CheckBox useDevCheckBox;
        private System.Windows.Forms.CheckBox useInternalNames;
    }
}