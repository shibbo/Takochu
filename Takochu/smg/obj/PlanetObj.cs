using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;

namespace Takochu.smg.obj
{
    class PlanetObj : AbstractObj
    {
        public PlanetObj(BCSV.Entry entry, Zone parentZone, string path) : base(entry)
        {
            mParentZone = parentZone;
            string[] content = path.Split('/');
            mDirectory = content[0];
            mLayer = content[1];
            mFile = content[2];

            mType = "PlanetObj";

            mTruePosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mTrueRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));

            Position = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            Rotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            Scale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));

            mID = Get<int>("l_id");
            mObjArgs = new int[4];

            for (int i = 0; i < 4; i++)
                mObjArgs[i] = Get<int>($"Obj_arg{i}");

            mRange = Get<float>("Range");
            mDistant = Get<float>("Distant");
            mPriority = Get<int>("Priority");
            mInverse = Get<int>("Inverse");
            mPower = Get<string>("Power");
            mGravityType = Get<string>("Gravity_type");

            mSwitchAppear = Get<int>("SW_APPEAR");
            mSwitchDead = Get<int>("SW_DEAD");
            mSwitchActivate = Get<int>("SW_A");
            mSwitchDeactivate = Get<int>("SW_B");
            mSwitchAwake = Get<int>("SW_AWAKE");

            mFollowID = Get<int>("FollowId");
            mPathID = Get<short>("CommonPath_ID");
            mClippingGroupID = Get<short>("ClippingGroupId");
            mGroupID = Get<short>("GroupId");
            mDemoGroupID = Get<short>("DemoGroupId");
            mMapPartsID = Get<short>("MapParts_ID");
            mObjID = Get<short>("Obj_ID");
        }

        int mID;
        int[] mObjArgs;
        float mRange;
        float mDistant;
        int mPriority;
        int mInverse;
        string mPower;
        string mGravityType;
        int mSwitchAppear;
        int mSwitchDead;
        int mSwitchActivate;
        int mSwitchDeactivate;
        int mSwitchAwake;
        int mFollowID;
        short mPathID;
        short mClippingGroupID;
        short mGroupID;
        short mDemoGroupID;
        short mMapPartsID;
        short mObjID;

        public override string ToString()
        {
            return $"[{Get<int>("l_id")}] {mName} [{mLayer}] [{mParentZone.mZoneName}]";
        }
    }
}
