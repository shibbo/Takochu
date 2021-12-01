using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.rnd;
using OpenTK.Graphics;

namespace Takochu.smg.obj
{
    public class AbstractObj
    {
        public AbstractObj(BCSV.Entry entry)
        {
            mEntry = entry;
            mName = Get<string>("name");
            mUnique = Program.sUniqueID++;
            
        }
        public void ApplyZoneOffset(Vector3 pos, Vector3 rot)
        {
            mPosition += pos;
            //Rotation = rot + Rotation;
        }

        public virtual void Save() { }

        public List<AbstractObj> GetObjsWithSameField(string type, int value)
        {
            List<AbstractObj> ret = new List<AbstractObj>();

            List<string> layers = mParentZone.GetLayersUsedOnZoneForCurrentScenario();
            List<AbstractObj> objs = mParentZone.GetObjectsFromLayers("Map", "Obj", layers);
            objs.AddRange(mParentZone.GetObjectsFromLayers("Map", "AreaObj", layers));

            foreach (AbstractObj o in objs)
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

        public virtual void Render(RenderMode mode)
        {

        }

        

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

        public Vector3 mPosition;
        public Vector3 mRotation;
        public Vector3 mScale;

        public string mDirectory;
        public string mLayer;
        public string mFile;

        public string mName;
        public string mType;

        public int mUnique;
        public RendererBase mRenderer;

        public int[] mObjArgs;
    }
}
