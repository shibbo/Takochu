using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Takochu.ui
{
    public partial class TextDialog : Form
    {
        public TextDialog()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string GetField()
        {
            return fieldName.Text;
        }
    }
}
