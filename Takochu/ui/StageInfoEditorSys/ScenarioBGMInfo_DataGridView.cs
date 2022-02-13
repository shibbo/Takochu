using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Takochu.smg.StageBgmInfoArcFile;

namespace Takochu.ui.StageInfoEditorSys
{
    public class ScenarioBGMInfo_DataGridView
    {
        protected readonly DataGridView _dataGridView;
        protected readonly int _maxHeight, _minHeight, _defHeight;
        private readonly string[] columnHeaderName = new string[] 
        {
            "ScenarioName",
            "ScenarioNo",
            "MusicID",
            "StartType",
            "StartFrame",
            "IsPrepare"
        };
        
        

        /// <summary>
        /// オブジェクトの値が変更されているかを取得します。
        /// </summary>
        public static bool IsChanged { get; protected set; }

        public ScenarioBGMInfo_DataGridView(DataGridView ScenarioBGMInfoDgv) 
        {
            _dataGridView = ScenarioBGMInfoDgv;

            //データグリッドビューの最大値最小値を初回読み込み時のみ設定します。
            if (_dataGridView.MaximumSize == new Size(0, 0))
            {
                _dataGridView.MaximumSize = _dataGridView.Size;
                _dataGridView.MinimumSize = new Size(_dataGridView.Width, _dataGridView.RowTemplate.Height);
            }


            if (_defHeight == default || _defHeight == 0)
            {
                _defHeight = _dataGridView.Size.Height;
                _minHeight = _dataGridView.MinimumSize.Height;
                _maxHeight = _dataGridView.MaximumSize.Height;
            }

            _dataGridView.RowHeadersVisible = false;
            _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public DataGridView GetDataTable()
        {
            Initialize();
            NullCheck();
            SetColumn();
            SetRow();
            GridSizeSetting();
            Console.WriteLine($"{_defHeight} : {_maxHeight} : {_minHeight}");
            return _dataGridView;
        }

        private void GridSizeSetting() 
        {
            var change = (_dataGridView.Rows.Count + 1) * _dataGridView.RowTemplate.Height;

            if (change < _maxHeight && change > _minHeight)
            {
                _dataGridView.Height = change;
                _dataGridView.ScrollBars = ScrollBars.None;
            }
            else if (change > _maxHeight)
            {
                _dataGridView.Height = _maxHeight;
                _dataGridView.ScrollBars = ScrollBars.Both;
            }
            else if (change < _minHeight)
            {
                _dataGridView.Height = _minHeight;
            }
        }

        private void NullCheck()
        {
            if (ScenarioBgmInfoBCSV.Count == 0 || ScenarioBgmInfoBCSV == default)
                throw new Exception("ScenarioBGMInfo is null");
        }

        private void Initialize()
        {
            if (_dataGridView.DataSource != null)
                _dataGridView.DataSource = null;

            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToResizeRows = false;
            _dataGridView.AllowUserToResizeColumns = false;
        }

        private void SetColumn(params string[] columnHeaderText)
        {
            _dataGridView.Columns.Clear();

            foreach (var str in columnHeaderName) 
            {
                _dataGridView.Columns.Add(str, str);
                /*
                * 表示名を編集不可にする。
                * ※変更すると保存の際に影響があるため
                * 値のソートを不可にする。
                */

                _dataGridView.Columns[str].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (str == columnHeaderName[0]) continue;
                _dataGridView.Columns[str].ReadOnly = true;
                

            }



        }

        private void SetRow()
        {
            for (int i = 0; i < ScenarioBgmInfoBCSV.Count; i++) 
            {
                //データグリッドビューにコントロールを追加
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell tbcCell = new DataGridViewTextBoxCell
                {
                    Value = ScenarioBgmInfoBCSV[i].StageName
                };

                DataGridViewTextBoxCell tbcCell2 = new DataGridViewTextBoxCell
                {
                    Value = ScenarioBgmInfoBCSV[i].ScenarioNo
                };

                DataGridViewTextBoxCell tbcCell3 = new DataGridViewTextBoxCell
                {
                    Value = ScenarioBgmInfoBCSV[i].BGMName
                };

                DataGridViewTextBoxCell tbcCell4 = new DataGridViewTextBoxCell
                {
                    Value = ScenarioBgmInfoBCSV[i].StartType
                };

                DataGridViewTextBoxCell tbcCell5 = new DataGridViewTextBoxCell
                {
                    Value = ScenarioBgmInfoBCSV[i].StartFrame
                };

                DataGridViewCheckBoxCell tbcCell6 = new DataGridViewCheckBoxCell
                {
                    Value = Convert.ToBoolean(ScenarioBgmInfoBCSV[i].IsPrepare)
                };


                row.Cells.Add(tbcCell);
                row.Cells.Add(tbcCell2);
                row.Cells.Add(tbcCell3);
                row.Cells.Add(tbcCell4);
                row.Cells.Add(tbcCell5);
                row.Cells.Add(tbcCell6);
                

                _dataGridView.Rows.Add(row);
            }
            
        }
    }
}
