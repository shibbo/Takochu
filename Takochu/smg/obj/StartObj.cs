using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.ui.editor;

namespace Takochu.smg.obj
{
    public class StartObj : AbstractObj
    {
        public StartObj(BCSV.Entry entry, Zone parentZone, string path) : base(entry)
        {
            mParentZone = parentZone;
            string[] content = path.Split('/');
            mDirectory = content[0];
            mLayer = content[1];
            mFile = content[2];

            mType = "StartObj";

            mTruePosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mTrueRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));

            mPosition = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            mRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            mScale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));

            mMarioNo = Get<int>("MarioNo");
            mCameraID = Get<int>("Camera_id");

            mObjArgs = new int[1];
            mObjArgs[0] = Get<int>("Obj_arg0");
        }

        public override void Save()
        {
            mEntry.Set("MarioNo", mMarioNo);
            mEntry.Set("Obj_arg0", mObjArgs[0]);
            mEntry.Set("Camera_id", mCameraID);

            mEntry.Set("pos_x", mTruePosition.X);
            mEntry.Set("pos_y", mTruePosition.Y);
            mEntry.Set("pos_z", mTruePosition.Z);

            mEntry.Set("dir_x", mTrueRotation.X);
            mEntry.Set("dir_y", mTrueRotation.Y);
            mEntry.Set("dir_z", mTrueRotation.Z);

            mEntry.Set("scale_x", mScale.X);
            mEntry.Set("scale_y", mScale.Y);
            mEntry.Set("scale_z", mScale.Z);
        }

        int mMarioNo;
        int mCameraID;

        public override string ToString()
        {
            return $"[{mMarioNo}] {mName} [{mLayer}]";
        }
    }
}
