using System;
using System.IO;
using System.Text;

namespace Manifold.IO
{
    public static partial class BinaryWriterExtensions
    {
        /// <summary>
        /// Writes in the <paramref name="writer"/>'s stream to align it to <paramref name="alignment"/> using the
        /// supplied <paramref name="paddingValue"/>.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="alignment">The stride of the alignment.</param>
        /// <param name="paddingValue">The value to use as padding.</param>
        /// <returns></returns>
        public static long WriteAlignment(this EndianBinaryWriter writer, long alignment, byte paddingValue = 0x00)
        {
            var bytesToAlign = StreamExtensions.GetLengthOfAlignment(writer.BaseStream, alignment);
            for (int i = 0; i < bytesToAlign; i++)
                writer.Write(paddingValue);

            return bytesToAlign;
        }

        /// <summary>
        /// Sets the stream's position to 0.
        /// </summary>
        /// <param name="writer"></param>
        public static void SeekBegin(this EndianBinaryWriter writer)
        {
            writer.BaseStream.Seek(0, SeekOrigin.Begin);
        }

    }
}