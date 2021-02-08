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
using Takochu.smg.msg;

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
        private MSBF mCurFlow;
        private string mCurrentSelectedFlow;

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

                flowNamesList.Items.Clear();

                if (mGalaxy.GetZone(name).HasFlows())
                {
                    ((Control)tabPage2).Enabled = true;
                    mCurFlow = mGalaxy.GetZone(name).GetFlows();
                    mCurFlow.GetFlowNames().ForEach(l => flowNamesList.Items.Add(l));
                }
                else
                    ((Control)tabPage2).Enabled = false;
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

        private void testMSBFBtn_Click(object sender, EventArgs e)
        {
            FlowEmulator emu = new FlowEmulator(mCurMessages, mCurFlow);
            emu.Start((int)mCurFlow.GetStartID(mCurrentSelectedFlow));
        }

        private void flowNamesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flowNamesList.SelectedIndex != -1)
            {
                mCurrentSelectedFlow = Convert.ToString(flowNamesList.SelectedItem);
                flowNamesListBox.Items.Clear();

                uint startID = mCurFlow.GetStartID(mCurrentSelectedFlow);

                uint curID = startID;
                while (true)
                {
                    flowNamesListBox.Items.Add(mCurFlow.GetNode((int)curID).ToString());

                    Node nextNode = mCurFlow.GetNextNode((int)curID);
                    if (nextNode == null || nextNode.mNodeType == Node.NodeType.NodeType_Entry)
                        break;

                    curID++;
                }
            }
        }
    }
}
