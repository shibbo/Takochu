using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Octokit;
using Takochu.util;
using static Takochu.util.Updater;

namespace Takochu.ui
{
    public partial class RepoOwnerForm : Form
    {
        public RepoOwnerForm()
        {
            InitializeComponent();


            UserTreeView.Nodes.Add(Client.User.Get("shibbo").GetAwaiter().GetResult().Login);

            foreach (var r in Client.Repository.Forks.GetAll("shibbo", "Takochu").GetAwaiter().GetResult())
            {
                UserTreeView.Nodes.Add(r.Owner.Name ?? r.Owner.Login);
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            SettingsForm.User = UserTreeView.SelectedNode.Text ?? UserTreeView.SelectedNode.Name;
            Updater.Update(Convert.ToBoolean(SettingsUtil.GetSetting("Dev")));
            Close();
        }
    }
}