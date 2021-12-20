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
    2. Reflect the changed data in the actual object.(Only partially works. rot,pos,scale)

    By penguin117117
    ********************************************************************************************************************
    */

    public class DataGridViewEdit
    {
        private DataColumn cLeft, cRight;
        private AbstractObj _abstObj;
        private readonly DataGridView _dataGridView;
        private DataTable _dt;

        /// <summary>
        /// Set "DataGridView" to display the properties of the object displayed in the editor window.
        /// </summary>
        /// <param name="dataGridView">Specify the target "DataGridView".</param>
        /// <param name="abstObj">Specify the "AbstractObj" class or the XXXXObj class that inherits from the "AbstractObj" class.</param>
        public DataGridViewEdit(DataGridView dataGridView, AbstractObj abstObj)
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

            _dt = dt;

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

        public void ChangeValue(int rowIndex, object value)
        {
            var a = _abstObj.mEntry.Keys;
            var name = BCSV.HashToFieldName(a.ElementAt(rowIndex));

            //Do not allow the object name to be changed.
            if (name == "name") return;


            Change_mValues(name, value);
        }

        /*
        [HACK]
        Not smart processing, 
        the object does not move without changing the mValue,
        so I have no choice but to use the switch case.
        If you have a solution, please fix it.
        By penguin117117
         */
        private void Change_mValues(string name, object value)
        {
            float ftmp = GetFloat_And_Limiter(value);

            switch (name)
            {
                case "pos_x":
                    _abstObj.mTruePosition.X = ftmp;
                    break;
                case "pos_y":
                    _abstObj.mTruePosition.Y = ftmp;
                    break;
                case "pos_z":
                    _abstObj.mTruePosition.Z = ftmp;
                    break;
                case "dir_x":
                    _abstObj.mTrueRotation.X = ftmp;
                    break;
                case "dir_y":
                    _abstObj.mTrueRotation.Y = ftmp;
                    break;
                case "dir_z":
                    _abstObj.mTrueRotation.Z = ftmp;
                    break;
                case "scale_x":
                    _abstObj.mScale.X = ftmp;
                    break;
                case "scale_y":
                    _abstObj.mScale.Y = ftmp;
                    break;
                case "scale_z":
                    _abstObj.mScale.Z = ftmp;
                    break;
                default:
                    //処理設定されている物以外は値の変更を行わない。
                    //You cannot change any value other than the one that is set.
                    return;
            }
            _abstObj.mEntry.Set(name, value);
        }

        private float GetFloat_And_Limiter(object value)
        {

            if (!float.TryParse(value.ToString(), out float ftmp)) return 0f;
            ftmp = Convert.ToSingle(value);
            if (ftmp > float.MaxValue) ftmp = float.MaxValue;
            if (ftmp < float.MinValue) ftmp = float.MinValue;

            return ftmp;
        }
    }
}
