using Microsoft.SqlServer.Server;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.io;
using Takochu.rnd;
using Takochu.smg.msg;
using Takochu.smg.obj;
using Takochu.util;

namespace Takochu.smg
{
    public class Zone
    {
        public string[] cPossibleFiles = { "Design", "Light", "Ghost", "Map", "Sound", "ZoneInfo" };
        public static int MissingPathArgumentsRemove { get; private set; }

        public Zone(Galaxy galaxy, string name)
        {
            mGalaxy = galaxy;
            mGame = galaxy.mGame;
            mFilesystem = mGame.mFilesystem;
            mZoneName = name;
            mIsMainGalaxy = (mGalaxy.mName == name);

            mMapFiles = new Dictionary<string, FilesystemBase>();
            mObjects = new Dictionary<string, Dictionary<string, List<AbstractObj>>>();
            mHasStageObjList = new Dictionary<string, List<StageObj>>();

            Load();
        }

        public void Load()
        {

            MissingPathArgumentsRemove = 0;
            if (GameUtil.IsSMG1())
            {
                string path = $"/StageData/{mZoneName}.arc";
                if (mFilesystem.DoesFileExist(path))
                {
                    mMapFiles.Add("Map", new RARCFilesystem(mFilesystem.OpenFile(path)));
                    Console.WriteLine(path);
                    if (mIsMainGalaxy)
                        LoadObjects("Map", "placement", "stageobjinfo");

                    LoadObjects("Map", "placement", "areaobjinfo");
                    LoadObjects("Map", "placement", "cameracubeinfo");
                    LoadObjects("Map", "placement", "objinfo");
                    LoadObjects("Map", "placement", "planetobjinfo");
                    LoadObjects("Map", "generalpos", "generalposinfo");
                    LoadObjects("Map", "debug", "debugmoveinfo");
                    LoadObjects("Map", "start", "startinfo");
                    LoadObjects("Map", "mapparts", "mappartsinfo");
                    LoadObjects("Map", "placement", "demoobjinfo");
                }
                //LoadMessages();
            }
            else
            {
                // so first we need to collect all of the files used in this zone
                // zones can use Design, Sound, Light, etc
                foreach (string file in cPossibleFiles)
                {
                    string path = $"/StageData/{mZoneName}/{mZoneName}{file}.arc";

                    if (mFilesystem.DoesFileExist(path))
                    {
                        RARCFilesystem fs = new RARCFilesystem(mFilesystem.OpenFile(path));

                        mMapFiles.Add(file, fs);

                        if (file == "Light")
                        {
                            LoadLight();
                        }
                        else if (file == "ZoneInfo")
                        {
                            LoadAttributes();
                        }
                        else if (file == "Ghost")
                        {
                            //LoadGhost();
                        }
                        else
                        {
                            // we load our StageObjInfo first because objects can use their offsets

                            if (mIsMainGalaxy)
                                LoadObjects(file, "Placement", "StageObjInfo");

                            LoadObjects(file, "Placement", "AreaObjInfo");
                            LoadObjects(file, "Placement", "CameraCubeInfo");
                            LoadObjects(file, "Placement", "ObjInfo");
                            LoadObjects(file, "Placement", "PlanetObjInfo");
                            LoadObjects(file, "GeneralPos", "GeneralPosInfo");
                            LoadObjects(file, "Debug", "DebugMoveInfo");
                            LoadObjects(file, "Start", "StartInfo");
                            LoadObjects(file, "MapParts", "MapPartsInfo");
                            LoadObjects(file, "Placement", "DemoObjInfo");
                        }
                    }
                }
                LoadMessages();
            }

            LoadCameras();
            LoadPaths();
        }

        public void LoadObjects(string archive, string directory, string file)
        {
            List<string> layers = mMapFiles[archive].GetDirectories("/root/jmp/" + directory);

            if (layers == null)
            {
                return;
            }

            layers.ForEach(l => AssignsObjectsToList(archive, $"{directory}/{l}/{file}"));
        }

        public void LoadCameras()
        {
            if (!mMapFiles.ContainsKey("Map"))
                return;

            BCSV cameras = new BCSV(mMapFiles["Map"].OpenFile("/root/camera/CameraParam.bcam"));
            cameras.RemoveField("no");
            mCameras = new List<Camera>();
            cameras.mEntries.ForEach(c => mCameras.Add(new Camera(c, this)));

            mIntroCameras = new Dictionary<string, CANM>();

            List<string> intro_cameras = mMapFiles["Map"].GetFilesWithExt("/root/camera", "canm");

            foreach (string intCamera in intro_cameras)
            {
                mIntroCameras.Add(intCamera, new CANM(mMapFiles["Map"].OpenFile($"/root/camera/{intCamera}")));
            }
        }

        public void LoadAttributes()
        {
            mAttributes = new ZoneAttributes(mMapFiles["ZoneInfo"]);
        }

        //public void LoadAttributes() { }

        public void LoadLight()
        {
            if (!mMapFiles["Light"].DoesFileExist($"/root/csv/{mZoneName}Light.bcsv"))
                return;

            BCSV light = new BCSV(mMapFiles["Light"].OpenFile($"/root/csv/{mZoneName}Light.bcsv"));
            mLights = new List<Light>();
            light.mEntries.ForEach(e => mLights.Add(new Light(e, mZoneName)));
        }

        public void LoadMessages()
        {
            if (mFilesystem.DoesFileExist($"/LocalizeData/{Program.sLanguage}/MessageData/{mZoneName}.arc"))
            {
                mMessagesFile = new RARCFilesystem(mFilesystem.OpenFile($"/LocalizeData/{Program.sLanguage}/MessageData/{mZoneName}.arc"));

                if (mMessagesFile.DoesFileExist($"/root/{mZoneName}.msbt"))
                    mMessages = new MSBT(mMessagesFile.OpenFile($"/root/{mZoneName}.msbt"));

                if (mMessagesFile.DoesFileExist($"/root/{mZoneName}.msbf"))
                    mMessageFlows = new MSBF(mMessagesFile.OpenFile($"/root/{mZoneName}.msbf"));
            }
        }

        public void LoadPaths()
        {
            if (!mMapFiles.ContainsKey("Map"))
                return;

            BCSV pathsBCSV = new BCSV(mMapFiles["Map"].OpenFile("/root/jmp/Path/CommonPathInfo"));

            bool didAdd = false;
            // check each path arg to see if we need to fix a known whitehole bug
            for (int i = 0; i < 8; i++)
            {
                if (!pathsBCSV.ContainsField($"path_arg{i}"))
                {
                    pathsBCSV.AddField($"path_arg{i}", 3, -1);
                    didAdd = true;
                }
            }
            
            if (didAdd)
            {
                pathsBCSV.Save();
                mMapFiles["Map"].Save();

                MissingPathArgumentsRemove++;
                

                pathsBCSV = new BCSV(mMapFiles["Map"].OpenFile("/root/jmp/Path/CommonPathInfo"));
            }

            mPaths = new List<PathObj>();

            foreach (BCSV.Entry e in pathsBCSV.mEntries)
            {
                mPaths.Add(new PathObj(e, this, (RARCFilesystem)mMapFiles["Map"]));
            }
        }

        public void LoadGhost()
        {
            if (!mMapFiles.ContainsKey("Ghost"))
            {
                return;
            }

            mGhostFiles = new Dictionary<string, GST>();

            List<string> ghost_files = mMapFiles["Ghost"].GetFilesWithExt("/root/gst", "gst");

            foreach (string gst in ghost_files)
            {
                mGhostFiles.Add(gst, new GST(mMapFiles["Ghost"].OpenFile($"/root/gst/{gst.ToLower()}")));
            }
        }


        public void AssignsObjectsToList(string archive, string path)
        {
            //Console.WriteLine(path);
            string[] data = path.Split('/');
            string layer = data[1];
            string dir = data[2];
            //Console.WriteLine("Layer "+layer);
            //Console.WriteLine(data[0] + data[1] + data[2] + "  " + data.Count());
            if (!mObjects.ContainsKey(archive))
            {
                mObjects.Add(archive, new Dictionary<string, List<AbstractObj>>());
            }

            if (!mObjects[archive].ContainsKey(layer))
            {
                mObjects[archive].Add(layer, new List<AbstractObj>());
            }

            if (!mHasStageObjList.ContainsKey(layer))
            {
                mHasStageObjList.Add(layer, new List<StageObj>());
            }

            BCSV bcsv = new BCSV(mMapFiles[archive].OpenFile($"/stage/jmp/{path}"));

            foreach (BCSV.Entry Entry in bcsv.mEntries)
            {
                dir = dir.ToLower();

                switch (dir)
                {
                    case "areaobjinfo":
                        mObjects[archive][layer].Add(new AreaObj(Entry, this, path));
                        break;
                    case "cameracubeinfo":
                        mObjects[archive][layer].Add(new CameraObj(Entry, this, path));
                        break;
                    case "stageobjinfo":
                        //case "StageObjInfo":
                        mHasStageObjList[layer].Add(new StageObj(Entry, this));
                        break;
                    case "objinfo":
                        //case "ObjInfo":
                        mObjects[archive][layer].Add(new LevelObj(Entry, this, path));
                        break;
                    case "demoobjinfo":
                        mObjects[archive][layer].Add(new DemoObj(Entry, this, path));
                        break;
                    case "generalposinfo":
                        mObjects[archive][layer].Add(new GeneralPos(Entry, this, path));
                        break;
                    case "debugmoveinfo":
                        mObjects[archive][layer].Add(new DebugMoveObj(Entry, this, path));
                        break;
                    case "planetobjinfo":
                        mObjects[archive][layer].Add(new PlanetObj(Entry, this, path));
                        break;
                    case "startinfo":
                        mObjects[archive][layer].Add(new StartObj(Entry, this, path));
                        break;
                    case "mappartsinfo":
                        mObjects[archive][layer].Add(new MapPartsObj(Entry, this, path));
                        break;
                }
            }

            bcsv.Close();
        }

        public void Close()
        {
            foreach (FilesystemBase fs in mMapFiles.Values)
            {
                fs.Close();
            }

            if (mMessages != null)
                mMessages.Close();

            if (mMessageFlows != null)
                mMessageFlows.Close();

            if (mMessagesFile != null)
                mMessagesFile.Close();
        }

        public bool IsZoneUsedOnLayer(string layer)
        {
            if (mHasStageObjList.ContainsKey(layer))
                return false;

            List<StageObj> zones = mHasStageObjList[layer];

            return zones.Any(s => mZoneName == s.mName);
        }

        public List<string> GetZonesUsedOnLayers(List<string> layers)
        {
            if (GameUtil.IsSMG1())
                layers = layers.ConvertAll(l => l.ToLower());

            List<string> zones = new List<string>();
            layers.ForEach(l =>
            {
                if (mHasStageObjList.ContainsKey(l))
                {
                    List<StageObj> z = mHasStageObjList[l];
                    z.ForEach(s => zones.Add(s.mName));
                }
            });

            return zones;
        }

        public List<AbstractObj> GetObjectsFromLayer(string archive, string layer)
        {
            return mObjects[archive][layer];
        }

        public List<AbstractObj> GetAllObjectsFromLayers(List<string> layers)
        {
            List<AbstractObj> ret = new List<AbstractObj>();

            foreach (string archive in cPossibleFiles)
            {
                if (!mObjects.ContainsKey(archive))
                    continue;

                Dictionary<string, List<AbstractObj>> objs = mObjects[archive];


                layers.ForEach(l =>
                {
                    if (objs.ContainsKey(l))
                    {
                        ret.AddRange(objs[l]);
                        //if (objs[l].Count > 0) Console.WriteLine(objs[l].ElementAt(0).mName);

                    }

                });
            }

            return ret;
        }

        public StageObj GetStageDataFromNameOnCurScenario(string stageName)
        {
            return GetAllStageDataForCurrentScenario().Find(o => o.mName == stageName);
        }

        public List<StageObj> GetAllStageDataFromLayers(List<string> layers)
        {
            List<StageObj> ret = new List<StageObj>();

            foreach (string layer in layers)
            {
                if (mHasStageObjList.ContainsKey(layer))
                {
                    ret.AddRange(mHasStageObjList[layer]);

                }
            }

            return ret;
        }

        public List<StageObj> GetAllStageDataForCurrentScenario()
        {
            List<string> layers = GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mZoneName));
            return GetAllStageDataFromLayers(layers);
        }

        public List<AbstractObj> GetObjectsFromLayers(string archive, string type, List<string> layers)
        {
            List<AbstractObj> ret = new List<AbstractObj>();

            // empty list to avoid a bunch of null exceptions
            if (!mObjects.ContainsKey(archive))
                return null;

            Dictionary<string, List<AbstractObj>> objs = mObjects[archive];

            layers.ForEach(l =>
            {
                // stage files don't share the same layers
                if (objs.ContainsKey(l))
                    ret.AddRange(objs[l].FindAll(o => o.mType == type));
            });

            return ret;
        }

        public List<AbstractObj> GetAllObjectsOfTypeFromCurrentScenario(string objType)
        {
            List<AbstractObj> ret = new List<AbstractObj>();
            List<string> layers = GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mZoneName));

            foreach(string file in cPossibleFiles)
            {
                if (mObjects.ContainsKey(file))
                {
                    ret.AddRange(GetObjectsFromLayers(file, objType, layers));
                }
            }

            return ret;
        }

        public List<AbstractObj> GetAllObjectsOfAllTypeFromCurrentScenario()
        {
            List<AbstractObj> ret = new List<AbstractObj>();
            string[] types = new string[] { "AreaObj", "CameraObj", "DebugObj", "DemoObj", "GeneralPosObj", "Obj", "MapPartsObj", "PathPointObj", "PlanetObj", "StartObj" };

            foreach(string type in types)
            {
                ret.AddRange(GetAllObjectsOfTypeFromCurrentScenario(type));
            }

            ret.AddRange(mPaths);
            mPaths.ForEach(p => ret.AddRange(p.mPathPointObjs));

            return ret;
        }

        public List<AbstractObj> GetAllObjectsWithAttributeNonZero(string attr)
        {
            List<AbstractObj> ret = new List<AbstractObj>();
            List<AbstractObj> allObjs = GetAllObjectsOfAllTypeFromCurrentScenario();

            foreach (AbstractObj obj in allObjs)
            {
                if (obj.mEntry.ContainsKey(attr))
                {
                    string fieldType = obj.mEntry.GetTypeOfField(attr).ToString();

                    switch(fieldType)
                    {
                        case "System.Int16":
                            short short_val = obj.Get<short>(attr);

                            if (short_val != -1)
                            {
                                ret.Add(obj);
                            }
                            break;
                        case "System.Int32":
                            int int_val = obj.Get<int>(attr);

                            if (int_val != -1)
                            {
                                ret.Add(obj);
                            }
                            break;
                        default:
                            Console.WriteLine($"lol type {fieldType}");
                            break;
                    }
                }
            }

            return ret;
        }

        public bool DoesAnyObjUsePathID(int id, out AbstractObj out_obj)
        {
            out_obj = null;
            List<AbstractObj> objs = GetAllObjectsOfTypeFromCurrentScenario("Obj");
            objs.AddRange(GetAllObjectsOfTypeFromCurrentScenario("MapPartsObj"));

            foreach(AbstractObj obj in objs)
            {
                if (obj.Get<short>("CommonPath_ID") == id)
                {
                    out_obj = obj;
                    return true;
                }
            }

            return false;
        }

        public List<int> GetAllUniqueIDS()
        {
            List<int> ids = new List<int>();

            foreach (string str in cPossibleFiles)
            {
                if (mObjects.ContainsKey(str))
                {
                    foreach (string layer in GameUtil.GalaxyLayersCommon)
                    {
                        if (mObjects[str].ContainsKey(layer))
                        {
                            mObjects[str][layer].ForEach(o => ids.Add(o.mUnique));
                        }
                    }
                }
            }

            mPaths.ForEach(p => ids.Add(p.mUnique));

            return ids;
        }

        public List<int> GetAllUniqueIDsFromZoneOnCurrentScenario() {
            List<string> layers = GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mZoneName));

            List<int> ids = new List<int>();

            foreach(string str in cPossibleFiles)
            {
                if (mObjects.ContainsKey(str))
                {
                    foreach(string l in layers)
                    {
                        List<AbstractObj> objs = mObjects[str][l];
                        objs.ForEach(o => ids.Add(o.mUnique));
                    }
                    
                }
            }

            mPaths.ForEach(p => ids.Add(p.mUnique));

            return ids;
        }

        public List<int> GetAllUniqueIDsFromObjectsOfType(string obj_type)
        {
            List<int> ids = new List<int>();

            if (obj_type == "PathObj")
            {
                mPaths.ForEach(p => ids.Add(p.mUnique));
                return ids;
            }

            List<string> layers = GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mZoneName));
           

            foreach (string str in cPossibleFiles)
            {
                if (mObjects.ContainsKey(str))
                {
                    foreach (string l in layers)
                    {
                        List<AbstractObj> objs = mObjects[str][l];

                        foreach(AbstractObj o in objs)
                        {
                            if (o.mType == obj_type)
                            {
                                ids.Add(o.mUnique);
                            }
                                
                        }
                    }

                }
            }

            return ids;
        }

        public void RenderObjFromUnique(int id, RenderMode mode, bool recalcPosRot = false)
        {
            List<string> layers = GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mZoneName));

            foreach (string str in cPossibleFiles)
            {
                if (mObjects.ContainsKey(str))
                {
                    foreach (string l in layers)
                    {
                        List<AbstractObj> objs = mObjects[str][l];

                        foreach(AbstractObj o in objs)
                        {
                            if (o.mUnique == id)
                            {
                                o.Render(mode);
                                // we found our unique id object, exit
                                return;
                            }
                        }
                    }
                }
            }

            foreach(PathObj pobj in mPaths)
            {
                if (pobj.mUnique == id)
                {
                    pobj.Render(mode);
                    return;
                }
            }
        }

        public PathObj GetPathFromID(int id)
        {
            foreach (PathObj pobj in mPaths)
            {
                if (pobj.mID == id)
                {
                    return pobj;
                }
            }

            return null;
        }

        public AbstractObj GetObjFromUniqueID(int id)
        {
            List<string> layers = GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mZoneName));

            foreach (string str in cPossibleFiles)
            {
                if (mObjects.ContainsKey(str))
                {
                    foreach (string l in layers)
                    {
                        if (!mObjects.ContainsKey(str))
                        {
                            break;
                        }

                        if (!mObjects[str].ContainsKey(l))
                        {
                            break;
                        }

                        List<AbstractObj> objs = mObjects[str][l];

                        foreach (AbstractObj o in objs)
                        {
                            if (o.mUnique == id)
                            {
                                return o;
                            }
                        }
                    }
                }
            }

            foreach (PathObj pobj in mPaths)
            {
                if (pobj.mUnique == id)
                {
                    return pobj;
                }
            }

            return null;
        }

        public void DeleteObjectWithUniqueID(int id)
        {
            int idx = -1;
            List<string> layers = GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mZoneName));

            foreach (string str in cPossibleFiles)
            {
                if (mObjects.ContainsKey(str))
                {
                    foreach (string l in layers)
                    {
                        List<AbstractObj> objs = mObjects[str][l];

                        foreach (AbstractObj o in objs)
                        {
                            if (o.mUnique == id)
                            {
                                idx = objs.IndexOf(o);
                                break;
                            }
                        }

                        if (idx != -1)
                        {
                            objs.RemoveAt(idx);
                            mObjects[str][l] = objs;
                            return;
                        }
                    }
                }
            }

            foreach (PathObj pobj in mPaths)
            {
                if (pobj.mUnique == id)
                {
                    idx = mPaths.IndexOf(pobj);
                }
            }

            if (idx != -1)
            {
                mPaths.RemoveAt(idx);
            }

            UpdatePathIndicies();
        }

        public void DeletePathPointFromPath(int id, int idx)
        {
            PathObj path = GetObjFromUniqueID(id) as PathObj;
            path.RemovePathPointAtIndex(idx);
        }

        public void UpdatePathIndicies()
        {
            int idx = 0;

            foreach (PathObj pobj in mPaths)
            {
                pobj.mID = idx;
                pobj.mEntry.Set("no", idx);
                idx++;
            }
        }

        public Camera GetCamera(string cameraName)
        {
            return mCameras.Find(c => c.mName == cameraName);
        }

        public CANM GetIntroCamera(int scenarioNo)
        {
            return mIntroCameras[$"StartScenario{scenarioNo + 1}.canm"];
        }

        public void SetIntroCamera(int scenarioNo, CANM camera)
        {
            //mIntroCameras[$"StartScenario{scenarioNo - 1}.canm"] = camera;
            //mMapFiles["Map"].Save();
            mMapFiles["Map"].ReplaceFileContents($"/camera/startscenario{scenarioNo + 1}.canm", camera.mFile.GetBuffer());
        }

        public bool HasMessages()
        {
            if (GameUtil.IsSMG1())
                return NameHolder.DoesMsgTblContain(mZoneName);
            else
                return mMessages != null;
        }

        public MSBT GetMessages()
        {
            return mMessages;
        }

        public MSBF GetFlows()
        {
            return mMessageFlows;
        }

        public bool HasFlows()
        {
            return mMessageFlows != null;
        }

        public List<string> GetLayersUsedOnZoneForCurrentScenario()
        {
            return GameUtil.GetGalaxyLayers(mGalaxy.GetMaskUsedInZoneOnCurrentScenario(mZoneName));
        }

        public void Save()
        {
            foreach (KeyValuePair<string, FilesystemBase> p in mMapFiles)
            {
                if (p.Key == "ZoneInfo")
                    continue;

                SaveObjects(p.Key, "Placement", "AreaObjInfo");
                SaveObjects(p.Key, "Placement", "CameraCubeInfo");
                SaveObjects(p.Key, "Placement", "DemoObjInfo");
                SaveObjects(p.Key, "Placement", "ObjInfo");
                SaveObjects(p.Key, "Placement", "PlanetObjInfo");
                SaveObjects(p.Key, "MapParts", "MapPartsInfo");
                SaveObjects(p.Key, "Start", "StartInfo");
                SaveObjects(p.Key, "Debug", "DebugMoveInfo");
                SaveObjects(p.Key, "GeneralPos", "GeneralPosInfo");
            }

            // todo -- why does this zone's MSBT not save right
            if (mZoneName != "MarioFaceShipZone")
            {
                if (mMessages != null)
                    mMessages.Save();

                if (mMessageFlows != null)
                    mMessageFlows.Save();

                if (mMessagesFile != null)
                    mMessagesFile.Save();
            }

            SaveCameras();

            if (mLights != null)
            {
                SaveLights();
                mMapFiles["Light"].Save();
            }

            SavePaths();

            // strip out unused files that we really don't need
            List<string> layers = mMapFiles["Map"].GetDirectories("/stage/jmp/placement");

            foreach (string layer in layers)
            {
                if (mMapFiles.ContainsKey("Design"))
                    mMapFiles["Design"].DeleteFile($"/stage/jmp/placement/{layer}/changeobjinfo");

                if (mMapFiles.ContainsKey("Sound"))
                    mMapFiles["Sound"].DeleteFile($"/stage/jmp/placement/{layer}/changeobjinfo");

                mMapFiles["Map"].DeleteFile($"/stage/jmp/placement/{layer}/changeobjinfo");
            }


            if (mMapFiles.ContainsKey("Design"))
            {
                mMapFiles["Design"].DeleteFile("/stage/jmp/list/changescenelistinfo");
                mMapFiles["Design"].DeleteFile("/stage/jmp/path/commonpathpointinfo");
                mMapFiles["Design"].Save();
            }

            if (mMapFiles.ContainsKey("Sound"))
            {
                mMapFiles["Sound"].DeleteFile("/stage/jmp/list/changescenelistinfo");
                mMapFiles["Sound"].DeleteFile("/stage/jmp/path/commonpathpointinfo");
                mMapFiles["Sound"].Save();
            }

            mMapFiles["Map"].DeleteFile("/stage/jmp/list/changescenelistinfo");
            mMapFiles["Map"].DeleteFile("/stage/jmp/path/commonpathpointinfo");
            mMapFiles["Map"].Save();
        }

        private void SaveObjects(string archive, string dir, string file)
        {
            //if (archive == "Light") return;
            //if (archive == "Ghost") return;
            //Console.WriteLine(archive+"  "+dir);
            List<string> layers = mMapFiles[archive].GetDirectories($"/stage/jmp/{dir}");
            if (layers == null) return;

            foreach (string layer in layers)
            {
                SaveObjectList(archive, $"{dir}/{layer}/{file}");
            }

            mMapFiles[archive].Save();
        }

        private void SaveObjectList(string archive, string path)
        {
            string[] content = path.Split('/');

            string dir = content[0];
            string layer = content[1];
            string file = content[2];

            BCSV bcsv = new BCSV(mMapFiles[archive].OpenFile($"/stage/jmp/{path}"));
            bcsv.mEntries.Clear();

            List<AbstractObj> objs = mObjects[archive][layer];

            foreach (AbstractObj o in objs)
            {
                if (o.mFile == file)
                {
                    o.Save();
                    bcsv.mEntries.Add(o.mEntry);
                }
            }

            bcsv.Save();
            bcsv.Close();
        }

        private void SaveCameras()
        {
            BCSV bcsv = new BCSV(mMapFiles["Map"].OpenFile("/root/camera/CameraParam.bcam"));
            bcsv.mEntries.Clear();
            bcsv.RemoveField("no");

            foreach (Camera c in mCameras)
            {
                c.Save();
                bcsv.mEntries.Add(c.mEntry);
            }

            bcsv.Save();
            bcsv.Close();
        }

        private void SaveLights()
        {
            BCSV light = new BCSV(mMapFiles["Light"].OpenFile($"/root/csv/{mZoneName}Light.bcsv"));
            light.mEntries.Clear();

            foreach (Light l in mLights)
            {
                l.Save();
                light.mEntries.Add(l.mEntry);
            }

            light.Save();
            light.Close();
        }

        private void SavePaths()
        {
            BCSV pathsBCSV = new BCSV(mMapFiles["Map"].OpenFile("/root/jmp/Path/CommonPathInfo"));
            pathsBCSV.mEntries.Clear();

            foreach (PathObj p in mPaths)
            {
                p.Save();
                pathsBCSV.mEntries.Add(p.mEntry);
            }

            pathsBCSV.Save();
        }

        public Galaxy mGalaxy;
        private Game mGame;
        private FilesystemBase mFilesystem;
        public string mZoneName;

        public bool mIsMainGalaxy;

        public Dictionary<string, Dictionary<string, List<AbstractObj>>> mObjects;

        public Dictionary<string, List<StageObj>> mHasStageObjList;
        public List<Camera> mCameras;
        public Dictionary<string, CANM> mIntroCameras;
        public Dictionary<string, GST> mGhostFiles;
        public List<Light> mLights;
        public List<PathObj> mPaths;

        Dictionary<string, FilesystemBase> mMapFiles;

        private RARCFilesystem mMessagesFile;
        private MSBT mMessages;
        private MSBF mMessageFlows;
        public ZoneAttributes mAttributes;
    }
}
