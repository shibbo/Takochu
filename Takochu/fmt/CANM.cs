using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.io;

namespace Takochu.fmt
{
    public class CANM
    {
        private static readonly string[] sNames = new string[0x8]{ "pos_x", "pos_y", "pos_z", "dir_x", "dir_y", "dir_z", "twist", "fovy" };

        public CANM(FileBase file)
        {
            mFile = file;

            if (file.ReadString(0x8) != "ANDOCKAN")
            {
                throw new Exception("CANM::CANM -- Invalid CANM file!");
            }

            _08 = mFile.ReadInt32();
            _0C = mFile.ReadInt32();
            _10 = mFile.ReadInt32();
            _14 = mFile.ReadInt32();
            _18 = mFile.ReadInt32();
            mTableOffs = mFile.ReadInt32();

            mKeyFrameIndicies = new Dictionary<string, KeyFrameIndexer>();

            foreach (string s in sNames)
            {
                KeyFrameIndexer idxer = new KeyFrameIndexer
                {
                    mKeyFrameCount = mFile.ReadInt32(),
                    mStartIdx = mFile.ReadInt32(),
                };

                mFile.Skip(0x4);

                mKeyFrameIndicies.Add(s, idxer);

                Console.WriteLine($"Count: {idxer.mKeyFrameCount} -- Start Index: {idxer.mStartIdx}");
            }

            int tableCount = mFile.ReadInt32() / 4;

            mTable = new float[tableCount];

            for (int i = 0; i < tableCount; i++)
                mTable[i] = mFile.ReadSingle();

            mKeyFrames = new Dictionary<string, KeyFrames>();

            foreach(KeyValuePair<string, KeyFrameIndexer> idx in mKeyFrameIndicies)
            {
                KeyFrameIndexer thing = idx.Value;

                float[] input;

                if (thing.mKeyFrameCount == 1)
                    input = new float[]{ 0, mTable[thing.mStartIdx], 0 };
                else
                {
                    input = new float[thing.mKeyFrameCount * 0x3];

                    for (int i = 0; i < thing.mKeyFrameCount * 0x3; i++)
                        input[i] = mTable[thing.mStartIdx + i];
                }

                mKeyFrames.Add(idx.Key, new KeyFrames(input));
            }
        }

        public void Save()
        {
            mFile.SetBuffer(new byte[0]);
            mFile.Seek(0);
            mFile.WriteString("ANDOCKAN");
            mFile.Write(1);
            mFile.Write(0);
            mFile.Write(1);
            mFile.Write(4);
            mFile.Write(0x1E0);
            mFile.Write(0x60);

            // so for saving, we first have to write the count, and the starting index
            int curIdx = 0;

            foreach (KeyValuePair<string, KeyFrames> frame in mKeyFrames)
            {
                if (frame.Value.GetCount() == 3)
                {
                    mFile.Write(1);
                    mFile.Write(curIdx);
                    mFile.Write(0);
                    curIdx += 1;
                }
                else
                {
                    int count = frame.Value.GetData().Length / 3;
                    mFile.Write(count);
                    mFile.Write(curIdx);
                    mFile.Write(0);
                    curIdx += frame.Value.GetData().Length;
                }
            }

            int pos = mFile.Position();
            mFile.Write(0);

            // now we re-iterate through the frames and write the floating data
            foreach (KeyValuePair<string, KeyFrames> frame in mKeyFrames)
            {
                if (frame.Value.GetCount() == 3)
                {
                    // if the count is 1, then we write a single floating value
                    mFile.Write(frame.Value.GetData()[1]);
                }
                else
                {
                    for (int i = 0; i < frame.Value.GetData().Length; i++)
                        mFile.Write(frame.Value.GetData()[i]);
                }
            }

            int dist = mFile.Position() - pos;
            int curPos = mFile.Position();
            mFile.Seek(pos);
            mFile.Write(dist - 0x4);
            mFile.Seek(curPos);

            mFile.Write(-1);
            // keyframes (0xC is each size), + 0x4 at the bottom (-1), 0x20 for the header, 0x4 for the table size,
            // then the floating table
            mFile.SetLength(mKeyFrames.Count * 0xC + 0x4 + 0x20 + 0x4 + dist - 0x4);
            System.IO.File.WriteAllBytes("butts.canm", mFile.GetBuffer());
        }

        public KeyFrames GetKeyFrames(string name)
        {
            return mKeyFrames[name];
        }

        public struct KeyFrameIndexer
        {
            public int mKeyFrameCount;
            public int mStartIdx;
        }

        public FileBase mFile;
        private Dictionary<string, KeyFrameIndexer> mKeyFrameIndicies;
        private float[] mTable;
        public Dictionary<string, KeyFrames> mKeyFrames;

        public int _08;
        public int _0C;
        public int _10;
        public int _14;
        public int _18;
        public int mTableOffs;
    }

    public class KeyFrames
    {
        public KeyFrames(float[] values)
        {
            mValues = values;
        }

        public void AddKeyframe()
        {
            List<float> data = mValues.ToList();
            // we load our floating values with temporary ones, we replace these when saving
            data.AddRange(new float[] { 0.0f, 0.0f, 0.0f });
            mValues = data.ToArray();
        }

        public int GetCount()
        {
            return mValues.Length;
        }

        public float[] GetData()
        {
            return mValues;
        }

        float[] mValues;
    }

}