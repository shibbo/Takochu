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
using Takochu.smg.img;
using Takochu.util;

namespace Takochu.ui
{
    public partial class StageInfoEditor : Form
    {
        public StageInfoEditor(ref Galaxy galaxy, int scenarioNo)
        {
            InitializeComponent();
            ToggleLayersEnabled(false);

            mGalaxy = galaxy;
            mScenarios = new Dictionary<int, Scenario>();
            mBGMRestrictedIDs = new List<int>();

            galaxyInfoTexture.Image = ImageHolder.GetImage(mGalaxy.mName);

            bool indexFound = false;
            int idx = 0;
            mCurScenario = 0;

            // we are allowed to load these and keep them, as they are not changed per scenario
            BGMInfo.GetBGMInfo(mGalaxy.mName, out mInfoEntry, out mScenarioEntries);

            foreach (KeyValuePair<int, Scenario> scenarios in mGalaxy.mScenarios)
            {
                Scenario s = scenarios.Value;

                if (s.mPowerStarType == "Green")
                    continue;

                if (!mScenarioEntries.Any(e => e.ScenarioNo == s.mScenarioNo))
                    mBGMRestrictedIDs.Add(s.mScenarioNo);

                TreeNode n = new TreeNode($"[{s.mScenarioNo}] {s.mScenarioName}")
                {
                    Tag = s.mScenarioNo
                };

                scenarioListTreeView.Nodes.Add(n);

                mScenarios.Add(s.mScenarioNo, mGalaxy.GetScenario(s.mScenarioNo));

                if (s.mScenarioNo == scenarioNo)
                {
                    indexFound = true;
                }

                if (!indexFound)
                    idx++;
            }

            // if there is no scenario selected, we just have it select the first one (since it does this by default)
            // otherwise, select the entry for the scenario we have selected in the editor
            if (scenarioNo != 0)
                scenarioListTreeView.SelectedNode = scenarioListTreeView.Nodes[idx];

            changeBgmIdName_0.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName0");
            changeBgmIdName_1.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName1");
            changeBgmIdName_2.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName2");
            changeBgmIdName_3.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName3");
            changeBgmIdName_4.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName4");

            changeBgmState_0.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState0");
            changeBgmState_1.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState1");
            changeBgmState_2.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState2");
            changeBgmState_3.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState3");
            changeBgmState_4.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState4");

            // so there's a specific edge case with this
            // if the ScenarioNo is 0, then it uses it for every scenario
            if (mScenarioEntries.Count == 1)
            {
                if (mScenarioEntries[0].ScenarioNo == 0)
                {
                    mIsRestrictBGM = true;
                }
            }

            foreach (KeyValuePair<string, Zone> z in mGalaxy.GetZones())
                zoneListsBox.Items.Add(z.Key);

            mIsInitialized = true;
        }

        Galaxy mGalaxy;
        BGMInfo.BGMInfoEntry mInfoEntry;
        List<BGMInfo.ScenarioBGMEntry> mScenarioEntries;
        List<int> mBGMRestrictedIDs;
        bool mIsRestrictBGM = false;
        Dictionary<int, Scenario> mScenarios;
        int mCurScenario;
        bool mIsInitialized = false;

        private void scenarioListTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (scenarioListTreeView.SelectedNode != null)
            {
                mIsInitialized = false;

                int scenarioNo = Convert.ToInt32(scenarioListTreeView.SelectedNode.Tag);
                mCurScenario = scenarioNo;

                if (mIsRestrictBGM || mBGMRestrictedIDs.Contains(scenarioNo))
                {
                    scenarioNo = 0;
                }

                BGMInfo.ScenarioBGMEntry scenarioEntry = mScenarioEntries.Find(entry => entry.ScenarioNo == scenarioNo);

                scenarioBGMId.Text = scenarioEntry.Entry.Get<string>("BgmIdName");
                scenarioBGMStartType.Value = scenarioEntry.Entry.Get<int>("StartType");
                scenarioBGMStartFrame.Value = scenarioEntry.Entry.Get<int>("StartFrame");
                scenarioBGMIsPrepare.Checked = scenarioEntry.Entry.Get<int>("IsPrepare") != 0;

                Scenario scenario = mScenarios[mCurScenario];
                scenarioNameTxt.Text = scenario.mEntry.Get<string>("ScenarioName");
                powerStarID.Value = scenario.mEntry.Get<int>("PowerStarId");
                appearPowerStarTxt.Text = scenario.mEntry.Get<string>("AppearPowerStarObj");
                powerStarTypeTxt.Text = scenario.mEntry.Get<string>("PowerStarType");
                cometTypeTxt.Text = scenario.mEntry.Get<string>("Comet");
                cometTimer.Value = scenario.mEntry.Get<int>("CometLimitTimer");

                mIsInitialized = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Tag != null)
            {
                if (tabControl1.SelectedTab.Tag.ToString() == "1")
                {
                    foreach (TreeNode n in scenarioListTreeView.Nodes)
                    {
                        if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
                            n.ForeColor = SystemColors.GrayText;
                    }
                }
                else
                {
                    foreach (TreeNode n in scenarioListTreeView.Nodes)
                    {
                        if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
                            n.ForeColor = SystemColors.ControlText;
                    }
                }
            }
            else
            {
                foreach (TreeNode n in scenarioListTreeView.Nodes)
                {
                    if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
                        n.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TreeNode n in scenarioListTreeView.Nodes)
            {
                if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
                    n.ForeColor = SystemColors.ControlText;
            }
        }

        private void zoneListsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetLayers();
            ToggleLayersEnabled(true);

            if (zoneListsBox.SelectedIndex != -1)
            {
                string zoneName = Convert.ToString(zoneListsBox.SelectedItem);

                int mask = mScenarios[mCurScenario].mEntry.Get<int>(zoneName);
                List<string> layers = GameUtil.GetGalaxyLayers(mask);

                foreach (Control c in layerMasksBox.Controls)
                {
                    if (c.Tag == null)
                        continue;

                    string tag = c.Tag.ToString();
                    if (c is CheckBox && tag.StartsWith("Layer"))
                    {
                        CheckBox box = c as CheckBox;
                        if (layers.Contains(tag))
                            box.Checked = true;
                    }
                }
            }
        }

        private void ResetLayers()
        {
            foreach (Control c in layerMasksBox.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox box = c as CheckBox;
                    box.Checked = false;
                }
            }
        }

        private void saveScenarioBtn_Click(object sender, EventArgs e)
        {
            mGalaxy.SaveScenario();
            BGMInfo.Save();
        }

        private void NumericInt_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInitialized)
                return;

            NumericUpDown n = sender as NumericUpDown;

            // time for some hacks
            if (tabControl2.SelectedTab.Text == "BGM")
            {
                if (tabControl1.SelectedTab.Text == "Stage")
                {
                    mInfoEntry.Entry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
                }
                else
                {
                    int scenario = 0;

                    // if we are in a restricted scenario, we default to changing the entry with scenario 0
                    if (!mBGMRestrictedIDs.Contains(mCurScenario))
                    {
                        scenario = mCurScenario;
                    }

                    BGMInfo.ScenarioBGMEntry scenarioEntry = mScenarioEntries.Find(entry => entry.Entry.Get<int>("ScenarioNo") == 0);
                    scenarioEntry.Entry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
                }
            }
            else
            {
                mScenarios[mCurScenario].mEntry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
            }
        }

        private void TextBox_ValueChanged(object sender, EventArgs e)
        {
            if (!mIsInitialized)
                return;

            TextBox n = sender as TextBox;

            // time for some hacks
            if (tabControl2.SelectedTab.Text == "BGM")
            {
                if (tabControl1.SelectedTab.Text == "Stage")
                {
                    mInfoEntry.Entry.Set(n.Tag.ToString(), n.Text);
                }
                else
                {
                    int scenario = 0;

                    // if we are in a restricted scenario, we default to changing the entry with scenario 0
                    if (!mBGMRestrictedIDs.Contains(mCurScenario))
                    {
                        scenario = mCurScenario;
                    }

                    BGMInfo.ScenarioBGMEntry scenarioEntry = mScenarioEntries.Find(entry => entry.Entry.Get<int>("ScenarioNo") == 0);
                    scenarioEntry.Entry.Set(n.Tag.ToString(), n.Text);
                }
            }
            else
            {
                mScenarios[mCurScenario].mEntry.Set(n.Tag.ToString(), n.Text);
            }
        }

        private void ToggleLayersEnabled(bool isEnabled)
        {
            foreach (Control c in layerMasksBox.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox box = c as CheckBox;
                    box.Enabled = isEnabled;
                }
            }
        }

        private void ZoneLayerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox c = sender as CheckBox;
            string zone = zoneListsBox.SelectedItem.ToString();
            int newMask = GameUtil.SetLayerOnMask(c.Tag.ToString(), mScenarios[mCurScenario].mEntry.Get<int>(zone), c.Checked);
            mScenarios[mCurScenario].mEntry.Set(zone, newMask);
        }
    }
}
