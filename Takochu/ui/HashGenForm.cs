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
using System.IO;

namespace Takochu.ui
{
    public partial class HashGenForm : Form
    {
        private string[] data;
        public HashGenForm()
        {
            InitializeComponent();
#if DEBUG
            var pathdir = Path.GetDirectoryName(Application.ExecutablePath);
            var path = pathdir + "\\GhidraStringData.txt";
            var savePath = pathdir + "\\GhidraHashData.txt";
            if (File.Exists(path)) 
            {
                 File.WriteAllLines(savePath, CalcHashFromTextFile(path));
                return;
            }
            Console.WriteLine($"NotRead: {path}");
#endif
        }

        private string[] CalcHashFromTextFile(string path) 
        {

            var stringdata = File.ReadAllLines(path);
            stringdata = stringdata.Distinct().ToArray();
            data = new string[stringdata.Length];
            for (int i = 0; i < stringdata.Length; i++) 
            {
                data[i] = BCSV.FieldNameToHash(stringdata[i].Replace("\n","")).ToString("X8")+","+stringdata[i];

            }

            return data;
            
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