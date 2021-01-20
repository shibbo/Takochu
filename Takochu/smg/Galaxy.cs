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
        }

        public void Close()
        {
            mFilesystem.Close();

            foreach(Zone zone in GetZones().Values)
            {
                zone.Close();
            }
        }

        public void SetScenario(uint no)
        {
            mScenarioNo = no;
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
            return mScenarioEntries.Find(e => e.Get<uint>("ScenarioNo") == mScenarioNo);
        }

        public uint GetMaskUsedInZoneOnCurrentScenario(string zoneName)
        {
            return GetScenarioInfoForCurrentScenario().Get<uint>(zoneName);
        }

        public List<string> GetGalaxyLayers(uint mask)
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
        uint mScenarioNo;

        public string mName;
        private Dictionary<string, Zone> mZones;
    }
}
