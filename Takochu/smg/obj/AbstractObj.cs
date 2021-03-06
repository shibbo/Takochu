﻿using GL_EditorFramework;
using GL_EditorFramework.EditorDrawables;
using GL_EditorFramework.GL_Core;
using GL_EditorFramework.Interfaces;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;

namespace Takochu.smg.obj
{
    public class AbstractObj : TransformableObject
    {
        public AbstractObj(BCSV.Entry entry) : base(Vector3.Zero, Vector3.Zero, Vector3.Zero)
        {
            mEntry = entry;
            mName = Get<string>("name");
        }
        
        public void ApplyZoneOffset(Vector3 pos, Vector3 rot)
        {
            Position += pos;
            //Rotation = rot + Rotation;
        }

        public List<AbstractObj> GetObjsWithSameField(string type, int value)
        {
            List<AbstractObj> ret = new List<AbstractObj>();

            List<string> layers = mParentZone.GetLayersUsedOnZoneForCurrentScenario();
            ObjectHolder objs = mParentZone.GetObjectsFromLayers("Map", "Obj", layers);
            objs.AddObjects(mParentZone.GetObjectsFromLayers("Map", "AreaObj", layers));

            foreach (AbstractObj o in objs.GetObjs())
            {
                if (!o.mEntry.ContainsKey(type))
                    continue;

                if (o.Get<int>(type) == value)
                {
                    ret.Add(o);
                }
            }

            return ret;
        }

        public virtual void Save() { }
        public virtual void InitializeRender() { }
        public virtual void StopRender() { }
        public override string ToString()
        {
            return "AbstractObj";
        }

        public T Get<T>(string key)
        {
            return mEntry.Get<T>(key);
        }

        public BCSV.Entry mEntry;
        public Zone mParentZone;

        public Vector3 mTruePosition;
        public Vector3 mTrueRotation;
        public Vector3 mScale;

        public string mDirectory;
        public string mLayer;
        public string mFile;

        public string mName;
        public string mType;

        public int[] mObjArgs;
    }
}
