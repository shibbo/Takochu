using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Takochu.ui.EditorWindowSys
{
    public class DataGridViewEdit
    {
        // Put the next line into the Declarations section.
        private DataSet dataSet;
        private DataGridView _dataGridView;

        //public DataSet GetDataSet => dataSet;
        //public DataTable GetDataTable => getDataTable();
        public DataGridViewEdit(DataGridView dataGridView) 
        {
            
            if (dataGridView.DataSource != null) 
                dataGridView.DataSource = null;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AllowUserToResizeColumns = false;

            dataGridView.DataSource = getDataTable();

            _dataGridView = dataGridView;

            

            
            //MakeDataTables();
        }

        private void CellJoint() 
        {
            
            //_dataGridView *= 2;
        }

        private DataTable getDataTable() 
        {
            DataTable dt = new DataTable();

            
            
            var cName = dt.Columns.Add("Name");
            {
                cName.DataType = Type.GetType("System.String");
                cName.ColumnName = "Info";
                cName.ReadOnly = true;
                cName.Unique = true;
                cName.AutoIncrement = false;
            }


            var cValue = dt.Columns.Add("Value");
            {
                cValue.DataType = Type.GetType("System.Object");
                cValue.ColumnName = "Value";
                cValue.ReadOnly = false;
                cValue.Unique = true;
                cValue.AutoIncrement = false;
            }
            
            

            float f = 0.5f;

            

            var row = dt.NewRow();
            {
                row.SetField(cName, ""); row.SetField(cValue, f);
            }
            dt.Rows.Add(row);
            
            return dt;
        }
        

        private void MakeDataTables()
        {
            // Run all of the functions.
            MakeParentTable();
            MakeChildTable();
            MakeDataRelation();
            //BindToDataGrid();
        }

        private void MakeParentTable()
        {
            // Create a new DataTable.
            DataTable table = new DataTable("ParentTable");
            // Declare variables for DataColumn and DataRow objects.
            DataColumn column;
            DataRow row;

            // Create new DataColumn, set DataType,
            // ColumnName and add to DataTable.
            column = new DataColumn();
            column.DataType = Type.GetType("System.Int32");
            column.ColumnName = "id";
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ParentItem";
            column.AutoIncrement = false;
            column.Caption = "ParentItem";
            column.ReadOnly = false;
            column.Unique = false;
            // Add the column to the table.
            table.Columns.Add(column);

            // Make the ID column the primary key column.
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["id"];
            table.PrimaryKey = PrimaryKeyColumns;

            // Instantiate the DataSet variable.
            dataSet = new DataSet();
            // Add the new DataTable to the DataSet.
            dataSet.Tables.Add(table);

            // Create three new DataRow objects and add
            // them to the DataTable
            for (int i = 0; i <= 2; i++)
            {
                row = table.NewRow();
                row["id"] = i;
                row["ParentItem"] = "ParentItem " + i;
                table.Rows.Add(row);
            }
        }

        private void MakeChildTable()
        {
            // Create a new DataTable.
            DataTable table = new DataTable("childTable");
            DataColumn column;
            DataRow row;

            // Create first column and add to the DataTable.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ChildID";
            column.AutoIncrement = true;
            column.Caption = "ID";
            column.ReadOnly = true;
            column.Unique = true;

            // Add the column to the DataColumnCollection.
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ChildItem";
            column.AutoIncrement = false;
            column.Caption = "ChildItem";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            // Create third column.
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ParentID";
            column.AutoIncrement = false;
            column.Caption = "ParentID";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            dataSet.Tables.Add(table);

            // Create three sets of DataRow objects,
            // five rows each, and add to DataTable.
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i;
                row["ChildItem"] = "Item " + i;
                row["ParentID"] = 0;
                table.Rows.Add(row);
            }
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i + 5;
                row["ChildItem"] = "Item " + i;
                row["ParentID"] = 1;
                table.Rows.Add(row);
            }
            for (int i = 0; i <= 4; i++)
            {
                row = table.NewRow();
                row["childID"] = i + 10;
                row["ChildItem"] = "Item " + i;
                row["ParentID"] = 2;
                table.Rows.Add(row);
            }
        }

        private void MakeDataRelation()
        {
            // DataRelation requires two DataColumn
            // (parent and child) and a name.
            DataColumn parentColumn =
                dataSet.Tables["ParentTable"].Columns["id"];
            DataColumn childColumn =
                dataSet.Tables["ChildTable"].Columns["ParentID"];
            DataRelation relation = new
                DataRelation("parent2Child", parentColumn, childColumn);
            dataSet.Tables["ChildTable"].ParentRelations.Add(relation);
        }

        private void BindToDataGrid(DataGridView dataGridView)
        {
            // Instruct the DataGrid to bind to the DataSet, with the
            // ParentTable as the topmost DataTable.

            //dataGridView.DataBindings.Add(,);
            //dataGrid1.SetDataBinding(dataSet, "ParentTable");
        }
    }
}
