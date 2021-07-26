using GL_EditorFramework.EditorDrawables;
using Microsoft.SqlServer.Server;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;
using Takochu.smg.msg;
using Takochu.smg.obj;
using Takochu.util;

namespace Takochu.smg
{
    public class Zone
    {
        public string[] cPossibleFiles = { "Design", "Light", "Map", "Sound", "ZoneInfo" };

        public Zone(Galaxy galaxy, string name)
        {
            mGalaxy = galaxy;
            mGame = galaxy.mGame;
            mFilesystem = mGame.mFilesystem;
            mZoneName = name;

            mIsMainGalaxy = mGalaxy.mName == name;

            mMapFiles = new Dictionary<string, FilesystemBase>();
            mObjects = new Dictionary<string, Dictionary<string, List<AbstractObj>>>();
            mZones = new Dictionary<string, List<StageObj>>();

            Load();
        }

        public void Load()
        {
            if (GameUtil.IsSMG1())
            {
                string path = $"/StageData/{mZoneName}.arc";

                if (mFilesystem.DoesFileExist(path))
                {
                    mMapFiles.Add("Map", new RARCFilesystem(mFilesystem.OpenFile(path)));

                    if (mIsMainGalaxy)
                        LoadObjects("Map", "Placement", "StageObjInfo");

                    LoadObjects("Map", "placement", "AreaObjInfo");
                    LoadObjects("Map", "placement", "CameraCubeInfo");
                    LoadObjects("Map", "placement", "ObjInfo");
                    LoadObjects("Map", "placement", "PlanetObjInfo");
                    LoadObjects("Map", "GeneralPos", "GeneralPosInfo");
                    LoadObjects("Map", "Debug", "DebugMoveInfo");
                    LoadObjects("Map", "Start", "StartInfo");
                    LoadObjects("Map", "MapParts", "MapPartsInfo");
                    LoadObjects("Map", "placement", "DemoObjInfo");
                }
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
                        mMapFiles.Add(file, new RARCFilesystem(mFilesystem.OpenFile(path)));

                        if (file == "Light")
                        {
                            LoadLight();
                        }
                        else if (file == "ZoneInfo")
                        {
                            LoadAttributes();
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
        }

        public void LoadAttributes()
        {
            mAttributes = new ZoneAttributes(mMapFiles["ZoneInfo"]);
        }

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
            if (mFilesystem.DoesFileExist($"/LocalizeData/{Program.lang}/MessageData/{mZoneName}.arc"))
            {
                mMessagesFile = new RARCFilesystem(mFilesystem.OpenFile($"/LocalizeData/{Program.lang}/MessageData/{mZoneName}.arc"));

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

            mPaths = new List<PathObj>();

            foreach (BCSV.Entry e in pathsBCSV.mEntries)
            {
                mPaths.Add(new PathObj(e, this, (RARCFilesystem)mMapFiles["Map"]));
            }
        }

        public void AssignsObjectsToList(string archive, string path)
        {
            string[] data = path.Split('/');
            string layer = data[1];
            string dir = data[2];

            if (!mObjects.ContainsKey(archive))
            {
                mObjects.Add(archive, new Dictionary<string, List<AbstractObj>>());
            }

            if (!mObjects[archive].ContainsKey(layer))
            {
                mObjects[archive].Add(layer, new List<AbstractObj>());
            }

            if (!mZones.ContainsKey(layer))
            {
                mZones.Add(layer, new List<StageObj>());
            }

            BCSV bcsv = new BCSV(mMapFiles[archive].OpenFile($"/stage/jmp/{path}"));

            foreach (BCSV.Entry e in bcsv.mEntries)
            {
                dir = dir.ToLower();

                switch (dir)
                {
                    case "areaobjinfo":
                        mObjects[archive][layer].Add(new AreaObj(e, this, path));
                        break;
                    case "cameracubeinfo":
                        mObjects[archive][layer].Add(new CameraObj(e, this, path));
                        break;
                    case "stageobjinfo":
                        mZones[layer].Add(new StageObj(e));
                        break;
                    case "objinfo":
                        mObjects[archive][layer].Add(new LevelObj(e, this, path));
                        break;
                    case "demoobjinfo":
                        mObjects[archive][layer].Add(new DemoObj(e, this, path));
                        break;
                    case "generalposinfo":
                        mObjects[archive][layer].Add(new GeneralPos(e, this, path));
                        break;
                    case "debugmoveinfo":
                        mObjects[archive][layer].Add(new DebugMoveObj(e, this, path));
                        break;
                    case "planetobjinfo":
                        mObjects[archive][layer].Add(new PlanetObj(e, this, path));
                        break;
                    case "startinfo":
                        mObjects[archive][layer].Add(new StartObj(e, this, path));
                        break;
                    case "mappartsinfo":
                        mObjects[archive][layer].Add(new MapPartsObj(e, this, path));
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
            if (mZones.ContainsKey(layer))
                return false;

            List<StageObj> zones = mZones[layer];

            return zones.Any(s => mZoneName == s.mName);
        }

        public List<string> GetZonesUsedOnLayers(List<string> layers)
        {
            if (GameUtil.IsSMG1())
                layers = layers.ConvertAll(l => l.ToLower());

            List<string> zones = new List<string>();
            layers.ForEach(l =>
            {
                if (mZones.ContainsKey(l))
                {
                    List<StageObj> z = mZones[l];
                    z.ForEach(s => zones.Add(s.mName));
                }
            });

            return zones;
        }

        public List<AbstractObj> GetObjectsFromLayer(string archive, string layer)
        {
            return mObjects[archive][layer];
        }

        public ObjectHolder GetAllObjectsFromLayers(List<string> layers)
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
                        ret.AddRange(objs[l]);
                });
            }

            return new ObjectHolder(ref ret);
        }

        public ObjectHolder GetObjectsFromLayers(string archive, string type, List<string> layers)
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

            return new ObjectHolder(ref ret);
        }

        public Camera GetCamera(string cameraName)
        {
            return mCameras.Find(c => c.mName == cameraName);
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
            //if (mZoneName != "MarioFaceShipZone")
            //{ 
                if (mMessages != null)
                    mMessages.Save();

                if (mMessageFlows != null)
                    mMessageFlows.Save();

                if (mMessagesFile != null)
                    mMessagesFile.Save();
            //}

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
            if (archive == "Light")
                return;

            List<string> layers = mMapFiles[archive].GetDirectories($"/stage/jmp/{dir}");

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
        public Dictionary<string, List<StageObj>> mZones;
        public List<Camera> mCameras;
        public List<Light> mLights;
        public List<PathObj> mPaths;

        Dictionary<string, FilesystemBase> mMapFiles;

        private RARCFilesystem mMessagesFile;
        private MSBT mMessages;
        private MSBF mMessageFlows;
        public ZoneAttributes mAttributes;
    }
}
