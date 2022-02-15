using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.rnd;
using OpenTK.Graphics;
using System.Drawing;
using static Takochu.util.EditorUtil;
using Takochu.util;

namespace Takochu.smg.obj
{
    public class AbstractObj
    {
        public readonly Dictionary<string, List<string>> cMultiRenderObjs = new Dictionary<string, List<string>>()
        {
            { "RedBlueTurnBlock", new List<string>() { "RedBlueTurnBlock", "RedBlueTurnBlockBase" } },
            //{ "PatakuriBig" , new List<string>(){"KuriboChief","PatakuriWingBig" } }
            { "PatakuriBig" , new List<string>(){"PatakuriWingBig","KuriboChief" } }
        };

        public AbstractObj(BCSV.Entry entry)
        {
            mEntry = entry;
            if (entry.ContainsKey("name"))
                mName = Get<string>("name");

            if (this is PathPointObj)
            {
                int curUnique = Program.sUniqueID;
                for (int i = 0; i < 4; i++)
                {
                    mUnique = Program.sUniqueID++;
                    Color c = Color.FromArgb(0xFF, GlobalRandom.GetNext(256), GlobalRandom.GetNext(256), GlobalRandom.GetNext(256));
                    ColorHolder.Add(mUnique, c);
                }
                mUnique = curUnique;
            }
            else
            {
                mUnique = Program.sUniqueID++;
                mPicking = Color.FromArgb(0xFF, GlobalRandom.GetNext(256), GlobalRandom.GetNext(256), GlobalRandom.GetNext(256));
                ColorHolder.Add(mUnique, mPicking);
            }
        }

        public bool CanUsePath()
        {
            switch(mType)
            {
                case "Obj":
                case "AreaObj":
                case "MapPartsObj":
                case "PlanetObj":
                    return true;
            }

            return false;
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

        public virtual void Reload_mValues()
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

        public void SetPosition(Vector3 pos)
        {
            mTruePosition = pos;
            mPosition = new Vector3(pos.X / 100, pos.Y / 100, pos.Z / 100);
        }

        
        public BCSV.Entry mEntry { get; protected set; }
        public Zone mParentZone { get; protected set; }


        public Vector3 mTruePosition { get; protected set; }
        public Vector3 mTrueRotation { get; protected set; }

        public Vector3 mPosition { get; protected set; }
        public Vector3 mRotation { get; protected set; }
        public Vector3 mScale { get; protected set; }

        public string mDirectory { get; protected set; }
        public string mLayer { get; protected set; }
        public string mFile { get; protected set; }

        public string mName { get; protected set; }
        public string mType { get; protected set; }

        public int mUnique { get; protected set; }
        public Color mPicking { get; protected set; }
        public RendererBase mRenderer { get; protected set; }
        public RendererBase mRenderer2 { get; protected set; }

        public int[] mObjArgs { get; protected set; }


    }
}
