using System;
using System.IO;

namespace Manifold.IO;

/// <summary>
///     Represents a start and end address range.
/// </summary>
public struct AddressRange
{
    // FIELDS
    public long startAddress;
    public long endAddress;

    public AddressRange() { }
    public AddressRange(long startAddress, long endAddress)
    {
        this.startAddress = startAddress;
        this.endAddress = endAddress;
    }

    // PROPERTIES
    /// <summary>
    /// Creates a pointer to this address range.
    /// </summary>
    public readonly Pointer Pointer => startAddress;
    public readonly int Size => (int)(endAddress - startAddress);


    // METHODS
    public void RecordStartAddress(Stream stream)
    {
        startAddress = stream.Position;
    }

    public void RecordStartAddress(EndianBinaryReader reader)
        => RecordStartAddress(reader.BaseStream);

    public void RecordStartAddress(EndianBinaryWriter writer)
        => RecordStartAddress(writer.BaseStream);


    public void RecordEndAddress(Stream stream)
    {
        endAddress = stream.Position;
    }

    public void RecordEndAddress(EndianBinaryReader reader)
        => RecordEndAddress(reader.BaseStream);

    public void RecordEndAddress(EndianBinaryWriter writer)
        => RecordEndAddress(writer.BaseStream);


    public readonly string PrintStartAddress(string prefix = "0x", string format = "x8")
    {
        return $"{prefix}{startAddress.ToString(format)}";
    }

    public readonly string PrintEndAddress(string prefix = "0x", string format = "x8")
    {
        return $"{prefix}{endAddress.ToString(format)}";
    }

    /// <summary>
    ///     Retrieves bytes from address range
    /// </summary>
    /// <param name="reader"></param>
    /// <returns>
    ///     
    /// </returns>
    public readonly byte[] GetBytes(EndianBinaryReader reader)
    {
        reader.JumpToAddress(startAddress);
        byte[] bytes = reader.ReadBytes(Size);
        return bytes;
    }

    public override readonly string ToString()
    {
        return $"{nameof(AddressRange)}(Start: {startAddress:x8}, End: {endAddress:x8}, Size: {Size} 0x{Size:x})";
    }

    public readonly Range ToRange()
    {
        int start = (int)startAddress;
        int end = (int)endAddress;
        Range range = new(start, end);
        return range;
    }

    public static implicit operator Range(AddressRange addressRange)
    {
        return addressRange.ToRange();
    }
}
