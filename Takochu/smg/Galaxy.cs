using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.io;
using Takochu.smg.msg;
using Takochu.smg.obj;
using Takochu.util;
using Takochu.io.SpecificNameARC;

namespace Takochu.smg
{
    public class Galaxy
    {
        public ScenarioArcFile ScenarioARC { get; private set; }

        public Galaxy(Game game, string galaxyName)
        {
            mGame = game;
            mFilesystem = game.mFilesystem;
            mName = galaxyName;
            mRemovedZones = new List<string>();
            //mZones = new Dictionary<string, Zone>();
            //mZoneEntries = new Dictionary<string, BCSV.Entry>();

            System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();
            Console.WriteLine("ReadStart---------------");
            s.Start();
            ReadScenarioArc();
            s.Stop();

            Console.WriteLine("ReadScenario: "+s.Elapsed.TotalSeconds);

            if (!NameHolder.HasGalaxyName(galaxyName))
                return;

            mHolderName = NameHolder.GetGalaxyName(galaxyName);
        }

        private void ReadScenarioArc() 
        {
            ScenarioARC = new ScenarioArcFile(mFilesystem, this);
            ScenarioARC.ReadAllFiles();
        }

        public void RemoveZone(string removeZoneName)
        {
            ScenarioARC.ZoneListBCSV[removeZoneName].Close();

            Zone mainGalaxy = GetMainGalaxyZone();



            foreach(KeyValuePair<string, List<StageObj>> stageObjs in mainGalaxy.mHasStageObjList)
            {
                //List<StageObj> objs = new List<StageObj>();

                foreach(StageObj stageObj in stageObjs.Value)
                {
                    if (stageObj.mName == removeZoneName)
                    {
                        var findStageObjIndex = mainGalaxy.mHasStageObjList[stageObjs.Key].IndexOf(stageObj);
                        mainGalaxy.mHasStageObjList[stageObjs.Key].RemoveAt(findStageObjIndex);
                        break;
                    }
                }
            }

            ScenarioARC.ZoneListBCSV.Remove(removeZoneName);
            ScenarioARC.ZoneListBCSV_Entries.Remove(removeZoneName);
            
            foreach(KeyValuePair<int, ScenarioEntry> scenarioBCSV in ScenarioARC.ScenarioDataBCSV)
            {
                scenarioBCSV.Value.RemoveZone(removeZoneName);
            }

            mRemovedZones.Add(removeZoneName);
        }

        public void Close()
        {
            mFilesystem.Close();

            foreach (Zone zone in GetZones().Values)
            {
                zone.Close();
            }

            ScenarioARC.RARCFileStream.Close();
        }

        public void SetScenario(int no)
        {
            mScenarioNo = no;

            if (!NameHolder.HasGalaxyName(mName))
                return;

            // Green stars are a little more complicated to determine
            // they don't follow the scenario number scheme that regular stars do
            int greenStarNum = GetGreenStarNo();

            // for some special galaxies with no green stars at all
            if (greenStarNum == 0)
            {
                mCurScenarioName = NameHolder.GetScenarioName(mName, no);
                return;
            }

            // if there are 3 total green stars, we need to see if we have selected scenarios 1, 2, or 3
            // if there are 2 total green stars, we need to see if we have selected scenarios 1 or 2
            // if we have, we just load the regular scenario names
            // if we have selected a green star, we now have to see what number,
            // by subtracting the number of regular stars to get our current green star index
            if (no < 4 && greenStarNum == 3 || no < 3 && greenStarNum == 2)
                mCurScenarioName = NameHolder.GetScenarioName(mName, no);
            else
                mCurScenarioName = NameHolder.GetScenarioName("GreenStar", greenStarNum == 2 ? no - 2 : no - 3);
        }

        public int GetGreenStarNo()
        {
            return (from KeyValuePair<int, ScenarioEntry> scenarios in ScenarioARC.ScenarioDataBCSV where scenarios.Value.PowerStarType == "Green" select scenarios).Count();
        }

        public bool ContainsZone(string zone)
        {
            return ScenarioARC.ZoneListBCSV.ContainsKey(zone);
        }

        public Dictionary<string, Zone> GetZones()
        {
            return ScenarioARC.ZoneListBCSV;
        }

        public List<string> GetZoneNames()
        {
            return ScenarioARC.ZoneListBCSV.Keys.ToList();
        }

        public Zone GetMainGalaxyZone()
        {
            return ScenarioARC.ZoneListBCSV[mName];
        }

        public List<string> GetZonesUsedOnCurrentScenario()
        {
            Zone galaxyZone = GetMainGalaxyZone();
            return galaxyZone.GetZonesUsedOnLayers(galaxyZone.GetLayersUsedOnZoneForCurrentScenario());
        }

        /// <summary>
        /// Gets the origin of the zone.<br/>
        /// ゾーンの原点を取得
        /// </summary>
        /// <param name="zoneName">ZoneName</param>
        /// <returns></returns>
        public Vector3 Get_Pos_GlobalOffset(string zoneName)
        {
            List<StageObj> SearchFile = new List<StageObj>();
            
            var ZoneGlobalOffset = new Vector3(0f,0f,0f);
            var ZoneCurrentLayers = GetMainGalaxyZone().GetLayersUsedOnZoneForCurrentScenario();

            Vector3 Result_v3 = Vector3.Zero;

            foreach (var Layer in ZoneCurrentLayers)
            {
                if (GameUtil.IsSMG2())
                {
                    if (GetMainGalaxyZone().mHasStageObjList.ContainsKey(Layer))
                    {
                        SearchFile = (GetMainGalaxyZone().mHasStageObjList[Layer]);
                    }
                    else if (GetMainGalaxyZone().mHasStageObjList.ContainsKey(Layer.ToLower())) {
                        SearchFile = (GetMainGalaxyZone().mHasStageObjList[Layer.ToLower()]);
                    }
                }
                else
                {
                    SearchFile = 
                        GetMainGalaxyZone().mHasStageObjList[Layer.ToLower()];
                }

                var FindIndex = 
                    SearchFile.FindIndex(x => x.mName == zoneName);

                if (FindIndex < 0) continue;
                

                Result_v3 = SearchFile.ElementAt(FindIndex).mPosition;
                break;
            }
            return Result_v3;

        }

        public Vector3 Get_Rot_GlobalOffset(string zoneName) 
        {
            List<StageObj> SearchFile = new List<StageObj>();

            var ZoneGlobalOffset = new Vector3(0f, 0f, 0f);
            var ZoneCurrentLayers = GetMainGalaxyZone().GetLayersUsedOnZoneForCurrentScenario();

            Vector3 Result_v3 = Vector3.Zero;

            foreach (var Layer in ZoneCurrentLayers)
            {
                if (GameUtil.IsSMG2())
                {
                    if (GetMainGalaxyZone().mHasStageObjList.ContainsKey(Layer))
                    {
                        SearchFile = (GetMainGalaxyZone().mHasStageObjList[Layer]);
                    }
                    else if (GetMainGalaxyZone().mHasStageObjList.ContainsKey(Layer.ToLower()))
                    {
                        SearchFile = (GetMainGalaxyZone().mHasStageObjList[Layer.ToLower()]);
                    }
                }
                else
                {
                    SearchFile = 
                        GetMainGalaxyZone().mHasStageObjList[Layer.ToLower()];
                }

                var FindIndex = SearchFile.FindIndex(x => x.mName == zoneName);
                SearchFile.ForEach(x => Console.WriteLine(x.mName));
                if (FindIndex < 0) continue;
                Result_v3 = SearchFile.ElementAt(FindIndex).mRotation;
                break;
            }
            return Result_v3;
        }

        public Zone GetZone(string name)
        {
            if (!ScenarioARC.ZoneListBCSV.ContainsKey(name))
                throw new Exception("Galaxy::GetZone() - Zone does not exist.");

            return ScenarioARC.ZoneListBCSV[name];
        }

        public BCSV.Entry GetScenarioInfoForCurrentScenario()
        {
            // it is smart to instead check for our scenario info in a loop
            // sometimes scenario data is not stored in order, so using an index may produce inaccurate results
            return ScenarioARC.ScenarioDataBCSV[mScenarioNo].Entry;
        }

        public ScenarioEntry GetScenario(int scenarioNo)
        {
            return ScenarioARC.ScenarioDataBCSV[scenarioNo];
        }

        public int GetMaskUsedInZoneOnCurrentScenario(string zoneName)
        {
            return GetScenarioInfoForCurrentScenario().Get<int>(zoneName);
        }

        public void Save()
        {
            BCSV zonesBCSV = new BCSV(ScenarioARC.RARCFileStream.OpenFile("/root/ZoneList.bcsv"));
            zonesBCSV.mEntries.Clear();

            foreach (KeyValuePair<string, Zone> z in ScenarioARC.ZoneListBCSV)
            {
                z.Value.Save();
                zonesBCSV.mEntries.Add(ScenarioARC.ZoneListBCSV_Entries[z.Key]);
            }

            foreach (string zone in mRemovedZones)
            {
                zonesBCSV.RemoveField(zone);
            }

            zonesBCSV.Save();
            ScenarioARC.RARCFileStream.Save();
            NameHolder.Save();
        }

        public void SaveScenario()
        {
            BCSV scenarioBCSV = new BCSV(ScenarioARC.RARCFileStream.OpenFile("/root/ScenarioData.bcsv"));
            scenarioBCSV.mEntries.Clear();

            foreach(KeyValuePair<int, ScenarioEntry> scenario in ScenarioARC.ScenarioDataBCSV)
            {
                scenarioBCSV.mEntries.Add(scenario.Value.Entry);
            }

            scenarioBCSV.Save();
            ScenarioARC.RARCFileStream.Save();
        }

        public Game mGame { get; private set; }
        private FilesystemBase mFilesystem;
        public int mScenarioNo { get; private set; }

        public string mName { get; private set; }
        private List<string> mRemovedZones;
        public string mHolderName { get; private set; }
        public string mCurScenarioName { get; private set; }
    }
}
