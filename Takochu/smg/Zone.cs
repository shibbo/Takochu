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
using Takochu.smg.obj;

namespace Takochu.smg
{
    public class Zone
    {
        public string[] cPossibleFiles = { "Design", "Light", "Map", "Sound" };

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
            // so first we need to collect all of the files used in this zone
            // zones can use Design, Sound, Light, etc
            foreach(string file in cPossibleFiles)
            {
                string path = $"/StageData/{mZoneName}/{mZoneName}{file}.arc";

                if (mFilesystem.DoesFileExist(path))
                {
                    mMapFiles.Add(file, new RARCFilesystem(mFilesystem.OpenFile(path)));

                    if (file == "Light")
                    {
                        LoadLight();
                    }
                    else
                    {
                        // we load our StageObjInfo first because objects can use their offsets

                        if (mIsMainGalaxy)
                            LoadObjects(file, "Placement", "StageObjInfo");

                        LoadObjects(file, "Placement", "AreaObjInfo");
                        LoadObjects(file, "Placement", "ObjInfo");
                        LoadObjects(file, "Start", "StartInfo");
                        LoadObjects(file, "MapParts", "MapPartsInfo");
                        LoadObjects(file, "Placement", "DemoObjInfo");
                    }
                }
            }

            LoadCameras();
            LoadMessages();
        }

        public void LoadObjects(string archive, string directory, string file)
        {
            List<string> layers = mMapFiles[archive].GetDirectories("/root/jmp/" + directory);
            layers.ForEach(l => AssignsObjectsToList(archive, $"{directory}/{l}/{file}"));
        }

        public void LoadCameras()
        {
            BCSV cameraBCSV = new BCSV(mMapFiles["Map"].OpenFile("/root/camera/CameraParam.bcam"));
            cameraBCSV.RemoveField("no");
            mCameras = new List<Camera>();
            cameraBCSV.mEntries.ForEach(c => mCameras.Add(new Camera(c, this)));
        }

        public void LoadLight()
        {
            // thank you, DrillUpDownHardPlanetZone for having a empty light file for god knows why
            if (mMapFiles["Light"].GetFiles("/root/csv").Count == 0)
                return;

            BCSV lightBCSV = new BCSV(mMapFiles["Light"].OpenFile($"/root/csv/{mZoneName}Light.bcsv"));
            mLights = new List<Light>();
            lightBCSV.mEntries.ForEach(e => mLights.Add(new Light(e, mZoneName)));
        }

        public void LoadMessages()
        {
            if (mFilesystem.DoesFileExist($"/LocalizeData/UsEnglish/MessageData/{mZoneName}.arc"))
            {
                RARCFilesystem msg = new RARCFilesystem(mFilesystem.OpenFile($"/LocalizeData/UsEnglish/MessageData/{mZoneName}.arc"));

                if (msg.DoesFileExist($"/root/{mZoneName}.msbt"))
                    mMessages = new MSBT(msg.OpenFile($"/root/{mZoneName}.msbt"));

                if (msg.DoesFileExist($"/root/{mZoneName}.msbf"))
                    mMessageFlows = new MSBF(msg.OpenFile($"/root/{mZoneName}.msbf"));
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
                switch (dir)
                {
                    case "AreaObjInfo":
                        mObjects[archive][layer].Add(new AreaObj(e, this, path));
                        break;
                    case "StageObjInfo":
                        mZones[layer].Add(new StageObj(e));
                        break;
                    case "ObjInfo":
                        mObjects[archive][layer].Add(new LevelObj(e, this, path));
                        break;
                    case "DemoObjInfo":
                        mObjects[archive][layer].Add(new DemoObj(e, this, path));
                        break;
                    case "StartInfo":
                        mObjects[archive][layer].Add(new StartObj(e, this, path));
                        break;
                    case "MapPartsInfo":
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

            foreach(string archive in cPossibleFiles)
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

        public Galaxy mGalaxy;
        private Game mGame;
        private FilesystemBase mFilesystem;
        public string mZoneName;
        public bool mIsMainGalaxy;

        public Dictionary<string, Dictionary<string, List<AbstractObj>>> mObjects;
        public Dictionary<string, List<StageObj>> mZones;
        public List<Camera> mCameras;
        public List<Light> mLights;

        Dictionary<string, FilesystemBase> mMapFiles;

        private MSBT mMessages;
        private MSBF mMessageFlows;
    }
}
