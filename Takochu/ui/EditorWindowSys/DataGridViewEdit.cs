using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.smg.obj;
using Takochu.smg;
using ObjDB = Takochu.smg.ObjectDB;


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

    //public class DataGridViewEdit
    //{
    //    private static bool _isChanged = false;
    //    public static bool IsChanged { get => _isChanged;  }

    //    private DataColumn cLeft, cRight;
    //    private AbstractObj _abstObj;
    //    private DataGridView _dataGridView;
    //    private DataTable _dt;

    //    /// <summary>
    //    /// Set "DataGridView" to display the properties of the object displayed in the editor window.
    //    /// </summary>
    //    /// <param name="dataGridView">Specify the target "DataGridView".</param>
    //    /// <param name="abstObj">Specify the "AbstractObj" class or the XXXXObj class that inherits from the "AbstractObj" class.</param>
    //    public DataGridViewEdit(DataGridView dataGridView, AbstractObj abstObj)
    //    {
    //        _dataGridView = dataGridView;
    //        _abstObj = abstObj;

    //    }

    //    public static void IsChangedClear() 
    //    {
    //        _isChanged = false;
    //    }

    //    /// <summary>
    //    /// Create and retrieve a data table.
    //    /// </summary>
    //    /// <returns><see cref="DataTable"/></returns>
    //    public DataTable GetDataTable()
    //    {
    //        Initialize();
    //        NullCheck();

    //        DataTable dt = new DataTable();

    //        SetColumn(ref dt);
    //        SetRow(ref dt);

    //        _dt = dt;

    //        return dt;
    //    }

    //    private void Initialize()
    //    {
    //        if (_dataGridView.DataSource != null)
    //            _dataGridView.DataSource = null;

    //        _dataGridView.AllowUserToAddRows = false;
    //        _dataGridView.AllowUserToResizeRows = false;
    //        _dataGridView.AllowUserToResizeColumns = false;
    //    }

    //    private void NullCheck()
    //    {
    //        if (_abstObj == null)
    //            throw new Exception("GalaxyObject is null");
    //    }

    //    private void SetColumn(ref DataTable dt)
    //    {
    //        cLeft = dt.Columns.Add("Name");
    //        {
    //            cLeft.DataType = Type.GetType("System.String");
    //            cLeft.ColumnName = "Info";
    //            cLeft.ReadOnly = true;
    //            cLeft.Unique = true;
    //            cLeft.AutoIncrement = false;
    //        }


    //        cRight = dt.Columns.Add("Value");
    //        {
    //            cRight.DataType = typeof(DataGridViewButtonCell);
    //            cRight.ColumnName = "Value";
    //            cRight.ReadOnly = false;
    //            cRight.Unique = false;
    //            cRight.AutoIncrement = false;
    //        }
    //    }

    //    private void SetRow(ref DataTable dt)
    //    {
    //        //Console.WriteLine(ObjDB.GetActorFromObjectName(_abstObj.mName).ActorName);

    //        foreach (var ObjEntry in _abstObj.mEntry)
    //        {
    //            var DisplayName = BCSV.HashToFieldName(ObjEntry.Key);
    //            if (DisplayName.Length > "Obj_arg".Length) 
    //            {
    //                if (DisplayName.Length == "Obj_arg".Length + 1) 
    //                {
    //                    var argIndexTest = DisplayName.IndexOf("Obj_arg");
    //                    if (argIndexTest != -1) 
    //                    {
    //                        var test = DisplayName.Skip("Obj_arg".Length).ToArray();
    //                        Console.WriteLine(test[0]);
    //                        var argIndex = Int32.Parse(test[0].ToString());
    //                        if (ObjDB.UsesObjArg(_abstObj.mName, argIndex))
    //                        {
    //                            var actorfield = ObjDB.GetFieldFromActor(ObjDB.GetActorFromObjectName(_abstObj.mName), argIndex);
    //                            DisplayName = actorfield.Name;
    //                        }
    //                    }


    //                }

    //            }

    //            //DataGridViewCheckBoxCell cellch = new DataGridViewCheckBoxCell();
    //            DataGridViewButtonCell cellbutton = new DataGridViewButtonCell();


    //            //DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();

    //            //_dataGridView.Rows.Add("",cellbutton);

    //            var row = dt.NewRow();
    //            {
    //                row.SetField(cLeft, DisplayName); row.(cRight, cellbutton/*ObjEntry.Value*/);
    //            }
    //            dt.Rows.Add(row);

    //        }
    //    }

    //    public void ChangeValue(int rowIndex, object value)
    //    {
    //        var a = _abstObj.mEntry.Keys;
    //        var name = BCSV.HashToFieldName(a.ElementAt(rowIndex));
    //        //foreach (var t in a) 
    //        //{
    //        //    var str = BCSV.HashToFieldName(t);
    //        //    Console.WriteLine(str);
    //        //    Console.WriteLine(_abstObj.mEntry.Get(str));
    //        //} 

    //        //Do not allow the object name to be changed.
    //        if (name == "name") 
    //        {

    //            return;
    //        }
    //        //_abstObj.mEntry.Set(name, value);
    //        //_abstObj.Reload_mValue();
    //        Change_mValues(name, value);
    //    }

    //    /*
    //    [HACK]
    //    Not smart processing, 
    //    the object does not move without changing the mValue,
    //    so I have no choice but to use the switch case.
    //    If you have a solution, please fix it.
    //    By penguin117117
    //     */
    //    private void Change_mValues(string name, object value)
    //    {
    //        Console.WriteLine($"Change_mValues: {name}");
    //        //float ftmp = GetFloat_And_Limiter(value);
    //        //Console.WriteLine("mDirectory: "+ _abstObj.mDirectory);
    //        //Console.WriteLine("mFile: " + _abstObj.mFile);
    //        //Console.WriteLine("mFile: " + _abstObj.mName);

    //        //foreach (var t in _abstObj.mEntry) Console.WriteLine(t.Key);

    //        _abstObj.mEntry.Set(name, value);
    //        _abstObj.Reload_mValues();
    //        _isChanged = true;
    //        //_abstObj.mTruePosition = new OpenTK.Vector3(GetFloat_And_Limiter(_abstObj.mEntry.Get("pos_x")), GetFloat_And_Limiter(_abstObj.mEntry.Get("pos_y")), GetFloat_And_Limiter(_abstObj.mEntry.Get("pos_z")));
    //        //switch (name)
    //        //{
    //        //    case "pos_x":
    //        //        _abstObj.mTruePosition.X = ftmp;
    //        //        break;
    //        //    case "pos_y":
    //        //        _abstObj.mTruePosition.Y = ftmp;
    //        //        break;
    //        //    case "pos_z":
    //        //        _abstObj.mTruePosition.Z = ftmp;
    //        //        break;
    //        //    case "dir_x":
    //        //        _abstObj.mTrueRotation.X = ftmp;
    //        //        break;
    //        //    case "dir_y":
    //        //        _abstObj.mTrueRotation.Y = ftmp;
    //        //        break;
    //        //    case "dir_z":
    //        //        _abstObj.mTrueRotation.Z = ftmp;
    //        //        break;
    //        //    case "scale_x":
    //        //        _abstObj.mScale.X = ftmp;
    //        //        break;
    //        //    case "scale_y":
    //        //        _abstObj.mScale.Y = ftmp;
    //        //        break;
    //        //    case "scale_z":
    //        //        _abstObj.mScale.Z = ftmp;
    //        //        break;
    //        //    default:
    //        //        //処理設定されている物以外は値の変更を行わない。
    //        //        //You cannot change any value other than the one that is set.
    //        //        return;
    //        //}

    //        //_abstObj.Save();
    //    }

    //    private bool IsStringParam(string name) 
    //    {
    //        switch (name) 
    //        {
    //            case "name":
    //                return true;
    //            default:
    //                return false;
    //        }
    //    }

    //    private float GetFloat_And_Limiter(object value)
    //    {

    //        if (!float.TryParse(value.ToString(), out float ftmp)) return 0f;
    //        ftmp = Convert.ToSingle(value);
    //        if (ftmp > float.MaxValue) ftmp = float.MaxValue;
    //        if (ftmp < float.MinValue) ftmp = float.MinValue;

    //        return ftmp;
    //    }
    //}


    public class DataGridViewEdit
    {
        private DataGridView _dataGridView;
        private AbstractObj _abstObj;

        private static bool _isChanged = false;
        public static bool IsChanged { get => _isChanged; }



        public DataGridViewEdit(DataGridView dgv, AbstractObj abstractObj)
        {
            _dataGridView = dgv;
            _abstObj = abstractObj;

        }

        public DataGridView GetDataTable()
        {
            Initialize();
            NullCheck();
            SetColumn();
            SetRow();
            return _dataGridView;
        }

        public static void IsChangedClear()
        {
            _isChanged = false;
        }

        public void ChangeValue(int rowIndex, object value)
        {
            var a = _abstObj.mEntry.Keys;
            var name = BCSV.HashToFieldName(a.ElementAt(rowIndex));
            //foreach (var t in a) 
            //{
            //    var str = BCSV.HashToFieldName(t);
            //    Console.WriteLine(str);
            //    Console.WriteLine(_abstObj.mEntry.Get(str));
            //} 

            //Do not allow the object name to be changed.
            if (name == "name")
            {

                return;
            }
            //_abstObj.mEntry.Set(name, value);
            //_abstObj.Reload_mValue();
            Change_mValues(name, value);
        }

        private void Change_mValues(string name, object value)
        {
            Console.WriteLine($"Change_mValues: {name}");


            //BCSV.HashToFieldName(name);
            //Console.WriteLine(CellType);
            //if (CellType == "checkbox") 
            //{
            //    Console.WriteLine(value);
            //    var a = int.Parse(value.ToString());
            //    if (a == 0) value = -1;
            //    else value = a;
            //    Console.WriteLine(value);
            //}
            
            if (name.Length == "Obj_arg".Length + 1)
            {
                if (name.IndexOf("Obj_arg") != -1)
                {
                    var a = Convert.ToInt32(value);
                    if (value.ToString() == "True" || value.ToString() == "False") 
                    {
                        if (a < 1) value = 0;
                        else value = a;
                        Console.WriteLine(value);
                    }
                    
                    
                    
                }
            }



            //if (value is DataGridViewCell) 
            //{
            //    var swapValue = value as DataGridViewCell;
            //    Console.WriteLine(swapValue.EditType);
            //}

            _abstObj.mEntry.Set(name, value);
            //_dataGridView.EndEdit();
            _abstObj.Reload_mValues();
            _isChanged = true;

        }

        private void NullCheck()
        {
            if (_abstObj == null)
                throw new Exception("GalaxyObject is null");
        }

        private void Initialize()
        {
            if (_dataGridView.DataSource != null)
                _dataGridView.DataSource = null;

            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToResizeRows = false;
            _dataGridView.AllowUserToResizeColumns = false;
        }

        private void SetColumn()
        {
            _dataGridView.Columns.Clear();
            _dataGridView.Columns.Add("PropertyName", "PropertyName");
            _dataGridView.Columns.Add("PropertyValue", "PropertyValue");

            //表示名を編集不可にする。
            _dataGridView.Columns["PropertyName"].ReadOnly = true;
        }

        private void SetRow()
        {
            //DataGridViewCell[] controlCollections = new DataGridViewCell[8];

            foreach (var ObjEntry in _abstObj.mEntry.Select((Value, Index) => (Value, Index)))
            {
                DataGridViewCell controlCollection;
                var argIndexTest = -1;
                var typename = "";
                //object objval;

                var DisplayName = BCSV.HashToFieldName(ObjEntry.Value.Key);
                if (DisplayName.Length > "Obj_arg".Length)
                {
                    if (DisplayName.Length == "Obj_arg".Length + 1)
                    {
                        argIndexTest = DisplayName.IndexOf("Obj_arg");
                        if (argIndexTest != -1)
                        {

                            var test = DisplayName.Skip("Obj_arg".Length).ToArray();
                            Console.WriteLine(test[0]);
                            var argIndex = Int32.Parse(test[0].ToString());
                            if (ObjDB.UsesObjArg(_abstObj.mName, argIndex))
                            {
                                var actorfield = ObjDB.GetFieldFromActor(ObjDB.GetActorFromObjectName(_abstObj.mName), argIndex);
                                DisplayName = actorfield.Name;
                                Console.WriteLine(actorfield.Type);
                                typename = actorfield.Type;
                                //controlCollection = Obj_arg_DataType[actorfield.Type];
                                //controlCollection.Value = ObjEntry.Value.Value;
                            }
                        }


                    }

                }




                //データグリッドビューに追加するコントロールの設定
                DataGridViewTextBoxCell tbcCell = new DataGridViewTextBoxCell
                {
                    Value = DisplayName
                };


                DataGridViewTextBoxCell tbcCell2 = new DataGridViewTextBoxCell
                {
                    Value = ObjEntry.Value.Value
                };

                //int.TryParse("1",out int tresult);

                //データグリッドビューにコントロールを追加
                DataGridViewRow row = new DataGridViewRow();

                switch (typename)
                {
                    case "checkbox":
                        CellType = typename;
                        row.Cells.Add(tbcCell);
                        DataGridViewCheckBoxCell cbCell = new DataGridViewCheckBoxCell();
                        bool isConversion = Convert.ToBoolean(ObjEntry.Value.Value);
                        //bool isConversion = bool.TryParse(ObjEntry.Value.Value.ToString(), out bool isResult);
                        Console.WriteLine($"DataGridView CheckBox Accurate Numbers: {ObjEntry.Value.Value}:{isConversion}");
                        //if (!isConversion)
                        //{
                        //    cbCell.Value = false;

                        //    row.Cells.Add(cbCell);
                        //    break;
                        //}
                        cbCell.Value = isConversion;
                        //cbCell.Value = true;
                        row.Cells.Add(cbCell);
                        break;
                    default:
                        CellType = string.Empty;
                        row.Cells.Add(tbcCell);
                        row.Cells.Add(tbcCell2);
                        break;
                }

                _dataGridView.Rows.Add(row);
                //if (controlCollection != null)
                //{
                //    row.Cells.Add(tbcCell);

                //    row.Cells.Add(controlCollection);
                //}

                //else 
                //{

                //}










            }
        }

        private static Dictionary<string, DataGridViewCell> Obj_arg_DataType = new Dictionary<string, DataGridViewCell>()
        {
            { string.Empty , new DataGridViewTextBoxCell() },
            { "value" , new DataGridViewTextBoxCell() },
            { "checkbox" , new DataGridViewTextBoxCell() }
        };

        private string CellType { get; set; }
    }
}
