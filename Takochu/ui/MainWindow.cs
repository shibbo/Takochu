using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.io;
using Takochu.smg;
using Takochu.smg.img;
using Takochu.smg.msg;
using Takochu.ui;
using Takochu.util;

namespace Takochu
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Properties.Settings.Default.BCSVPaths == null)
            {
                Properties.Settings.Default.BCSVPaths = new List<string>();
            }

            string gamePath = Properties.Settings.Default.GamePath;

            if (gamePath == "")
            {
                MessageBox.Show("Please select a path that contains the dump of your SMG1 / SMG2 copy.");
                bool res = ProgramUtil.SetGamePath();

                if (!res)
                    return;
            }

            // is it valid AND does it still exist?
            if (gamePath != "" && Directory.Exists(gamePath))
            {
                bcsvEditorBtn.Enabled = true;
                ProgramUtil.Setup(galaxyTreeView);
            }
        }

        private void selectGameFolderBtn_Click(object sender, EventArgs e)
        {
            bool res = ProgramUtil.SetGamePath();

            if (res)
            {
                bcsvEditorBtn.Enabled = true;
                ProgramUtil.Setup(galaxyTreeView);
            }
        }

        private void bcsvEditorBtn_Click(object sender, EventArgs e)
        {
            BCSVEditorForm bcsvEditor = new BCSVEditorForm();
            bcsvEditor.Show();
        }

        private void galaxyTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (galaxyTreeView.SelectedNode != null) 
            {
                EditorWindow win = new EditorWindow(Convert.ToString(galaxyTreeView.SelectedNode.Tag));
                win.Show();
            }
        }

        private void rarcExplorer_Btn_Click(object sender, EventArgs e)
        {
            RARCExplorer explorer = new RARCExplorer();
            explorer.Show();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            NameHolder.Close();
        }

        private void showMessageEditorBtn_Click(object sender, EventArgs e)
        {
            MessageEditor editor = new MessageEditor();
            editor.Show();
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.Show();
        }
    }
}
