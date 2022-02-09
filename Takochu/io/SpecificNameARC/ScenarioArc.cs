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
    public class ScenarioArc : SpecificArcBase,IARC_IO
    {
        public Dictionary<string, Zone> mZones { get; private set; }
        public Dictionary<string, BCSV.Entry> mZoneEntries { get; private set; }
        public Dictionary<int, Scenario> mScenarios { get; private set; }

        private readonly Galaxy _galaxy;


        public ScenarioArc(FilesystemBase fsb, Galaxy galaxy)
        {
            _galaxy = galaxy;
            var galaxyName = galaxy.mName;
            InFiles = new RARCFilesystem(fsb.OpenFile($"/StageData/{galaxyName}/{galaxyName}Scenario.arc"));

            mZones = new Dictionary<string, Zone>();
            mZoneEntries = new Dictionary<string, BCSV.Entry>();
            mScenarios = new Dictionary<int, Scenario>();
        }

        public void ReadAllFiles()
        {
            ReadZoneListBCSV();
            ReadScenarioListBCSV();
        }

        public void WriteAllFiles()
        {
            throw new NotImplementedException();
        }

        private void ReadZoneListBCSV() 
        {
            BCSV zonesList = BCSV_Open(BCSV_Name.ZoneList);
            {
                var MissingPathArgumentsRemove = 0;
                foreach (BCSV.Entry zoneEntry in zonesList.mEntries)
                {
                    string name = zoneEntry.Get<string>("ZoneName");

                    if (name == "PoleUnizoZone")
                        continue;

                    mZones.Add(name, new Zone(_galaxy, name));
                    mZoneEntries.Add(name, zoneEntry);
                    MissingPathArgumentsRemove += Zone.MissingPathArgumentsRemove;
                }
                if (MissingPathArgumentsRemove > 0)
                    MessageBox.Show($"Takochu just added in missing path arguments that Whitehole was known to remove.\nRemove arguments count: {MissingPathArgumentsRemove}");
            }
            zonesList.Close();
        }

        private void ReadScenarioListBCSV() 
        {
            BCSV scenarios = BCSV_Open(BCSV_Name.ScenarioList);
            {
                mScenarios = new Dictionary<int, Scenario>();

                foreach (BCSV.Entry scenarioEntry in scenarios.mEntries)
                {
                    mScenarios.Add(scenarioEntry.Get<int>("ScenarioNo"), new Scenario(scenarioEntry, mZones.Keys.ToList()));
                }
            }
            scenarios.Close();
        }
    }
}
