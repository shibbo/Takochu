using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takochu.calc
{
    /*
     note:
        Try not to rely on this class too much.
        The more dependencies you have, the more troublesome it will be to modify.
        
     */
    /// <summary>
    /// This class was created mainly to deal with the parameters of the data grid view.
    /// </summary>
    public class ObjectTypeChange
    {
        /// <summary>
        /// Object To Int32
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isDefNumNegativeOne">true = -1 : false = 0</param>
        /// <returns></returns>
        public static int ToInt32(object value , bool isDefNumNegativeOne = true) 
        {
            if (!Int32.TryParse(value.ToString(), out Int32 tmp))
            {
                if (isDefNumNegativeOne)
                    return -1;
                else
                    return 0;
            } 
            tmp = Convert.ToInt32(value);
            if (tmp > Int32.MaxValue) tmp = Int32.MaxValue;
            if (tmp < Int32.MinValue) tmp = Int32.MinValue;

            return tmp;
        }
        public static short ToInt16(object value, bool isDefNumNegativeOne = true)
        {
            if (!Int16.TryParse(value.ToString(), out Int16 tmp))
            {
                if (isDefNumNegativeOne)
                    return -1;
                else
                    return 0;
            }
            tmp = Convert.ToInt16(value);
            if (tmp > Int16.MaxValue) tmp = Int16.MaxValue;
            if (tmp < Int16.MinValue) tmp = Int16.MinValue;

            return tmp;
        }
        public static float ToFloat(object value)
        {

            if (!float.TryParse(value.ToString(), out float ftmp)) return 0f;
            ftmp = Convert.ToSingle(value);
            if (ftmp > float.MaxValue) ftmp = float.MaxValue;
            if (ftmp < float.MinValue) ftmp = float.MinValue;

            return ftmp;
        }
    }
}
