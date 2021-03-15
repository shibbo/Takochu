using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;
using static Takochu.fmt.Entry;

namespace Takochu.fmt
{
    public class MSBF
    {
        public MSBF(FileBase file)
        {
            mFile = file;
            if (file.ReadString(0x8) != "MsgFlwBn")
            {
                throw new Exception("MSBF::MSBF() -- Not a valid MSBF file!");
            }

            file.Skip(0x18);

            while(file.Position() != file.GetLength())
            {
                string magic = file.ReadString(4);

                switch (magic)
                {
                    case "FLW2":
                        mFlow = new Flow(ref file);
                        break;
                    case "FEN1":
                        mEntries = new Entry(ref file);
                        break;
                    case "REF1":
                        throw new Exception("MSBF::MSBF() -- REF1 section found, but this is not supported.");
                }
            }
        }

        public void Save()
        {
            mFile.WriteString("MsgFlwbn");
            mFile.Write((ushort)0xFEFF);
            mFile.Write(0x3);
            mFile.Write((short)0x2);
            mFile.Write((short)0);

            int size = 0x20 + mFlow.CalcSize() + mEntries.CalcSize();
            mFile.Write(size);
            mFile.WritePadding(0, 0xA);

            mFlow.Save(ref mFile);
            mEntries.Save(ref mFile);
        }

        public void Close()
        {
            mFile.Close();
        }

        public List<string> GetFlowNames()
        {
            List<string> ret = new List<string>();

            foreach (TableEntry e in mEntries.mEntries)
            {
                if (e.Count > 0)
                {
                    e.Pairs.ForEach(p => ret.Add(p.Label));
                }
            }

            return ret;
        }

        public uint GetStartID(string name)
        {
            foreach (TableEntry e in mEntries.mEntries)
            {
                if (e.Count > 0)
                {
                    foreach (TablePair p in e.Pairs)
                    {
                        if (p.Label == name)
                            return p.FlowOffset;
                    }
                }
            }

            return 0xFFFFFFFF;
        }

        public int NodeCount()
        {
            return mFlow.mNodes.Count;
        }

        public Node GetNode(int id)
        {
            return mFlow.mNodes[id];
        }

        public Node GetNextNode(int id)
        {
            if (mFlow.mNodes.Count == id + 1)
                return null;

            return mFlow.mNodes[id + 1];
        }

        public void GetJumpLabels(int startID, out ushort firstID, out ushort secondID)
        {
            firstID = mFlow.mLabels[startID];
            secondID = mFlow.mLabels[startID + 1];
        }

        Flow mFlow;
        Entry mEntries;
        FileBase mFile;
    }

    public class Node
    {
        public enum NodeType : ushort
        {
            NodeType_Message = 1,
            NodeType_Branch = 2,
            NodeType_Event = 3,
            NodeType_Entry = 4
        }

        public Node() {  }

        public virtual void Save(ref FileBase file) { }

        public NodeType mNodeType;
        public ushort mNextNode;
    }

    class MessageNode : Node
    {
        public MessageNode(ref FileBase file) : base()
        {
            mNodeType = NodeType.NodeType_Message;
            // skip 0x4 bytes because they are unused
            file.Skip(0x4);
            mMessageID = file.ReadUInt16();
            mNextNode = file.ReadUInt16();
            file.Skip(0x2);
        }

        public override void Save(ref FileBase file)
        {
            file.Write((short)0x1);
            file.WritePadding(0, 0x4);
            file.Write(mMessageID);
            file.Write(mNextNode);
            file.WritePadding(0, 0x2);
        }

        public override string ToString()
        {
            return "Message Node";
        }

        public ushort mMessageID;
    }
    
    class BranchNode : Node
    {
        public BranchNode(ref FileBase file) : base()
        {
            mNodeType = NodeType.NodeType_Branch;
            file.Skip(0x2); // Unk1
            mUnk2 = file.ReadUInt16(); // Unk2
            mCondition = file.ReadUInt16(); // Unk3
            mYesNoBoxChoices = file.ReadUInt16(); // Unk4
            mLabelsToUse = file.ReadUInt16(); // Unk5
        }

        public override void Save(ref FileBase file)
        {
            file.Write((short)0x2);
            file.WritePadding(0, 0x2);
            file.Write(mUnk2);
            file.Write(mCondition);
            file.Write(mYesNoBoxChoices);
            file.Write(mLabelsToUse);
        }

        public override string ToString()
        {
            return "Branch Node";
        }

        public ushort mUnk2;
        public ushort mCondition;
        public ushort mLabelsToUse;
        public ushort mYesNoBoxChoices;
    }

    class EventNode : Node
    {
        public EventNode(ref FileBase file) : base()
        {
            mNodeType = NodeType.NodeType_Event;
            file.Skip(0x2);
            mEvent = file.ReadUInt16();
            mJumpFlow = file.ReadUInt16();
            file.Skip(0x2);
            mFlowID = file.ReadUInt16();
        }

        public override void Save(ref FileBase file)
        {
            file.Write((short)0x3);
            file.WritePadding(0, 0x2);
            file.Write(mEvent);
            file.Write(mJumpFlow);
            file.WritePadding(0, 0x2);
            file.Write(mFlowID);
        }

        public override string ToString()
        {
            return "Event Node";
        }

        public ushort mEvent;
        public ushort mJumpFlow;
        public ushort mFlowID;
    }
    
    class EntryNode : Node
    {
        public EntryNode(ref FileBase file) : base()
        {
            mNodeType = NodeType.NodeType_Entry;
            file.Skip(0x2);
            mNextNode = file.ReadUInt16();
            file.Skip(0x6);
        }

        public override void Save(ref FileBase file)
        {
            file.Write((short)0x4);
            file.WritePadding(0, 0x2);
            file.Write(mNextNode);
            file.WritePadding(0, 0x6);
        }

        public override string ToString()
        {
            return "Entry Node";
        }
    }

    class Flow
    {
        public Flow(ref FileBase file)
        {
            mNodes = new List<Node>();

            file.Skip(0xC);
            ushort flowCount = file.ReadUInt16();
            ushort labelCount = file.ReadUInt16();
            file.Skip(0x4);

            for (int i = 0; i < flowCount; i++)
            {
                Node.NodeType type = (Node.NodeType)file.ReadUInt16();

                switch (type)
                {
                    case Node.NodeType.NodeType_Message:
                        mNodes.Add(new MessageNode(ref file));
                        break;
                    case Node.NodeType.NodeType_Entry:
                        mNodes.Add(new EntryNode(ref file));
                        break;
                    case Node.NodeType.NodeType_Event:
                        mNodes.Add(new EventNode(ref file));
                        break;
                    case Node.NodeType.NodeType_Branch:
                        mNodes.Add(new BranchNode(ref file));
                        break;
                    default:
                        Console.WriteLine($"Unsupported type: {(int)type}");
                        break;
                }
            }

            mLabels = new List<ushort>();

            for (int i = 0; i < labelCount; i++)
            {
                mLabels.Add(file.ReadUInt16());
            }

            while (file.Position() % 0x10 != 0)
                file.Skip(0x1);
        }

        public void Save(ref FileBase file)
        {
            file.WriteString("FLW2");
            file.Write(CalcSizeNoPadding());
            file.WritePadding(0, 0x8);
            file.Write((short)mNodes.Count);
            file.Write((short)mLabels.Count);

            foreach (Node n in mNodes)
                n.Save(ref file);

            foreach (ushort label in mLabels)
                file.Write(label);

            while (file.Position() % 0x10 != 0)
                file.Write((byte)0xAB);
        }

        public int CalcSize()
        {
            int size = 0x18;
            size += (mNodes.Count * 0xC);
            size += (mLabels.Count * 0x2);

            while (size % 0x10 != 0)
                size++;

            return size;
        }

        public int CalcSizeNoPadding()
        {
            int size = 0x8;
            size += (mNodes.Count * 0xC);
            size += (mLabels.Count * 0x2);
            return size;
        }

        public List<Node> mNodes;
        public List<ushort> mLabels;
    }

    public class Entry
    {
        public struct TableEntry
        {
            public uint Count;
            public List<TablePair> Pairs;
        }

        public struct TablePair
        {
            public string Label;
            public uint FlowOffset;
        }

        public Entry(ref FileBase file)
        {
            //mTable = new Dictionary<string, uint>();
            mEntries = new List<TableEntry>();

            int size = file.ReadInt32();
            file.Skip(0x8);
            int loc = file.Position();
            uint count = file.ReadUInt32();

            for (int i = 0; i < count; i++)
            {
                TableEntry e = new TableEntry();
                e.Count = file.ReadUInt32();

                int offs = file.ReadInt32();

                e.Pairs = new List<TablePair>();

                int pos = file.Position();
                file.Seek(loc + offs);

                for (int j = 0; j < e.Count; j++)
                {
                    TablePair p = new TablePair();
                    p.Label = file.ReadStringLenPrefix();
                    p.FlowOffset = file.ReadUInt32();

                    e.Pairs.Add(p);
                }

                file.Seek(pos);

                /*if (e.mIsValid == 1)
                {
                    string str = file.ReadStringAt(loc + e.mPtr);
                    uint val = file.ReadUInt32At(loc + e.mPtr + str.Length + 1);
                    mTable.Add(str, val);
                }*/

                mEntries.Add(e);
            }

            file.Seek(loc + size);

            while ((file.Position() % 0x10) != 0)
                file.Skip(0x1);
        }

        public void Save(ref FileBase file)
        {
            file.WriteString("FEN1");
            file.Write(CalcSizeNoPadding());
            file.WritePadding(0, 0x8);
            file.Write(mEntries.Count);
            file.WritePadding(0, 0x4);

            int dataOffset = (mEntries.Count * 0x8) + 4;

            List<TablePair> pairs = new List<TablePair>();

            foreach (TableEntry e in mEntries)
            {
                // if the pair count is greater than zero, we can write a new count and up our current data offset
                if (e.Pairs.Count > 0)
                {
                    file.Write(e.Pairs.Count);
                    file.Write(dataOffset);

                    pairs.AddRange(e.Pairs);

                    // increment our data offset by the length of each string in the pair
                    for (int i = 0; i < e.Pairs.Count; i++)
                    {
                        dataOffset += e.Pairs[i].Label.Length + 5;
                    }
                }
                else
                {
                    // otherwise we just write another entry with no pairs and the same data offset
                    file.Write(0);
                    file.Write(dataOffset);
                }
            }

            // now we write the actual data
            foreach (TablePair p in pairs)
            {
                file.Write((byte)p.Label.Length);
                file.WriteString(p.Label);
                file.Write(p.FlowOffset);
            }

            while (file.Position() % 0x10 != 0)
                file.Write((byte)0xAB);
        }

        public int CalcSize()
        {
            int size = 0x14;
            size += (mEntries.Count * 0x8);

            // now we go through our pairs and calculate more sizes based on the strings
            foreach (TableEntry e in mEntries)
            {
                foreach (TablePair p in e.Pairs)
                {
                    // value
                    size += 4;
                    // string length itself (+1 for string size)
                    size += p.Label.Length + 1;
                }
            }

            while ((size % 0x10) != 0)
                size++;

            return size;
        }

        public int CalcSizeNoPadding()
        {
            int size = 4;
            size += (mEntries.Count * 0x8);

            // now we go through our pairs and calculate more sizes based on the strings
            foreach (TableEntry e in mEntries)
            {
                foreach (TablePair p in e.Pairs)
                {
                    // value
                    size += 4;
                    // string length itself (+1 for string size)
                    size += p.Label.Length + 1;
                }
            }

            return size;
        }

        public List<TableEntry> mEntries;
        //public Dictionary<string, uint> mTable;
    };
}
