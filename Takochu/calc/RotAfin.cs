using OpenTK;
using System;


namespace Takochu.calc
{
    public class RotAfin
    {
        /// <summary>
        /// Specify the axis to be targeted.<br/>
        /// 対象となる軸を指定します。
        /// </summary>
        public enum TargetVector
        {
            /// <summary>
            /// Specify the axis of rotation as the "X" axis.<br/>
            /// 回転軸を "X"軸に指定します。
            /// </summary>
            X,
            /// <summary>
            /// Specify the axis of rotation as the "Y" axis.<br/>
            /// 回転軸を "Y"軸に指定します。
            /// </summary>
            Y,
            /// <summary>
            /// Specify the axis of rotation as the "Z" axis.<br/>
            /// 回転軸を "Z"軸に指定します。
            /// </summary>
            Z,
            /// <summary>
            /// Apply rotation to all X, Y, and Z axes.<br/>
            /// X軸、Y軸、Z軸のすべてに回転を適用します。
            /// </summary>
            All
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectTruePosition"></param>
        /// <param name="zoneRot_Degree"></param>
        /// <param name="Axis"></param>
        /// <returns></returns>
        public static Vector3 GetPositionAfterRotation(Vector3 objectTruePosition, Vector3 zoneRot_Degree, TargetVector Axis)
        {
            Vector3 v3Afin = Vector3.Zero;
            switch (Axis)
            {
                case TargetVector.X:
                    v3Afin = AfinX(objectTruePosition, zoneRot_Degree);
                    break;
                case TargetVector.Y:
                    v3Afin = AfinY(objectTruePosition, zoneRot_Degree);
                    break;
                case TargetVector.Z:
                    v3Afin = AfinZ(objectTruePosition, zoneRot_Degree);
                    break;
                case TargetVector.All:
                    v3Afin = AfinAll(objectTruePosition,zoneRot_Degree);
                    break;
            }
            //Console.WriteLine("//////////afin//////////");
            //Console.WriteLine(v3Afin);
            return v3Afin;
        }

        private static Vector3 AfinX(Vector3 grobalTruePosition, Vector3 objRot)
        {
            float x = grobalTruePosition.X;
            float y = (float)(grobalTruePosition.Y * Math.Cos(objRot.X / 180 * (Math.PI))) - (float)(grobalTruePosition.Z * Math.Sin(objRot.X / 180 * (Math.PI)));
            float z = (float)(grobalTruePosition.Y * Math.Sin(objRot.X / 180 * (Math.PI))) + (float)(grobalTruePosition.Z * Math.Cos(objRot.X / 180 * (Math.PI)));
            return new Vector3(x, y, z);
        }

        private static Vector3 AfinY(Vector3 grobalTruePosition, Vector3 objRot)
        {
            float x = (float)(grobalTruePosition.Z * Math.Sin(objRot.Y / 180 * (Math.PI))) + (float)(grobalTruePosition.X * Math.Cos(objRot.Y / 180 * (Math.PI)));
            float y = grobalTruePosition.Y;
            float z = (float)(grobalTruePosition.Z * Math.Cos(objRot.Y / 180 * (Math.PI))) - (float)(grobalTruePosition.X * Math.Sin(objRot.Y / 180 * (Math.PI)));
            return new Vector3(x, y, z);
        }

        private static Vector3 AfinZ(Vector3 grobalTruePosition, Vector3 objRot)
        {
            float x = (float)(grobalTruePosition.X * Math.Cos(objRot.Z / 180 * (Math.PI))) - (float)(grobalTruePosition.Y * (Math.Sin(objRot.Z / 180 * (Math.PI))));
            float y = (float)(grobalTruePosition.X * Math.Sin(objRot.Z / 180 * (Math.PI))) + (float)(grobalTruePosition.Y * Math.Cos(objRot.Z / 180 * (Math.PI)));
            float z = grobalTruePosition.Z;
            return new Vector3(x, y, z);
        }

        private static Vector3 AfinAll(Vector3 grobalTruePosition, Vector3 objRot)
        {
            var AX = GetPositionAfterRotation(grobalTruePosition ,objRot ,TargetVector.X);
            var AY = GetPositionAfterRotation(AX ,objRot , TargetVector.Y);
            var AZ = GetPositionAfterRotation(AY ,objRot , TargetVector.Z);
            return AZ;
        }

        
    }
}
