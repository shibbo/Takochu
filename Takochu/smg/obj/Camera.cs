using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.ui;
using Takochu.util;

namespace Takochu.smg.obj
{
    public class Camera : AbstractObj
    {
        public enum CameraType
        {
            Cube,
            Event,
            Group,
            Start,
            Other
        }

        public Dictionary<string, CameraType> cameraDict = new Dictionary<string, CameraType>()
        {
            { "c", CameraType.Cube },
            { "e", CameraType.Event },
            { "g", CameraType.Group },
            { "s", CameraType.Start },
            { "o", CameraType.Other }
        };

        public Camera(BCSV.Entry entry, Zone parent) : base(entry)
        {
            mEntry = entry;

            mName = mEntry.Get<string>("id");

            mParentZone = parent;

            mType = mEntry.Get<string>("camtype");

            mFields = new Dictionary<string, object>();

            // the first thing we load are the camera-type specific fields
            List<string> fields = CameraUtil.GetStrings(mType).ToList();

            foreach(string field in fields)
            {
                if (field == "none")
                    break;

                string type = CameraUtil.GetTypeOfField(field);

                if (!mEntry.ContainsKey(field))
                    continue;

                switch (type)
                {
                    case "float":
                        mFields[field] = mEntry.Get<float>(field);
                        break;
                    case "int":
                        mFields[field] = mEntry.Get<int>(field);
                        break;
                    case "string":
                        mFields[field] = mEntry.Get<string>(field);
                        break;
                }
            }

            Dictionary<string, string> allFields = CameraUtil.GetAll();

            foreach(string s in allFields.Keys)
            {
                string type = CameraUtil.GetTypeOfField(s);

                if (!mEntry.ContainsKey(s))
                    continue;

                switch (type)
                {
                    case "float":
                        mFields[s] = mEntry.Get<float>(s);
                        break;
                    case "int":
                        mFields[s] = mEntry.Get<int>(s);
                        break;
                    case "string":
                        mFields[s] = mEntry.Get<string>(s);
                        break;
                }
            }
        }

        public override void Save()
        {
            // load camera specific fields
            List<string> fields = CameraUtil.GetStrings(mType).ToList();

            foreach (string field in fields)
            {
                // skip if there are no fields here
                if (field == "none")
                    break;

                // write the rest
                mEntry.Set(field, mFields[field]);
            }

            Dictionary<string, string> allFields = CameraUtil.GetAll();

            foreach (string s in allFields.Keys)
            {
                // skip if it doesn't exist
                if (!mEntry.ContainsKey(s))
                    continue;

                // write the remaining fields
                mEntry.Set(s, mFields[s]);
            }

            mEntry.Set("id", mName);
            mEntry.Set("camtype", mType);
        }

        public override void Reload_mValues()
        {

        }

        public CameraType GetCameraType()
        {
            if (mName == "")
                return CameraType.Cube;

            string type = mName.Substring(0, 1);
            return cameraDict.ContainsKey(type) ? cameraDict[type] : CameraType.Cube;
        }

        /*public uint mVersion;
        public Vector3 mWorldOffset;
        public float mLookOffset;
        public float mVerticalLookOffset;
        public float mRoll;
        public float mFovy;
        public int mCamInt;
        public float mUpper;
        public float mLower;
        public int mGNDInt;
        public float mUPlay;
        public float mLPlay;
        public int mPushDelay;
        public int mPushDelayLow;
        public int mUDown;
        public int mPanUse;
        public Vector3 mPanAxis;

        public uint mNoReset;
        public uint mNoFovy;
        public uint mLOFserpOff;
        public uint mAntiBlurOff;
        public uint mCollisionOff;
        public uint mSubjectiveOff;

        public float mDist;
        public Vector3 mAxis;
        public Vector3 mWorldPoint;
        public Vector3 mUp;

        public float angleA;
        public float angleB;
        public uint mNum1;
        public uint mNum2;

        public string mString;*/

        Dictionary<string, object> mFields;
    }
}
