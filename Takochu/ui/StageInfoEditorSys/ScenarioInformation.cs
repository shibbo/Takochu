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
        public Dictionary<int, Scenario> Scenarios { get; private set; }
        public Dictionary<int, Scenario> EditScenarios { get; private set; }

        

        public ScenarioInformation(Galaxy galaxy, TreeView ScenarioListTreeView) 
        {
            _galaxy = galaxy;
            Scenarios = new Dictionary<int, Scenario>();
            EditScenarios = new Dictionary<int, Scenario>();
            SetTreeView(ScenarioListTreeView);
        }

        /// <summary>
        /// Sets the scenario to the TreeView specified in the argument.<br/>
        /// 引数で指定したTreeViewにシナリオをセットします。
        /// </summary>
        /// <param name="ScenarioListTreeView"><see cref="Scenario"/>をセットしたい<see cref="TreeView"/></param>
        private void SetTreeView(TreeView ScenarioListTreeView) 
        {
            foreach (KeyValuePair<int, Scenario> scenarios in _galaxy.mScenarios)
            {
                Scenario ReadScenario = scenarios.Value;

                ScenarioListTreeView.Nodes.Add(SetScenarioForTreeNodeTag(ReadScenario));

                Scenarios.Add(ReadScenario.mScenarioNo, _galaxy.GetScenario(ReadScenario.mScenarioNo));
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
        private TreeNode SetScenarioForTreeNodeTag(Scenario scenario) 
        {
            TreeNode ScenarioTreeNode = new TreeNode($"[{scenario.mScenarioNo}] {scenario.mScenarioName}")
            {
                Tag = scenario.mScenarioNo
            };

            return ScenarioTreeNode;
        }

        public enum ShowScenarioNo : int
        {
            Scenario1 = 1,
            Scenario2 = 2,
            Scenario3 = 4,
            Scenario4 = 8,
            Scenario5 = 16,
            Scenario6 = 32,
            Scenario7 = 64,
            Scenario8 = 128
        }
    }
}
