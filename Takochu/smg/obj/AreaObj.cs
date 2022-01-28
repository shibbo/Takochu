using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.util;
using Takochu.rnd;
using OpenTK.Graphics.OpenGL;
using static Takochu.smg.ObjectDB;
using Takochu.calc;
using System.Windows.Forms;

namespace Takochu.smg.obj
{
    /*
     [ToDo]
        1.  We need to fix the problem where the location of the area is not displayed correctly.
            It is very likely that the location of the origin is different in each area.

            //2022/01/03
            Enabled area display in a cubic wire model.
            It seems that the origin is probably well adjusted.
        2.  The shape should be able to be changed by the shape number.
            The fourth shape of the "water area" is a bowl shape.
        
     */
    class AreaObj : AbstractObj
    {
        public static bool IsDisplay_Renderer = true;
        public AreaObj(BCSV.Entry entry, Zone parentZone, string path) : base(entry)
        {
            mParentZone = parentZone;
            string[] content = path.Split('/');
            mDirectory = content[0];
            mLayer = content[1];
            mFile = content[2];

            mType = "AreaObj";

            mTruePosition = new Vector3(Get<float>("pos_x"), Get<float>("pos_y"), Get<float>("pos_z"));
            mTrueRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));

            mPosition = new Vector3(Get<float>("pos_x") / 100, Get<float>("pos_y") / 100, Get<float>("pos_z") / 100);
            mRotation = new Vector3(Get<float>("dir_x"), Get<float>("dir_y"), Get<float>("dir_z"));
            mScale = new Vector3(Get<float>("scale_x"), Get<float>("scale_y"), Get<float>("scale_z"));

            mID = Get<int>("l_id");
            mObjArgs = new int[8];

            for (int i = 0; i < 8; i++)
                mObjArgs[i] = Get<int>($"Obj_arg{i}");
            
            mSwitchAppear = Get<int>("SW_APPEAR");
            mSwitchActivate = Get<int>("SW_A");
            mSwitchDeactivate = Get<int>("SW_B");

            if (GameUtil.IsSMG2())
            {
                mPriority = Get<int>("Priority");
                mSwitchAwake = Get<int>("SW_AWAKE");
                mAreaShapeNo = Get<short>("AreaShapeNo");
            }
            else
            {
                mChildObjID = Get<short>("ChildObjId");
                mSwitchSleep = Get<int>("SW_SLEEP");
            }
            
            mPathID = Get<short>("CommonPath_ID");
            mClippingGroupID = Get<short>("ClippingGroupId");
            mGroupID = Get<short>("GroupId");
            mDemoGroupID = Get<short>("DemoGroupId");
            mMapPartsID = Get<short>("MapParts_ID");
            mObjID = Get<short>("Obj_ID");

            
            colorWireRenderer = new ColorWireRenderer(AreaType.Normal, mAreaShapeNo);
            mRenderer = colorWireRenderer;
            mTruePosition = AdjustmentPosition;
        }

        public override void Render(RenderMode mode)
        {
            if (!IsDisplay_Renderer) return;
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

        public override void Reload_mValues()
        {
            if (mAreaShapeNo < 0)
            {
                Translate.GetMessageBox.Show(MessageBoxText.ShapeNoNotValid, MessageBoxCaption.Error, MessageBoxButtons.OK);
            }

            //string values
            //Currently, it is not linked to ObjectDB, so it cannot be changed temporarily.
            {
                mName = mEntry.Get("name").ToString();
            }


            //Int32 ID
            {
                mID = ObjectTypeChange.ToInt32(mEntry.Get("l_id"));
                mPriority = ObjectTypeChange.ToInt32(mEntry.Get("Priority"));
                
            }

            //Int16 param
            {
                mPathID = ObjectTypeChange.ToInt16(mEntry.Get("CommonPath_ID"));
                mClippingGroupID = ObjectTypeChange.ToInt16(mEntry.Get("ClippingGroupId"));
                mGroupID = ObjectTypeChange.ToInt16(mEntry.Get("GroupId"));
                mDemoGroupID = ObjectTypeChange.ToInt16(mEntry.Get("DemoGroupId"));
                mMapPartsID = ObjectTypeChange.ToInt16(mEntry.Get("MapParts_ID"));
                mAreaShapeNo = ObjectTypeChange.ToInt16(mEntry.Get("AreaShapeNo"));
                mObjID = ObjectTypeChange.ToInt16(mEntry.Get("Obj_ID"));
                if (GameUtil.IsSMG1())
                {
                    mChildObjID = ObjectTypeChange.ToInt16(mEntry.Get("ChildObjId"));
                }
            }

            //Int32 Switch
            {
                mSwitchAppear = ObjectTypeChange.ToInt32(mEntry.Get("SW_APPEAR"));
                mSwitchActivate = ObjectTypeChange.ToInt32(mEntry.Get("SW_A"));
                mSwitchDeactivate = ObjectTypeChange.ToInt32(mEntry.Get("SW_B"));
                if (GameUtil.IsSMG1())
                {
                    mSwitchSleep = ObjectTypeChange.ToInt32(mEntry.Get("SW_SLEEP"));
                }
                if (GameUtil.IsSMG2())
                {
                    mSwitchAwake = ObjectTypeChange.ToInt32(mEntry.Get("SW_AWAKE"));
                }

            }

            //Obj_args
            for (int i = 0; i < mObjArgs.Length; i++)
                mObjArgs[i] = ObjectTypeChange.ToInt32(mEntry.Get($"Obj_arg{i}"));

            //Vector3Values
            {
                mTruePosition =
                    new Vector3(
                        ObjectTypeChange.ToFloat(mEntry.Get("pos_x")),
                        ObjectTypeChange.ToFloat(mEntry.Get("pos_y")),
                        ObjectTypeChange.ToFloat(mEntry.Get("pos_z"))
                    );

                //原点調整
                mTruePosition = AdjustmentPosition;

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

            //Console.WriteLine(dytest);
        }

        public override void Save()
        {
            mEntry.Set("name", mName);
            mEntry.Set("l_id", mID);

            for (int i = 0; i < 8; i++)
                mEntry.Set($"Obj_arg{i}", mObjArgs[i]);

            mEntry.Set("Priority", mPriority);
            mEntry.Set("SW_APPEAR", mSwitchAppear);
            mEntry.Set("SW_A", mSwitchActivate);
            mEntry.Set("SW_B", mSwitchDeactivate);
            if (GameUtil.IsSMG1())
                mEntry.Set("SW_SLEEP", mSwitchSleep);
            
            if (GameUtil.IsSMG2())
                mEntry.Set("SW_AWAKE", mSwitchAwake);

            mEntry.Set("pos_x", ObjectTypeChange.ToFloat(mEntry.Get("pos_x")));
            mEntry.Set("pos_y", ObjectTypeChange.ToFloat(mEntry.Get("pos_y")));
            mEntry.Set("pos_z", ObjectTypeChange.ToFloat(mEntry.Get("pos_z")));

            mEntry.Set("dir_x", mTrueRotation.X);
            mEntry.Set("dir_y", mTrueRotation.Y);
            mEntry.Set("dir_z", mTrueRotation.Z);

            mEntry.Set("scale_x", mScale.X);
            mEntry.Set("scale_y", mScale.Y);
            mEntry.Set("scale_z", mScale.Z);

            mEntry.Set("AreaShapeNo", mAreaShapeNo);
            mEntry.Set("CommonPath_ID", mPathID);
            mEntry.Set("ClippingGroupId", mClippingGroupID);
            mEntry.Set("GroupId", mGroupID);
            mEntry.Set("DemoGroupId", mDemoGroupID);
            mEntry.Set("MapParts_ID", mMapPartsID);
            mEntry.Set("Obj_ID", mObjID);
            if (GameUtil.IsSMG1())
                mEntry.Set("ChildObjId", mChildObjID);
        }

        private int mID;
        private int mPriority;
        private int mSwitchAppear;
        private int mSwitchActivate;
        private int mSwitchDeactivate;
        private int mSwitchAwake;
        private short mAreaShapeNo;
        private short mPathID;
        private short mClippingGroupID;
        private short mGroupID;
        private short mDemoGroupID;
        private short mMapPartsID;
        private short mObjID;

        private int mSwitchSleep;
        private short mChildObjID;

        private readonly ColorWireRenderer  colorWireRenderer;

        public Vector3 AdjustmentPosition 
        {
            get 
            {

                //全てのエリアに適用する場合はこのifは不要です
                if (mAreaShapeNo == 0 || mAreaShapeNo == 1)
                {
                    float swapPositionY = mTruePosition.Y + ((colorWireRenderer.m_Size.Y) * mScale.Y);
                    mTruePosition = new Vector3(mTruePosition.X, swapPositionY, mTruePosition.Z);
                }
                return mTruePosition;
            }
        }

        public override string ToString()
        {
            return $"[{Get<int>("l_id")}] {ObjectDB.GetFriendlyObjNameFromObj(mName)} [{mLayer}]";
        }
    }
}
