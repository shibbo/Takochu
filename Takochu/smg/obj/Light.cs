using GL_EditorFramework;
using GL_EditorFramework.EditorDrawables;
using GL_EditorFramework.GL_Core;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.ui;

namespace Takochu.smg.obj
{
    public class Light : SingleObject
    {
        public Light(BCSV.Entry e, string parent) : base(Vector3.Zero)
        {
            mEntry = e;
            mLightName = mEntry.Get<string>("AreaLightName");
            mLightNo = mEntry.Get<int>("LightID");
            mParent = parent;
        }

        public override string ToString()
        {
            return $"[{mLightNo}] {mLightName} [{mParent}]";
        }

        public override uint Select(int index, GL_ControlBase control)
        {
            if (!Selected)
            {
                Selected = true;
            }
            return 0;
        }

        public override uint SelectDefault(GL_ControlBase control)
        {
            if (!Selected)
            {
                Selected = true;
            }
            return 0;
        }

        public override uint SelectAll(GL_ControlBase control)
        {
            if (!Selected)
            {
                Selected = true;
            }
            return 0;
        }

        public override uint Deselect(int index, GL_ControlBase control)
        {
            if (Selected)
            {
                Selected = false;
            }
            return 0;
        }

        public override uint DeselectAll(GL_ControlBase control)
        {
            if (Selected)
            {
                Selected = false;
            }
            return 0;
        }

        public override bool TrySetupObjectUIControl(EditorSceneBase scene, ObjectUIControl objectUIControl)
        {
            if (!Selected)
                return false;

            objectUIControl.AddObjectUIContainer(new LightUI(this, scene), "General Settings");

            return true;
        }

        BCSV.Entry mEntry;
        public string mParent;

        public string mLightName;
        public int mLightNo;

        public class LightUI : IObjectUIContainer
        {
            Light obj;
            EditorSceneBase scene;
            int lightID;
            string lightName;

            public LightUI(Light obj, EditorSceneBase scene)
            {
                this.obj = obj;
                this.scene = scene;
            }

            public void DoUI(IObjectUIControl control)
            {
                lightID = Convert.ToInt32(control.NumberInput(obj.mEntry.Get<int>("LightID"), "LightID"));
                lightName = control.FullWidthTextInput(obj.mEntry.Get<string>("AreaLightName"), "AreaLightName");

                if (control.Button("View Light Attributes"))
                {
                    LightAttribEditor l = new LightAttribEditor(lightName);
                    l.Show();
                }
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
