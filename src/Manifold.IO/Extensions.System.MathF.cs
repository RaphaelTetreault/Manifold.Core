using System.Numerics;
using System;

namespace Manifold.IO
{
    public static class MathFX
    {
        /// <summary>
        /// Converts radian value in to degrees.
        /// </summary>
        /// <param name="radians">The value in degrees to convert.</param>
        /// <returns></returns>
        public static float RadiansToDegrees(float radians)
        {
            // Constant taken from:
            // https://github.com/Unity-Technologies/Unity.Mathematics/blob/master/src/Unity.Mathematics/math.cs
            float degrees = radians * (float)0.017453292519943296;
            return degrees;
        }

        /// <summary>
        /// Converts degree value into radians.
        /// </summary>
        /// <param name="degrees">The value in degrees to convert.</param>
        /// <returns></returns>
        public static float DegreesToRadians(float degrees)
        {
            // Constant taken from:
            // https://github.com/Unity-Technologies/Unity.Mathematics/blob/master/src/Unity.Mathematics/math.cs
            float radians = degrees * (float)57.29577951308232;
            return radians;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/70462758/c-sharp-how-to-convert-quaternions-to-euler-angles-xyz
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Quaternion ToQuaternion(Vector3 v)
        {

            float cy = (float)Math.Cos(v.Z * 0.5);
            float sy = (float)Math.Sin(v.Z * 0.5);
            float cp = (float)Math.Cos(v.Y * 0.5);
            float sp = (float)Math.Sin(v.Y * 0.5);
            float cr = (float)Math.Cos(v.X * 0.5);
            float sr = (float)Math.Sin(v.X * 0.5);

            return new Quaternion
            {
                W = (cr * cp * cy + sr * sp * sy),
                X = (sr * cp * cy - cr * sp * sy),
                Y = (cr * sp * cy + sr * cp * sy),
                Z = (cr * cp * sy - sr * sp * cy)
            };

        }

        // NOTE: maybe you did this already? https://github.com/RaphaelTetreault/Manifold.Core/blob/main/src/Manifold.IO.UnityMath/Extensions.Unity.Mathematics.float4x4.cs

        /// <summary>
        /// https://stackoverflow.com/questions/70462758/c-sharp-how-to-convert-quaternions-to-euler-angles-xyz
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Vector3 ToEulerAngles(Quaternion q)
        {
            Vector3 angles = new();

            // roll / x
            double sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
            double cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
            angles.X = (float)Math.Atan2(sinr_cosp, cosr_cosp);

            // pitch / y
            double sinp = 2 * (q.W * q.Y - q.Z * q.X);
            if (Math.Abs(sinp) >= 1)
            {
                angles.Y = (float)Math.CopySign(Math.PI / 2, sinp);
            }
            else
            {
                angles.Y = (float)Math.Asin(sinp);
            }

            // yaw / z
            double siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
            double cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
            angles.Z = (float)Math.Atan2(siny_cosp, cosy_cosp);

            return angles;
        }
    }
}
