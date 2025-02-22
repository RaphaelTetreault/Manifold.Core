using System.Numerics;

namespace Manifold.IO;

/// <summary>
///     
/// </summary>
public static partial class BinaryReaderExtensions
{
    public static Vector2 ReadFloat2(this EndianBinaryReader reader)
    {
        return new Vector2(
            reader.ReadFloat(),
            reader.ReadFloat());
    }

    public static Vector3 ReadFloat3(this EndianBinaryReader reader)
    {
        return new Vector3(
            reader.ReadFloat(),
            reader.ReadFloat(),
            reader.ReadFloat());
    }

    public static Vector4 ReadFloat4(this EndianBinaryReader reader)
    {
        return new Vector4(
            reader.ReadFloat(),
            reader.ReadFloat(),
            reader.ReadFloat(),
            reader.ReadFloat());
    }

    public static Quaternion ReadMathQuaternion(this EndianBinaryReader reader)
    {
        return new Quaternion(
            reader.ReadFloat(),
            reader.ReadFloat(),
            reader.ReadFloat(),
            reader.ReadFloat());
    }


    // Function forwarding
    public static Vector2 Read(this EndianBinaryReader reader, ref Vector2 value)
        => value = reader.ReadFloat2();

    public static Vector3 Read(this EndianBinaryReader reader, ref Vector3 value)
        => value = reader.ReadFloat3();

    public static Vector4 Read(this EndianBinaryReader reader, ref Vector4 value)
        => value = reader.ReadFloat4();

    public static Quaternion Read(this EndianBinaryReader reader, ref Quaternion value)
        => value = reader.ReadMathQuaternion();

    public static Vector2[] Read(this EndianBinaryReader reader, ref Vector2[] value, int length)
        => value = reader.ReadArray(length, ReadFloat2);

    public static Vector3[] Read(this EndianBinaryReader reader, ref Vector3[] value, int length)
        => value = reader.ReadArray(length, ReadFloat3);

    public static Vector4[] Read(this EndianBinaryReader reader, ref Vector4[] value, int length)
        => value = reader.ReadArray(length, ReadFloat4);

    public static Quaternion[] Read(this EndianBinaryReader reader, ref Quaternion[] value, int length)
        => value = reader.ReadArray(length, ReadMathQuaternion);
}
