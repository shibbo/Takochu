﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using OpenTK;
using System.Windows.Forms;
using System.Security.Policy;
using System.Security.Cryptography;
using Takochu.ui;
using static Takochu.smg.ObjectDB;
using Takochu.util;
using Takochu.io;
using OpenTK.Graphics.OpenGL;
using Takochu.rnd;

namespace Takochu.smg.obj
{
    public class LevelObj : AbstractObj
    {
        public LevelObj(BCSV.Entry entry, Zone parentZone, string path) : base(entry)
        {
            mParentZone = parentZone;
            //var test = parentZone.mZones.Values;

            string[] content = path.Split('/');
            mDirectory = content[0];
            mLayer = content[1];
            mFile = content[2];

            mType = "Obj";

            mTruePosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mTrueRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));

            mPosition = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            //mPosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            mScale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));
            Console.WriteLine();
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

            GL.Translate(mTruePosition);
            GL.Rotate(mTrueRotation.X, 0f, 0f, 1f);
            GL.Rotate(mTrueRotation.Y, 0f, 1f, 0f);
            GL.Rotate(mTrueRotation.Z, 1f, 0f, 0f);
            GL.Scale(mScale.X, mScale.Y, mScale.Z);

            mRenderer.Render(inf);

            GL.PopMatrix();
        }

        public void ReRender() 
        {
            RenderInfo inf = new RenderInfo();
            inf.Mode = RenderMode.Translucent;
            GL.PushMatrix();

            GL.Translate(mTruePosition);
            GL.Rotate(mTrueRotation.X, 0f, 0f, 1f);
            GL.Rotate(mTrueRotation.Y, 0f, 1f, 0f);
            GL.Rotate(mTrueRotation.Z, 1f, 0f, 0f);
            GL.Scale(mScale.X, mScale.Y, mScale.Z);
            
            
            mRenderer.Render(inf);

            GL.PopMatrix();
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
            return $"[{Get<int>("l_id")}] {mName} [{mLayer}]";
        }
    }
}
