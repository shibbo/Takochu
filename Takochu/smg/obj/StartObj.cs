using System;
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
using System.Drawing;
using Takochu.calc;

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

            if (ModelCache.HasRenderer(mName))
            {
                mRenderer = ModelCache.GetRenderer(mName);
            }

            if (Program.sGame.DoesFileExist("/ObjectData/Mario.arc"))
            {
                RARCFilesystem rarc = new RARCFilesystem(Program.sGame.mFilesystem.OpenFile("/ObjectData/Mario.arc"));

                if (rarc.DoesFileExist("/root/Mario.bdl"))
                {
                    mRenderer = new BmdRenderer(new BMD(rarc.OpenFile("/root/Mario.bdl")));
                    ModelCache.AddRenderer(mName, (BmdRenderer)mRenderer);
                }

                rarc.Close();
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

        public override void Reload_mValues()
        {
            {
                mMarioNo = ObjectTypeChange.ToInt32(mEntry.Get("MarioNo"));
                mCameraID = ObjectTypeChange.ToInt32(mEntry.Get("Camera_id"));
                mObjArgs[0] = ObjectTypeChange.ToInt32(mEntry.Get($"Obj_arg0"));
            }

            {
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
        }

        int mMarioNo;
        int mCameraID;

        public override string ToString()
        {
            return $"[{mMarioNo}] {ObjectDB.GetFriendlyObjNameFromObj(mName)} [{mLayer}]";
        }
    }
}
