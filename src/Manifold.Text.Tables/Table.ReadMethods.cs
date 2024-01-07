using System;
using System.Globalization;

namespace Manifold.Text.Tables
{
    public sealed partial class Table
    {
        // TODO: add nint?
        private static readonly ParseType<bool> ParseBool = bool.Parse;
        private static readonly ParseType<byte> ParseUInt8 = byte.Parse;
        private static readonly ParseType<ushort> ParseUInt16 = ushort.Parse;
        private static readonly ParseType<uint> ParseUInt32 = uint.Parse;
        private static readonly ParseType<ulong> ParseUInt64 = ulong.Parse;
        private static readonly ParseType<UInt128> ParseUInt128 = UInt128.Parse;
        private static readonly ParseType<sbyte> ParseInt8 = sbyte.Parse;
        private static readonly ParseType<short> ParseInt16 = short.Parse;
        private static readonly ParseType<int> ParseInt32 = int.Parse;
        private static readonly ParseType<long> ParseInt64 = long.Parse;
        private static readonly ParseType<Int128> ParseInt128 = Int128.Parse;
        private static readonly ParseType<float> ParseSingle = float.Parse;
        private static readonly ParseType<double> ParseDouble = double.Parse;
        private static readonly ParseType<decimal> ParseDecimal = decimal.Parse;
        private static readonly ParseType<DateTime> ParseDateTime = DateTime.Parse;

        // NOTE: These functions return functions!
        public static ParseType<byte> ParseUInt8WithStyle(NumberStyles numberStyles) => (string str) => { return byte.Parse(str, numberStyles); };
        public static ParseType<ushort> ParseUInt16WithStyle(NumberStyles numberStyles) => (string str) => { return ushort.Parse(str, numberStyles); };
        public static ParseType<uint> ParseUInt32WithStyle(NumberStyles numberStyles) => (string str) => { return uint.Parse(str, numberStyles); };
        public static ParseType<ulong> ParseUInt64WithStyle(NumberStyles numberStyles) => (string str) => { return ulong.Parse(str, numberStyles); };
        public static ParseType<UInt128> ParseUInt128WithStyle(NumberStyles numberStyles) => (string str) => { return UInt128.Parse(str, numberStyles); };
        public static ParseType<sbyte> ParseInt8WithStyle(NumberStyles numberStyles) => (string str) => { return sbyte.Parse(str, numberStyles); };
        public static ParseType<short> ParseInt16WithStyle(NumberStyles numberStyles) => (string str) => { return short.Parse(str, numberStyles); };
        public static ParseType<int> ParseInt32WithStyle(NumberStyles numberStyles) => (string str) => { return int.Parse(str, numberStyles); };
        public static ParseType<long> ParseInt64WithStyle(NumberStyles numberStyles) => (string str) => { return long.Parse(str, numberStyles); };
        public static ParseType<Int128> ParseInt128WithStyle(NumberStyles numberStyles) => (string str) => { return Int128.Parse(str, numberStyles); };
        public static ParseType<float> ParseSingleWithStyle(NumberStyles numberStyles) => (string str) => { return float.Parse(str, numberStyles); };
        public static ParseType<double> ParseDoubleWithStyle(NumberStyles numberStyles) => (string str) => { return double.Parse(str, numberStyles); };
        public static ParseType<decimal> ParseDecimalWithStyle(NumberStyles numberStyles) => (string str) => { return decimal.Parse(str, numberStyles); };
        public static ParseType<DateTime> ParseDateTimeWithStyle(IFormatProvider dateTimeFormatProvider) => (string str) => { return DateTime.Parse(str, dateTimeFormatProvider); };
        public static ParseType<TEnum> ParseEnum<TEnum>() where TEnum : struct, Enum => Enum.Parse<TEnum>;
        public static ParseType<TEnum> ParseEnum<TEnum>(bool ignoreCase) where TEnum : struct, Enum => (string value) => { return Enum.Parse<TEnum>(value, ignoreCase); };


        #region BOOL

        public bool GetCellAsBool(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseBool);
        public bool GetNextCellAsBool() => GetNextCellAs(ParseBool);
        public bool GetNextCellAsBool(out bool isAtEndOfAxis) => GetNextCellAs(ParseBool, out isAtEndOfAxis);

        public bool GetCell(int rowIndex, int columnIndex, ref bool value) => value = GetCellAs(rowIndex, columnIndex, ParseBool);
        public bool GetNext(ref bool value) => value = GetNextCellAs(ParseBool);
        public bool GetNext(out bool isAtEndOfAxis, ref bool value) => value = GetNextCellAs(ParseBool, out isAtEndOfAxis);
        
        #endregion

        #region BYTE / UINT8

        public byte GetCellAsByte(int rowIndex, int columnIndex) => GetCellAsUInt8(rowIndex, columnIndex);
        public byte GetCellAsByte(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsUInt8(rowIndex, columnIndex, numberStyles);
        public byte GetNextCellAsByte() => GetNextCellAsUInt8();
        public byte GetNextCellAsByte(NumberStyles numberStyles) => GetNextCellAsUInt8(numberStyles);
        
        public byte GetCellAsUInt8(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt8);
        public byte GetCellAsUInt8(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt8WithStyle(numberStyles));
        public byte GetNextCellAsUInt8() => GetNextCellAs(ParseUInt8);
        public byte GetNextCellAsUInt8(NumberStyles numberStyles) => GetNextCellAs(ParseUInt8WithStyle(numberStyles));

        public byte GetCell(int rowIndex, int columnIndex, ref byte value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt8);
        public byte GetCell(int rowIndex, int columnIndex, ref byte value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt8WithStyle(numberStyles));
        public byte GetNext(ref byte value) => value = GetNextCellAs(ParseUInt8);
        public byte GetNext(ref byte value, NumberStyles numberStyles) => value = GetNextCellAs(ParseUInt8WithStyle(numberStyles));
        
        #endregion

        #region USHORT / UINT16

        public ushort GetCellAsUShort(int rowIndex, int columnIndex) => GetCellAsUInt16(rowIndex, columnIndex);
        public ushort GetCellAsUShort(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsUInt16(rowIndex, columnIndex, numberStyles);
        public ushort GetNextCellAsUShort() => GetNextCellAsUInt16();
        public ushort GetNextCellAsUShort(NumberStyles numberStyles) => GetNextCellAsUInt16(numberStyles);
        
        public ushort GetCellAsUInt16(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt16);
        public ushort GetCellAsUInt16(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt16WithStyle(numberStyles));
        public ushort GetNextCellAsUInt16() => GetNextCellAs(ParseUInt16);
        public ushort GetNextCellAsUInt16(NumberStyles numberStyles) => GetNextCellAs(ParseUInt16WithStyle(numberStyles));

        public ushort GetCell(int rowIndex, int columnIndex, ref ushort value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt16);
        public ushort GetCell(int rowIndex, int columnIndex, ref ushort value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt16WithStyle(numberStyles));
        public ushort GetNext(ref ushort value) => value = GetNextCellAs(ParseUInt16);
        public ushort GetNext(ref ushort value, NumberStyles numberStyles) => value = GetNextCellAs(ParseUInt16WithStyle(numberStyles));
        
        #endregion

        #region UINT / UINT32

        public uint GetCellAsUInt(int rowIndex, int columnIndex) => GetCellAsUInt32(rowIndex, columnIndex);
        public uint GetCellAsUInt(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsUInt32(rowIndex, columnIndex, numberStyles);
        public uint GetNextCellAsUInt() => GetNextCellAsUInt32();
        public uint GetNextCellAsUInt(NumberStyles numberStyles) => GetNextCellAsUInt32(numberStyles);
        
        public uint GetCellAsUInt32(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt32);
        public uint GetCellAsUInt32(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt32WithStyle(numberStyles));
        public uint GetNextCellAsUInt32() => GetNextCellAs(ParseUInt32);
        public uint GetNextCellAsUInt32(NumberStyles numberStyles) => GetNextCellAs(ParseUInt32WithStyle(numberStyles));

        public uint GetCell(int rowIndex, int columnIndex, ref uint value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt32);
        public uint GetCell(int rowIndex, int columnIndex, ref uint value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt32WithStyle(numberStyles));
        public uint GetNext(ref uint value) => value = GetNextCellAs(ParseUInt32);
        public uint GetNext(ref uint value, NumberStyles numberStyles) => value = GetNextCellAs(ParseUInt32WithStyle(numberStyles));
        
        #endregion

        #region ULONG / UINT64

        public ulong GetCellAsULong(int rowIndex, int columnIndex) => GetCellAsUInt64(rowIndex, columnIndex);
        public ulong GetCellAsULong(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsUInt64(rowIndex, columnIndex, numberStyles);
        public ulong GetNextCellAsULong() => GetNextCellAsUInt64();
        public ulong GetNextCellAsULong(NumberStyles numberStyles) => GetNextCellAsUInt64(numberStyles);
        
        public ulong GetCellAsUInt64(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt64);
        public ulong GetCellAsUInt64(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt64WithStyle(numberStyles));
        public ulong GetNextCellAsUInt64() => GetNextCellAs(ParseUInt64);
        public ulong GetNextCellAsUInt64(NumberStyles numberStyles) => GetNextCellAs(ParseUInt64WithStyle(numberStyles));

        public ulong GetCell(int rowIndex, int columnIndex, ref ulong value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt64);
        public ulong GetCell(int rowIndex, int columnIndex, ref ulong value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt64WithStyle(numberStyles));
        public ulong GetNext(ref ulong value) => value = GetNextCellAs(ParseUInt64);
        public ulong GetNext(ref ulong value, NumberStyles numberStyles) => value = GetNextCellAs(ParseUInt64WithStyle(numberStyles));
        
        #endregion

        #region UINT128

        public UInt128 GetCellAsUInt128(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt128);
        public UInt128 GetCellAsUInt128(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNextCellAsUInt128() => GetNextCellAs(ParseUInt128);
        public UInt128 GetNextCellAsUInt128(NumberStyles numberStyles) => GetNextCellAs(ParseUInt128WithStyle(numberStyles));

        public UInt128 GetCell(int rowIndex, int columnIndex, ref UInt128 value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt128);
        public UInt128 GetCell(int rowIndex, int columnIndex, ref UInt128 value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNext(ref UInt128 value) => value = GetNextCellAs(ParseUInt128);
        public UInt128 GetNext(ref UInt128 value, NumberStyles numberStyles) => value = GetNextCellAs(ParseUInt128WithStyle(numberStyles));
        
        #endregion

        #region SBYTE / INT8

        public sbyte GetCellAsSByte(int rowIndex, int columnIndex) => GetCellAsInt8(rowIndex, columnIndex);
        public sbyte GetCellAsSByte(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsInt8(rowIndex, columnIndex, numberStyles);
        public sbyte GetNextCellAsSByte() => GetNextCellAsInt8();
        public sbyte GetNextCellAsSByte(NumberStyles numberStyles) => GetNextCellAsInt8(numberStyles);
        
        public sbyte GetCellAsInt8(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt8);
        public sbyte GetCellAsInt8(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt8WithStyle(numberStyles));
        public sbyte GetNextCellAsInt8() => GetNextCellAs(ParseInt8);
        public sbyte GetNextCellAsInt8(NumberStyles numberStyles) => GetNextCellAs(ParseInt8WithStyle(numberStyles));

        public sbyte GetCell(int rowIndex, int columnIndex, ref sbyte value) => value = GetCellAs(rowIndex, columnIndex, ParseInt8);
        public sbyte GetCell(int rowIndex, int columnIndex, ref sbyte value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt8WithStyle(numberStyles));
        public sbyte GetNext(ref sbyte value) => value = GetNextCellAs(ParseInt8);
        public sbyte GetNext(ref sbyte value, NumberStyles numberStyles) => value = GetNextCellAs(ParseInt8WithStyle(numberStyles));

        #endregion

        #region SHORT / INT16

        public short GetCellAsShort(int rowIndex, int columnIndex) => GetCellAsInt16(rowIndex, columnIndex);
        public short GetCellAsShort(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsInt16(rowIndex, columnIndex, numberStyles);
        public short GetNextCellAsShort() => GetNextCellAsInt16();
        public short GetNextCellAsShort(NumberStyles numberStyles) => GetNextCellAsInt16(numberStyles);
        
        public short GetCellAsInt16(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt16);
        public short GetCellAsInt16(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt16WithStyle(numberStyles));
        public short GetNextCellAsInt16() => GetNextCellAs(ParseInt16);
        public short GetNextCellAsInt16(NumberStyles numberStyles) => GetNextCellAs(ParseInt16WithStyle(numberStyles));

        public short GetCell(int rowIndex, int columnIndex, ref short value) => value = GetCellAs(rowIndex, columnIndex, ParseInt16);
        public short GetCell(int rowIndex, int columnIndex, ref short value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt16WithStyle(numberStyles));
        public short GetNext(ref short value) => value = GetNextCellAs(ParseInt16);
        public short GetNext(ref short value, NumberStyles numberStyles) => value = GetNextCellAs(ParseInt16WithStyle(numberStyles));

        #endregion

        #region INT / INT32

        public int GetCellAsInt(int rowIndex, int columnIndex) => GetCellAsInt32(rowIndex, columnIndex);
        public int GetCellAsInt(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsInt32(rowIndex, columnIndex, numberStyles);
        public int GetNextCellAsInt() => GetNextCellAsInt32();
        public int GetNextCellAsInt(NumberStyles numberStyles) => GetNextCellAsInt32(numberStyles);

        public int GetCellAsInt32(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt32);
        public int GetCellAsInt32(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt32WithStyle(numberStyles));
        public int GetNextCellAsInt32() => GetNextCellAs(ParseInt32);
        public int GetNextCellAsInt32(NumberStyles numberStyles) => GetNextCellAs(ParseInt32WithStyle(numberStyles));

        public int GetCell(int rowIndex, int columnIndex, ref int value) => value = GetCellAs(rowIndex, columnIndex, ParseInt32);
        public int GetCell(int rowIndex, int columnIndex, ref int value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt32WithStyle(numberStyles));
        public int GetNext(ref int value) => value = GetNextCellAs(ParseInt32);
        public int GetNext(ref int value, NumberStyles numberStyles) => value = GetNextCellAs(ParseInt32WithStyle(numberStyles));

        #endregion

        #region LONG / INT64

        public long GetCellAsLong(int rowIndex, int columnIndex) => GetCellAsInt64(rowIndex, columnIndex);
        public long GetCellAsLong(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsInt64(rowIndex, columnIndex, numberStyles);
        public long GetNextCellAsLong() => GetNextCellAsInt64();
        public long GetNextCellAsLong(NumberStyles numberStyles) => GetNextCellAsInt64(numberStyles);
        
        public long GetCellAsInt64(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt64);
        public long GetCellAsInt64(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt64WithStyle(numberStyles));
        public long GetNextCellAsInt64() => GetNextCellAs(ParseInt64);
        public long GetNextCellAsInt64(NumberStyles numberStyles) => GetNextCellAs(ParseInt64WithStyle(numberStyles));

        public long GetCell(int rowIndex, int columnIndex, ref long value) => value = GetCellAs(rowIndex, columnIndex, ParseInt64);
        public long GetCell(int rowIndex, int columnIndex, ref long value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt64WithStyle(numberStyles));
        public long GetNext(ref long value) => value = GetNextCellAs(ParseInt64);
        public long GetNext(ref long value, NumberStyles numberStyles) => value = GetNextCellAs(ParseInt64WithStyle(numberStyles));

        #endregion

        #region INT128

        public Int128 GetCellAsInt128(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt128);
        public Int128 GetCellAsInt128(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt128WithStyle(numberStyles));
        public Int128 GetNextCellAsInt128() => GetNextCellAs(ParseInt128);
        public Int128 GetNextCellAsInt128(NumberStyles numberStyles) => GetNextCellAs(ParseInt128WithStyle(numberStyles));

        public Int128 GetCell(int rowIndex, int columnIndex, ref Int128 value) => value = GetCellAs(rowIndex, columnIndex, ParseInt128);
        public Int128 GetCell(int rowIndex, int columnIndex, ref Int128 value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt128WithStyle(numberStyles));
        public Int128 GetNext(ref Int128 value) => value = GetNextCellAs(ParseInt128);
        public Int128 GetNext(ref Int128 value, NumberStyles numberStyles) => value = GetNextCellAs(ParseInt128WithStyle(numberStyles));

        #endregion

        #region FLOAT / SINGLE

        public float GetCellAsSingle(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseSingle);
        public float GetCellAsSingle(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseSingleWithStyle(numberStyles));
        public float GetNextCellAsSingle() => GetNextCellAs(ParseSingle);
        public float GetNextCellAsSingle(NumberStyles numberStyles) => GetNextCellAs(ParseSingleWithStyle(numberStyles));

        public float GetCellAsFloat(int rowIndex, int columnIndex) => GetCellAsSingle(rowIndex, columnIndex);
        public float GetCellAsFloat(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsSingle(rowIndex, columnIndex, numberStyles);
        public float GetNextCellAsFloat() => GetNextCellAsSingle();
        public float GetNextCellAsFloat(NumberStyles numberStyles) => GetNextCellAsSingle(numberStyles);

        public float GetCell(int rowIndex, int columnIndex, ref float value) => value = GetCellAs(rowIndex, columnIndex, ParseSingle);
        public float GetCell(int rowIndex, int columnIndex, ref float value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseSingleWithStyle(numberStyles));
        public float GetNext(ref float value) => value = GetNextCellAs(ParseSingle);
        public float GetNext(ref float value, NumberStyles numberStyles) => value = GetNextCellAs(ParseSingleWithStyle(numberStyles));

        #endregion

        #region DOUBLE

        public double GetCellAsDouble(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseDouble);
        public double GetCellAsDouble(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseDoubleWithStyle(numberStyles));
        public double GetNextCellAsDouble() => GetNextCellAs(ParseDouble);
        public double GetNextCellAsDouble(NumberStyles numberStyles) => GetNextCellAs(ParseDoubleWithStyle(numberStyles));

        public double GetCell(int rowIndex, int columnIndex, ref double value) => value = GetCellAs(rowIndex, columnIndex, ParseDouble);
        public double GetCell(int rowIndex, int columnIndex, ref double value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseDoubleWithStyle(numberStyles));
        public double GetNext(ref double value) => value = GetNextCellAs(ParseDouble);
        public double GetNext(ref double value, NumberStyles numberStyles) => value = GetNextCellAs(ParseDoubleWithStyle(numberStyles));

        #endregion

        #region DECIMAL

        public decimal GetCellAsDecimal(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseDecimal);
        public decimal GetCellAsDecimal(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseDecimalWithStyle(numberStyles));
        public decimal GetNextCellAsDecimal() => GetNextCellAs(ParseDecimal);
        public decimal GetNextCellAsDecimal(NumberStyles numberStyles) => GetNextCellAs(ParseDecimalWithStyle(numberStyles));

        public decimal GetCell(int rowIndex, int columnIndex, ref decimal value) => value = GetCellAs(rowIndex, columnIndex, ParseDecimal);
        public decimal GetCell(int rowIndex, int columnIndex, ref decimal value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseDecimalWithStyle(numberStyles));
        public decimal GetNext(ref decimal value) => value = GetNextCellAs(ParseDecimal);
        public decimal GetNext(ref decimal value, NumberStyles numberStyles) => value = GetNextCellAs(ParseDecimalWithStyle(numberStyles));

        #endregion

        #region ENUM

        public TEnum GetCellAsEnum<TEnum>(int rowIndex, int columnIndex) where TEnum : struct, Enum => GetCellAs(rowIndex, columnIndex, ParseEnum<TEnum>(true) );
        public TEnum GetCellAsEnum<TEnum>(int rowIndex, int columnIndex, bool ignoreCase) where TEnum : struct, Enum => GetCellAs(rowIndex, columnIndex, ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNextCellAsEnum<TEnum>() where TEnum : struct, Enum => GetNextCellAs(ParseEnum<TEnum>());
        public TEnum GetNextCellAsEnum<TEnum>(bool ignoreCase) where TEnum : struct, Enum => GetNextCellAs(ParseEnum<TEnum>(ignoreCase));

        public TEnum GetCell<TEnum>(int rowIndex, int columnIndex, ref TEnum value) where TEnum : struct, Enum => value = GetCellAs(rowIndex, columnIndex, ParseEnum<TEnum>());
        public TEnum GetCell<TEnum>(int rowIndex, int columnIndex, ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetCellAs(rowIndex, columnIndex, ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNext<TEnum>(ref TEnum value) where TEnum : struct, Enum => value = GetNextCellAs(ParseEnum<TEnum>());
        public TEnum GetNext<TEnum>(ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetNextCellAs(ParseEnum<TEnum>(ignoreCase));

        #endregion

    }
}
