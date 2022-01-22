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
using Takochu.util;

namespace Takochu.ui.EditorWindowSys
{
    /*
    ********************************************************************************************************************
    In progress. 2021/12～
    Create a data grid view to display the properties of the object in the data grid view.

    [ToDo]
    1. Add specific controls to the DataGrid view by referring to the object database.
       オブジェクトデータベースを参照して特定のコントロールを、データグリッドビューに追加する。

    
    By penguin117117
    ********************************************************************************************************************
    */

    /// <summary>
    /// エディターウィンドウのオブジェクト専用のデータグリッドビューを生成します。
    /// </summary>
    public class DataGridViewEdit
    {
        private readonly DataGridView _dataGridView;
        private readonly AbstractObj _abstObj;
        private static bool _isChanged = false;

        /// <summary>
        /// オブジェクトの値が変更されているかを取得します。
        /// </summary>
        public static bool IsChanged { get => _isChanged; }


        /// <summary>
        /// エディターウィンドウのオブジェクト専用のデータグリッドビューを生成します。
        /// </summary>
        /// <param name="editorWindowDatagridView">エディターウィンドウのオブジェクト専用のデータグリッドビュー</param>
        /// <param name="abstractObj"><see cref="AbstractObj"/>を継承しているObject<br/>例：<see cref="LevelObj"/>など</param>
        public DataGridViewEdit(DataGridView editorWindowDatagridView, AbstractObj abstractObj)
        {
            _dataGridView = editorWindowDatagridView;
            _abstObj = abstractObj;
        }

        /// <summary>
        /// データグリッドビューの設定をしてデータグリッドビューを取得します。
        /// </summary>
        /// <returns></returns>
        public DataGridView GetDataTable()
        {
            Initialize();
            NullCheck();
            SetColumn();
            SetRow();
            return _dataGridView;
        }

        public DataGridView GetDataTable(Camera camera)
        {
            Initialize();
            NullCheck();
            SetColumn();
            SetRow();

            // here comes the tricky part
            List<string> unusedTypes = CameraUtil.GetUnusedEntriesByCameraType(camera.mType);

            foreach(string type in unusedTypes)
            {
                FindAndRemove(type);
            }

            return _dataGridView;
        }

        public void FindAndRemove(string type)
        {
            for (int i = 0; i < _dataGridView.Rows.Count; i++)
            {
                DataGridViewRow row = _dataGridView.Rows[i];

                if (row.Cells[0].Value.ToString() == type)
                {
                    Console.WriteLine($"found unused {row.Cells[0].Value}");
                    _dataGridView.Rows.RemoveAt(i);
                    return;
                }
            }
        }

        /// <summary>
        /// データ変更のフラグをキャンセルします。<br/>
        /// データは元にはもどりません。
        /// </summary>
        public static void IsChangedClear()
        {
            _isChanged = false;
        }

        /// <summary>
        /// データグリッドビューの値が変更された際に呼び出します。
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="value"></param>
        public void ChangeValue(int rowIndex, object value)
        {
            var Keys = _abstObj.mEntry.Keys;
            var Name = BCSV.HashToFieldName(Keys.ElementAt(rowIndex));
            
            if (value == null)  return;

            //Do not allow the object name to be changed.
            if (Name == "name") return;

            Change_mValues(Name, value);
        }

        private void Change_mValues(string name, object value)
        {
            Console.WriteLine($"Change_mValues: {name}");

            if (IsObj_args(name)) 
            {
               var ChangedInt32 = Convert.ToInt32(value);
                if (value.ToString() == "True" || value.ToString() == "False")
                {
                    if (ChangedInt32 < 1) value = 0;
                    else value = ChangedInt32;
                    Console.WriteLine(value);
                }
            }
            _abstObj.mEntry.Set(name, value);
            _abstObj.Reload_mValues();
            _isChanged = true;

        }

        /// <summary>
        /// Obj_argX (Xは0～7)であるかどうかをチェックします。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsObj_args(string name) 
        {
            if (name.Length != "Obj_arg".Length + 1) return false;

            if (name.IndexOf("Obj_arg") == -1) return false;
            
            return true;
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


            /*
             * 表示名を編集不可にする。
             * ※変更すると保存の際に影響があるため
             * 値のソートを不可にする。
             */
            _dataGridView.Columns["PropertyName"].ReadOnly = true;
            _dataGridView.Columns["PropertyValue"].SortMode  = DataGridViewColumnSortMode.NotSortable;
        }

        private void SetRow()
        {
            foreach (var ObjEntry in _abstObj.mEntry.Select((Value, Index) => (Value, Index)))
            {
                var typename = "";
                var DisplayName = BCSV.HashToFieldName(ObjEntry.Value.Key);

                if (IsObj_args(DisplayName)) 
                {
                    var Obj_argNo = DisplayName.Skip("Obj_arg".Length).ToArray();
                    var argIndex = Int32.Parse(Obj_argNo[0].ToString());

                    if (ObjDB.UsesObjArg(_abstObj.mName, argIndex))
                    {
                        var actorfield = ObjDB.GetFieldFromActor(ObjDB.GetActorFromObjectName(_abstObj.mName), argIndex);
                        DisplayName = actorfield.Name;
                        typename = actorfield.Type;
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

                //データグリッドビューにコントロールを追加
                DataGridViewRow row = new DataGridViewRow();

                switch (typename)
                {
                    case "checkbox":
                        row.Cells.Add(tbcCell);
                        DataGridViewCheckBoxCell cbCell = new DataGridViewCheckBoxCell();
                        bool isConversion = Convert.ToBoolean(ObjEntry.Value.Value);
                        cbCell.Value = isConversion;
                        row.Cells.Add(cbCell);
                        break;
                    default:
                        row.Cells.Add(tbcCell);
                        row.Cells.Add(tbcCell2);
                        break;
                }

                _dataGridView.Rows.Add(row);
            }
        }
    }
}
