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

namespace Takochu.ui
{
    public partial class HashGenForm : Form
    {
        public HashGenForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = BCSV.FieldNameToHash(textBox1.Text).ToString("X8");
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void AddToFieldNamesBtn_Click(object sender, EventArgs e)
        {
            BCSV.AddHash(textBox1.Text);
        }
    }
}
