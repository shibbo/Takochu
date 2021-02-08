using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;

namespace Takochu.smg.msg
{
    class FlowEmulator
    {
        public FlowEmulator(MSBT msgs, MSBF flows)
        {
            mMessages = msgs;
            mFlows = flows;
        }

        public void Start(int where)
        {
            mViewedIndicies = new List<uint>();
            EntryNode n = mFlows.GetNode(where) as EntryNode;
            ProcessNextNode(n.mNextNode);
        }

        public void ProcessNextNode(ushort id)
        {
            if (id == 65535)
                id = 0;

            if (mViewedIndicies.Contains(id))
                return;

            Node n = mFlows.GetNode(id);

            // we should never have to process an entry node since there is one per flow
            switch(n.ToString())
            {
                case "Message Node":
                {
                        MessageNode temp = n as MessageNode;
                        ushort msg = temp.mMessageID;

                        string message = mMessages.GetStringFromIndex(msg);

                        MessageBox.Show(message);

                        if (temp.mNextNode == id)
                            return;


                        mViewedIndicies.Add(id);
                        ProcessNextNode(temp.mNextNode);
                    break;
                }
                case "Event Node":
                    {
                        EventNode temp = n as EventNode;

                        switch(temp.mEvent)
                        {
                            case 0:
                            case 1:
                                MessageBox.Show("Unknown");
                                break;
                            case 2:
                                ushort flowToJumpTo = temp.mFlowID;
                                ProcessNextNode(flowToJumpTo);
                                break;

                            case 3:
                            case 4:
                                MessageBox.Show("Unknown behavior");
                                break;

                            case 5:
                                MessageBox.Show("SW_A for this NPC activated!");
                                break;

                            case 6:
                                MessageBox.Show("SW_B for this NPC activated!");
                                break;

                            case 7:
                                MessageBox.Show("SW_DEAD for this NPC activate!");
                                break;

                            case 8:
                                MessageBox.Show("SW_A for this NPC deactivated!");
                                break;

                            case 9:
                                MessageBox.Show("SW_B for this NPC deactivated!");
                                break;
                        }

                        if (temp.mNextNode == id)
                            return;

                        mViewedIndicies.Add(id);
                        ProcessNextNode(temp.mNextNode);
                        break;
                    }

                case "Branch Node":
                    {
                        BranchNode temp = n as BranchNode;

                        ushort trueLabel, falseLabel;
                        mFlows.GetJumpLabels(temp.mLabelsToUse, out trueLabel, out falseLabel);

                        string messageBoxText = "";

                        switch (temp.mCondition)
                        {
                            case 0:
                                messageBoxText = "Special YesNoResult condition";
                                break;
                            case 1:
                                messageBoxText = "Special condition";
                                break;
                            case 2:
                                messageBoxText = "Is the player near the NPC?";
                                break;
                            case 3:
                                messageBoxText = "Is the SW_A switch of this object activated?";
                                break;
                            case 4:
                                messageBoxText = "Is the SW_B switch of this object activated?";
                                break;
                            case 5:
                                messageBoxText = "Does the player currently have no Power Up?";
                                break;
                            case 6:
                                messageBoxText = "Does the player currently have the Bee Power Up?";
                                break;
                            case 7:
                                messageBoxText = "Does the player currentlyhave the Boo Power Up?";
                                break;
                            case 8:
                                messageBoxText = "Did a Power Star appear?";
                                break;
                            case 9:
                                messageBoxText = "Is the player currently playing as Luigi?";
                                break;
                            case 10:
                                messageBoxText = "Is there currently a demo active?";
                                break;
                            case 11:
                                messageBoxText = "Has the text already been read?";
                                break;
                            case 12:
                                messageBoxText = "Does the save file have at least 120 stars?";
                                break;
                            case 13:
                                messageBoxText = "Unknown condition";
                                break;
                            case 14:
                                messageBoxText = "Is the player riding Yoshi?";
                                break;
                            case 15:
                                messageBoxText = "Does the player currently have the Cloud Flower Power Up?";
                                break;
                            case 16:
                                messageBoxText = "Does the player currentlyhave the RockPower Up?";
                                break;
                        }

                        DialogResult res = MessageBox.Show(messageBoxText, "Condition", MessageBoxButtons.YesNo);

                        if (res == DialogResult.Yes)
                            ProcessNextNode(trueLabel);
                        else
                            ProcessNextNode(falseLabel);

                        break;
                    }
            }
        }

        private MSBT mMessages;
        private MSBF mFlows;

        private List<uint> mViewedIndicies;
    }
}
