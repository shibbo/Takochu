using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.smg;

namespace Takochu.ui
{
    public partial class LightAttribEditor : Form
    {
        public LightAttribEditor(string areaLightName)
        {
            InitializeComponent();

            List<string> areaNames = LightData.GetNames();

            areaNames.ForEach(n => lightsList.Items.Add(n));

            lightsList.SelectedIndex = areaNames.IndexOf(areaLightName);
        }

        void UpdateEditor()
        {
            LightEntry e = LightData.Get(mCurrentLight);

            lightNameTxt.Text = e.mAreaLightName;
            lightInterpolate.Value = Convert.ToDecimal(e.Get<int>("Interpolate"));


            #region Player Light
            playerLight0ColorR.Value = Convert.ToDecimal(e.Get<byte>("PlayerLight0ColorR"));
            playerLight0ColorG.Value = Convert.ToDecimal(e.Get<byte>("PlayerLight0ColorG"));
            playerLight0ColorB.Value = Convert.ToDecimal(e.Get<byte>("PlayerLight0ColorB"));
            playerLight0ColorA.Value = Convert.ToDecimal(e.Get<byte>("PlayerLight0ColorA"));

            playerLight0Color.BackColor = Color.FromArgb((byte)playerLight0ColorR.Value, (byte)playerLight0ColorG.Value, (byte)playerLight0ColorB.Value);

            playerLight1ColorR.Value = Convert.ToDecimal(e.Get<byte>("PlayerLight1ColorR"));
            playerLight1ColorG.Value = Convert.ToDecimal(e.Get<byte>("PlayerLight1ColorG"));
            playerLight1ColorB.Value = Convert.ToDecimal(e.Get<byte>("PlayerLight1ColorB"));
            playerLight1ColorA.Value = Convert.ToDecimal(e.Get<byte>("PlayerLight1ColorA"));

            playerLight1Color.BackColor = Color.FromArgb((byte)playerLight1ColorR.Value, (byte)playerLight1ColorG.Value, (byte)playerLight1ColorB.Value);

            playerLightAmbientR.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientR"));
            playerLightAmbientG.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientG"));
            playerLightAmbientB.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientB"));
            playerLightAmbientA.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientA"));

            playerLightAmbient.BackColor = Color.FromArgb((byte)playerLightAmbientR.Value, (byte)playerLightAmbientG.Value, (byte)playerLightAmbientB.Value);

            playerLight0X.Value = Convert.ToDecimal(e.Get<float>("PlayerLight0PosX"));
            playerLight0Y.Value = Convert.ToDecimal(e.Get<float>("PlayerLight0PosY"));
            playerLight0Z.Value = Convert.ToDecimal(e.Get<float>("PlayerLight0PosZ"));

            playerLight1X.Value = Convert.ToDecimal(e.Get<float>("PlayerLight1PosX"));
            playerLight1Y.Value = Convert.ToDecimal(e.Get<float>("PlayerLight1PosY"));
            playerLight1Z.Value = Convert.ToDecimal(e.Get<float>("PlayerLight1PosZ"));

            playerLightAlpha2.Value = Convert.ToDecimal(e.Get<byte>("PlayerAlpha2"));

            playerLight0FollowCamera.Checked = e.Get<int>("PlayerLight0FollowCamera") != 0;
            playerLight1FollowCamera.Checked = e.Get<int>("PlayerLight1FollowCamera") != 0;
            #endregion

            #region Strong Light
            strongLight0ColorR.Value = Convert.ToDecimal(e.Get<byte>("StrongLight0ColorR"));
            strongLight0ColorG.Value = Convert.ToDecimal(e.Get<byte>("StrongLight0ColorG"));
            strongLight0ColorB.Value = Convert.ToDecimal(e.Get<byte>("StrongLight0ColorB"));
            strongLight0ColorA.Value = Convert.ToDecimal(e.Get<byte>("StrongLight0ColorA"));

            strongLight0Color.BackColor = Color.FromArgb((byte)strongLight0ColorR.Value, (byte)strongLight0ColorG.Value, (byte)strongLight0ColorB.Value);

            strongLight1ColorR.Value = Convert.ToDecimal(e.Get<byte>("StrongLight1ColorR"));
            strongLight1ColorG.Value = Convert.ToDecimal(e.Get<byte>("StrongLight1ColorG"));
            strongLight1ColorB.Value = Convert.ToDecimal(e.Get<byte>("StrongLight1ColorB"));
            strongLight1ColorA.Value = Convert.ToDecimal(e.Get<byte>("StrongLight1ColorA"));

            strongLight1Color.BackColor = Color.FromArgb((byte)strongLight1ColorR.Value, (byte)strongLight1ColorG.Value, (byte)strongLight1ColorB.Value);

            strongLightAmbientR.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientR"));
            strongLightAmbientG.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientG"));
            strongLightAmbientB.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientB"));
            strongLightAmbientA.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientA"));

            strongLightAmbient.BackColor = Color.FromArgb((byte)strongLightAmbientR.Value, (byte)strongLightAmbientG.Value, (byte)strongLightAmbientB.Value);

            strongLight0X.Value = Convert.ToDecimal(e.Get<float>("StrongLight0PosX"));
            strongLight0Y.Value = Convert.ToDecimal(e.Get<float>("StrongLight0PosY"));
            strongLight0Z.Value = Convert.ToDecimal(e.Get<float>("StrongLight0PosZ"));

            strongLight1X.Value = Convert.ToDecimal(e.Get<float>("StrongLight1PosX"));
            strongLight1Y.Value = Convert.ToDecimal(e.Get<float>("StrongLight1PosY"));
            strongLight1Z.Value = Convert.ToDecimal(e.Get<float>("StrongLight1PosZ"));

            strongLightAlpha2.Value = Convert.ToDecimal(e.Get<byte>("PlayerAlpha2"));

            strongLight0FollowCamera.Checked = e.Get<int>("StrongLight0FollowCamera") != 0;
            strongLight1FollowCamera.Checked = e.Get<int>("StrongLight1FollowCamera") != 0;
            #endregion

            #region Weak Light
            weakLight0ColorR.Value = Convert.ToDecimal(e.Get<byte>("WeakLight0ColorR"));
            weakLight0ColorG.Value = Convert.ToDecimal(e.Get<byte>("WeakLight0ColorG"));
            weakLight0ColorB.Value = Convert.ToDecimal(e.Get<byte>("WeakLight0ColorB"));
            weakLight0ColorA.Value = Convert.ToDecimal(e.Get<byte>("WeakLight0ColorA"));

            weakLight0Color.BackColor = Color.FromArgb((byte)weakLight0ColorR.Value, (byte)weakLight0ColorG.Value, (byte)weakLight0ColorB.Value);

            weakLight1ColorR.Value = Convert.ToDecimal(e.Get<byte>("WeakLight1ColorR"));
            weakLight1ColorG.Value = Convert.ToDecimal(e.Get<byte>("WeakLight1ColorG"));
            weakLight1ColorB.Value = Convert.ToDecimal(e.Get<byte>("WeakLight1ColorB"));
            weakLight1ColorA.Value = Convert.ToDecimal(e.Get<byte>("WeakLight1ColorA"));

            weakLight1Color.BackColor = Color.FromArgb((byte)weakLight1ColorR.Value, (byte)weakLight1ColorG.Value, (byte)weakLight1ColorB.Value);

            weakLightAmbientR.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientR"));
            weakLightAmbientG.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientG"));
            weakLightAmbientB.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientB"));
            weakLightAmbientA.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientA"));

            weakLightAmbient.BackColor = Color.FromArgb((byte)weakLightAmbientR.Value, (byte)weakLightAmbientG.Value, (byte)weakLightAmbientB.Value);

            weakLight0X.Value = Convert.ToDecimal(e.Get<float>("WeakLight0PosX"));
            weakLight0Y.Value = Convert.ToDecimal(e.Get<float>("WeakLight0PosY"));
            weakLight0Z.Value = Convert.ToDecimal(e.Get<float>("WeakLight0PosZ"));

            weakLight1X.Value = Convert.ToDecimal(e.Get<float>("WeakLight1PosX"));
            weakLight1Y.Value = Convert.ToDecimal(e.Get<float>("WeakLight1PosY"));
            weakLight1Z.Value = Convert.ToDecimal(e.Get<float>("WeakLight1PosZ"));

            weakLightAlpha2.Value = Convert.ToDecimal(e.Get<byte>("PlayerAlpha2"));

            weakLight0FollowCamera.Checked = e.Get<int>("WeakLight0FollowCamera") != 0;
            weakLight1FollowCamera.Checked = e.Get<int>("WeakLight1FollowCamera") != 0;
            #endregion

            #region Planet Light
            planetLight0ColorR.Value = Convert.ToDecimal(e.Get<byte>("PlanetLight0ColorR"));
            planetLight0ColorG.Value = Convert.ToDecimal(e.Get<byte>("PlanetLight0ColorG"));
            planetLight0ColorB.Value = Convert.ToDecimal(e.Get<byte>("PlanetLight0ColorB"));
            planetLight0ColorA.Value = Convert.ToDecimal(e.Get<byte>("PlanetLight0ColorA"));

            planetLight0Color.BackColor = Color.FromArgb((byte)planetLight0ColorR.Value, (byte)planetLight0ColorG.Value, (byte)planetLight0ColorB.Value);

            planetLight1ColorR.Value = Convert.ToDecimal(e.Get<byte>("PlanetLight1ColorR"));
            planetLight1ColorG.Value = Convert.ToDecimal(e.Get<byte>("PlanetLight1ColorG"));
            planetLight1ColorB.Value = Convert.ToDecimal(e.Get<byte>("PlanetLight1ColorB"));
            planetLight1ColorA.Value = Convert.ToDecimal(e.Get<byte>("PlanetLight1ColorA"));

            planetLight1Color.BackColor = Color.FromArgb((byte)planetLight1ColorR.Value, (byte)planetLight1ColorG.Value, (byte)planetLight1ColorB.Value);

            planetLightAmbientR.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientR"));
            planetLightAmbientG.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientG"));
            planetLightAmbientB.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientB"));
            planetLightAmbientA.Value = Convert.ToDecimal(e.Get<byte>("PlayerAmbientA"));

            planetLightAmbient.BackColor = Color.FromArgb((byte)planetLightAmbientR.Value, (byte)planetLightAmbientG.Value, (byte)planetLightAmbientB.Value);

            planetLight0X.Value = Convert.ToDecimal(e.Get<float>("PlanetLight0PosX"));
            planetLight0Y.Value = Convert.ToDecimal(e.Get<float>("PlanetLight0PosY"));
            planetLight0Z.Value = Convert.ToDecimal(e.Get<float>("PlanetLight0PosZ"));

            planetLight1X.Value = Convert.ToDecimal(e.Get<float>("PlanetLight1PosX"));
            planetLight1Y.Value = Convert.ToDecimal(e.Get<float>("PlanetLight1PosY"));
            planetLight1Z.Value = Convert.ToDecimal(e.Get<float>("PlanetLight1PosZ"));

            planetLightAlpha2.Value = Convert.ToDecimal(e.Get<byte>("PlayerAlpha2"));

            planetLight0FollowCamera.Checked = e.Get<int>("PlanetLight0FollowCamera") != 0;
            planetLight1FollowCamera.Checked = e.Get<int>("PlanetLight1FollowCamera") != 0;
            #endregion
        }

        private void lightsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lightsList.SelectedIndex != -1)
            {
                mCurrentLight = Convert.ToString(lightsList.SelectedItem);
                UpdateEditor();
            }
        }

        string mCurrentLight;

        private void NumericFloat_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown n = sender as NumericUpDown;
            LightData.Get(mCurrentLight).Set(Convert.ToString(n.Tag), Convert.ToSingle(n.Value));
        }

        private void NumericByte_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown n = sender as NumericUpDown;
            LightData.Get(mCurrentLight).Set(Convert.ToString(n.Tag), Convert.ToByte(n.Value));

            UpdateEditor();
        }

        private void NumericBoolean_ValueChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            LightData.Get(mCurrentLight).Set(Convert.ToString(cb.Tag), cb.Checked ? 1 : 0);
        }

        private void NumericInt_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown n = sender as NumericUpDown;
            LightData.Get(mCurrentLight).Set(Convert.ToString(n.Tag), Convert.ToInt32(n.Value));
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LightData.Save();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
