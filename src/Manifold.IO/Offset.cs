using System;

namespace Manifold.IO;

public struct Offset :
    IBinarySerializable,
    IEquatable<Offset>,
    IOffset
{
    // FIELDS
    public int addressOffset;

    // CONSTRUCTORS
    public Offset(int offset)
    {
        this.addressOffset = offset;
    }

    public Offset(long offset)
    {
        this.addressOffset = (int)offset;
    }


    // PROPERTIES
    readonly int IOffset.AddressOffset => addressOffset;
    public readonly bool IsNotNull => addressOffset != 0;
    public readonly bool IsNull => addressOffset == 0;
    public readonly string PrintAddressOffset => $"{addressOffset:x8}";


    // OPERATORS
    public static implicit operator int(Offset offset)
    {
        return offset.addressOffset;
    }
    public static explicit operator uint(Offset offset)
    {
        return (uint)offset.addressOffset;
    }

    public static implicit operator Offset(int addressOffset)
    {
        return new Offset(addressOffset);
    }

    public static implicit operator Offset(long addressOffset)
    {
        return new Offset(addressOffset);
    }


    //METHODS
    public static Pointer CreatePointer(Offset lhs, Offset rhs)
    {
        return new Pointer(lhs.addressOffset + rhs.addressOffset);
    }
    public static Pointer CreatePointer(Pointer lhs, Offset rhs)
    {
        return new Pointer(lhs.address + rhs.addressOffset);
    }
    public static Pointer CreatePointer(Offset lhs, Pointer rhs)
    {
        return new Pointer(lhs.addressOffset + rhs.address);
    }


    public void Deserialize(EndianBinaryReader reader)
    {
        reader.Read(ref addressOffset);
    }

    public readonly void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(addressOffset);
    }

    public override readonly string ToString()
    {
        return PrintAddressOffset;
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        return Equals((Offset)obj);
    }

    public readonly bool Equals(Offset obj)
    {
        return obj.addressOffset == this.addressOffset;
    }

    public override readonly int GetHashCode()
    {
        return base.GetHashCode();
    }


    public static bool operator ==(Offset left, Offset right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Offset left, Offset right)
    {
        return !(left == right);
    }
}