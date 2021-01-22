using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;

namespace Takochu.fmt
{
    class MSBF
    {
        public MSBF(FileBase file)
        {
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

        Flow mFlow;
        Entry mEntries;
    }

    class Node
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
        }

        public ushort mMessageID;
    }
    
    class BranchNode : Node
    {
        public BranchNode(ref FileBase file) : base()
        {
            mNodeType = NodeType.NodeType_Branch;
            file.Skip(0x2);
            mUnk2 = file.ReadUInt16();
            mCondition = file.ReadUInt16();
            mYesNoBoxChoices = file.ReadUInt16();
            mLabelsToUse = file.ReadUInt16();
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
            file.Skip(0x4);
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
        Dictionary<string, uint> mTable;
    };
}
