using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.smg;
using Takochu.util;

namespace Takochu.ui
{
    public partial class SettingsForm : Form
    {
        internal static string User = "shibbo";
        private static TreeView GalaxyNameTreeView;

        public SettingsForm(TreeView tb)
        {
            InitializeComponent();
            CenterToScreen();
            GalaxyNameTreeView = tb;
            GamePathTextBox.Text = Convert.ToString(SettingsUtil.GetSetting("GameFolder"));
            DbInfoLbl.Text = "ObjectDatabase last generated on: " + File.GetLastWriteTime(ObjectDB.Xml_PathString).ToString();
            ShowArgs.Checked = Convert.ToBoolean(SettingsUtil.GetSetting("ShowArgs"));
            LanguageComboBox.Text = Convert.ToString(SettingsUtil.GetSetting("Translation"));
            useDevCheckBox.Checked = Convert.ToBoolean(SettingsUtil.GetSetting("Dev"));

            foreach (string langs in typeof(Language).GetEnumNames())
            {
                LanguageComboBox.Items.Add(langs);
            }

            BuildLabel.Text += Updater.CompileDate;
        }

        private void updateGamePathBtn_Click(object sender, EventArgs e)
        {
            bool res = ProgramUtil.SetGamePath();

            if (res)
            {
                ProgramUtil.Setup(GalaxyNameTreeView);
            }

            GamePathTextBox.Text = Convert.ToString(SettingsUtil.GetSetting("GameFolder"));
        }

        private void RegenDBBtn_Click(object sender, EventArgs e)
        {
            ObjectDB.GenDB();
            ObjectDB.Load();
            DbInfoLbl.Text = "ObjectDatabase last generated on: " + File.GetLastWriteTime(ObjectDB.Xml_PathString).ToString();
        }

        private void ShowArgs_CheckedChanged(object sender, EventArgs e)
        {
            SettingsUtil.SetSetting("ShowArgs", ShowArgs.Checked);
        }

        private void LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (LanguageComboBox.SelectedItem.ToString().Equals(SettingsUtil.GetSetting("Translation")))
            //{
            //    return;
            //}

            SettingsUtil.SetSetting("Translation", LanguageComboBox.SelectedItem);
            ProgramUtil.UpdateTranslation(GalaxyNameTreeView);
        }

        private void tryUpdateBtn_Click(object sender, EventArgs e)
        {
            var RoF = new RepoOwnerForm();
            RoF.ShowDialog();
        }

        private void useDevCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsUtil.SetSetting("Dev", useDevCheckBox.Checked);
        }
    }
}
