using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.io;
using Takochu.smg;
using Takochu.smg.img;
using Takochu.smg.msg;

namespace Takochu.util
{
    public static class ProgramUtil
    {
        public static void Setup(TreeView TView)
        {
            if (TView != null)
                sTView = TView;

            Program.sGame = new smg.Game(new ExternalFilesystem(Properties.Settings.Default.GamePath));

            LightData.Initialize();

            if (GameUtil.IsSMG2())
                BGMInfo.Initialize();

            NameHolder.Initialize();
            ImageHolder.Initialize();

            UpdateTranslation();
        }

        public static void UpdateTranslation()
        {
            Program.sTranslator = new Translator(Convert.ToString(SettingsUtil.GetSetting("Translation")));
            PopulateGalaxyTreeView(sTView);
        }

        static void PopulateGalaxyTreeView(TreeView TView)
        {
            TView.Nodes.Clear();

            List<string> galaxies = Program.sGame.GetGalaxies();
            Dictionary<string, string> simpleNames = Program.sTranslator.GetGalaxyNames();

            foreach (string galaxy in galaxies)
            {
                if (simpleNames.ContainsKey(galaxy))
                {
                    TreeNode node = new TreeNode(simpleNames[galaxy]);
                    node.ToolTipText = galaxy;
                    node.Tag = galaxy;
                    TView.Nodes.Add(node);
                }
                else
                {
                    TreeNode node = new TreeNode(galaxy);
                    node.ToolTipText = galaxy;
                    node.Tag = galaxy;
                    TView.Nodes.Add(node);
                }
            }
        }

        public static bool SetGamePath()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true, Multiselect = false };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = dialog.FileName;
                if (path != Convert.ToString(SettingsUtil.GetSetting("GameFolder")))
                {
                    if (Directory.Exists($"{path}/StageData") && Directory.Exists($"{path}/ObjectData"))
                    {

                        Properties.Settings.Default.GamePath = dialog.FileName;
                        Properties.Settings.Default.Save();

                        Program.sGame = new smg.Game(new ExternalFilesystem(dialog.FileName));

                        MessageBox.Show("Path set successfully! You may now use Takochu.");
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid folder. If you have already selected a correct folder, that will continue to be your base folder.");
                        return false;
                    }
                }
            }

            return false;
        }

        private static TreeView sTView;
    }
}