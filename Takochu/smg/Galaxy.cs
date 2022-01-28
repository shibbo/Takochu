using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;
using Takochu.smg.msg;
using Takochu.smg.obj;
using Takochu.util;

namespace Takochu.smg
{
    public class Galaxy
    {
        public Galaxy(Game game, string name)
        {
            mGame = game;
            mFilesystem = game.mFilesystem;
            mName = name;

            mRemovedZones = new List<string>();
            mZones = new Dictionary<string, Zone>();
            mZoneEntries = new Dictionary<string, BCSV.Entry>();
            //var a = mFilesystem.OpenFile($"/StageData/{name}/{name}Scenario.arc");
            mScenarioFile = new RARCFilesystem(mFilesystem.OpenFile($"/StageData/{name}/{name}Scenario.arc"));
            //a.Close();

            var text = "/root/ZoneList.bcsv";
            if (GameUtil.IsSMG1()) text = "/root/zonelist.bcsv";
            BCSV zonesBCSV = new BCSV(mScenarioFile.OpenFile(text));

            foreach(BCSV.Entry e in zonesBCSV.mEntries)
            {
                string n = e.Get<string>("ZoneName");

                if (n == "PoleUnizoZone")
                    continue;

                mZones.Add(n, new Zone(this, n));
                mZoneEntries.Add(n, e);
            }

            zonesBCSV.Close();
            var text2 = "/root/ScenarioData.bcsv";
            if (GameUtil.IsSMG1()) text2 = "/root/scenariodata.bcsv";
            BCSV scenarioBCSV = new BCSV(mScenarioFile.OpenFile("/root/ScenarioData.bcsv"));

            mScenarios = new Dictionary<int, Scenario>();

            foreach (BCSV.Entry e in scenarioBCSV.mEntries)
            {
                mScenarios.Add(e.Get<int>("ScenarioNo"), new Scenario(e, mZones.Keys.ToList()));
            }

            scenarioBCSV.Close();

            if (!NameHolder.HasGalaxyName(name))
                return;

            mGalaxyName = NameHolder.GetGalaxyName(name);
        }

        public void RemoveZone(string zoneName)
        {
            mZones[zoneName].Close();
            mZones.Remove(zoneName);
            mZoneEntries.Remove(zoneName);
            
            foreach(KeyValuePair<int, Scenario> kvp in mScenarios)
            {
                kvp.Value.RemoveZone(zoneName);
            }

            mRemovedZones.Add(zoneName);
        }

        public void Close()
        {
            mFilesystem.Close();

            foreach (Zone zone in GetZones().Values)
            {
                zone.Close();
            }

            mScenarioFile.Close();
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
            return (from KeyValuePair<int, Scenario> scenarios in mScenarios where scenarios.Value.mPowerStarType == "Green" select scenarios).Count();
        }

        public bool ContainsZone(string zone)
        {
            return mZones.ContainsKey(zone);
        }

        public Dictionary<string, Zone> GetZones()
        {
            return mZones;
        }

        public List<string> GetZoneNames()
        {
            return mZones.Keys.ToList();
        }

        public Zone GetGalaxyZone()
        {
            return mZones[mName];
        }

        public List<string> GetZonesUsedOnCurrentScenario()
        {
            Zone galaxyZone = GetGalaxyZone();
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
            var ZoneCurrentLayers = GetGalaxyZone().GetLayersUsedOnZoneForCurrentScenario();

            Vector3 Result_v3 = Vector3.Zero;

            foreach (var Layer in ZoneCurrentLayers)
            {
                if (GameUtil.IsSMG2())
                {
                    if (GetGalaxyZone().mZones.ContainsKey(Layer))
                    {
                        SearchFile = (GetGalaxyZone().mZones[Layer]);
                    }
                    else if (GetGalaxyZone().mZones.ContainsKey(Layer.ToLower())) {
                        SearchFile = (GetGalaxyZone().mZones[Layer.ToLower()]);
                    }
                }
                else
                {
                    SearchFile = 
                        GetGalaxyZone().mZones[Layer.ToLower()];
                }

                var FindIndex = 
                    SearchFile.FindIndex(x => x.mName == zoneName);

                if (FindIndex < 0) continue;
                

                Result_v3 = SearchFile.ElementAt(FindIndex).mPosition;
                //Console.WriteLine("//////////Pos_GlobalOffset//////////");
                //Console.Write("X_" + SearchFile.ElementAt(FindIndex).mPosition.X);
                //Console.Write("  Y_" + SearchFile.ElementAt(FindIndex).mPosition.Y);
                //Console.WriteLine("  Z_" + SearchFile.ElementAt(FindIndex).mPosition.Z + "\n\r");
                break;
            }
            return Result_v3/*SearchFile.ElementAt(FindIndex).mPosition*/;

        }

        public Vector3 Get_Rot_GlobalOffset(string zoneName) 
        {
            List<StageObj> SearchFile = new List<StageObj>();

            var ZoneGlobalOffset = new Vector3(0f, 0f, 0f);
            var ZoneCurrentLayers = GetGalaxyZone().GetLayersUsedOnZoneForCurrentScenario();

            Vector3 Result_v3 = Vector3.Zero;

            foreach (var Layer in ZoneCurrentLayers)
            {
                if (GameUtil.IsSMG2())
                {
                    if (GetGalaxyZone().mZones.ContainsKey(Layer))
                    {
                        SearchFile = (GetGalaxyZone().mZones[Layer]);
                    }
                    else if (GetGalaxyZone().mZones.ContainsKey(Layer.ToLower()))
                    {
                        SearchFile = (GetGalaxyZone().mZones[Layer.ToLower()]);
                    }
                }
                else
                {
                    SearchFile = 
                        GetGalaxyZone().mZones[Layer.ToLower()];
                }

                var FindIndex = SearchFile.FindIndex(x => x.mName == zoneName);
                SearchFile.ForEach(x => Console.WriteLine(x.mName));
                if (FindIndex < 0) continue;
                //Console.WriteLine("//////////Ros_GlobalOffset_Rot//////////" + "  " + zoneName);
                //Console.Write("X_" + Math.Truncate(SearchFile.ElementAt(FindIndex).mRotation.X));
                //Console.Write("  Y_" + SearchFile.ElementAt(FindIndex).mRotation.Y);
                //Console.WriteLine("  Z_" + SearchFile.ElementAt(FindIndex).mRotation.Z + "\n\r");
                Result_v3 = SearchFile.ElementAt(FindIndex).mRotation;
                break;
            }
            return Result_v3/*SearchFile.ElementAt(FindIndex).mRotation*/;
        }

        public Zone GetZone(string name)
        {
            if (!mZones.ContainsKey(name))
                throw new Exception("Galaxy::GetZone() - Zone does not exist.");

            return mZones[name];
        }

        public BCSV.Entry GetScenarioInfoForCurrentScenario()
        {
            // it is smart to instead check for our scenario info in a loop
            // sometimes scenario data is not stored in order, so using an index may produce inaccurate results
            return mScenarios[mScenarioNo].mEntry;
        }

        public Scenario GetScenario(int scenarioNo)
        {
            return mScenarios[scenarioNo];
        }

        public int GetMaskUsedInZoneOnCurrentScenario(string zoneName)
        {
            return GetScenarioInfoForCurrentScenario().Get<int>(zoneName);
        }

        public void Save()
        {
            BCSV zonesBCSV = new BCSV(mScenarioFile.OpenFile("/root/ZoneList.bcsv"));
            zonesBCSV.mEntries.Clear();

            foreach (KeyValuePair<string, Zone> z in mZones)
            {
                z.Value.Save();
                zonesBCSV.mEntries.Add(mZoneEntries[z.Key]);
            }

            foreach (string zone in mRemovedZones)
            {
                zonesBCSV.RemoveField(zone);
            }

            zonesBCSV.Save();
            mScenarioFile.Save();
            NameHolder.Save();
        }

        public void SaveScenario()
        {
            BCSV scenarioBCSV = new BCSV(mScenarioFile.OpenFile("/root/ScenarioData.bcsv"));
            scenarioBCSV.mEntries.Clear();

            foreach(KeyValuePair<int, Scenario> scenario in mScenarios)
            {
                scenarioBCSV.mEntries.Add(scenario.Value.mEntry);
            }

            scenarioBCSV.Save();
            mScenarioFile.Save();
        }

        public Game mGame;
        public FilesystemBase mFilesystem;
        public RARCFilesystem mScenarioFile;
        public Dictionary<int, Scenario> mScenarios;
        public int mScenarioNo;

        public string mName;
        private Dictionary<string, Zone> mZones;
        private Dictionary<string, BCSV.Entry> mZoneEntries;
        private List<string> mRemovedZones;
        public string mGalaxyName;
        public string mCurScenarioName;
    }
}
