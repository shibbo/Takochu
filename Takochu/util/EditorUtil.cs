using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.smg.obj;

namespace Takochu.util
{
    public static class EditorUtil
    {
        public static class EditorActionHolder
        {
            public enum ActionType
            {
                ActionType_EditObject,
                ActionType_DeleteObject,
                ActionType_AddObject
            }

            public static void Initialize()
            {
                mActions = new List<EditorAction>();
                mCurrentIndex = 0;
            }

            public static void AddAction(ActionType actionType, EditorAction before, EditorAction after)
            {
                EditorAction action = new EditorAction();
                action.mActionType = actionType;
                action.mActionPrev = before;
                action.mActionNow = after;
                mActions.Add(action);
                mCurrentIndex++;
            }

            public static bool CanUndo()
            {
                return mCurrentIndex > 0;
            }

            public static bool CanRedo()
            {
                return mCurrentIndex < mActions.Count;
            }

            public static void ClearActionsAfterCurrent()
            {
                if (mActions.Count == 0) return;
                mActions.RemoveRange(mCurrentIndex, mActions.Count - mCurrentIndex);
            }

            public static void DoUndo(out EditorAction before, out EditorAction after)
            {
                // go back our current index by 1
                mCurrentIndex--;
                before = mActions[mCurrentIndex].mActionPrev;
                after = mActions[mCurrentIndex].mActionNow;
            }

            public static void DoRedo(out EditorAction before, out EditorAction after)
            {
                before = mActions[mCurrentIndex].mActionPrev;
                after = mActions[mCurrentIndex].mActionNow;
                mCurrentIndex++;
            }

            public static List<EditorAction> mActions;
            private static int mCurrentIndex;
        }

        public class EditorAction
        {
            public EditorActionHolder.ActionType mActionType;
            public EditorAction mActionPrev;
            public EditorAction mActionNow;
        }

        public class ObjectEditAction : EditorAction
        {
            public ObjectEditAction(AbstractObj obj, string fieldName, object value)
            {
                Console.WriteLine($"Adding new ObjectAction -- {fieldName} to {value}");
                mEditedObject = obj;
                mFieldName = fieldName;
                mValue = value;
            }

            public AbstractObj mEditedObject;
            public string mFieldName;
            public object mValue;
        }

        public class ColorHolder
        {
            public static void Add(int id, Color color)
            {
                mColors.Add(id, color);
            }

            public static int IDFromColor(Color color)
            {
                foreach(KeyValuePair<int, Color> kvp in mColors)
                {
                    if (kvp.Value.Equals(color))
                    {
                        return kvp.Key;
                    }
                }

                return -1;
            }

            public static bool Contains(Color color)
            {
                foreach(KeyValuePair<int, Color> kvp in mColors)
                {
                    if (kvp.Value == color)
                    {
                        return true;
                    }
                }

                return false;
            }

            public static Dictionary<int, Color> mColors = new Dictionary<int, Color>();
        }

        public static Color UIntToColor(uint color)
        {
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(0xFF, r, g, b);
        }
    }

    public class GlobalRandom
    {
        public static int GetNext(int max)
        {
            return mRandom.Next(max);
        }

        private static Random mRandom = new Random(Guid.NewGuid().GetHashCode());
    }
}
