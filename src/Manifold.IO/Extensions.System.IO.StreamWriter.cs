using System;
using System.IO;
using System.Linq;

namespace Manifold.IO
{
    public static class StreamWriterExtensions
    {
        const char CharTabulator = '\t';
        const char CharLineFeed = '\n';
        const char CharCarriageReturn = '\r';

        public static void WriteNextCol(this StreamWriter writer)
        {
            writer.Write(CharTabulator);
        }

        public static void WriteNextRow(this StreamWriter writer)
        {
            writer.Write(CharCarriageReturn);
            writer.Write(CharLineFeed);
        }

        public static void WriteNextCol(this StreamWriter writer, object value)
        {
            writer.Write(value);
            WriteNextCol(writer);
        }

        public static void WriteNextRow(this StreamWriter writer, object value)
        {
            writer.Write(value);
            WriteNextRow(writer);
        }

        public static void WriteNextCol(this StreamWriter writer, string value)
        {
            writer.Write(value);
            WriteNextCol(writer);
        }

        public static void WriteNextRow(this StreamWriter writer, string value)
        {
            writer.Write(value);
            WriteNextRow(writer);
        }


        public static void WriteFlagNames<TEnum>(this StreamWriter writer)
            where TEnum : struct, Enum
        {
            var type = Enum.GetUnderlyingType(typeof(TEnum));

            if (type == typeof(uint))
                WriteFlags(writer, (TEnum)(object)uint.MaxValue);
            else if (type == typeof(int))
                WriteFlags(writer, (TEnum)(object)int.MaxValue);
            else if (type == typeof(ushort))
                WriteFlags(writer, (TEnum)(object)ushort.MaxValue);
            else if (type == typeof(short))
                WriteFlags(writer, (TEnum)(object)short.MaxValue);
            else if (type == typeof(byte))
                WriteFlags(writer, (TEnum)(object)byte.MaxValue);
            else if (type == typeof(sbyte))
                WriteFlags(writer, (TEnum)(object)sbyte.MaxValue);
        }

        public static void WriteFlags<TEnum>(this StreamWriter writer, TEnum @enum)
            where TEnum : struct, Enum
        {
            var flags = @enum.GetFlags(true).Reverse();
            foreach (var flag in flags)
            {
                object value = flag is null ? string.Empty : flag;
                writer.WriteNextCol(value);
            }
        }

        public static void WriteNextColNicify(this StreamWriter writer, string value)
        {
            var name = value.Replace("_", " ");

            // TODO: make your own "nicify" function
#if UNITY_EDITOR
            var prettyName = UnityEditor.ObjectNames.NicifyVariableName(name);
#else
            var prettyName = name;
#endif

            writer.WriteNextCol(prettyName);
        }

        public static void WriteStartAddress(this StreamWriter writer, IBinaryAddressable binaryAddressable)
        {
            writer.WriteNextCol(binaryAddressable.AddressRange.PrintStartAddress());
        }


        public static void WriteLineWithTail(this StreamWriter writer, string value, char tail, int maxLength)
        {
            writer.Write(value);
            writer.Write(" ");

            int tailLength = maxLength - value.Length - 1;
            for (int i = 0; i < tailLength; i++)
                writer.Write(tail);
            writer.WriteLine();
        }
    }
}
