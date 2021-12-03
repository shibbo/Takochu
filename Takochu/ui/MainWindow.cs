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
using Microsoft.WindowsAPICodePack.Dialogs;

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

            //Program.sTranslator = new Translator();

            string gamePath = Properties.Settings.Default.GamePath;

            if (gamePath == "\"\"" || Directory.Exists(gamePath))
            {
                MessageBox.Show("Please select a path that contains the dump of your SMG1 / SMG2 copy.");
                bool res = SetGamePath();

                if (res==false)
                {
                    Application.Exit();
                    return;
                }
                else
                {
                    gamePath = Properties.Settings.Default.GamePath;
                    if (Directory.Exists(gamePath))
                    {
                        Setup();
                        return;
                    }

                }
            }

            // is it valid AND does it still exist?
            if (gamePath != "\"\"" && Directory.Exists(gamePath))
            {
                Setup();
            }
        }

        private void Setup(bool reSetup = false)
        {
            var isNull = Program.sGame == null;
            var a = new ExternalFilesystem(Properties.Settings.Default.GamePath);
            Program.sGame = new Game(a);

                if(reSetup)
                LightData.Close();
            
                LightData.Initialize();
            
            

            if (GameUtil.IsSMG2()) 
            {

                if (reSetup)
                    BGMInfo.Close();
                
                    BGMInfo.Initialize();
                
                
            }

            if (reSetup)
                NameHolder.Close();
             
            
                NameHolder.Initialize();
            
            
            
                ImageHolder.Initialize();
            
            

            bcsvEditorBtn.Enabled = true;
            galaxyTreeView.Nodes.Clear();
            
            List<string> galaxies = Program.sGame.GetGalaxies();
            Dictionary<string, string> simpleNames = Translate.GetGalaxyNames();

            foreach(string galaxy in galaxies)
            {
                if (simpleNames.ContainsKey(galaxy))
                {
                    TreeNode node = new TreeNode(simpleNames[galaxy]);
                    node.ToolTipText = galaxy;
                    node.Tag = galaxy;
                    galaxyTreeView.Nodes.Add(node);
                }
                else
                {
                    TreeNode node = new TreeNode(galaxy);
                    node.ToolTipText = galaxy;
                    node.Tag = galaxy;
                    galaxyTreeView.Nodes.Add(node);
                }
            }
        }

        private void selectGameFolderBtn_Click(object sender, EventArgs e)
        {
            bool res =
            SetGamePath();

            if (res) Setup(true);
        }

        private void BcsvEditorBtn_Click(object sender, EventArgs e)
        {
            BCSVEditorForm bcsvEditor = new BCSVEditorForm();
            bcsvEditor.Show();
        }

        private bool SetGamePath()
        {
            var SetPath = Properties.Settings.Default.GamePath;

            if (!Directory.Exists(SetPath))
                SetPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            CommonOpenFileDialog cofd = new CommonOpenFileDialog
            {
                InitialDirectory = SetPath,
                IsFolderPicker = true
            };
            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = cofd.FileName;
                if (Directory.Exists($"{path}/StageData") && Directory.Exists($"{path}/ObjectData"))
                {
                    Properties.Settings.Default.GamePath = path;
                    Properties.Settings.Default.Save();

                    Program.sGame = new Game(new ExternalFilesystem(path));

                    Translate.GetMessageBox.Show(MessageBoxText.FolderPathCorrectly, MessageBoxCaption.Info);
                    return true;
                }
                else
                {
                    Translate.GetMessageBox.Show(MessageBoxText.InvalidFolder,MessageBoxCaption.Error);
                    return false;
                }
            }
            return false;
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
            // this is our main program getting closed, so we can update our name table if needed
            List<string> fields = new List<string>(); 

            foreach(KeyValuePair<int, string> kvp in BCSV.sHashTable)
            {
                fields.Add(kvp.Value);
            }

            File.WriteAllLines("res/FieldNames.txt", fields.ToArray());

            NameHolder.Close();
        }

        private void showMessageEditorBtn_Click(object sender, EventArgs e)
        {
            MessageEditor editor = new MessageEditor();
            editor.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RenderingTest test = new RenderingTest();
            test.Show();
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm(galaxyTreeView);
            settings.Show();
        }
    }
}
