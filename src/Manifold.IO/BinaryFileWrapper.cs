using System;

namespace Manifold.IO
{
    public abstract class BinaryFileWrapper<TBinarySerializable> :
        IBinaryFileType,
        IBinarySerializable
        where TBinarySerializable : IBinarySerializable, new()
    {
        public abstract Endianness Endianness { get; }
        public abstract string FileExtension { get; }
        public abstract string FileName { get; set; }
        public Type Type => typeof(TBinarySerializable);
        public abstract string Version { get; }

        public TBinarySerializable Value { get; set; } = new TBinarySerializable();

        public void Deserialize(EndianBinaryReader reader)
        {
            Value.Deserialize(reader);
        }

        public void Serialize(EndianBinaryWriter writer)
        {
            Value.Serialize(writer);
        }

        public static implicit operator TBinarySerializable(BinaryFileWrapper<TBinarySerializable> fileWrapper)
        {
            return fileWrapper.Value;
        }
    }
}
