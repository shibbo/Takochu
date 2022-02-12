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
        private readonly Galaxy _galaxy;
        private List<BGMInfo.ScenarioBGMEntry> _scenarioEntries;
        private List<int> _bgmRestrictedIDs;
        private int _currentScenario;
        private readonly ScenarioInformation _scenarioInformation;


        public StageInfoEditor(ref Galaxy galaxy, int scenarioNo)
        {
            _galaxy = galaxy;
            InitializeComponent();

            SetGalaxyImageLayouts();

            Timer_Label.Text = ScenarioEntry.TimerNameFromGameVer;

            
            

            SetComboBoxItems(CometTypeComboBox  ,ScenarioEntry.Comets                 );
            SetComboBoxItems(AppearStarObj      ,ScenarioEntry.AppearStarObjs         );
            SetComboBoxItems(ZoneComboBox       ,_galaxy.GetZoneNames().ToArray());

            //SMG1,SMG2それぞれ固有の設定を行う。
            if (GameUtil.IsSMG1())
            {
                StarType.Enabled = false;
                MainTabControl.TabPages.Remove(MainTabControl.TabPages["BGM_InfoTabPage"]);
            }
            else 
            {
                HiddenCheckBox.Enabled = false;
                SetComboBoxItems(StarType, ScenarioEntry.StarType);
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

            StageBGMListBox.SelectedIndex = StageBGMListBox.Items.IndexOf(_galaxy.mName);
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
            GalaxyNameLabel.Text        = _galaxy.mHolderName;
        }

        /// <summary>
        /// テキストボックスなどにシナリオの情報を表示
        /// </summary>
        /// <param name="scenario">対象のシナリオ</param>
        private void SetScenarioInfo(ScenarioEntry scenario)
        {
            ScenarioName.Text     = scenario.ScenarioName;
            AppearPowerStar.Text  = scenario.AppearStarObj;
            PowerStarID.Value     = scenario.PowerStarID;


            if (GameUtil.IsSMG2())
            {
                TimerNumericUpDown.Value = scenario.CometLimitTimer;
                StarType.SelectedIndex = Array.IndexOf(ScenarioEntry.StarType, scenario.PowerStarType);
            }
            else 
            {
                HiddenCheckBox.Checked = Convert.ToBoolean(scenario.IsHidden);

                //値が存在しない場合があるのでチェックしています。
                if (scenario.LuigiModeTimer != default)
                {
                    TimerNumericUpDown.Value = scenario.LuigiModeTimer;
                    TimerNumericUpDown.Enabled = true;
                }
                else 
                {
                    TimerNumericUpDown.Value = 0;
                    TimerNumericUpDown.Enabled = false;
                }
                    
            }
            
            AppearStarObj.SelectedItem = scenario.AppearStarObj;

            if (!scenario.Entry.ContainsKey("Comet")) 
            {
                CometTypeComboBox.Enabled = false;
                return;
            }

            CometTypeComboBox.Enabled = true;
            if (scenario.Comet == string.Empty) 
            {
                CometTypeComboBox.SelectedIndex = 0;
                return;
            }

            CometTypeComboBox.SelectedIndex = Array.IndexOf(ScenarioEntry.Comets, scenario.Comet);
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
                SetScenarioInfo(_scenarioInformation.ScenarioBCSV[_currentScenario]);
                ZoneComboBox.SelectedItem = _galaxy.mName;
                UseLayerCheckBoxReload();
            }
            ReStart_ControlsEvent();
        }

        /// <summary>
        /// コントロールの BindingContextChanged を無効にします。
        /// </summary>
        private void Pause_ControlsEvent() 
        {
            foreach (Control control in UseLayerFlags.Controls) 
            {
                control.BindingContextChanged -= new EventHandler(ZoneLayerCheckbox_CheckedChanged);
            }
            foreach (Control control in ShowScenarioStarFlags.Controls)
            {
                control.BindingContextChanged -= new EventHandler(ShowScenarioCheckBox_CheckedChanged);
            }
            TimerNumericUpDown.ValueChanged -= new EventHandler(TimerNumericInt_ValueChanged);

        }

        /// <summary>
        /// コントロールの BindingContextChanged を有効にします。
        /// </summary>
        private void ReStart_ControlsEvent() 
        {
            TimerNumericUpDown.ValueChanged += new EventHandler(TimerNumericInt_ValueChanged);
            foreach (Control control in UseLayerFlags.Controls)
            {
                control.BindingContextChanged += new EventHandler(ZoneLayerCheckbox_CheckedChanged);
            }
            foreach (Control control in ShowScenarioStarFlags.Controls)
            {
                control.BindingContextChanged += new EventHandler(ShowScenarioCheckBox_CheckedChanged);
            }
        }

        

        private void BGMTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        

        private void ZoneComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Pause_ControlsEvent();
            {
                UseLayerCheckBoxReload();
            }
            ReStart_ControlsEvent();
        }

        /// <summary>
        /// ゾーンで使用されるレイヤーのチェックボックスを再読み込みします。
        /// </summary>
        private void UseLayerCheckBoxReload()
        {
            if (ZoneComboBox.SelectedIndex < 0) return;

            var scenario   = _scenarioInformation.ScenarioBCSV[_currentScenario];
            var zoneName = Convert.ToString(ZoneComboBox.SelectedItem);
            
            foreach (Control control in UseLayerFlags.Controls)
            {
                if (control.Tag == null) continue;

                string tagName = control.Tag.ToString();

                //tagName = GameUtil.IsSMG1() ? tagName.ToLower() : tagName;

                if (!(control is CheckBox)) continue;
                CheckBox checkBox = control as CheckBox;

                checkBox.Checked = scenario.ZoneMasks[zoneName].First(layer => layer.Name == tagName).Checked;
            }

            int[] powerStarIDArray = new int[] { scenario.PowerStarID };
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

        private void saveScenarioBtn_Click(object sender, EventArgs e)
        {

            //BGMInfo.Save();
        }

        private void TimerNumericInt_ValueChanged(object sender, EventArgs e)
        {
            
            
        }

        private void TextBox_ValueChanged(object sender, EventArgs e)
        {
            //if (!misInitialized)
            //    return;

            

            //_scenarioInformation.Scenarios[_currentScenario].ScenarioName
            // time for some hacks
            //if (MainTabControl.SelectedTab.Text != "BGM_InfoTabPage") return;



            //if (BGMTabControl.SelectedTab.Text == "StageBGM_InfoTabPage")
            //{
            //    //_infoEntry.Entry.Set(textBox.Tag.ToString(), textBox.Text);
            //}
            //else
            //{
            //    int scenario = 0;

            //    // if we are in a restricted scenario, we default to changing the entry with scenario 0
            //    if (!_bgmRestrictedIDs.Contains(_currentScenario))
            //    {
            //        scenario = _currentScenario;
            //    }

            //    BGMInfo.ScenarioBGMEntry scenarioEntry = _scenarioEntries.Find(entry => entry.Entry.Get<int>("ScenarioNo") == 0);
            //    //scenarioEntry.Entry.Set(textBox.Tag.ToString(), textBox.Text);
            //}


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
            var scenario = _scenarioInformation.ScenarioBCSV[_currentScenario];

            scenario.ChangeZoneMask(checkbox, zoneName);
            scenario.SetZoneMask(zoneName);
            UseLayerCheckBoxReload();

            //int newMask = /*GetLayerMask(checkbox)*/scenario.mTest_ZoneMasks[zoneName][scenario.GetLayerIndex(checkbox,zoneName)];
            /*GameUtil.SetLayerOnMask(checkbox.Tag.ToString(), scenario.mZoneMasks[zoneName], checkbox.Checked);*/
            //_scenarioInformation.Scenarios[_currentScenario].mEntry.Set(zoneName, newMask);
        }

        private void ShowScenarioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            var scenario = _scenarioInformation.ScenarioBCSV[_currentScenario];

            var a = checkbox.Checked;



            int[] powerStarIDArray = new int[] { scenario.PowerStarID };
            var bitarray = new BitArray(powerStarIDArray);

            for (int i = 0; i < ShowScenarioStarFlags.Controls.Count; i++)
            {
                var ControlTagString = ShowScenarioStarFlags.Controls[i].Tag.ToString();
                if (!ControlTagString.StartsWith("Scenario")) continue;

                

                var scenarioNoChars = ControlTagString.Skip("Scenario".Length).ToArray();
                var scenarioNo = int.Parse(string.Concat(scenarioNoChars));

                if (!(ShowScenarioStarFlags.Controls[i] is CheckBox)) continue;

                var cb = ShowScenarioStarFlags.Controls[i] as CheckBox;

                bitarray[scenarioNo - 1] = cb.Checked;
            }



            scenario.ChangeShowScenario(bitarray);
            scenario.SetShowScenario(ZoneComboBox.SelectedItem.ToString());

            UseLayerCheckBoxReload();
        }


        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _galaxy.ScenarioARC.ScenarioDataSave(_scenarioInformation.ScenarioBCSV);
            _galaxy.SaveScenario();
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

        private void ScenarioName_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            var scenario = _scenarioInformation.ScenarioBCSV[_currentScenario];
            scenario.ChangeScenarioName(textBox.Text);
            scenario.SetScenarioName();
        }

        private void AppearStarObj_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var scenario = _scenarioInformation.ScenarioBCSV[_currentScenario];
            scenario.ChangeAppearStarObj(comboBox.SelectedItem.ToString());
            scenario.SetAppearStarObj();
        }

        private void StarType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.Enabled == false) return;
            var scenario = _scenarioInformation.ScenarioBCSV[_currentScenario];
            scenario.ChangePowerStarType(comboBox.SelectedItem.ToString());
            scenario.SetPowerStarType();
        }

        private void CometTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.Enabled == false) return;
            var scenario = _scenarioInformation.ScenarioBCSV[_currentScenario];
            scenario.ChangeComet(comboBox.SelectedItem.ToString());
            scenario.SetComet();
        }

        private void TimerNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var numericInt = sender as NumericUpDown;
            if (numericInt.Enabled == false) return;
            var scenario = _scenarioInformation.ScenarioBCSV[_currentScenario];
            scenario.ChangeTimer(Convert.ToInt32(numericInt.Value));
            scenario.SetTimer();
        }

        private void HiddenCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (GameUtil.IsSMG2()) return;
            CheckBox checkbox = sender as CheckBox;
            if (checkbox.Enabled == false) return;
            var scenario = _scenarioInformation.ScenarioBCSV[_currentScenario];
            scenario.ChangeIsHidden(checkbox.Checked);
            scenario.SetIsHidden();
        }
    }
}
