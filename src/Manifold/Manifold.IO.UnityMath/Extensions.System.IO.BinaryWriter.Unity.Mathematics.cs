using Unity.Mathematics;

namespace Manifold.IO
{
    public static partial class BinaryWriterExtensions
    {
        public static void Write(this EndianBinaryWriter writer, float2 value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
        }

        public static void Write(this EndianBinaryWriter writer, float3 value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
        }

        public static void Write(this EndianBinaryWriter writer, float4 value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
            writer.Write(value.w);
        }

        public static void Write(this EndianBinaryWriter writer, quaternion value)
        {
            // value.value pulls the float4 from within quaternion
            writer.Write(value.value.x);
            writer.Write(value.value.y);
            writer.Write(value.value.z);
            writer.Write(value.value.w);
        }

    }
}
