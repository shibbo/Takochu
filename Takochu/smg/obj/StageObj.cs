using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;



namespace Takochu.smg.obj
{
    public class StageObj : AbstractObj
    {
        
        public StageObj(BCSV.Entry entry) : base(entry)
        {
            mEntry = entry;
            mName = mEntry.Get<string>("name");
            mID = mEntry.Get<int>("l_id");
            mPosition = new Vector3(mEntry.Get<float>("pos_x"), mEntry.Get<float>("pos_y"), mEntry.Get<float>("pos_z"));
            mRotation = new Vector3(mEntry.Get<float>("dir_x"), mEntry.Get<float>("dir_y"), mEntry.Get<float>("dir_z"));
        }

        public override void Save()
        {
            mEntry.Set("name", mName);
            mEntry.Set("l_id", mID);

            mEntry.Set("pos_x", mPosition.X);
            mEntry.Set("pos_y", mPosition.Y);
            mEntry.Set("pos_z", mPosition.Z);

            mEntry.Set("dir_x", mRotation.X);
            mEntry.Set("dir_y", mRotation.Y);
            mEntry.Set("dir_z", mRotation.Z);
        }

        public Vector3 Position
        {
            get
            {
                return mPosition;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return mRotation;
            }
        }

        public int mID;
    }
}
