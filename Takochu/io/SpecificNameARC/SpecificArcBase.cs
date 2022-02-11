using System;
using System.Linq;
using Takochu.io;
using Takochu.fmt;
using Takochu.util;
using System.Collections.Generic;

namespace Takochu.io.SpecificNameARC
{
    public class SpecificArcBase
    {
        public RARCFilesystem RARCFileStream { get; protected set; }

        protected static readonly Dictionary<BCSV_Name, string> BCSV_Path = new Dictionary<BCSV_Name, string>()
        {
            { BCSV_Name.ZoneList , "/root/ZoneList.bcsv"},
            { BCSV_Name.ScenarioData , "/root/ScenarioData.bcsv"}
        };

        public enum BCSV_Name
        {
            ZoneList,
            ScenarioData
        }

        protected BCSV BCSV_Open(BCSV_Name bcsvName)
        {
            var bcsvPath = PathGenerate_FromGameVer(BCSV_Path[bcsvName]);
            return new BCSV(RARCFileStream.OpenFile(bcsvPath));
        }

        protected string PathGenerate_FromGameVer(string path)
        {
            return GameUtil.IsSMG1() ? path.ToLower() : path;
        }
    }
}