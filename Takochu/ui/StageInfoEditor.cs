using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Takochu.fmt;
using Takochu.smg;
using Takochu.smg.img;
using Takochu.util;
using Takochu.ui.StageInfoEditorSys;
using System.Collections;

namespace Takochu.ui
{
    public partial class StageInfoEditor : Form
    {
        private Galaxy _galaxy;
        private BGMInfo.BGMInfoEntry _infoEntry;
        private List<BGMInfo.ScenarioBGMEntry> _scenarioEntries;
        private List<int> _bgmRestrictedIDs;
        private bool _isRestrictBGM = false;
        private int _currentScenario;
        private bool misInitialized = false;
        private readonly ScenarioInformation _scenarioInformation;
        private readonly string[] _cometType;
        private readonly string[] _starType;
        private readonly string[] AppearPowerStarObj;


        public StageInfoEditor(ref Galaxy galaxy, int scenarioNo)
        {
            InitializeComponent();
            ToggleLayersEnabled(UseLayerFlags, false);

            //コメットのコンボボックスにコメット名を入れる

            if (GameUtil.IsSMG1())
            {
                _cometType   = Scenario.SMG1Comets;
                AppearPowerStarObj = Scenario.SMG1AppearPowerStarObj;
                CometTimerLabel.Text = "LuigiModeTimer:";
                PowerStarTypeComboBox.Enabled = false;
                MainTabControl.TabPages.Remove(MainTabControl.TabPages["BGM_InfoTabPage"]);
            }
            else 
            {
                _cometType   = Scenario.SMG2Comets;
                _starType    = Scenario.StarType;
                AppearPowerStarObj = Scenario.SMG2AppearPowerStarObj;
                CometTimerLabel.Text = "CometTimer:";

                foreach (var starType in _starType)
                {
                    PowerStarTypeComboBox.Items.Add(starType);
                }

                var scenarioBGM_dgv = new ScenarioBGMInfo_DataGridView(ScenBGM_dgv);
                ScenBGM_dgv = scenarioBGM_dgv.GetDataTable();
            }
            

            foreach (var cometName in _cometType)
            {
                CometTypeComboBox.Items.Add(cometName);
            }

            foreach (var str in AppearPowerStarObj) 
            {
                AppearPowerStarObjComboBox.Items.Add(str);
            }

#if DEBUG
#else
            //デバッグのタブページを削除
            MainTabControl.TabPages.Remove(MainTabControl.TabPages["DebugTabPage"]);
#endif
            //初期化
            _galaxy = galaxy;
            _scenarioInformation = new ScenarioInformation(_galaxy, ScenarioListTreeView);
            GalaxyNameLabel.Text = _galaxy.mGalaxyName;
            ScenarioListTreeView.SelectedNode = ScenarioListTreeView.Nodes[0];

            SetGalaxyImageLayouts();




            //コンボボックスにゾーンを入れる
            foreach (KeyValuePair<string, Zone> zone in _galaxy.GetZones())
            {
                ZoneComboBox.Items.Add(zone.Key);
            }

            if(GameUtil.IsSMG2())
                SetStageBGMListBox();

            //必要か不明
            //_currentScenario = 0;
            misInitialized = true;
        }

        private void SetStageBGMListBox()
        {
            StageBGMListBox.Items.Clear();
            foreach (var stageBGM in BGMInfo.mStageEntries)
            {
                StageBGMListBox.Items.Add(stageBGM.Key);
            }

        }

        /// <summary>
        /// ギャラクシーのサムネイルを設定
        /// </summary>
        private void SetGalaxyImageLayouts()
        {
            //ギャラクシーイメージの制御
            GalaxyInfoPictureBox.Image = ImageHolder.GetImage(_galaxy.mName);

            //ピクチャーボックスにラベルコントロールを追加
            GalaxyInfoPictureBox.Controls.Add(GalaxyNameLabel);

            //ピクチャーボックス上のラベルの処理
            GalaxyNameLabel.Top      -= GalaxyInfoPictureBox.Top;
            GalaxyNameLabel.Left     -= GalaxyInfoPictureBox.Left;
            GalaxyNameLabel.BackColor = Color.Transparent;
        }

        /// <summary>
        /// テキストボックスなどにシナリオの情報を表示
        /// </summary>
        /// <param name="scenario">対象のシナリオ</param>
        private void SetScenarioInfo(Scenario scenario)
        {
            ScenarioNameTextBox.Text     = scenario.mScenarioName;
            AppearPowerStarTextBox.Text  = scenario.mAppearPowerStar;
            PowerStarIDTextBox.Value     = scenario.mPowerStarID;

            if (GameUtil.IsSMG2())
            {
                CometAndLuigi_TimerNumericUpDown.Value = scenario.mCometLimitTimer;
                PowerStarTypeComboBox.SelectedIndex = Array.IndexOf(_starType,scenario.mPowerStarType);
            }
            else 
            {
                if (scenario.mLuigiModeTimer != default)
                {
                    CometAndLuigi_TimerNumericUpDown.Value = scenario.mLuigiModeTimer;
                    CometAndLuigi_TimerNumericUpDown.Enabled = true;
                }
                else 
                {
                    CometAndLuigi_TimerNumericUpDown.Enabled = false;
                }
                    
            }
            
            AppearPowerStarObjComboBox.SelectedItem = scenario.mAppearPowerStar;

            if (!scenario.mEntry.ContainsKey("Comet")) 
            {
                CometTypeComboBox.Enabled = false;
                return;
            }

            CometTypeComboBox.Enabled = true;
            if (scenario.mComet == string.Empty) 
            {
                CometTypeComboBox.SelectedIndex = 0;
                return;
            }
            
            CometTypeComboBox.SelectedIndex = Array.IndexOf(_cometType,scenario.mComet);
        }

        private void ScenarioListTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var scenarioTreeView = (TreeView)sender;

            if (scenarioTreeView.Nodes.Count < 1) return;

            _currentScenario = Convert.ToInt32(scenarioTreeView.SelectedNode.Tag);

            SetScenarioInfo(_scenarioInformation.Scenarios[_currentScenario]);
            ZoneComboBox.SelectedItem = _galaxy.mName;
            UseLayerCheckBoxReload();

            misInitialized = false;
            misInitialized = true;

        }



        private void BGMTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Tag != null)
            //{
            //    if (tabControl1.SelectedTab.Tag.ToString() == "1")
            //    {
            //        foreach (TreeNode n in scenarioListTreeView.Nodes)
            //        {
            //            if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
            //                n.ForeColor = SystemColors.GrayText;
            //        }
            //    }
            //    else
            //    {
            //        foreach (TreeNode n in scenarioListTreeView.Nodes)
            //        {
            //            if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
            //                n.ForeColor = SystemColors.ControlText;
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (TreeNode n in scenarioListTreeView.Nodes)
            //    {
            //        if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
            //            n.ForeColor = SystemColors.ControlText;
            //    }
            //}
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (TreeNode n in scenarioListTreeView.Nodes)
            //{
            //    if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
            //        n.ForeColor = SystemColors.ControlText;
            //}
        }

        private void ZoneComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseLayerCheckBoxReload();
        }


        /// <summary>
        /// ゾーンで使用されるレイヤーのチェックボックスを再読み込みします。
        /// </summary>
        private void UseLayerCheckBoxReload()
        {
            ResetLayers(UseLayerFlags);
            ToggleLayersEnabled(UseLayerFlags, true);

            if (ZoneComboBox.SelectedIndex < 0) return;

            //var zoneName = Convert.ToString(ZoneComboBox.SelectedItem);

            var bcsv_Entry   = _scenarioInformation.Scenarios[_currentScenario].mEntry;
            var powerStarID  = bcsv_Entry.Get<int>("PowerStarId");
            var maskBitData  = bcsv_Entry.Get<int>(Convert.ToString(ZoneComboBox.SelectedItem));
            var layerList    = GameUtil.GetGalaxyLayers(maskBitData);

            DebugTextBox.Text = string.Empty;
            DebugTextBox.Text = maskBitData.ToString();

            foreach (Control control in UseLayerFlags.Controls)
            {
                if (control.Tag == null) continue;

                string TagStr = control.Tag.ToString();

                TagStr = GameUtil.IsSMG1() ? TagStr.ToLower() : TagStr;

                if (!(control is CheckBox /*&& TagStr.StartsWith("Layer")*/)) continue;
                CheckBox checkBox = control as CheckBox;

                if (!layerList.Contains(TagStr)) continue;
                checkBox.Checked = true;
            }


            ResetLayers(ShowScenarioStarFlags);
            ToggleLayersEnabled(ShowScenarioStarFlags, true);
            int[] powerStarIDArray = new int[] { powerStarID };
            var bitarray = new BitArray(powerStarIDArray);


            for (int i = 0; i < ShowScenarioStarFlags.Controls.Count; i++)
            {
                var ControlTagString = ShowScenarioStarFlags.Controls[i].Tag.ToString();
                if (!ControlTagString.StartsWith("Scenario")) continue;

                var scenarioNoChars  = ControlTagString.Skip("Scenario".Length).ToArray();
                var scenarioNo       = int.Parse(string.Concat(scenarioNoChars));

                if (!(ShowScenarioStarFlags.Controls[i] is CheckBox)) continue;

                var cb = ShowScenarioStarFlags.Controls[i] as CheckBox;
                cb.Checked = bitarray[scenarioNo - 1];



            }
        }

        /// <summary>
        /// グループボックス内の「<see cref="CheckBox.Checked"/>」プロパティをすべて<see langword="false"/>にする
        /// </summary>
        /// <param name="groupBox"></param>
        private void ResetLayers(GroupBox groupBox)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (!(control is CheckBox)) continue;
                CheckBox checkBox = control as CheckBox;
                checkBox.Checked = false;

            }
        }

        private void saveScenarioBtn_Click(object sender, EventArgs e)
        {

            //BGMInfo.Save();
        }

        private void NumericInt_ValueChanged(object sender, EventArgs e)
        {
            //if (!misInitialized)
            //    return;

            //NumericUpDown n = sender as NumericUpDown;

            //// time for some hacks
            //if (MainTabControl.SelectedTab.Text == "BGM")
            //{
            //    if (BGMTabControl.SelectedTab.Text == "Stage")
            //    {
            //        _infoEntry.Entry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
            //    }
            //    else
            //    {
            //        int scenario = 0;

            //        // if we are in a restricted scenario, we default to changing the entry with scenario 0
            //        if (!_bgmRestrictedIDs.Contains(_currentScenario))
            //        {
            //            scenario = _currentScenario;
            //        }

            //        BGMInfo.ScenarioBGMEntry scenarioEntry = _scenarioEntries.Find(entry => entry.Entry.Get<int>("ScenarioNo") == 0);
            //        scenarioEntry.Entry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
            //    }
            //}
            //else
            //{
            //    _scenarioInformation.Scenarios[_currentScenario].mEntry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
            //}
        }

        private void TextBox_ValueChanged(object sender, EventArgs e)
        {
            //if (!misInitialized)
            //    return;

            TextBox textBox = sender as TextBox;

            // time for some hacks
            if (MainTabControl.SelectedTab.Text != "BGM_InfoTabPage") return;



            if (BGMTabControl.SelectedTab.Text == "StageBGM_InfoTabPage")
            {
                //_infoEntry.Entry.Set(textBox.Tag.ToString(), textBox.Text);
            }
            else
            {
                int scenario = 0;

                // if we are in a restricted scenario, we default to changing the entry with scenario 0
                if (!_bgmRestrictedIDs.Contains(_currentScenario))
                {
                    scenario = _currentScenario;
                }

                BGMInfo.ScenarioBGMEntry scenarioEntry = _scenarioEntries.Find(entry => entry.Entry.Get<int>("ScenarioNo") == 0);
                //scenarioEntry.Entry.Set(textBox.Tag.ToString(), textBox.Text);
            }


        }


        /// <summary>
        /// グループボックス内にある「<see cref="CheckBox"/>」の「<see cref="Control.Enabled"/>」プロパティをすべて「<paramref name="isEnabled"/>」にする
        /// </summary>
        /// <param name="groupBox"></param>
        /// <param name="isEnabled"></param>
        private void ToggleLayersEnabled(GroupBox groupBox, bool isEnabled)
        {
            foreach (Control control in groupBox.Controls)
            {
                if (!(control is CheckBox)) continue;
                CheckBox checkBox = control as CheckBox;
                checkBox.Enabled = isEnabled;
            }
        }

        private void ZoneLayerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            string zoneName = ZoneComboBox.SelectedItem.ToString();
            int newMask = GameUtil.SetLayerOnMask(checkbox.Tag.ToString(), _scenarioInformation.Scenarios[_currentScenario].mEntry.Get<int>(zoneName), checkbox.Checked);
            //_scenarioInformation.Scenarios[_currentScenario /*+ 1*/].mEntry.Set(zone, newMask);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_galaxy.SaveScenario();
        }

        private void StageBGMListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listBox = (ListBox)sender;
            if (listBox.Items.Count == 0) return;

            var selectString = listBox.Items[listBox.SelectedIndex].ToString();
            var stageBgm = BGMInfo.mStageEntries[selectString];
            changeBgmIdName_0.Text = stageBgm.ChangeBGMIDName[0];
            changeBgmIdName_1.Text = stageBgm.ChangeBGMIDName[1];
            changeBgmIdName_2.Text = stageBgm.ChangeBGMIDName[2];
            changeBgmIdName_3.Text = stageBgm.ChangeBGMIDName[3];
            changeBgmIdName_4.Text = stageBgm.ChangeBGMIDName[4];

            changeBgmState_0.Value = stageBgm.ChangeBGMState[0];
            changeBgmState_1.Value = stageBgm.ChangeBGMState[1];
            changeBgmState_2.Value = stageBgm.ChangeBGMState[2];
            changeBgmState_3.Value = stageBgm.ChangeBGMState[3];
            changeBgmState_4.Value = stageBgm.ChangeBGMState[4];

        }

        //public StageInfoEditor(ref Galaxy galaxy, int scenarioNo)
        //{
        //    InitializeComponent();
        //    ToggleLayersEnabled(false);

        //    mGalaxy = galaxy;
        //    mScenarios = new Dictionary<int, Scenario>();
        //    mBGMRestrictedIDs = new List<int>();

        //    galaxyInfoTexture.Image = ImageHolder.GetImage(mGalaxy.mName);

        //    bool indexFound = false;
        //    int idx = 0;
        //    mCurScenario = 0;

        //    // we are allowed to load these and keep them, as they are not changed per scenario
        //    BGMInfo.GetBGMInfo(mGalaxy.mName, out mInfoEntry, out mScenarioEntries);

        //    foreach (KeyValuePair<int, Scenario> scenarios in mGalaxy.mScenarios)
        //    {
        //        Scenario s = scenarios.Value;

        //        if (s.mPowerStarType == "Green")
        //            continue;

        //        if (!mScenarioEntries.Any(e => e.ScenarioNo == s.mScenarioNo))
        //            mBGMRestrictedIDs.Add(s.mScenarioNo);

        //        TreeNode n = new TreeNode($"[{s.mScenarioNo}] {s.mScenarioName}")
        //        {
        //            Tag = s.mScenarioNo
        //        };

        //        scenarioListTreeView.Nodes.Add(n);

        //        mScenarios.Add(s.mScenarioNo, mGalaxy.GetScenario(s.mScenarioNo));

        //        if (s.mScenarioNo == scenarioNo)
        //        {
        //            indexFound = true;
        //        }

        //        if (!indexFound)
        //            idx++;
        //    }

        //    // if there is no scenario selected, we just have it select the first one (since it does this by default)
        //    // otherwise, select the entry for the scenario we have selected in the editor
        //    // or if it's a green star, don't select anything
        //    if (scenarioNo != 0 && idx < scenarioListTreeView.Nodes.Count)
        //        scenarioListTreeView.SelectedNode = scenarioListTreeView.Nodes[idx];

        //    changeBgmIdName_0.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName0");
        //    changeBgmIdName_1.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName1");
        //    changeBgmIdName_2.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName2");
        //    changeBgmIdName_3.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName3");
        //    changeBgmIdName_4.Text = mInfoEntry.Entry.Get<string>("ChangeBgmIdName4");

        //    changeBgmState_0.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState0");
        //    changeBgmState_1.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState1");
        //    changeBgmState_2.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState2");
        //    changeBgmState_3.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState3");
        //    changeBgmState_4.Value = mInfoEntry.Entry.Get<int>("ChangeBgmState4");

        //    // so there's a specific edge case with this
        //    // if the ScenarioNo is 0, then it uses it for every scenario
        //    if (mScenarioEntries.Count == 1)
        //    {
        //        if (mScenarioEntries[0].ScenarioNo == 0)
        //        {
        //            mIsRestrictBGM = true;
        //        }
        //    }

        //    foreach (KeyValuePair<string, Zone> z in mGalaxy.GetZones())
        //        zoneListsBox.Items.Add(z.Key);

        //    mIsInitialized = true;
        //}

        //Galaxy mGalaxy;
        //BGMInfo.BGMInfoEntry mInfoEntry;
        //List<BGMInfo.ScenarioBGMEntry> mScenarioEntries;
        //List<int> mBGMRestrictedIDs;
        //bool mIsRestrictBGM = false;
        //Dictionary<int, Scenario> mScenarios;
        //int mCurScenario;
        //bool mIsInitialized = false;

        //private void scenarioListTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    int scenarioNo = Convert.ToInt32(scenarioListTreeView.SelectedNode.Tag);

        //    if (scenarioListTreeView.SelectedNode != null && !mBGMRestrictedIDs.Contains(scenarioNo))
        //    {
        //        mIsInitialized = false;
        //        mCurScenario = scenarioNo;

        //        if (mIsRestrictBGM || mBGMRestrictedIDs.Contains(scenarioNo))
        //        {
        //            scenarioNo = 0;
        //        }

        //        BGMInfo.ScenarioBGMEntry scenarioEntry = mScenarioEntries.Find(entry => entry.ScenarioNo == scenarioNo);

        //        scenarioBGMId.Text = scenarioEntry.Entry.Get<string>("BgmIdName");
        //        scenarioBGMStartType.Value = scenarioEntry.Entry.Get<int>("StartType");
        //        scenarioBGMStartFrame.Value = scenarioEntry.Entry.Get<int>("StartFrame");
        //        scenarioBGMIsPrepare.Checked = scenarioEntry.Entry.Get<int>("IsPrepare") != 0;

        //        Scenario scenario = mScenarios[mCurScenario];
        //        scenarioNameTxt.Text = scenario.mEntry.Get<string>("ScenarioName");
        //        powerStarID.Value = scenario.mEntry.Get<int>("PowerStarId");
        //        appearPowerStarTxt.Text = scenario.mEntry.Get<string>("AppearPowerStarObj");
        //        powerStarTypeTxt.Text = scenario.mEntry.Get<string>("PowerStarType");
        //        cometTypeTxt.Text = scenario.mEntry.Get<string>("Comet");
        //        cometTimer.Value = scenario.mEntry.Get<int>("CometLimitTimer");

        //        mIsInitialized = true;
        //    }
        //}

        //private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tabControl1.SelectedTab != null && tabControl1.SelectedTab.Tag != null)
        //    {
        //        if (tabControl1.SelectedTab.Tag.ToString() == "1")
        //        {
        //            foreach (TreeNode n in scenarioListTreeView.Nodes)
        //            {
        //                if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
        //                    n.ForeColor = SystemColors.GrayText;
        //            }
        //        }
        //        else
        //        {
        //            foreach (TreeNode n in scenarioListTreeView.Nodes)
        //            {
        //                if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
        //                    n.ForeColor = SystemColors.ControlText;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (TreeNode n in scenarioListTreeView.Nodes)
        //        {
        //            if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
        //                n.ForeColor = SystemColors.ControlText;
        //        }
        //    }
        //}

        //private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (TreeNode n in scenarioListTreeView.Nodes)
        //    {
        //        if (mBGMRestrictedIDs.Contains(Convert.ToInt32(n.Tag)))
        //            n.ForeColor = SystemColors.ControlText;
        //    }
        //}

        //private void zoneListsBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ResetLayers();
        //    ToggleLayersEnabled(true);

        //    if (zoneListsBox.SelectedIndex != -1)
        //    {
        //        string zoneName = Convert.ToString(zoneListsBox.SelectedItem);

        //        BCSV.Entry entry = mScenarios[mCurScenario + 1].mEntry;

        //        int mask = entry.Get<int>(zoneName);
        //        List<string> layers = GameUtil.GetGalaxyLayers(mask);

        //        foreach (Control c in layerMasksBox.Controls)
        //        {
        //            if (c.Tag == null)
        //                continue;

        //            string tag = c.Tag.ToString();
        //            if (c is CheckBox && tag.StartsWith("Layer"))
        //            {
        //                CheckBox box = c as CheckBox;
        //                if (layers.Contains(tag))
        //                    box.Checked = true;
        //            }
        //        }
        //    }
        //}

        //private void ResetLayers()
        //{
        //    foreach (Control c in layerMasksBox.Controls)
        //    {
        //        if (c is CheckBox)
        //        {
        //            CheckBox box = c as CheckBox;
        //            box.Checked = false;
        //        }
        //    }
        //}

        //private void saveScenarioBtn_Click(object sender, EventArgs e)
        //{
        //    mGalaxy.SaveScenario();
        //    BGMInfo.Save();
        //}

        //private void NumericInt_ValueChanged(object sender, EventArgs e)
        //{
        //    if (!mIsInitialized)
        //        return;

        //    NumericUpDown n = sender as NumericUpDown;

        //    // time for some hacks
        //    if (tabControl2.SelectedTab.Text == "BGM")
        //    {
        //        if (tabControl1.SelectedTab.Text == "Stage")
        //        {
        //            mInfoEntry.Entry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
        //        }
        //        else
        //        {
        //            int scenario = 0;

        //            // if we are in a restricted scenario, we default to changing the entry with scenario 0
        //            if (!mBGMRestrictedIDs.Contains(mCurScenario))
        //            {
        //                scenario = mCurScenario;
        //            }

        //            BGMInfo.ScenarioBGMEntry scenarioEntry = mScenarioEntries.Find(entry => entry.Entry.Get<int>("ScenarioNo") == 0);
        //            scenarioEntry.Entry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
        //        }
        //    }
        //    else
        //    {
        //        mScenarios[mCurScenario].mEntry.Set(n.Tag.ToString(), Convert.ToInt32(n.Value));
        //    }
        //}

        //private void TextBox_ValueChanged(object sender, EventArgs e)
        //{
        //    if (!mIsInitialized)
        //        return;

        //    TextBox n = sender as TextBox;

        //    // time for some hacks
        //    if (tabControl2.SelectedTab.Text == "BGM")
        //    {
        //        if (tabControl1.SelectedTab.Text == "Stage")
        //        {
        //            mInfoEntry.Entry.Set(n.Tag.ToString(), n.Text);
        //        }
        //        else
        //        {
        //            int scenario = 0;

        //            // if we are in a restricted scenario, we default to changing the entry with scenario 0
        //            if (!mBGMRestrictedIDs.Contains(mCurScenario))
        //            {
        //                scenario = mCurScenario;
        //            }

        //            BGMInfo.ScenarioBGMEntry scenarioEntry = mScenarioEntries.Find(entry => entry.Entry.Get<int>("ScenarioNo") == 0);
        //            scenarioEntry.Entry.Set(n.Tag.ToString(), n.Text);
        //        }
        //    }
        //    else
        //    {
        //        mScenarios[mCurScenario].mEntry.Set(n.Tag.ToString(), n.Text);
        //    }
        //}

        //private void ToggleLayersEnabled(bool isEnabled)
        //{
        //    foreach (Control c in layerMasksBox.Controls)
        //    {
        //        if (c is CheckBox)
        //        {
        //            CheckBox box = c as CheckBox;
        //            box.Enabled = isEnabled;
        //        }
        //    }
        //}

        //private void ZoneLayerCheckbox_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox c = sender as CheckBox;
        //    string zone = zoneListsBox.SelectedItem.ToString();
        //    int newMask = GameUtil.SetLayerOnMask(c.Tag.ToString(), mScenarios[mCurScenario + 1].mEntry.Get<int>(zone), c.Checked);
        //    mScenarios[mCurScenario + 1].mEntry.Set(zone, newMask);
        //}
    }
}
