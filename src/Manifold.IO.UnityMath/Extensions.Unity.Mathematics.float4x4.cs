using System.Diagnostics.CodeAnalysis;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Manifold.IO
{
    /// <summary>
    /// Extensions to make matrix4x4 easier to handle.
    /// </summary>
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Extension is for 'float4x4'")]
    public static partial class float4x4Extensions
    {
        public static float3 Position(this float4x4 matrix)
        {
            return matrix.c3.xyz;
        }

        // Currently very lacking. Rotations do not come out right.
        public static quaternion Rotation(this float4x4 matrix)
        {
            return new quaternion(matrix);
        }

        public static float3 RotationEuler(this float4x4 matrix)
        {
            // Get the relevant parts of the rotation from the matrix
            // https://nghiaho.com/?page_id=846
            float r11 = matrix.c0.x;
            float r21 = matrix.c1.y;
            float r31 = matrix.c2.x;
            float r32 = matrix.c2.y;
            float r33 = matrix.c2.z;

            // Compute discrete rotation steps
            float xRadians = atan2(r32, r33);
            float yRadians = atan2(-r31, sqrt(pow(r32, 2) + pow(r33, 2)));
            float zRadians = atan2(r21, r11);

            // Set angles to be in degrees, not radians
            float3 decomposedEulers = degrees(new float3(xRadians, yRadians, zRadians));
            
            return decomposedEulers;
        }

        public static float3 Scale(this float4x4 matrix)
        {
            return new float3(
                math.length(matrix.c0),
                math.length(matrix.c1),
                math.length(matrix.c2)
                );
        }

        public static float3x3 AsFloat3x3(this float4x4 m4)
        {
            return new float3x3(m4.c0.xyz, m4.c1.xyz, m4.c2.xyz);
        }
    }
}
