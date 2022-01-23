using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;
using Takochu.rnd;
using Takochu.util;

namespace Takochu.smg.obj
{
    public class MapPartsObj : AbstractObj
    {
        public MapPartsObj(BCSV.Entry entry, Zone parentZone, string path) : base(entry)
        {
            mParentZone = parentZone;
            string[] content = path.Split('/');
            mDirectory = content[0];
            mLayer = content[1];
            mFile = content[2];

            mType = "MapPartsObj";

            mTruePosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mTrueRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));

            mPosition = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            mRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            mScale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));

            mID = Get<int>("l_id");
            mObjArgs = new int[4];

            for (int i = 0; i < 4; i++)
                mObjArgs[i] = Get<int>($"Obj_arg{i}");

            mFarClip = Get<int>("FarClip");
            mPressType = Get<int>("PressType");
            mShadowType = Get<int>("ShadowType");
            mMoveConditionType = Get<int>("MoveConditionType");
            mRotateSpeed = Get<int>("RotateSpeed");
            mRotateAngle = Get<int>("RotateAngle");
            mRotateAxis = Get<int>("RotateAxis");
            mRotateAccelType = Get<int>("RotateAccelType");
            mRotateStopTime = Get<int>("RotateStopTime");
            mRotateType = Get<int>("RotateType");
            mSignMotionType = Get<int>("SignMotionType");

            if (GameUtil.IsSMG2())
            {
                mSwitchAwake = Get<int>("SW_AWAKE");
                mSwitchParameter = Get<int>("SW_PARAM");
                mParamScale = Get<float>("ParamScale");
                mObjID = Get<short>("Obj_ID");
                mMapPartsID = Get<short>("MapParts_ID");
                mParentID = Get<short>("ParentId");
            }

            mCameraSetID = Get<int>("CameraSetId");

            mSwitchAppear = Get<int>("SW_APPEAR");
            mSwitchDead = Get<int>("SW_DEAD");
            mSwitchActivate = Get<int>("SW_A");
            mSwitchDeactivate = Get<int>("SW_B");

            mCastID = Get<int>("CastId");
            mViewGroupID = Get<int>("ViewGroupId");
            mShapeModelNo = Get<short>("ShapeModelNo");
            mPathID = Get<short>("CommonPath_ID");
            mClippingGroupID = Get<short>("ClippingGroupId");
            mGroupID = Get<short>("GroupId");
            mDemoGroupID = Get<short>("DemoGroupId");

            if (ModelCache.HasRenderer(mName))
                mRenderer = ModelCache.GetRenderer(mName);

            // initalize the renderer
            if (Program.sGame.DoesFileExist($"/ObjectData/{mName}.arc"))
            {
                RARCFilesystem rarc = new RARCFilesystem(Program.sGame.mFilesystem.OpenFile($"/ObjectData/{mName}.arc"));

                if (rarc.DoesFileExist($"/root/{mName}.bdl"))
                {
                    mRenderer = new BmdRenderer(new BMD(rarc.OpenFile($"/root/{mName}.bdl")));
                    ModelCache.AddRenderer(mName, (BmdRenderer)mRenderer);
                }
                else
                {
                    mRenderer = new ColorCubeRenderer(200f, new Vector4(1f, 1f, 1f, 1f), new Vector4(1f, 0f, 1f, 1f), true);
                }

                rarc.Close();
            }
            else
            {
                mRenderer = new ColorCubeRenderer(200f, new Vector4(1f, 1f, 1f, 1f), new Vector4(1f, 0f, 1f, 1f), true);
            }
        }

        public override void Render(RenderMode mode)
        {
            RenderInfo inf = new RenderInfo();
            inf.Mode = mode;

            if (!mRenderer.GottaRender(inf))
                return;

            GL.PushMatrix();
            {
                GL.Translate(mTruePosition);
                //"RotateZYX"の順番を変えない事
                //Do not change the order of "RotateZYX"
                GL.Rotate(mTrueRotation.Z, 0f, 0f, 1f);
                GL.Rotate(mTrueRotation.Y, 0f, 1f, 0f);
                GL.Rotate(mTrueRotation.X, 1f, 0f, 0f);
                GL.Scale(mScale.X, mScale.Y, mScale.Z);
            }
            mRenderer.Render(inf);
            GL.PopMatrix();
        }

        public override void Save()
        {
            mEntry.Set("FarClip", mFarClip);
            mEntry.Set("PressType", mPressType);
            mEntry.Set("ShadowType", mShadowType);
            mEntry.Set("MoveConditionType", mMoveConditionType);
            mEntry.Set("RotateSpeed", mRotateSpeed);
            mEntry.Set("RotateAngle", mRotateAngle);
            mEntry.Set("RotateAxis", mRotateAxis);
            mEntry.Set("RotateAccelType", mRotateAccelType);
            mEntry.Set("RotateStopTime", mRotateStopTime);
            mEntry.Set("RotateType", mRotateType);
            mEntry.Set("SignMotionType", mSignMotionType);

            mEntry.Set("name", mName);
            mEntry.Set("l_id", mID);

            for (int i = 0; i < 4; i++)
                mEntry.Set($"Obj_arg{i}", mObjArgs[i]);

            mEntry.Set("CameraSetId", mCameraSetID);
            mEntry.Set("ParamScale", mParamScale);
            mEntry.Set("SW_APPEAR", mSwitchAppear);
            mEntry.Set("SW_DEAD", mSwitchDead);
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

            mEntry.Set("CastId", mCastID);
            mEntry.Set("ViewGroupId", mViewGroupID);
            mEntry.Set("ShapeModelNo", mShapeModelNo);
            mEntry.Set("CommonPath_ID", mPathID);
            mEntry.Set("ClippingGroupId", mClippingGroupID);
            mEntry.Set("GroupId", mGroupID);
            mEntry.Set("DemoGroupId", mDemoGroupID);
            mEntry.Set("MapParts_ID", mMapPartsID);
            mEntry.Set("Obj_ID", mObjID);
        }

        public override string ToString()
        {
            return $"[{Get<int>("l_id")}] {ObjectDB.GetFriendlyObjNameFromObj(mName)} [{mLayer}]";
        }

        int mID;

        int mFarClip;
        int mPressType;
        int mShadowType;
        int mMoveConditionType;
        int mRotateSpeed;
        int mRotateAngle;
        int mRotateAxis;
        int mRotateAccelType;
        int mRotateStopTime;
        int mRotateType;
        int mSignMotionType;

        int mSwitchAppear;
        int mSwitchDead;
        int mSwitchActivate;
        int mSwitchDeactivate;
        int mSwitchAwake;
        int mSwitchParameter;
        float mParamScale;
        int mCameraSetID;

        int mCastID;
        int mViewGroupID;
        short mShapeModelNo;
        short mPathID;
        short mClippingGroupID;
        short mGroupID;
        short mDemoGroupID;
        short mMapPartsID;
        short mObjID;
        short mParentID;

    }
}
