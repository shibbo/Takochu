using OpenTK;
using System;


namespace Takochu.calc
{
    public class RotAfin
    {
        public enum TargetVector
        {
            X,
            Y,
            Z,
            All
        }
        public static Vector3 Get(Vector3 objectTruePosition, Vector3 zoneRot_Degree, TargetVector Axis)
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
            var AX = Get(grobalTruePosition ,objRot ,TargetVector.X);
            var AY = Get(AX ,objRot , TargetVector.Y);
            var AZ = Get(AY ,objRot , TargetVector.Z);
            return AZ;
        }

        
    }
}
