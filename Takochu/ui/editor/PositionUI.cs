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
    public class PositionUI : IObjectUIContainer
    {
        AbstractObj obj;
        EditorSceneBase scene;
        bool useScale;

        public PositionUI(AbstractObj obj, EditorSceneBase scene, bool usesScale = true)
        {
            this.obj = obj;
            this.scene = scene;
            this.scene = scene;
            useScale = usesScale;
        }

        public void DoUI(IObjectUIControl control)
        {
            control.PlainText("Position");
            obj.mTruePosition.X = control.NumberInput(obj.mTruePosition.X, "X:");
            obj.mTruePosition.Y = control.NumberInput(obj.mTruePosition.Y, "Y:");
            obj.mTruePosition.Z = control.NumberInput(obj.mTruePosition.Z, "Z:");

            control.VerticalSeperator();

            control.PlainText("Rotation");
            obj.mTrueRotation.X = control.NumberInput(obj.mTrueRotation.X, "X:");
            obj.mTrueRotation.Y = control.NumberInput(obj.mTrueRotation.Y, "Y:");
            obj.mTrueRotation.Z = control.NumberInput(obj.mTrueRotation.Z, "Z:");

            if (useScale)
            {
                control.VerticalSeperator();

                control.PlainText("Scale");
                obj.mScale.X = control.NumberInput(obj.mScale.X, "X:");
                obj.mScale.Y = control.NumberInput(obj.mScale.Y, "Y:");
                obj.mScale.Z = control.NumberInput(obj.mScale.Z, "Z:");
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
}
