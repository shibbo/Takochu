using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;

namespace Takochu.smg.obj
{
    public class StageObj
    {
        public StageObj(BCSV.Entry entry)
        {
            mEntry = entry;
            mName = mEntry.Get<string>("name");
            mPosition = new Vector3(mEntry.Get<float>("pos_x"), mEntry.Get<float>("pos_y"), mEntry.Get<float>("pos_z"));
            mRotation = new Vector3(mEntry.Get<float>("dir_x"), mEntry.Get<float>("dir_y"), mEntry.Get<float>("dir_z"));
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

        public string mName;
        public BCSV.Entry mEntry;
        public Vector3 mPosition, mRotation;
    }
}
