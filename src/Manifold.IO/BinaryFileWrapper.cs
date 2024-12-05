using System;
using System.IO;

namespace Manifold.IO
{
    public abstract class BinaryFileWrapper<TBinarySerializable> :
        IBinaryFileType,
        IBinarySerializable
        where TBinarySerializable : IBinarySerializable, new()
    {
        // CONSTRUCTORS

        /// <summary>
        ///     Create a new <see cref="BinaryFileWrapper{TBinarySerializable}"/> with default
        ///     <see cref="Value"/> and blank <see cref="FileName"/>.
        /// </summary>
        public BinaryFileWrapper() { }

        /// <summary>
        ///     Create a new <see cref="BinaryFileWrapper{TBinarySerializable}"/> where
        ///     <see cref="Value"/> is loaded from <paramref name="inputPath"/> and
        ///     <see cref="FileName"/> is set based on the <paramref name="inputPath"/>'s file name.
        /// </summary>
        public BinaryFileWrapper(string inputPath)
        {
            // Set properties
            FileName = Path.GetFileNameWithoutExtension(inputPath);

            // Read in new value
            Value = new TBinarySerializable();
            using var reader = new EndianBinaryReader(File.OpenRead(inputPath), Endianness);
            Value.Deserialize(reader);
        }


        public abstract Endianness Endianness { get; }
        public abstract string FileExtension { get; }
        public abstract string FileName { get; set; }
        public Type Type => typeof(TBinarySerializable);

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
