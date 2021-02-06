using GL_EditorFramework.EditorDrawables;
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

namespace Takochu.smg
{
    public class Galaxy
    {
        public Galaxy(Game game, string name)
        {
            mGame = game;
            mFilesystem = game.mFilesystem;
            mName = name;

            mZones = new Dictionary<string, Zone>();
            RARCFilesystem scenarioFile = new RARCFilesystem(mFilesystem.OpenFile($"/StageData/{name}/{name}Scenario.arc"));
            BCSV scenarioBCSV = new BCSV(scenarioFile.OpenFile("/root/ScenarioData.bcsv"));
            mScenarioEntries = scenarioBCSV.mEntries;
            scenarioBCSV.Close();

            BCSV zonesBCSV = new BCSV(scenarioFile.OpenFile("/root/ZoneList.bcsv"));

            foreach(BCSV.Entry e in zonesBCSV.mEntries)
            {
                string n = e.Get<string>("ZoneName");
                mZones.Add(n, new Zone(this, n));
            }

            zonesBCSV.Close();
            scenarioFile.Close();

            if (mName == "FileSelect" || mName == "PeachCastleGalaxy" || mName == "StaffRollGalaxy")
                return;

            mGalaxyName = NameHolder.GetGalaxyName(name);
        }

        public void Close()
        {
            mFilesystem.Close();

            foreach (Zone zone in GetZones().Values)
            {
                zone.Close();
            }
        }

        public void SetScenario(int no)
        {
            mScenarioNo = no;

            if (mName == "FileSelect" || mName == "PeachCastleGalaxy" || mName == "StaffRollGalaxy")
                return;

            // Green stars are a little more complicated to determine
            // they don't follow the scenario number scheme that regular stars do
            int greenStarNum = GetGreenStarNo();

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
            return mScenarioEntries.Where(e => e.Get<string>("PowerStarType") == "Green").Count();
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
            return mScenarioEntries.Find(e => e.Get<int>("ScenarioNo") == mScenarioNo);
        }

        public int GetMaskUsedInZoneOnCurrentScenario(string zoneName)
        {
            return GetScenarioInfoForCurrentScenario().Get<int>(zoneName);
        }

        public List<string> GetGalaxyLayers(int mask)
        {
            List<string> layers = new List<string>
            {
                "Common",
            };

            string[] GalaxyLayers = new string[]
            {
                "LayerA",
                "LayerB",
                "LayerC",
                "LayerD",
                "LayerE",
                "LayerF",
                "LayerG",
                "LayerH",
                "LayerI",
                "LayerJ",
                "LayerK",
                "LayerL",
                "LayerM",
                "LayerN",
                "LayerO",
                "LayerP"
            };

            for (int i = 0; i < 16; i++)
            {
                if (((mask >> i) & 0x1) != 0x0)
                    layers.Add(GalaxyLayers[i]);
            }

            return layers;
        }

        public Game mGame;
        private FilesystemBase mFilesystem;
        public List<BCSV.Entry> mScenarioEntries;
        int mScenarioNo;

        public string mName;
        private Dictionary<string, Zone> mZones;
        public string mGalaxyName;
        public string mCurScenarioName;
    }
}
