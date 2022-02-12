using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;
using Takochu.smg;
using Takochu.fmt;
using Takochu.util;
using System.Windows.Forms;

namespace Takochu.io.SpecificNameARC
{
    public class ScenarioArcFile : SpecificArcBase,IARC_IO
    {
        public Dictionary<string, Zone> ZoneListBCSV { get; private set; }
        public Dictionary<string, BCSV.Entry> ZoneListBCSV_Entries { get; private set; }
        public Dictionary<int, ScenarioEntry> ScenarioDataBCSV { get; private set; }

        private readonly Galaxy _galaxy;


        public ScenarioArcFile(FilesystemBase fsb, Galaxy galaxy)
        {
            _galaxy = galaxy;
            var galaxyName = galaxy.mName;
            RARCFileStream = new RARCFilesystem(fsb.OpenFile($"/StageData/{galaxyName}/{galaxyName}Scenario.arc"));

            ZoneListBCSV = new Dictionary<string, Zone>();
            ZoneListBCSV_Entries = new Dictionary<string, BCSV.Entry>();
            ScenarioDataBCSV = new Dictionary<int, ScenarioEntry>();
        }

        public void ReadAllFiles()
        {
            ReadZoneListBCSV();
            ReadScenarioDataBCSV();
        }

        public void WriteAllFiles()
        {
            throw new NotImplementedException();
        }

        public void ScenarioDataSave(Dictionary<int, ScenarioEntry> scenarioDataBcsv) 
        {
            ScenarioDataBCSV = new Dictionary<int, ScenarioEntry>(scenarioDataBcsv);
        }

        private void ReadZoneListBCSV() 
        {
            using (BCSV zonesList = BCSV_Open(BCSV_Name.ZoneList))
            {
                var MissingPathArgumentsRemove = 0;
                foreach (BCSV.Entry zoneEntry in zonesList.mEntries)
                {
                    string name = zoneEntry.Get<string>("ZoneName");

                    if (name == "PoleUnizoZone")
                        continue;

                    ZoneListBCSV.Add(name, new Zone(_galaxy, name));
                    ZoneListBCSV_Entries.Add(name, zoneEntry);
                    MissingPathArgumentsRemove += Zone.MissingPathArgumentsRemove;
                }
                if (MissingPathArgumentsRemove > 0)
                    MessageBox.Show($"Takochu just added in missing path arguments that Whitehole was known to remove.\nRemove arguments count: {MissingPathArgumentsRemove}");
            }
        }

        private void ReadScenarioDataBCSV() 
        {
            using (BCSV scenarios = BCSV_Open(BCSV_Name.ScenarioData))
            {
                ScenarioDataBCSV = new Dictionary<int, ScenarioEntry>();

                foreach (BCSV.Entry scenarioEntry in scenarios.mEntries)
                {
                    ScenarioDataBCSV.Add(scenarioEntry.Get<int>("ScenarioNo"), new ScenarioEntry(scenarioEntry, ZoneListBCSV.Keys.ToList()));
                }
            }
        }
    }
}
