using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takochu.smg.obj
{
    public class ObjectHolder
    {
        public ObjectHolder()
        {
            mObjects = new List<AbstractObj>();
        }

        public ObjectHolder(ref List<AbstractObj> objs)
        {
            mObjects = objs;
        }

        public List<AbstractObj> GetObjs()
        {
            return mObjects;
        }

        public List<AbstractObj> GetObjectsOfType(string type)
        {
            return mObjects.FindAll(o => o.mType == type);
        }

        public void ApplyZoneOffset(Vector3 pos, Vector3 rot)
        {
            mObjects.ForEach(o => o.ApplyZoneOffset(pos, rot));
        }

        public void AddObjects(ObjectHolder holder)
        {
            mObjects.AddRange(holder.GetObjs());
        }

        List<AbstractObj> mObjects;
    }
}
