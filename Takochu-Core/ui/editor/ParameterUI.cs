using GL_EditorFramework;
using GL_EditorFramework.EditorDrawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.smg;
using Takochu.smg.obj;
using static Takochu.smg.ObjectDB;

namespace Takochu.ui.editor
{
    public class ParameterUI : IObjectUIContainer
    {
        AbstractObj obj;
        EditorSceneBase scene;
        int arg_count;

        public ParameterUI(AbstractObj obj, EditorSceneBase scene, int arg)
        {
            this.obj = obj;
            this.scene = scene;
            this.arg_count = arg;
        }

        public void DoUI(IObjectUIControl control)
        {
            for (int i = 0; i < arg_count; i++)
            {
                if (ObjectDB.UsesObjArg(obj.mName, i))
                {
                    Actor actor = ObjectDB.GetActorFromObjectName(obj.mName);
                    ActorField field = GetFieldFromActor(actor, i);

                    switch (field.Type)
                    {
                        case "checkbox":
                            bool check = obj.mObjArgs[i] == Int32.Parse(field.Value);
                            int intVal = Int32.Parse(field.Value);
                            obj.mObjArgs[i] = control.CheckBox(field.Name, check) ? intVal : -1;
                            break;
                        case "list":
                            // this code is a little complicated, but to sum it up:
                            // the list has a syntax, value = name
                            // so we get the fields as a list, then we get the index of the field we need to select, based on the Obj_arg value
                            // then after that, we insert the list into the combo box, and set the current selected index based on our Obj_arg value
                            // to properly set the value again, we simply take the selected item and set the value on the left side and set it to that
                            string[] fields = ObjectDB.GetFieldAsList(field);
                            int index = ObjectDB.IndexOfSelectedListField(field, obj.mObjArgs[i]);
                            string val = control.DropDownTextInput(field.Name, fields[index], fields, false);
                            obj.mObjArgs[i] = Int32.Parse(val.Split('=')[0]);
                            break;
                        case "value":
                            obj.mObjArgs[i] = (int)control.NumberInput(obj.mObjArgs[i], field.Name);
                            break;
                    }
                }
                else
                    obj.mObjArgs[i] = (int)control.NumberInput(obj.mObjArgs[i], $"Obj_arg{i}");
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
