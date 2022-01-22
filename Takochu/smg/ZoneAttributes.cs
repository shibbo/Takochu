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
    public class ZoneAttributes
    {
        public ZoneAttributes(FilesystemBase filesystem)
        {
            mFilesystem = filesystem as RARCFilesystem;

            mFlagTable = new List<FlagNameTable>();

            if (mFilesystem.DoesFileExist("/stage/csv/InStageFlagNameTable.bcsv"))
            {
                BCSV b = new BCSV(mFilesystem.OpenFile("/stage/csv/InStageFlagNameTable.bcsv"));

                foreach(BCSV.Entry entry in b.mEntries)
                {
                    mFlagTable.Add(new FlagNameTable(entry));
                }
            }

            mShadowParams = new List<ShadowParam>();

            if (mFilesystem.DoesFileExist("/stage/csv/ShadowControllerParam.bcsv"))
            {
                BCSV b = new BCSV(mFilesystem.OpenFile("/stage/csv/ShadowControllerParam.bcsv"));

                foreach(BCSV.Entry entry in b.mEntries)
                {
                    mShadowParams.Add(new ShadowParam(entry));
                }
            }

            mWaterParams = new List<WaterCameraParam>();

            if (mFilesystem.DoesFileExist("/stage/csv/WaterCameraFilterParam.bcsv"))
            {
                BCSV b = new BCSV(mFilesystem.OpenFile("/stage/csv/WaterCameraFilterParam.bcsv"));

                foreach (BCSV.Entry entry in b.mEntries)
                {   
                    mWaterParams.Add(new WaterCameraParam(entry));
                }
            }
        }

        public List<ShadowParam> mShadowParams;
        public List<WaterCameraParam> mWaterParams;
        public List<FlagNameTable> mFlagTable;

        public class ShadowParam : AbstractObj
        {
            public ShadowParam(BCSV.Entry entry) : base(entry) 
            {
                FarClip = entry.Get<int>("FarClip");
                FarClipDistance = entry.Get<float>("FarClipDistance");
            }

            public override string ToString()
            {
                return "Shadow Parameter";
            }

            public int FarClip;
            public float FarClipDistance;
        }

        public class WaterCameraParam : AbstractObj
        {
            public WaterCameraParam(BCSV.Entry entry) : base(entry)
            {
                DepthLow = entry.Get<float>("DepthLow");
                IndirectScale = entry.Get<float>("IndirectScale");
                HighR = entry.Get<byte>("HighR");
                HighG = entry.Get<byte>("HighG");
                HighB = entry.Get<byte>("HighB");
                HighA = entry.Get<byte>("HighA");
                LowR = entry.Get<byte>("LowR");
                LowG = entry.Get<byte>("LowG");
                LowB = entry.Get<byte>("LowB");
                LowA = entry.Get<byte>("LowA");
            }

            public override string ToString()
            {
                return "Water Camera Parameter";
            }

            public float DepthLow;
            public float IndirectScale;

            public byte HighR;
            public byte HighG;
            public byte HighB;
            public byte HighA;

            public byte LowR;
            public byte LowG;
            public byte LowB;
            public byte LowA;
        }

        public class FlagNameTable : AbstractObj
        {
            public FlagNameTable(BCSV.Entry entry) : base(entry)
            {
                FlagName = entry.Get<string>("FlagName");
            }

            public override string ToString()
            {
                return $"{FlagName}";
            }

            public string FlagName;
        }

        RARCFilesystem mFilesystem;
    }
}
