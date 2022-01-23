using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.util;

namespace Takochu.smg.obj
{
    class CameraObj : AbstractObj
    {
        public CameraObj(BCSV.Entry entry, Zone parentZone, string path) : base(entry)
        {
            mParentZone = parentZone;
            string[] content = path.Split('/');
            mDirectory = content[0];
            mLayer = content[1];
            mFile = content[2];

            mType = "CameraObj";

            mTruePosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mTrueRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));

            mPosition = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            mRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            mScale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));

            mID = Get<int>("l_id");
            mObjArgs = new int[4];

            for (int i = 0; i < 4; i++)
                mObjArgs[i] = Get<int>($"Obj_arg{i}");

            mValidity = Get<string>("Validity");
            mSwitchAppear = Get<int>("SW_APPEAR");
            mSwitchActivate = Get<int>("SW_A");
            mSwitchDeactivate = Get<int>("SW_B");

            if (GameUtil.IsSMG2())
            {
                mSwitchAwake = Get<int>("SW_AWAKE");
                mAreaShapeNo = Get<short>("AreaShapeNo");
            }
            else
            {
                mChildObjID = Get<short>("ChildObjId");
                mSwitchSleep = Get<int>("SW_SLEEP");
            }

            mFollowID = Get<int>("FollowId");
            mMapPartsID = Get<short>("MapParts_ID");
            mObjID = Get<short>("Obj_ID");
        }

        public override void Save()
        {
            mEntry.Set("name", mName);
            mEntry.Set("l_id", mID);

            for (int i = 0; i < 4; i++)
                mEntry.Set($"Obj_arg{i}", mObjArgs[i]);

            mEntry.Set("Validity", mValidity);
            mEntry.Set("SW_APPEAR", mSwitchAppear);
            mEntry.Set("SW_A", mSwitchActivate);
            mEntry.Set("SW_B", mSwitchDeactivate);
            mEntry.Set("SW_AWAKE", mSwitchAwake);

            mEntry.Set("pos_x", mTruePosition.X);
            mEntry.Set("pos_y", mTruePosition.Y);
            mEntry.Set("pos_z", mTruePosition.Z);

            mEntry.Set("dir_x", mTrueRotation.X);
            mEntry.Set("dir_y", mTrueRotation.Y);
            mEntry.Set("dir_z", mTrueRotation.Z);

            mEntry.Set("scale_x", mScale.X);
            mEntry.Set("scale_y", mScale.Y);
            mEntry.Set("scale_z", mScale.Z);

            mEntry.Set("AreaShapeNo", mAreaShapeNo);
            mEntry.Set("FollowId", mFollowID);
            mEntry.Set("MapParts_ID", mMapPartsID);
            mEntry.Set("Obj_ID", mObjID);
        }

        int mID;
        string mValidity;
        int mSwitchAppear;
        int mSwitchActivate;
        int mSwitchDeactivate;
        int mSwitchAwake;
        short mAreaShapeNo;
        int mFollowID;
        short mMapPartsID;
        short mObjID;

        int mSwitchSleep;
        short mChildObjID;

        public override string ToString()
        {
            return $"[{Get<int>("l_id")}] {ObjectDB.GetFriendlyObjNameFromObj(mName)} [{mLayer}]";
        }
    }
}
