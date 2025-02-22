namespace Manifold.IO;

/// <summary>
///     
/// </summary>
public struct ArrayPointer :
    IBinarySerializable,
    IPointer
{
    // FIELDS
    public int length;
    public int address;


    // CONSTRUCTORS
    public ArrayPointer(int length = 0, int address = 0)
    {
        this.length = length;
        this.address = address;
    }


    // PROPERTIES
    readonly int IPointer.Address => address;
    public readonly bool IsNotNull => address != 0;
    public readonly bool IsNull => address == 0;
    public readonly Pointer Pointer => new(address);
    public readonly string PrintAddress => $"{address:x8}";


    // METHODS
    public void Deserialize(EndianBinaryReader reader)
    {
        reader.Read(ref length);
        reader.Read(ref address);
    }

    public readonly void Serialize(EndianBinaryWriter writer)
    {
        writer.Write(length);
        writer.Write(address);
    }
    public override string ToString()
    {
        return $"Length: {length}, Address: {PrintAddress}";
    }


    // OPERATORS
    public static bool operator ==(ArrayPointer lhs, ArrayPointer rhs)
    {
        return lhs.address == rhs.address && lhs.length == rhs.length;
    }

    public static bool operator !=(ArrayPointer lhs, ArrayPointer rhs)
    {
        return lhs.address != rhs.address || lhs.length != rhs.length;
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj.GetType() != typeof(ArrayPointer))
            return false;

        bool isEqual = (ArrayPointer)obj == this;
        return isEqual;
    }

    public override readonly int GetHashCode()
    {
        return base.GetHashCode();
    }

}
