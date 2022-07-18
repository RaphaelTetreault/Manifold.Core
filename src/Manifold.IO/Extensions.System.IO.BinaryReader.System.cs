//using System;
//using System.IO;

//namespace Manifold.IO
//{
//    public static partial class BinaryReaderExtensions
//    {
//        public static void Read(this EndianBinaryReader reader, ref DateTime value)
//        {
//            long dateTimeBinary = 0;
//            reader.Read(ref dateTimeBinary);
//            value = DateTime.FromBinary(dateTimeBinary);
//        }
//    }
//}
