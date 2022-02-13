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
    public partial class StageObjectAttrFinder : Form
    {
        public StageObjectAttrFinder(string attribute_name)
        {
            InitializeComponent();
            mAttributeName = attribute_name;
            objectsDataGrid.Columns[3].HeaderText = attribute_name;
        }

        public void AddRow(int id, string name, string zone_name, object value)
        {
            objectsDataGrid.Rows.Add(new object[] { id, name, zone_name, value });
        }

        string mAttributeName;
    }
}
