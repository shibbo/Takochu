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
using Takochu.io;

namespace Takochu.ui
{
    public partial class RARCExplorer : Form
    {
        public RARCExplorer()
        {
            InitializeComponent();
            openRARC_Btn.Enabled = false;
        }

        private void PopulateTreeView(ref TreeNode node, string parent)
        {
            List<string> dirs = mFilesystem.GetDirectories(parent);
            List<string> files = mFilesystem.GetFiles(parent);

            foreach (string dir in dirs)
            {
                TreeNode tn = new TreeNode(dir);
                tn.Tag = Convert.ToString(parent + "/" + dir);
                node.Nodes.Add(tn);

                PopulateTreeView(ref tn, parent + "/" + dir);
            }

            foreach (string file in files)
            {
                TreeNode tn = new TreeNode(file);
                tn.Tag = Convert.ToString(parent + "/" + file);
                node.Nodes.Add(tn);
            }
        }

        private void ExportFilesAndDirs(TreeNode node, string parent, string folderPath)
        {
            string name = Convert.ToString(node.Tag);
            // directory
            if (node.Nodes.Count != 0)
            {
                string[] temp = name.Split('/');
                string folderName = temp[temp.Length - 1];
                Directory.CreateDirectory(folderPath + parent + "/" + folderName);

                foreach (TreeNode curNode in node.Nodes)
                {
                    ExportFilesAndDirs(curNode, parent + "/" + folderName, folderPath);
                }
            }
            // file
            else
            {
                // write it, and move on
                string[] temp = name.Split('/');
                string fileName = temp[temp.Length - 1];
                File.WriteAllBytes(folderPath + parent + "/" + fileName, mFilesystem.GetContents(name));
            }
        }

        public RARCFilesystem mFilesystem;

        private void openRARC_Btn_Click(object sender, EventArgs e)
        {
            rarc_TreeView.Nodes.Clear();
            if (mFilesystem != null)
            {
                mFilesystem.Close();
            }

            mFilesystem = new RARCFilesystem(Program.sGame.mFilesystem.OpenFile(rarcName_TextBox.Text));

            TreeNode root = new TreeNode("/");

            PopulateTreeView(ref root, "/root");

            rarc_TreeView.Nodes.Add(root);
        }

        private void rarc_TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (rarc_TreeView.SelectedNode != null)
            {
                export_Btn.Enabled = true;
            }
            else
            {
                export_Btn.Enabled = false;
            }
        }

        private void export_Btn_Click(object sender, EventArgs e)
        {
            if (rarc_TreeView.SelectedNode == null)
                return;

            string name = Convert.ToString(rarc_TreeView.SelectedNode.Tag);
            // first see if we're dealing with a file or directory
            if (rarc_TreeView.SelectedNode.Nodes.Count == 0)
            {
                byte[] data = mFilesystem.GetContents(name);

                SaveFileDialog dialog = new SaveFileDialog();
                string[] temp = name.Split('/');
                dialog.FileName = temp[temp.Length - 1];

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(dialog.FileName, data);
                    MessageBox.Show("Exported successfully.");
                }
            }
            else
            {
                // directory
                FolderBrowserDialog d = new FolderBrowserDialog();

                if (d.ShowDialog() == DialogResult.OK)
                {
                    ExportFilesAndDirs(rarc_TreeView.SelectedNode, (string)rarc_TreeView.SelectedNode.Tag, d.SelectedPath);
                }
            }
        }

        private void rarcName_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (Program.sGame.DoesFileExist(rarcName_TextBox.Text))
                openRARC_Btn.Enabled = true;
            else
                openRARC_Btn.Enabled = false;
        }

        private void RARCExplorer_Closing(object sender, FormClosingEventArgs e)
        {
            if (mFilesystem != null)
            {
                mFilesystem.Close();
            }
        }
    }
}
