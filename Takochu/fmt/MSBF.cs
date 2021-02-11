using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;

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


        }

        public void Close()
        {
            mFile.Close();
        }

        public List<string> GetFlowNames()
        {
            List<string> ret = new List<string>();

            foreach(KeyValuePair<string, uint> p in mEntries.mTable)
            {
                ret.Add(p.Key);
            }

            return ret;
        }

        public uint GetStartID(string name)
        {
            return mEntries.mTable[name];
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
        public enum NodeType
        {
            NodeType_Message = 1,
            NodeType_Branch = 2,
            NodeType_Event = 3,
            NodeType_Entry = 4
        }

        public Node() {  }

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

        public int CalcSize()
        {
            int size = 0x18;

            return size;
        }

        public List<Node> mNodes;
        public List<ushort> mLabels;
    }

    class Entry
    {
        struct TableEntry
        {
            public int mPtr;
            public uint mIsValid;
        }

        public Entry(ref FileBase file)
        {
            mTable = new Dictionary<string, uint>();
            mEntries = new List<TableEntry>();

            int size = file.ReadInt32();
            file.Skip(0x8);
            int loc = file.Position();
            uint count = file.ReadUInt32();

            for (int i = 0; i < count; i++)
            {
                TableEntry e = new TableEntry();
                e.mIsValid = file.ReadUInt32();
                e.mPtr = file.ReadInt32();

                mEntries.Add(e);

                if (e.mIsValid == 1)
                {
                    string str = file.ReadStringAt(loc + e.mPtr);
                    uint val = file.ReadUInt32At(loc + e.mPtr + str.Length + 1);
                    mTable.Add(str, val);
                }
            }

            file.Seek(loc + size);

            while ((file.Position() % 0x10) != 0)
                file.Skip(0x1);
        }

        List<TableEntry> mEntries;
        public Dictionary<string, uint> mTable;
    };
}
