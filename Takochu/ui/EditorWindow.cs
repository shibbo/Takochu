using OpenTK.Graphics.ES10;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.io;
using Takochu.smg;
using Takochu.smg.obj;
using Takochu.util;
using OpenTK;

namespace Takochu.ui
{
    public partial class EditorWindow : Form
    {
        public EditorWindow(string galaxyName)
        {
            InitializeComponent();
            mGalaxyName = galaxyName;

            if (GameUtil.IsSMG1())
                saveGalaxyBtn.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            mGalaxy = Program.sGame.OpenGalaxy(mGalaxyName);
            galaxyNameTxtBox.Text = mGalaxy.mGalaxyName;

            foreach(KeyValuePair<int, Scenario> scenarios in mGalaxy.mScenarios)
            {
                Scenario s = scenarios.Value;
                TreeNode n = new TreeNode($"[{s.mScenarioNo}] {s.mScenarioName}")
                {
                    Tag = s.mScenarioNo
                };

                scenarioTreeView.Nodes.Add(n);
            }

            if (!BGMInfo.HasBGMInfo(mGalaxy.mName))
                stageInformationBtn.Enabled = false;
        }

        private string mGalaxyName;
        public int mCurrentScenario;

        private Galaxy mGalaxy;

        public void LoadScenario(int scenarioNo)
        {
            layerViewerDropDown.DropDownItems.Clear();

            // we want to clear out the children of the 5 camera type root nodes
            //for (int i = 0; i < 5; i++)
            //    camerasTree.Nodes[i].Nodes.Clear();

            mGalaxy.SetScenario(scenarioNo);
            scenarioNameTxtBox.Text = mGalaxy.mCurScenarioName;

            // first we need to get the proper layers that the galaxy itself uses
            List<string> layers = GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mGalaxyName));

            layers.ForEach(l => layerViewerDropDown.DropDownItems.Add(l));

            // get our main galaxy's zone
            Zone mainZone = mGalaxy.GetZone(mGalaxyName);

            // now we get the zones used on these layers
            List<string> zonesUsed = new List<string>
            {
                // add our galaxy name itself so we can properly add it to a scene list with the other zones
                mGalaxyName
            };
            
            zonesUsed.AddRange(mainZone.GetZonesUsedOnLayers(layers));

            Dictionary<string, int> zoneMasks = new Dictionary<string, int>();

            ObjectHolder mainHolder = new ObjectHolder();

            Dictionary<string, List<Camera>> cameras = new Dictionary<string, List<Camera>>();
            List<Light> lights = new List<Light>();
            List<PathPointObj> pathpoints = new List<PathPointObj>();

            List<ZoneAttributes.ShadowParam> shadowParams = new List<ZoneAttributes.ShadowParam>();
            List<ZoneAttributes.FlagNameTable> flags = new List<ZoneAttributes.FlagNameTable>();
            List<ZoneAttributes.WaterCameraParam> waterParams = new List<ZoneAttributes.WaterCameraParam>();

            foreach (string zone in zonesUsed)
            {
                zoneMasks.Add(zone, mGalaxy.GetMaskUsedInZoneOnCurrentScenario(zone));

                Zone z = mGalaxy.GetZone(zone);
                if (z.mAttributes != null)
                {
                    shadowParams.AddRange(z.mAttributes.mShadowParams);
                    flags.AddRange(z.mAttributes.mFlagTable);
                    waterParams.AddRange(z.mAttributes.mWaterParams);
                }

                List<string> curlayers = GameUtil.GetGalaxyLayers(zoneMasks[zone]);

                if (GameUtil.IsSMG1())
                    curlayers = curlayers.ConvertAll(l => l.ToLower());
                ObjectHolder curHolder = z.GetAllObjectsFromLayers(curlayers);

                foreach (PathObj pobj in z.mPaths)
                {
                    pathpoints.AddRange(pobj.mPathPointObjs);
                }

                cameras.Add(zone, z.mCameras);

                if (z.mLights != null)
                    lights.AddRange(z.mLights);

                if (!z.mIsMainGalaxy)
                {
                    Zone galaxyZone = mGalaxy.GetGalaxyZone();

                    // the first step
                    List<string> galaxyLayers = GameUtil.GetGalaxyLayers(zoneMasks[mGalaxy.mName]);

                    if (GameUtil.IsSMG1())
                        galaxyLayers = galaxyLayers.ConvertAll(l => l.ToLower());

                    foreach(string layer in galaxyLayers)
                    {
                        List<StageObj> stages = galaxyZone.mZones[layer];

                        foreach(StageObj o in stages)
                        {
                            if (o.mName == z.mZoneName)
                            {
                                curHolder.ApplyZoneOffset(o.mPosition, o.mRotation);
                            }
                        }
                    }
                }

                mainHolder.AddObjects(curHolder);
            }

            List<Camera> cubeCameras = new List<Camera>();
            List<Camera> groupCameras = new List<Camera>();
            List<Camera> eventCameras = new List<Camera>();
            List<Camera> startCameras = new List<Camera>();
            List<Camera> otherCameras = new List<Camera>();

            foreach (string zone in zonesUsed)
            {
                cameras[zone].ForEach(c =>
                {
                    if (c.GetCameraType() == Camera.CameraType.Cube)
                        cubeCameras.Add(c);
                });

                cameras[zone].ForEach(c =>
                {
                    if (c.GetCameraType() == Camera.CameraType.Group)
                        groupCameras.Add(c);
                });

                cameras[zone].ForEach(c =>
                {
                    if (c.GetCameraType() == Camera.CameraType.Event)
                        eventCameras.Add(c);
                });

                cameras[zone].ForEach(c =>
                {
                    if (c.GetCameraType() == Camera.CameraType.Start)
                        startCameras.Add(c);
                });

                cameras[zone].ForEach(c =>
                {
                    if (c.GetCameraType() == Camera.CameraType.Other)
                        otherCameras.Add(c);
                });
            }
        }


        private void scenarioTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        private void scenarioTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (scenarioTreeView.SelectedNode != null)
            {
                mCurrentScenario = Convert.ToInt32(scenarioTreeView.SelectedNode.Tag);
                LoadScenario(mCurrentScenario);
            }
        }

        private void EditorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            mGalaxy.Close();
        }

        private void EditorWindow_Load(object sender, EventArgs e)
        {

        }

        private void galaxyViewControl_Load(object sender, EventArgs e)
        {

        }
        private void openMsgEditorButton_Click(object sender, EventArgs e)
        {
            MessageEditor editor = new MessageEditor(ref mGalaxy);
            editor.Show();
        }

        private void saveGalaxyBtn_Click(object sender, EventArgs e)
        {
            mGalaxy.Save();
        }

        private void closeEditorBtn_Click(object sender, EventArgs e)
        {
            mGalaxy.Close();
            Close();
        }

        private void stageInformationBtn_Click(object sender, EventArgs e)
        {
            StageInfoEditor stageInfo = new StageInfoEditor(ref mGalaxy, mCurrentScenario);
            stageInfo.Show();
        }
    }
}
