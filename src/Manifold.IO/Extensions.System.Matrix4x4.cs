using System.Numerics;

namespace Manifold.IO
{
    public static class Matrix4x4Extensions
    {
        public static Vector3 Position(this Matrix4x4 matrix)
        {
            //Vector3 position = new Vector3(matrix.M41, matrix.M42, matrix.M43);
            Vector3 position = matrix.Translation;
            return position;
        }

        public static Quaternion Rotation(this Matrix4x4 matrix)
        {
            Quaternion orientation = Quaternion.CreateFromRotationMatrix(matrix);
            return orientation;
        }

        public static Vector3 RotationEuler(this Matrix4x4 matrix)
        {
            Quaternion orientation = Quaternion.CreateFromRotationMatrix(matrix);
            Vector3 euler = MathFX.ToEulerAngles(orientation);
            return euler;
        }

        public static Vector3 Scale(this Matrix4x4 matrix)
        {
            Vector3 scale = new Vector3(matrix.M11, matrix.M22, matrix.M33);
            return scale;
        }
    }
}
