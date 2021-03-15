using GL_EditorFramework.EditorDrawables;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;

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
                    FlagNameTable tbl = new FlagNameTable();
                    tbl.FlagName = entry.Get<string>("FlagName");
                    mFlagTable.Add(tbl);
                }
            }

            mShadowParams = new List<ShadowParam>();

            if (mFilesystem.DoesFileExist("/stage/csv/ShadowControllerParam.bcsv"))
            {
                BCSV b = new BCSV(mFilesystem.OpenFile("/stage/csv/ShadowControllerParam.bcsv"));

                foreach(BCSV.Entry entry in b.mEntries)
                {
                    ShadowParam p = new ShadowParam();
                    p.FarClip = entry.Get<int>("FarClip");
                    p.FarClipDistance = entry.Get<float>("FarClipDistance");
                    mShadowParams.Add(p);
                }
            }

            mWaterParams = new List<WaterCameraParam>();

            if (mFilesystem.DoesFileExist("/stage/csv/WaterCameraFilterParam.bcsv"))
            {
                BCSV b = new BCSV(mFilesystem.OpenFile("/stage/csv/WaterCameraFilterParam.bcsv"));

                foreach (BCSV.Entry entry in b.mEntries)
                {
                    WaterCameraParam param = new WaterCameraParam();
                    param.DepthLow = entry.Get<float>("DepthLow");
                    param.IndirectScale = entry.Get<float>("IndirectScale");
                    param.HighR = entry.Get<byte>("HighR");
                    param.HighG = entry.Get<byte>("HighG");
                    param.HighB = entry.Get<byte>("HighB");
                    param.HighA = entry.Get<byte>("HighA");
                    param.LowR = entry.Get<byte>("LowR");
                    param.LowG = entry.Get<byte>("LowG");
                    param.LowB = entry.Get<byte>("LowB");
                    param.LowA = entry.Get<byte>("LowA");
                    mWaterParams.Add(param);
                }
            }
        }

        public List<ShadowParam> mShadowParams;
        public List<WaterCameraParam> mWaterParams;
        public List<FlagNameTable> mFlagTable;

        public class ShadowParam : SingleObject
        {
            public ShadowParam() : base(Vector3.Zero) { }

            public override string ToString()
            {
                return "Shadow Parameter";
            }

            public int FarClip;
            public float FarClipDistance;
        }

        public class WaterCameraParam : SingleObject
        {
            public WaterCameraParam() : base(Vector3.Zero) { }

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

        public class FlagNameTable : SingleObject
        {
            public FlagNameTable() : base(Vector3.Zero) { }

            public override string ToString()
            {
                return $"{FlagName}";
            }

            public string FlagName;
        }

        RARCFilesystem mFilesystem;
    }
}
