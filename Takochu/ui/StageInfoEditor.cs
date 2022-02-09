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
using System.Reflection;

namespace Takochu.ui
{
    public partial class StageInfoEditor : Form
    {
        private Galaxy _galaxy;
        private List<BGMInfo.ScenarioBGMEntry> _scenarioEntries;
        private List<int> _bgmRestrictedIDs;
        private int _currentScenario;
        private readonly ScenarioInformation _scenarioInformation;


        public StageInfoEditor(ref Galaxy galaxy, int scenarioNo)
        {
            _galaxy = galaxy;
            InitializeComponent();
            //ToggleLayersEnabled(UseLayerFlags, false);

            SetGalaxyImageLayouts();

            Timer_Label.Text = Scenario.TimerNameFromGameVer;

            SetComboBoxItems(CometTypeComboBox  ,Scenario.Comets                 );
            SetComboBoxItems(AppearStarObj      ,Scenario.AppearStarObjs         );
            SetComboBoxItems(ZoneComboBox       ,_galaxy.GetZoneNames().ToArray());

            //SMG1,SMG2それぞれ固有の設定を行う。
            if (GameUtil.IsSMG1())
            {
                StarType.Enabled = false;
                MainTabControl.TabPages.Remove(MainTabControl.TabPages["BGM_InfoTabPage"]);
            }
            else 
            {
                SetComboBoxItems(StarType, Scenario.StarType);
                var scenarioBGM_dgv = new ScenarioBGMInfo_DataGridView(ScenBGM_dgv);
                ScenBGM_dgv = scenarioBGM_dgv.GetDataTable();
                SetStageBGMListBox();
            }

            _scenarioInformation = new ScenarioInformation(_galaxy, ScenarioListTreeView);
            

#if DEBUG
#else
            //デバッグのタブページを削除
            MainTabControl.TabPages.Remove(MainTabControl.TabPages["DebugTabPage"]);
#endif
        }

        private void SetComboBoxItems(ComboBox cb, string[] strs) 
        {
            foreach (var cometName in strs)
            {
                cb.Items.Add(cometName);
            }
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
            GalaxyNameLabel.Top        -= GalaxyInfoPictureBox.Top;
            GalaxyNameLabel.Left       -= GalaxyInfoPictureBox.Left;
            GalaxyNameLabel.BackColor   = Color.Transparent;
            GalaxyNameLabel.Text        = _galaxy.mGalaxyName;
        }

        /// <summary>
        /// テキストボックスなどにシナリオの情報を表示
        /// </summary>
        /// <param name="scenario">対象のシナリオ</param>
        private void SetScenarioInfo(Scenario scenario)
        {
            ScenarioName.Text     = scenario.mScenarioName;
            AppearPowerStar.Text  = scenario.mAppearPowerStar;
            PowerStarID.Value     = scenario.mPowerStarID;

            if (GameUtil.IsSMG2())
            {
                TimerNumericUpDown.Value = scenario.mCometLimitTimer;
                StarType.SelectedIndex = Array.IndexOf(Scenario.StarType, scenario.mPowerStarType);
            }
            else 
            {
                //値が存在しない場合があるのでチェックしています。
                if (scenario.mLuigiModeTimer != default)
                {
                    TimerNumericUpDown.Value = scenario.mLuigiModeTimer;
                    TimerNumericUpDown.Enabled = true;
                }
                else 
                {
                    TimerNumericUpDown.Value = 0;
                    TimerNumericUpDown.Enabled = false;
                }
                    
            }
            
            AppearStarObj.SelectedItem = scenario.mAppearPowerStar;

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

            CometTypeComboBox.SelectedIndex = Array.IndexOf(Scenario.Comets, scenario.mComet);
        }

        private void ScenarioListTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var scenarioTreeView = (TreeView)sender;

            if (scenarioTreeView.Nodes.Count < 1) return;

            _currentScenario = Convert.ToInt32(scenarioTreeView.SelectedNode.Tag);

            /*
             * ツリーノードのインデックスを選択した際に、
             * データが再入力されるのでイベントを一時停止する必要がある。
             */
            Pause_ControlsEvent();
            {
                SetScenarioInfo(_scenarioInformation.Scenarios[_currentScenario]);
                ZoneComboBox.SelectedItem = _galaxy.mName;
                //ResetLayers(layerMasksBox);
                UseLayerCheckBoxReload();
            }
            ReStart_ControlsEvent();

            
            //StageBGM_InfoTabPage.Controls;
        }

        /// <summary>
        /// コントロールの BindingContextChanged を無効にします。
        /// </summary>
        private void Pause_ControlsEvent() 
        {
            
            ZoneComboBox.SelectedIndexChanged -= new EventHandler(ZoneComboBox_SelectedIndexChanged);
            foreach (Control control in UseLayerFlags.Controls) 
            {
                control.BindingContextChanged -= new System.EventHandler(this.ZoneLayerCheckbox_CheckedChanged);
            }

        }

        private void ReStart_ControlsEvent() 
        {
            ZoneComboBox.SelectedIndexChanged += new EventHandler(ZoneComboBox_SelectedIndexChanged);
            foreach (Control control in UseLayerFlags.Controls)
            {
                control.BindingContextChanged += new System.EventHandler(this.ZoneLayerCheckbox_CheckedChanged);
            }
        }

        

        private void BGMTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ZoneComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control control in UseLayerFlags.Controls)
            {
                control.BindingContextChanged -= new EventHandler(this.ZoneLayerCheckbox_CheckedChanged);
            }
            UseLayerCheckBoxReload();
            foreach (Control control in UseLayerFlags.Controls)
            {
                control.BindingContextChanged += new EventHandler(this.ZoneLayerCheckbox_CheckedChanged);
            }
        }


        /// <summary>
        /// ゾーンで使用されるレイヤーのチェックボックスを再読み込みします。
        /// </summary>
        private void UseLayerCheckBoxReload()
        {
            

            if (ZoneComboBox.SelectedIndex < 0) return;

            
            //ResetLayers(UseLayerFlags);
            


            //ToggleLayersEnabled(UseLayerFlags, true);

            var scenario   = _scenarioInformation.Scenarios[_currentScenario];
            var zoneName = Convert.ToString(ZoneComboBox.SelectedItem);
            var maskBitData = scenario.GetZoneMaskInt(zoneName);
            var layerList    = GameUtil.GetGalaxyLayers(maskBitData);

            DebugTextBox.Text = string.Empty;
            DebugTextBox.Text = maskBitData.ToString();




            foreach (Control control in UseLayerFlags.Controls)
            {
                if (control.Tag == null) continue;

                string tag = control.Tag.ToString();

                //var maskchecked = scenario.mTest_ZoneMasks[zoneName].Select(x => x.Name == tag).ToArray();

                tag = GameUtil.IsSMG1() ? tag.ToLower() : tag;



                if (!(control is CheckBox)) continue;
                CheckBox checkBox = control as CheckBox;



                if (!layerList.Contains(tag)) continue;
                checkBox.Checked = scenario.mTest_ZoneMasks[zoneName].First(x => x.Name == tag).Checked;
            }


            //ResetLayers(ShowScenarioStarFlags);
            //ToggleLayersEnabled(ShowScenarioStarFlags, true);
            int[] powerStarIDArray = new int[] { scenario.mPowerStarID };
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
            var scenario = _scenarioInformation.Scenarios[_currentScenario];


            scenario.ChangeZoneMask(checkbox, zoneName);
            //ResetLayers(layerMasksBox);
            //UseLayerCheckBoxReload();

            scenario.SetZoneMask(zoneName);


            //int newMask = /*GetLayerMask(checkbox)*/scenario.mTest_ZoneMasks[zoneName][scenario.GetLayerIndex(checkbox,zoneName)];
            /*GameUtil.SetLayerOnMask(checkbox.Tag.ToString(), scenario.mZoneMasks[zoneName], checkbox.Checked);*/
            //_scenarioInformation.Scenarios[_currentScenario].mEntry.Set(zoneName, newMask);
        }

        private int GetLayerMask(CheckBox cb) 
        {


            int newMask = 0;

            var scenario = _scenarioInformation.Scenarios[_currentScenario];
            var powerStarID = scenario.mPowerStarID;
            var maskBitData = scenario.mZoneMasks[Convert.ToString(ZoneComboBox.SelectedItem)];
            var layerList = GameUtil.GetGalaxyLayers(maskBitData);

            int[] maskBitArr = { maskBitData };
            var bitarr = new BitArray(maskBitArr);

            var checkBoxText = cb.Text;
            if (checkBoxText == "Common") return maskBitData;
            if (GameUtil.IsSMG1()) checkBoxText = checkBoxText.ToLower();

            var find = layerList.IndexOf(checkBoxText);

            if (find > -1) throw new OverflowException($"Negative values for array indices are not allowed.");

            bitarr.Set(find + 1, cb.Checked);

            bitarr.CopyTo(maskBitArr, 0);

            newMask = maskBitArr[0];
            //foreach (Control control in UseLayerFlags.Controls)
            //{
            //    if (control.Tag == null) continue;

            //    string layerName = control.Tag.ToString();

            //    layerName = GameUtil.IsSMG1() ? layerName.ToLower() : layerName;

            //    if (!(control is CheckBox)) continue;
            //    CheckBox checkBox = control as CheckBox;

            //    if (!layerList.Contains(layerName)) continue;
            //    checkBox.Checked = bitarr;
            //}


            return newMask;
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
    }
}
