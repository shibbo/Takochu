using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.smg;

namespace Takochu.ui.StageInfoEditorSys
{
    /// <summary>
    /// Set the information for the scenario.<br/>
    /// シナリオの情報を設定します。
    /// </summary>
    public class ScenarioInformation
    {
        private readonly Galaxy _galaxy;
        public Dictionary<int, ScenarioEntry> ScenarioBCSV { get; private set; }
        //public Dictionary<int, ScenarioEntry> EditScenarios { get; private set; }

        

        public ScenarioInformation(Galaxy galaxy, TreeView ScenarioListTreeView) 
        {
            _galaxy = galaxy;
            ScenarioBCSV = new Dictionary<int, ScenarioEntry>();
            //EditScenarios = new Dictionary<int, ScenarioEntry>();
            SetTreeView(ScenarioListTreeView);
            ScenarioListTreeView.SelectedNode = ScenarioListTreeView.Nodes[0];
        }

        /// <summary>
        /// Sets the scenario to the TreeView specified in the argument.<br/>
        /// 引数で指定したTreeViewにシナリオをセットします。
        /// </summary>
        /// <param name="ScenarioListTreeView"><see cref="ScenarioEntry"/>をセットしたい<see cref="TreeView"/></param>
        private void SetTreeView(TreeView ScenarioListTreeView) 
        {
            foreach (KeyValuePair<int, ScenarioEntry> scenEntry in _galaxy.ScenarioARC.ScenarioDataBCSV)
            {
                ScenarioEntry scenarioEntry = scenEntry.Value;

                ScenarioListTreeView.Nodes.Add(SetScenarioForTreeNodeTag(scenarioEntry));

                ScenarioBCSV.Add(scenarioEntry.ScenarioNo, _galaxy.GetScenario(scenarioEntry.ScenarioNo));
            }

            //ツリーノードの数をチェックして0個の場合エラーを出す。
            if (ScenarioListTreeView.Nodes.Count == 0)
            {
                throw new Exception("scenarioListTreeView Node Count is 0");
            }
        }

        /// <summary>
        /// ツリーノードのタグにシナリオをセットします。
        /// </summary>
        /// <param name="scenario"></param>
        /// <returns></returns>
        private TreeNode SetScenarioForTreeNodeTag(ScenarioEntry scenario) 
        {
            TreeNode ScenarioTreeNode = new TreeNode($"[{scenario.ScenarioNo}] {scenario.ScenarioName}")
            {
                Tag = scenario.ScenarioNo
            };

            return ScenarioTreeNode;
        }

        
    }
}
