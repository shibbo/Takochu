using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.calc;
using Takochu.fmt;

namespace Takochu.smg.obj
{
    public class DebugMoveObj : AbstractObj
    {
        public DebugMoveObj(BCSV.Entry entry, Zone parentZone, string path) : base(entry)
        {
            mParentZone = parentZone;
            string[] content = path.Split('/');
            mDirectory = content[0];
            mLayer = content[1];
            mFile = content[2];
            mParentZone = parentZone;

            mType = "DebugMoveObj";

            mTruePosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mTrueRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));

            mPosition = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            mRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            mScale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));
        }

        public override void Reload_mValues()
        {
            {
                mName = mEntry.Get("name").ToString();
                mID = ObjectTypeChange.ToInt32(mEntry.Get("l_id"));
            }

            mTruePosition =
                new Vector3(
                    ObjectTypeChange.ToFloat(mEntry.Get("pos_x")),
                    ObjectTypeChange.ToFloat(mEntry.Get("pos_y")),
                    ObjectTypeChange.ToFloat(mEntry.Get("pos_z"))
                );
            mTrueRotation =
                new Vector3(
                    ObjectTypeChange.ToFloat(mEntry.Get("dir_x")),
                    ObjectTypeChange.ToFloat(mEntry.Get("dir_y")),
                    ObjectTypeChange.ToFloat(mEntry.Get("dir_z"))
                );
            mScale =
                new Vector3(
                    ObjectTypeChange.ToFloat(mEntry.Get("scale_x")),
                    ObjectTypeChange.ToFloat(mEntry.Get("scale_y")),
                    ObjectTypeChange.ToFloat(mEntry.Get("scale_z"))
                );
            mPosition = new Vector3(mTruePosition) / 100;
            mRotation = new Vector3(mTrueRotation) / 100;
        }

        public override void Save()
        {
            mEntry.Set("l_id", mID);
            mEntry.Set("name", mName);

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

        int mID;

        public override string ToString()
        {
            return $"[{Get<int>("l_id")}] {ObjectDB.GetFriendlyObjNameFromObj(mName)} [{mLayer}]";
        }
    }
}
