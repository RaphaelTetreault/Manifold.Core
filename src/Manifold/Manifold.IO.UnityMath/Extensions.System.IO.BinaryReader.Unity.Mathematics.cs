using Unity.Mathematics;

namespace Manifold.IO
{
    public static partial class BinaryReaderExtensions
    {
        public static float2 ReadFloat2(this EndianBinaryReader reader)
        {
            return new float2(
                reader.ReadFloat(),
                reader.ReadFloat());
        }

        public static float3 ReadFloat3(this EndianBinaryReader reader)
        {
            return new float3(
                reader.ReadFloat(),
                reader.ReadFloat(),
                reader.ReadFloat());
        }

        public static float4 ReadFloat4(this EndianBinaryReader reader)
        {
            return new float4(
                reader.ReadFloat(),
                reader.ReadFloat(),
                reader.ReadFloat(),
                reader.ReadFloat());
        }

        public static quaternion ReadMathQuaternion(this EndianBinaryReader reader)
        {
            return new quaternion(
                reader.ReadFloat(),
                reader.ReadFloat(),
                reader.ReadFloat(),
                reader.ReadFloat());
        }


        // Function forwarding
        public static float2 Read(this EndianBinaryReader reader, ref float2 value)
            => value = reader.ReadFloat2();

        public static float3 Read(this EndianBinaryReader reader, ref float3 value)
            => value = reader.ReadFloat3();

        public static float4 Read(this EndianBinaryReader reader, ref float4 value)
            => value = reader.ReadFloat4();

        public static quaternion Read(this EndianBinaryReader reader, ref quaternion value)
            => value = reader.ReadMathQuaternion();

        public static float2[] Read(this EndianBinaryReader reader, ref float2[] value, int length)
            => value = reader.ReadArray(length, ReadFloat2);

        public static float3[] Read(this EndianBinaryReader reader, ref float3[] value, int length)
            => value = reader.ReadArray(length, ReadFloat3);

        public static float4[] Read(this EndianBinaryReader reader, ref float4[] value, int length)
            => value = reader.ReadArray(length, ReadFloat4);

        public static quaternion[] Read(this EndianBinaryReader reader, ref quaternion[] value, int length)
            => value = reader.ReadArray(length, ReadMathQuaternion);
    }
}
