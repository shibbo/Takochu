using GL_EditorFramework;
using GL_EditorFramework.EditorDrawables;
using GL_EditorFramework.GL_Core;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;

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

            Position = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            Rotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            Scale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));

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

            mCameraSetID = Get<int>("CameraSetId");
            mParamScale = Get<float>("ParamScale");

            mSwitchAppear = Get<int>("SW_APPEAR");
            mSwitchDead = Get<int>("SW_DEAD");
            mSwitchActivate = Get<int>("SW_A");
            mSwitchDeactivate = Get<int>("SW_B");
            mSwitchAwake = Get<int>("SW_AWAKE");
            mSwitchParameter = Get<int>("SW_PARAM");

            mCastID = Get<int>("CastId");
            mViewGroupID = Get<int>("ViewGroupId");
            mShapeModelNo = Get<short>("ShapeModelNo");
            mPathID = Get<short>("CommonPath_ID");
            mClippingGroupID = Get<short>("ClippingGroupId");
            mGroupID = Get<short>("GroupId");
            mDemoGroupID = Get<short>("DemoGroupId");
            mMapPartsID = Get<short>("MapParts_ID");
            mObjID = Get<short>("Obj_ID");
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

            mEntry.Set("scale_x", Scale.X);
            mEntry.Set("scale_y", Scale.Y);
            mEntry.Set("scale_z", Scale.Z);

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
            return $"[{Get<int>("l_id")}] {mName} [{mLayer}] [{mParentZone.mZoneName}]";
        }

        int mID;
        int[] mObjArgs;

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

        public override uint Select(int index, GL_ControlBase control)
        {

            if (!Selected)
            {
                Selected = true;
                control.AttachPickingRedrawer();
            }
            return 0;
        }

        public override uint SelectDefault(GL_ControlBase control)
        {

            if (!Selected)
            {
                Selected = true;
                control.AttachPickingRedrawer();
            }
            return 0;
        }

        public override uint SelectAll(GL_ControlBase control)
        {

            if (!Selected)
            {
                Selected = true;
                control.AttachPickingRedrawer();
            }
            return 0;
        }

        public override uint Deselect(int index, GL_ControlBase control)
        {

            if (Selected)
            {
                Selected = false;
                control.DetachPickingRedrawer();
            }
            return 0;
        }

        public override uint DeselectAll(GL_ControlBase control)
        {

            if (Selected)
            {
                Selected = false;
                control.DetachPickingRedrawer();
            }
            return 0;
        }

        public override bool TrySetupObjectUIControl(EditorSceneBase scene, ObjectUIControl objectUIControl)
        {
            if (!Selected)
                return false;

            objectUIControl.AddObjectUIContainer(new PropertyProvider(this, scene), "Transform");
            objectUIControl.AddObjectUIContainer(new MapPartsObjUI(this, scene), "Example Controls");
            return true;
        }

        public class MapPartsObjUI : IObjectUIContainer
        {
            AbstractObj obj;
            EditorSceneBase scene;

            string text = "";

            public MapPartsObjUI(AbstractObj obj, EditorSceneBase scene)
            {
                this.obj = obj;
                this.scene = scene;
            }

            public void DoUI(IObjectUIControl control)
            {
                text = control.TextInput(text, "TextInput");
                control.PlainText("I hate people");
            }

            public void OnValueChangeStart()
            {

            }

            public void OnValueChanged()
            {
                scene.Refresh();
            }

            public void OnValueSet()
            {

            }

            public void UpdateProperties()
            {

            }
        }
    }
}
