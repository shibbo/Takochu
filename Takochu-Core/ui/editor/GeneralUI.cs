using GL_EditorFramework;
using GL_EditorFramework.EditorDrawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.smg.obj;

namespace Takochu.ui.editor
{
    class GeneralUI : IObjectUIContainer
    {
        AbstractObj obj;
        EditorSceneBase scene;

        List<string> zones;

        public GeneralUI(AbstractObj obj, EditorSceneBase scene)
        {
            this.obj = obj;
            this.scene = scene;

            zones = new List<string>();
            zones.AddRange(obj.mParentZone.mGalaxy.GetZones().Keys);
        }

        public void DoUI(IObjectUIControl control)
        {
            control.PlainText(obj.mName);
            obj.mName = control.FullWidthTextInput(obj.mName, "Name");
            control.DropDownTextInput("Zone", obj.mParentZone.mZoneName, zones.ToArray(), false);
            control.DropDownTextInput("Layer", obj.mLayer, obj.mParentZone.GetLayersUsedOnZoneForCurrentScenario().ToArray(), false);
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