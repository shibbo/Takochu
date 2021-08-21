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

namespace Takochu.ui
{
    public partial class IntroEditor : Form
    {
        public IntroEditor(ref Galaxy galaxy)
        {
            InitializeComponent();

            mParentZone = galaxy.GetGalaxyZone();
            mCamera = mParentZone.GetIntroCamera(galaxy.mScenarioNo - 1);

            this.Text = $"Intro Editor -- Editing {galaxy.mName}, Scenario {galaxy.mScenarioNo}";
        }

        Zone mParentZone;
        CANM mCamera;

        private void framesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            framesDataGrid.Rows.Clear();

            KeyFrames frames = mCamera.GetKeyFrames(framesList.SelectedItem.ToString());

            for (int i = 0; i < frames.GetCount(); i += 3)
            {
                framesDataGrid.Rows.Add(frames.GetData()[i + 0], frames.GetData()[i + 1], frames.GetData()[i + 2]);
            }
        }
    }
}
