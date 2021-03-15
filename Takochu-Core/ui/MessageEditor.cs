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
using Takochu.fmt;
using Takochu.io;
using Takochu.smg;
using Takochu.smg.msg;

namespace Takochu.util
{
    public partial class MessageEditor : Form
    {
        public MessageEditor()
        {
            InitializeComponent();
        }

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
        private Dictionary<string, List<MessageBase>> mCurMessageDict;
        private RARCFilesystem mFilesystem;

        private void zoneNamesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (zoneNamesComboBox.SelectedIndex != -1)
            {
                labelsComboBox.Items.Clear();

                string name = (string)zoneNamesComboBox.SelectedItem;
                mSelectedZone = name;

                if (GameUtil.IsSMG1())
                {
                    // disable flows for now
                    ((Control)tabPage2).Enabled = false;

                    mCurMessageDict = NameHolder.GetAllMessagesInZone(mSelectedZone);

                    foreach (string str in mCurMessageDict.Keys)
                    {
                        labelsComboBox.Items.Add(str);
                    }
                }
                else
                {
                    mCurMessages = mGalaxy.GetZone(name).GetMessages();

                    Dictionary<string, List<MessageBase>> dur = mCurMessages.GetMessages();


                    foreach (string str in dur.Keys)
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
        }

        private void labelsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (labelsComboBox.SelectedIndex != -1)
            {
                labelTextBox.Text = "";

                string lbl = labelsComboBox.Text;

                ATR1.AttributeEntry entry = mCurMessages.GetAttributeEntry(labelsComboBox.SelectedIndex);
                attribute0.Value = entry._0;
                cameraTypeComboBox.SelectedIndex = entry._1;
                talkTypeList.SelectedIndex = entry._2;
                dialogTypeList.SelectedIndex = entry._3;
                attribute4.Value = entry._4;
                attribute5.Value = entry._5;
                attribute6.Value = entry._6;

                List<MessageBase> msg;

                if (GameUtil.IsSMG1())
                    msg = mCurMessageDict[lbl];
                else
                    msg = mCurMessages.GetMessageFromLabel(lbl);

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
                testMSBFBtn.Enabled = true;

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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "RARC archives (*.arc)|*.arc";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                mFilesystem = new RARCFilesystem(new ExternalFile(dialog.FileName));

                string filename = Path.GetFileNameWithoutExtension(dialog.FileName);

                if (!mFilesystem.DoesFileExist($"/derp/{filename}.msbt"))
                {
                    MessageBox.Show("This file does not contain a message file.");
                    return;
                }

                mCurMessages = new MSBT(mFilesystem.OpenFile($"/derp/{filename}.msbt"));

                if (mFilesystem.DoesFileExist($"/derp/{filename}.msbf"))
                    mCurFlow = new MSBF(mFilesystem.OpenFile($"/derp/{filename}.msbf"));

                zoneNamesComboBox.Enabled = false;
                labelsComboBox.Items.Clear();

                Dictionary<string, List<MessageBase>> dur = mCurMessages.GetMessages();

                foreach (string str in dur.Keys)
                {
                    labelsComboBox.Items.Add(str);
                }

                flowNamesList.Items.Clear();

                if (mCurFlow != null)
                {
                    ((Control)tabPage2).Enabled = true;
                    mCurFlow.GetFlowNames().ForEach(l => flowNamesList.Items.Add(l));
                }
                else
                    ((Control)tabPage2).Enabled = false;

                testMSBFBtn.Enabled = false;
                this.Text = $"MessageEditor -- {filename}";
            }
        }
    }
}
