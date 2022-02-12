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

            mParentZone = galaxy.GetMainGalaxyZone();
            mCamera = mParentZone.GetIntroCamera(galaxy.mScenarioNo - 1);
            mScenarioNo = galaxy.mScenarioNo - 1;

            this.Text = $"Intro Editor -- Editing {galaxy.mName}, Scenario {galaxy.mScenarioNo}";
        }

        int mScenarioNo;
        Zone mParentZone;
        CANM mCamera;
        bool mIsLoaded = false;

        private void framesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            mIsLoaded = false;

            framesDataGrid.Rows.Clear();

            KeyFrames frames = mCamera.GetKeyFrames(framesList.SelectedItem.ToString());

            for (int i = 0; i < frames.GetCount(); i += 3)
            {
                framesDataGrid.Rows.Add(frames.GetData()[i + 0], frames.GetData()[i + 1], frames.GetData()[i + 2]);
            }

            mIsLoaded = true;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            mCamera.Save();
            mParentZone.SetIntroCamera(mScenarioNo, mCamera);
        }

        private void framesDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (mIsLoaded)
            {
                KeyFrames frames = mCamera.GetKeyFrames(framesList.SelectedItem.ToString());
                frames.AddKeyframe();
            }
        }

        private void framesDataGrid_CellValueChanged(object sender, EventArgs e)
        {
            if (mIsLoaded)
            {
                KeyFrames frames = mCamera.GetKeyFrames(framesList.SelectedItem.ToString());

                // find what value we are at
                int floatIdx = (framesDataGrid.CurrentCell.RowIndex * 3) + (framesDataGrid.CurrentCell.ColumnIndex);

                // and now we replace it
                float[] fr = frames.GetData();
                float.TryParse(framesDataGrid.CurrentCell.Value.ToString(), out fr[floatIdx]);
            }
        }

        private void framesDataGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (mIsLoaded)
            {
                KeyFrames frames = mCamera.GetKeyFrames(framesList.SelectedItem.ToString());

                List<float> fr = frames.GetData().ToList();
                fr.RemoveRange(e.RowIndex * 3, 3);
                frames.SetData(fr.ToArray());
            }
        }
    }
}
