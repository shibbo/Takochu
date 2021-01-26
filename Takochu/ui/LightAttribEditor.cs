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
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lightsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lightsList.SelectedIndex != -1)
            {

            }
        }
    }
}
