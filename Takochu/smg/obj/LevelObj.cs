﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using OpenTK;
using System.Windows.Forms;
using GL_EditorFramework.GL_Core;
using GL_EditorFramework.Interfaces;
using GL_EditorFramework.EditorDrawables;
using GL_EditorFramework;
using System.Security.Policy;
using System.Security.Cryptography;
using Takochu.ui;
using static Takochu.smg.ObjectDB;
using Takochu.util;
using Takochu.ui.editor;

namespace Takochu.smg.obj
{
    public class LevelObj : AbstractObj
    {
        public LevelObj(BCSV.Entry entry, Zone parentZone, string path) : base(entry)
        {
            mParentZone = parentZone;
            string[] content = path.Split('/');
            mDirectory = content[0];
            mLayer = content[1];
            mFile = content[2];

            mType = "Obj";

            mTruePosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mTrueRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));

            Position = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            Rotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            mScale = Scale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));

            mID = Get<int>("l_id");
            mObjArgs = new int[8];

            for (int i = 0; i < 8; i++)
                mObjArgs[i] = Get<int>($"Obj_arg{i}");

            mCameraSetID = Get<int>("CameraSetId");
            mSwitchAppear = Get<int>("SW_APPEAR");
            mSwitchDead = Get<int>("SW_DEAD");
            mSwitchActivate = Get<int>("SW_A");
            mSwitchDeactivate = Get<int>("SW_B");
           
            mMessageID = Get<int>("MessageId");

            if (GameUtil.IsSMG2())
            {
                mSwitchAwake = Get<int>("SW_AWAKE");
                mSwitchParameter = Get<int>("SW_PARAM");
                mParamScale = Get<float>("ParamScale");
                mObjID = Get<short>("Obj_ID");
                mGeneratorID = Get<short>("GeneratorID");
            }

            mCastID = Get<int>("CastId");
            mViewGroupID = Get<int>("ViewGroupId");
            mShapeModelNo = Get<short>("ShapeModelNo");
            mPathID = Get<short>("CommonPath_ID");
            mClippingGroupID = Get<short>("ClippingGroupId");
            mGroupID = Get<short>("GroupId");
            mDemoGroupID = Get<short>("DemoGroupId");
            mMapPartsID = Get<short>("MapParts_ID");
            
        }

        public override void Save()
        {
            mEntry.Set("name", mName);
            mEntry.Set("l_id", mID);

            for (int i = 0; i < 8; i++)
                mEntry.Set($"Obj_arg{i}", mObjArgs[i]);

            mEntry.Set("CameraSetId", mCameraSetID);
            mEntry.Set("SW_APPEAR", mSwitchAppear);
            mEntry.Set("SW_DEAD", mSwitchDead);
            mEntry.Set("SW_A", mSwitchActivate);
            mEntry.Set("SW_B", mSwitchDeactivate);
            mEntry.Set("SW_AWAKE", mSwitchAwake);
            mEntry.Set("SW_PARAM", mSwitchParameter);
            mEntry.Set("MessageId", mMessageID);
            mEntry.Set("ParamScale", mParamScale);

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
            mEntry.Set("GeneratorID", mGeneratorID);
        }

        int mID;
        int mCameraSetID;
        int mSwitchAppear;
        int mSwitchDead;
        int mSwitchActivate;
        int mSwitchDeactivate;
        int mSwitchAwake;
        int mSwitchParameter;
        int mMessageID;
        float mParamScale;
        int mCastID;
        int mViewGroupID;
        short mShapeModelNo;
        short mPathID;
        short mClippingGroupID;
        short mGroupID;
        short mDemoGroupID;
        short mMapPartsID;
        short mObjID;
        short mGeneratorID;

        public override string ToString()
        {
            return $"[{Get<int>("l_id")}] {mName} [{mLayer}] [{mParentZone.mZoneName}]";
        }


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

            objectUIControl.AddObjectUIContainer(new GeneralUI(this, scene), "General");
            objectUIControl.AddObjectUIContainer(new PositionUI(this, scene), "Position");
            objectUIControl.AddObjectUIContainer(new ParameterUI(this, scene, 8), "Object Parameters");
            objectUIControl.AddObjectUIContainer(new LevelObjSwitchParameters(this, scene), "Switch Parameters");
            objectUIControl.AddObjectUIContainer(new LevelObjGeneralParameterUI(this, scene), "Misc Parameters");
            return true;
        }

        public class LevelObjSwitchParameters : IObjectUIContainer
        {
            LevelObj obj;
            EditorSceneBase scene;

            public LevelObjSwitchParameters(LevelObj obj, EditorSceneBase scene)
            {
                this.obj = obj;
                this.scene = scene;
            }

            public void DoUI(IObjectUIControl control)
            {
                obj.mSwitchAppear = (int)control.NumberInput(obj.mSwitchAppear, "Switch Appear");
                obj.mSwitchDead = (int)control.NumberInput(obj.mSwitchDead, "Switch Dead");
                obj.mSwitchActivate = (int)control.NumberInput(obj.mSwitchActivate, "Switch Activate");
                obj.mSwitchDeactivate = (int)control.NumberInput(obj.mSwitchDeactivate, "Switch Deactivate");
                obj.mSwitchAwake = (int)control.NumberInput(obj.mSwitchAwake, "Switch Awake");
                obj.mSwitchParameter = (int)control.NumberInput(obj.mSwitchParameter, "Switch Parameter");

                if (obj.mSwitchAppear != -1 || obj.mSwitchDead != -1 || obj.mSwitchActivate != -1 || obj.mSwitchDeactivate != -1 || obj.mSwitchAwake != -1 || obj.mSwitchParameter != -1)
                {
                    if (control.Button("View Switch Usage"))
                    {
                        if (obj.mSwitchAppear != -1)
                            obj.GetObjsWithSameField("SW_APPEAR", obj.mSwitchAppear).ForEach(o => scene.SelectedObjects.Add(o));
                        if (obj.mSwitchDead != -1)
                            obj.GetObjsWithSameField("SW_DEAD", obj.mSwitchDead).ForEach(o => scene.SelectedObjects.Add(o));
                        if (obj.mSwitchActivate != -1)
                            obj.GetObjsWithSameField("SW_A", obj.mSwitchActivate).ForEach(o => scene.SelectedObjects.Add(o));
                        if (obj.mSwitchDeactivate != -1)
                            obj.GetObjsWithSameField("SW_B", obj.mSwitchDeactivate).ForEach(o => scene.SelectedObjects.Add(o));
                        if (obj.mSwitchAwake != -1)
                            obj.GetObjsWithSameField("SW_AWAKE", obj.mSwitchAwake).ForEach(o => scene.SelectedObjects.Add(o));
                        if (obj.mSwitchParameter != -1)
                            obj.GetObjsWithSameField("SW_PARAM", obj.mSwitchParameter).ForEach(o => scene.SelectedObjects.Add(o));
                    }
                }
            }

            public void OnValueChangeStart() { }
            public void OnValueChanged()
            {
                scene.Refresh();
            }

            public void OnValueSet() { }
            public void UpdateProperties() { }
        }

        public class LevelObjGeneralParameterUI : IObjectUIContainer
        {
            LevelObj obj;
            EditorSceneBase scene;

            public LevelObjGeneralParameterUI(LevelObj obj, EditorSceneBase scene)
            {
                this.obj = obj;
                this.scene = scene;
            }

            public void DoUI(IObjectUIControl control)
            {
                obj.mID = (int)control.NumberInput(obj.mID, "ID");
                obj.mMessageID = (int)control.NumberInput(obj.mMessageID, "Message ID");
                obj.mCameraSetID = (int)control.NumberInput(obj.mCameraSetID, "Camera ID");

                obj.mParamScale = control.NumberInput(obj.mParamScale, "Parameter Scale");
                obj.mCastID = (int)control.NumberInput(obj.mCastID, "Cast ID");
                obj.mViewGroupID = (int)control.NumberInput(obj.mViewGroupID, "View Group ID");
                obj.mShapeModelNo = (short)control.NumberInput(obj.mShapeModelNo, "Shape Model Number");
                obj.mPathID = (short)control.NumberInput(obj.mPathID, "Path ID");
                obj.mClippingGroupID = (short)control.NumberInput(obj.mClippingGroupID, "Clipping Group ID");
                obj.mGroupID = (short)control.NumberInput(obj.mGroupID, "Group ID");
                obj.mDemoGroupID = (short)control.NumberInput(obj.mDemoGroupID, "Demo Group ID");
                obj.mMapPartsID = (short)control.NumberInput(obj.mMapPartsID, "Map Parts ID");
                obj.mObjID = (short)control.NumberInput(obj.mObjID, "Obj ID");
                obj.mGeneratorID = (short)control.NumberInput(obj.mGeneratorID, "Generator ID");
            }

            public void OnValueChangeStart() { }
            public void OnValueChanged()
            {
                scene.Refresh();
            }

            public void OnValueSet() { }
            public void UpdateProperties() { }
        }
    }
}
