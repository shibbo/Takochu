using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.smg;

namespace Takochu.util
{
    public partial class MessageEditor : Form
    {
        public MessageEditor(ref Galaxy galaxy)
        {
            InitializeComponent();
            mGalaxy = galaxy;

            foreach (var str in mGalaxy.GetZoneNames().Where(str => mGalaxy.GetZone(str).HasMessages()))
            {
                zoneNamesComboBox.Items.Add(str);
            }
        }

        private Galaxy mGalaxy;
        private string mSelectedZone;
        private MSBT mCurMessages;

        private void zoneNamesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (zoneNamesComboBox.SelectedIndex != -1)
            {
                labelsComboBox.Items.Clear();

                string name = (string)zoneNamesComboBox.SelectedItem;
                mSelectedZone = name;

                mCurMessages = mGalaxy.GetZone(name).GetMessages();

                Dictionary<string, List<MessageBase>> dur = mCurMessages.GetMessages();

                foreach(string str in dur.Keys)
                {
                    labelsComboBox.Items.Add(str);
                }
            }
        }

        private void labelsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelsComboBox.SelectedIndex != -1)
            {
                labelTextBox.Text = "";

                string lbl = labelsComboBox.Text;
                List<MessageBase> msg = mCurMessages.GetMessageFromLabel(lbl);

                foreach(MessageBase m in msg)
                {
                    labelTextBox.Text += m.ToString();
                }
            }
        }
    }
}
