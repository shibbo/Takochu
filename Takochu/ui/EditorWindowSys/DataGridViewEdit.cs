using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.smg.obj;


namespace Takochu.ui.EditorWindowSys
{
    /*
    ********************************************************************************************************************
    In progress. 2021/12～
    Create a class to display the properties of an object in the data grid view.
    Specifically, you can generate a data table to put into the data source of the data grid view in the editor window.

    [Implemented features]
    1. Viewing Object Properties

    [ToDo]
    1. Implement a function to translate data names.
    2. Reflect the changed data in the actual object.

    By penguin117117
    ********************************************************************************************************************
    */

    public class DataGridViewEdit
    {
        private DataColumn cLeft, cRight;
        private AbstractObj _abstObj;
        private DataGridView _dataGridView;

        /// <summary>
        /// Set "DataGridView" to display the properties of the object displayed in the editor window.
        /// </summary>
        /// <param name="dataGridView">Specify the target "DataGridView".</param>
        /// <param name="abstObj">Specify the "AbstractObj" class or the XXXXObj class that inherits from the "AbstractObj" class.</param>
        public DataGridViewEdit(DataGridView dataGridView,AbstractObj abstObj) 
        {
            _dataGridView = dataGridView;
            _abstObj = abstObj;
        }

        /// <summary>
        /// Create and retrieve a data table.
        /// </summary>
        /// <returns><see cref="DataTable"/></returns>
        public DataTable GetDataTable() 
        {
            Initialize();
            NullCheck();

            DataTable dt = new DataTable();
            
            SetColumn(ref dt);
            SetRow(ref dt);

            return dt;
        }

        private void Initialize()
        {
            if (_dataGridView.DataSource != null)
                _dataGridView.DataSource = null;

            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToResizeRows = false;
            _dataGridView.AllowUserToResizeColumns = false;
        }

        private void NullCheck() 
        {
            if (_abstObj == null)
                throw new Exception("GalaxyObject is null");
        }

        private void SetColumn(ref DataTable dt) 
        {
            cLeft = dt.Columns.Add("Name");
            {
                cLeft.DataType = Type.GetType("System.String");
                cLeft.ColumnName = "Info";
                cLeft.ReadOnly = true;
                cLeft.Unique = true;
                cLeft.AutoIncrement = false;
            }


            cRight = dt.Columns.Add("Value");
            {
                cRight.DataType = Type.GetType("System.Object");
                cRight.ColumnName = "Value";
                cRight.ReadOnly = false;
                cRight.Unique = false;
                cRight.AutoIncrement = false;
            }
        }

        private void SetRow(ref DataTable dt) 
        {
            foreach (var ObjEntry in _abstObj.mEntry)
            {
                var DisplayName = BCSV.HashToFieldName(ObjEntry.Key);

                var row = dt.NewRow();
                {
                    row.SetField(cLeft, DisplayName); row.SetField(cRight, ObjEntry.Value);
                }
                dt.Rows.Add(row);
            }
        }
    }
}
