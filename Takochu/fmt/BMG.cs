using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;
using Takochu.smg.msg;

namespace Takochu.fmt
{
    class BMG
    {
        public BMG(FileBase file)
        {
            mFile = file;

            if (mFile.ReadString(0x8) != "MESGbmg1")
                throw new Exception("BMG::BMG() -- Invalid BMG file!");

            file.Skip(0x18);

            while (file.Position() != file.GetLength())
            {
                string magic = file.ReadString(4);

                switch (magic)
                {
                    case "INF1":
                        mInfo = new INF1(ref file);
                        break;
                }
            }
        }

        public string GetStringAtIdx(int idx)
        {
            INF1.INF1Entry entry = mInfo.mEntries[idx];

            string ret = "";
            entry.mMessage.ForEach(m => ret += m.ToString());

            return ret;
        }

        public List<MessageBase> GetMessageAtIdx(int idx)
        {
            return mInfo.mEntries[idx].mMessage;
        }

        FileBase mFile;

        INF1 mInfo;
    }

    class INF1
    {
        public INF1(ref FileBase file)
        {
            int size = file.ReadInt32();
            int datOffset = (size - 4 + (file.Position() - 8)) + 0xC;

            ushort count = file.ReadUInt16();
            file.Skip(0x6);

            mEntries = new List<INF1Entry>();

            for (int i = 0; i < count; i++)
            {
                INF1Entry e = new INF1Entry
                {
                    Unknown = file.ReadInt32(),
                    _4 = file.ReadUInt16(),
                    _6 = file.ReadByte(),
                    _7 = file.ReadByte(),
                    _8 = file.ReadByte(),
                    _9 = file.ReadByte(),
                    _A = file.ReadByte(),
                    _B = file.ReadByte()
                };

                int pos = file.Position();

                file.Seek(e.Unknown + datOffset);

                e.mMessage = new List<MessageBase>();

                short cur = -1;

                while (cur != 0)
                {
                    cur = file.ReadInt16();

                    if (cur == 0)
                    {
                        break;
                    }

                    switch (cur)
                    {
                        // new command
                        case 0x1A:
                            byte dataSize = file.ReadByte();
                            byte opcode = file.ReadByte();

                            switch (opcode)
                            {
                                case 0xFF:
                                    e.mMessage.Add(new SystemGroup(ref file));
                                    break;

                                case 1:
                                    e.mMessage.Add(new DisplayGroup(ref file));
                                    break;

                                case 2:
                                    e.mMessage.Add(new SoundGroup(ref file));
                                    break;

                                case 3:
                                    e.mMessage.Add(new PictureGroup(ref file));
                                    break;

                                case 4:
                                    e.mMessage.Add(new FontSizeGroup(ref file));
                                    break;

                                case 5:
                                    e.mMessage.Add(new LocalizeGroup(ref file));
                                    break;

                                case 6:
                                    e.mMessage.Add(new NumberGroup(ref file, dataSize - 0x4));
                                    break;

                                case 7:
                                    e.mMessage.Add(new StringGroup(ref file, dataSize - 0x4));
                                    break;

                                case 9:
                                    e.mMessage.Add(new RaceTimeGroup(ref file));
                                    break;

                                default:
                                    Console.WriteLine($"Unsupported opcode {opcode}");
                                    break;
                            }
                            break;
                        default:
                            // normal char
                            e.mMessage.Add(new Character(cur));
                            break;
                    }
                }

                mEntries.Add(e);

                file.Seek(pos);
            }

            while (file.Position() % 0x20 != 0)
                file.Skip(0x1);
        }

        public List<INF1Entry> mEntries;

        public struct INF1Entry
        {
            public int Unknown;
            public ushort _4;
            public byte _6;
            public byte _7;
            public byte _8;
            public byte _9;
            public byte _A;
            public byte _B;

            public List<MessageBase> mMessage;
        }
    }
}
