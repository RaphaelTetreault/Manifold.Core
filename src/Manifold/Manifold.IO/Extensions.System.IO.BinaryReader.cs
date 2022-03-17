using System.IO;

namespace Manifold.IO
{
    public static partial class BinaryReaderExtensions
    {
        /// <summary>
        /// Mimics the functionality of StreamReader.EndOfStream
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>True when at the end of the stream</returns>
        public static bool IsAtEndOfStream(this EndianBinaryReader reader)
            => StreamExtensions.IsAtEndOfStream(reader.BaseStream);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static long AlignTo(this EndianBinaryReader reader, long alignment)
        {
            var bytesToAlign = StreamExtensions.GetLengthOfAlignment(reader.BaseStream, alignment);
            reader.BaseStream.Seek(bytesToAlign, SeekOrigin.Current);
            return bytesToAlign;
        }

        /// <summary>
        /// Sets the stream's position to 0.
        /// </summary>
        /// <param name="reader"></param>
        public static void SeekBegin(this EndianBinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
        }

    }
}
