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

            mZones = new Dictionary<string, Zone>();
            mScenarioFile = new RARCFilesystem(mFilesystem.OpenFile($"/StageData/{name}/{name}Scenario.arc"));
            
            BCSV zonesBCSV = new BCSV(mScenarioFile.OpenFile("/root/ZoneList.bcsv"));

            foreach(BCSV.Entry e in zonesBCSV.mEntries)
            {
                string n = e.Get<string>("ZoneName");
                mZones.Add(n, new Zone(this, n));
            }

            zonesBCSV.Close();

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
            foreach(KeyValuePair<string, Zone> z in mZones)
            {
                z.Value.Save();
            }

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
        int mScenarioNo;

        public string mName;
        private Dictionary<string, Zone> mZones;
        public string mGalaxyName;
        public string mCurScenarioName;
    }
}
