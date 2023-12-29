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
        public bool GetDataCellAsBool(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseBool);
        public bool GetNextAsBool() => GetNextAs(ParseBool);
        public bool GetNextAsBool(out bool isAtEndOfAxis) => GetNextAs(ParseBool, out isAtEndOfAxis);
        public bool GetNextFromDataColumnAsBool() => GetNextFromDataColumnAs(ParseBool);
        public bool GetNextFromDataColumnAsBool(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseBool, out isAtEndOfAxis);
        public bool GetNextFromDataRowAsBool() => GetNextFromDataRowAs(ParseBool);
        public bool GetNextFromDataRowAsBool(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseBool, out isAtEndOfAxis);

        public bool GetCell(int rowIndex, int columnIndex, ref bool value) => value = GetCellAs(rowIndex, columnIndex, ParseBool);
        public bool GetDataCell(int rowIndex, int columnIndex, ref bool value) => value = GetDataCellAs(rowIndex, columnIndex, ParseBool);
        //public bool GetNext(ref bool value) => value = GetNextAs(ParseBool);
        public bool GetNext(out bool isAtEndOfAxis, ref bool value) => value = GetNextAs(ParseBool, out isAtEndOfAxis);
        //public bool GetNextFromDataColumn(ref bool value) => value = GetNextFromDataColumnAs(ParseBool);
        public bool GetNextFromDataColumn(out bool isAtEndOfAxis, ref bool value) => value = GetNextFromDataColumnAs(ParseBool, out isAtEndOfAxis);
        //public bool GetNextFromDataRow(ref bool value) => value = GetNextFromDataRowAs(ParseBool);
        public bool GetNextFromDataRow(out bool isAtEndOfAxis, ref bool value) => value = GetNextFromDataRowAs(ParseBool, out isAtEndOfAxis);
        #endregion

        #region BYTE / UINT8
        public byte GetCellAsByte(int rowIndex, int columnIndex) => GetCellAsUInt8(rowIndex, columnIndex);
        public byte GetCellAsUInt8(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt8);
        public byte GetCellAsByte(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsUInt8(rowIndex, columnIndex, numberStyles);
        public byte GetCellAsUInt8(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt8WithStyle(numberStyles));
        public byte GetDataCellAsByte(int rowIndex, int columnIndex) => GetDataCellAsUInt8(rowIndex, columnIndex);
        public byte GetDataCellAsUInt8(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt8);
        public byte GetDataCellAsByte(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsUInt8(rowIndex, columnIndex, numberStyles);
        public byte GetDataCellAsUInt8(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseUInt8WithStyle(numberStyles));
        public byte GetNextAsByte() => GetNextAsUInt8();
        public byte GetNextAsUInt8() => GetNextAs(ParseUInt8);
        public byte GetNextAsByte(NumberStyles numberStyles) => GetNextAsUInt8(numberStyles);
        public byte GetNextAsUInt8(NumberStyles numberStyles) => GetNextAs(ParseUInt8WithStyle(numberStyles));
        public byte GetNextAsByte(out bool isAtEndOfAxis) => GetNextAsUInt8(out isAtEndOfAxis);
        public byte GetNextAsUInt8(out bool isAtEndOfAxis) => GetNextAs(ParseUInt8, out isAtEndOfAxis);
        public byte GetNextAsByte(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsUInt8(out isAtEndOfAxis, numberStyles);
        public byte GetNextAsUInt8(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseUInt8WithStyle(numberStyles), out isAtEndOfAxis);
        public byte GetNextFromDataColumnAsByte() => GetNextFromDataColumnAsUInt8();
        public byte GetNextFromDataColumnAsUInt8() => GetNextFromDataColumnAs(ParseUInt8);
        public byte GetNextFromDataColumnAsByte(NumberStyles numberStyles) => GetNextFromDataColumnAsUInt8(numberStyles);
        public byte GetNextFromDataColumnAsUInt8(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseUInt8WithStyle(numberStyles));
        public byte GetNextFromDataColumnAsByte(out bool isAtEndOfAxis) => GetNextFromDataColumnAsUInt8(out isAtEndOfAxis);
        public byte GetNextFromDataColumnAsUInt8(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt8, out isAtEndOfAxis);
        public byte GetNextFromDataRowAsByte() => GetNextFromDataRowAsUInt8();
        public byte GetNextFromDataRowAsUInt8() => GetNextFromDataRowAs(ParseUInt8);
        public byte GetNextFromDataRowAsByte(NumberStyles numberStyles) => GetNextFromDataRowAsUInt8(numberStyles);
        public byte GetNextFromDataRowAsUInt8(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt8WithStyle(numberStyles));
        public byte GetNextFromDataRowAsByte(out bool isAtEndOfAxis) => GetNextFromDataRowAsUInt8(out isAtEndOfAxis);
        public byte GetNextFromDataRowAsUInt8(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt8, out isAtEndOfAxis);
        public byte GetNextFromDataRowAsByte(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsUInt8(out isAtEndOfAxis, numberStyles);
        public byte GetNextFromDataRowAsUInt8(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt8WithStyle(numberStyles), out isAtEndOfAxis);

        public byte GetDataCell(int rowIndex, int columnIndex, ref byte value) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt8);
        public byte GetDataCell(int rowIndex, int columnIndex, ref byte value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt8WithStyle(numberStyles));
        public byte GetCell(int rowIndex, int columnIndex, ref byte value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt8);
        public byte GetCell(int rowIndex, int columnIndex, ref byte value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt8WithStyle(numberStyles));
        public byte GetNext(ref byte value) => value = GetNextAs(ParseUInt8);
        public byte GetNext(ref byte value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt8WithStyle(numberStyles));
        public byte GetNext(out bool isAtEndOfAxis, ref byte value) => value = GetNextAs(ParseUInt8, out isAtEndOfAxis);
        public byte GetNext(out bool isAtEndOfAxis, ref byte value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt8WithStyle(numberStyles), out isAtEndOfAxis);
        public byte GetNextFromDataColumn(ref byte value) => value = GetNextFromDataColumnAs(ParseUInt8);
        public byte GetNextFromDataColumn(ref byte value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt8WithStyle(numberStyles));
        public byte GetNextFromDataColumn(out bool isAtEndOfAxis, ref byte value) => value = GetNextFromDataColumnAs(ParseUInt8, out isAtEndOfAxis);
        public byte GetNextFromDataColumn(out bool isAtEndOfAxis, ref byte value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt8WithStyle(numberStyles), out isAtEndOfAxis);
        public byte GetNextFromDataRow(ref byte value) => value = GetNextFromDataRowAs(ParseUInt8);
        public byte GetNextFromDataRow(ref byte value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt8WithStyle(numberStyles));
        public byte GetNextFromDataRow(out bool isAtEndOfAxis, ref byte value) => value = GetNextFromDataRowAs(ParseUInt8, out isAtEndOfAxis);
        public byte GetNextFromDataRow(out bool isAtEndOfAxis, ref byte value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt8WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region USHORT / UINT16
        public ushort GetCellAsUShort(int rowIndex, int columnIndex) => GetCellAsUInt16(rowIndex, columnIndex);
        public ushort GetCellAsUInt16(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt16);
        public ushort GetCellAsUShort(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsUInt16(rowIndex, columnIndex, numberStyles);
        public ushort GetCellAsUInt16(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt16WithStyle(numberStyles));
        public ushort GetDataCellAsUShort(int rowIndex, int columnIndex) => GetDataCellAsUInt16(rowIndex, columnIndex);
        public ushort GetDataCellAsUInt16(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt16);
        public ushort GetDataCellAsUShort(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsUInt16(rowIndex, columnIndex, numberStyles);
        public ushort GetDataCellAsUInt16(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseUInt16WithStyle(numberStyles));
        public ushort GetNextAsUShort() => GetNextAsUInt16();
        public ushort GetNextAsUInt16() => GetNextAs(ParseUInt16);
        public ushort GetNextAsUShort(NumberStyles numberStyles) => GetNextAsUInt16(numberStyles);
        public ushort GetNextAsUInt16(NumberStyles numberStyles) => GetNextAs(ParseUInt16WithStyle(numberStyles));
        public ushort GetNextAsUShort(out bool isAtEndOfAxis) => GetNextAsUInt16(out isAtEndOfAxis);
        public ushort GetNextAsUInt16(out bool isAtEndOfAxis) => GetNextAs(ParseUInt16, out isAtEndOfAxis);
        public ushort GetNextAsUShort(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsUInt16(out isAtEndOfAxis, numberStyles);
        public ushort GetNextAsUInt16(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseUInt16WithStyle(numberStyles), out isAtEndOfAxis);
        public ushort GetNextFromDataColumnAsUShort() => GetNextFromDataColumnAsUInt16();
        public ushort GetNextFromDataColumnAsUInt16() => GetNextFromDataColumnAs(ParseUInt16);
        public ushort GetNextFromDataColumnAsUShort(NumberStyles numberStyles) => GetNextFromDataColumnAsUInt16(numberStyles);
        public ushort GetNextFromDataColumnAsUInt16(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseUInt16WithStyle(numberStyles));
        public ushort GetNextFromDataColumnAsUShort(out bool isAtEndOfAxis) => GetNextFromDataColumnAsUInt16(out isAtEndOfAxis);
        public ushort GetNextFromDataColumnAsUInt16(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt16, out isAtEndOfAxis);
        public ushort GetNextFromDataRowAsUShort() => GetNextFromDataRowAsUInt16();
        public ushort GetNextFromDataRowAsUInt16() => GetNextFromDataRowAs(ParseUInt16);
        public ushort GetNextFromDataRowAsUShort(NumberStyles numberStyles) => GetNextFromDataRowAsUInt16(numberStyles);
        public ushort GetNextFromDataRowAsUInt16(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt16WithStyle(numberStyles));
        public ushort GetNextFromDataRowAsUShort(out bool isAtEndOfAxis) => GetNextFromDataRowAsUInt16(out isAtEndOfAxis);
        public ushort GetNextFromDataRowAsUInt16(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt16, out isAtEndOfAxis);
        public ushort GetNextFromDataRowAsUShort(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsUInt16(out isAtEndOfAxis, numberStyles);
        public ushort GetNextFromDataRowAsUInt16(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt16WithStyle(numberStyles), out isAtEndOfAxis);

        public ushort GetDataCell(int rowIndex, int columnIndex, ref ushort value) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt16);
        public ushort GetDataCell(int rowIndex, int columnIndex, ref ushort value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt16WithStyle(numberStyles));
        public ushort GetCell(int rowIndex, int columnIndex, ref ushort value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt16);
        public ushort GetCell(int rowIndex, int columnIndex, ref ushort value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt16WithStyle(numberStyles));
        public ushort GetNext(ref ushort value) => value = GetNextAs(ParseUInt16);
        public ushort GetNext(ref ushort value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt16WithStyle(numberStyles));
        public ushort GetNext(out bool isAtEndOfAxis, ref ushort value) => value = GetNextAs(ParseUInt16, out isAtEndOfAxis);
        public ushort GetNext(out bool isAtEndOfAxis, ref ushort value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt16WithStyle(numberStyles), out isAtEndOfAxis);
        public ushort GetNextFromDataColumn(ref ushort value) => value = GetNextFromDataColumnAs(ParseUInt16);
        public ushort GetNextFromDataColumn(ref ushort value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt16WithStyle(numberStyles));
        public ushort GetNextFromDataColumn(out bool isAtEndOfAxis, ref ushort value) => value = GetNextFromDataColumnAs(ParseUInt16, out isAtEndOfAxis);
        public ushort GetNextFromDataColumn(out bool isAtEndOfAxis, ref ushort value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt16WithStyle(numberStyles), out isAtEndOfAxis);
        public ushort GetNextFromDataRow(ref ushort value) => value = GetNextFromDataRowAs(ParseUInt16);
        public ushort GetNextFromDataRow(ref ushort value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt16WithStyle(numberStyles));
        public ushort GetNextFromDataRow(out bool isAtEndOfAxis, ref ushort value) => value = GetNextFromDataRowAs(ParseUInt16, out isAtEndOfAxis);
        public ushort GetNextFromDataRow(out bool isAtEndOfAxis, ref ushort value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt16WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region UINT / UINT32
        public uint GetCellAsUInt(int rowIndex, int columnIndex) => GetCellAsUInt32(rowIndex, columnIndex);
        public uint GetCellAsUInt32(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt32);
        public uint GetCellAsUInt(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsUInt32(rowIndex, columnIndex, numberStyles);
        public uint GetCellAsUInt32(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt32WithStyle(numberStyles));
        public uint GetDataCellAsUInt(int rowIndex, int columnIndex) => GetDataCellAsUInt32(rowIndex, columnIndex);
        public uint GetDataCellAsUInt32(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt32);
        public uint GetDataCellAsUInt(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsUInt32(rowIndex, columnIndex, numberStyles);
        public uint GetDataCellAsUInt32(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseUInt32WithStyle(numberStyles));
        public uint GetNextAsUInt() => GetNextAsUInt32();
        public uint GetNextAsUInt32() => GetNextAs(ParseUInt32);
        public uint GetNextAsUInt(NumberStyles numberStyles) => GetNextAsUInt32(numberStyles);
        public uint GetNextAsUInt32(NumberStyles numberStyles) => GetNextAs(ParseUInt32WithStyle(numberStyles));
        public uint GetNextAsUInt(out bool isAtEndOfAxis) => GetNextAsUInt32(out isAtEndOfAxis);
        public uint GetNextAsUInt32(out bool isAtEndOfAxis) => GetNextAs(ParseUInt32, out isAtEndOfAxis);
        public uint GetNextAsUInt(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsUInt32(out isAtEndOfAxis, numberStyles);
        public uint GetNextAsUInt32(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseUInt32WithStyle(numberStyles), out isAtEndOfAxis);
        public uint GetNextFromDataColumnAsUInt() => GetNextFromDataColumnAsUInt32();
        public uint GetNextFromDataColumnAsUInt32() => GetNextFromDataColumnAs(ParseUInt32);
        public uint GetNextFromDataColumnAsUInt(NumberStyles numberStyles) => GetNextFromDataColumnAsUInt32(numberStyles);
        public uint GetNextFromDataColumnAsUInt32(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseUInt32WithStyle(numberStyles));
        public uint GetNextFromDataColumnAsUInt(out bool isAtEndOfAxis) => GetNextFromDataColumnAsUInt32(out isAtEndOfAxis);
        public uint GetNextFromDataColumnAsUInt32(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt32, out isAtEndOfAxis);
        public uint GetNextFromDataRowAsUInt() => GetNextFromDataRowAsUInt32();
        public uint GetNextFromDataRowAsUInt32() => GetNextFromDataRowAs(ParseUInt32);
        public uint GetNextFromDataRowAsUInt(NumberStyles numberStyles) => GetNextFromDataRowAsUInt32(numberStyles);
        public uint GetNextFromDataRowAsUInt32(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt32WithStyle(numberStyles));
        public uint GetNextFromDataRowAsUInt(out bool isAtEndOfAxis) => GetNextFromDataRowAsUInt32(out isAtEndOfAxis);
        public uint GetNextFromDataRowAsUInt32(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt32, out isAtEndOfAxis);
        public uint GetNextFromDataRowAsUInt(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsUInt32(out isAtEndOfAxis, numberStyles);
        public uint GetNextFromDataRowAsUInt32(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt32WithStyle(numberStyles), out isAtEndOfAxis);

        public uint GetDataCell(int rowIndex, int columnIndex, ref uint value) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt32);
        public uint GetDataCell(int rowIndex, int columnIndex, ref uint value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt32WithStyle(numberStyles));
        public uint GetCell(int rowIndex, int columnIndex, ref uint value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt32);
        public uint GetCell(int rowIndex, int columnIndex, ref uint value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt32WithStyle(numberStyles));
        public uint GetNext(ref uint value) => value = GetNextAs(ParseUInt32);
        public uint GetNext(ref uint value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt32WithStyle(numberStyles));
        public uint GetNext(out bool isAtEndOfAxis, ref uint value) => value = GetNextAs(ParseUInt32, out isAtEndOfAxis);
        public uint GetNext(out bool isAtEndOfAxis, ref uint value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt32WithStyle(numberStyles), out isAtEndOfAxis);
        public uint GetNextFromDataColumn(ref uint value) => value = GetNextFromDataColumnAs(ParseUInt32);
        public uint GetNextFromDataColumn(ref uint value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt32WithStyle(numberStyles));
        public uint GetNextFromDataColumn(out bool isAtEndOfAxis, ref uint value) => value = GetNextFromDataColumnAs(ParseUInt32, out isAtEndOfAxis);
        public uint GetNextFromDataColumn(out bool isAtEndOfAxis, ref uint value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt32WithStyle(numberStyles), out isAtEndOfAxis);
        public uint GetNextFromDataRow(ref uint value) => value = GetNextFromDataRowAs(ParseUInt32);
        public uint GetNextFromDataRow(ref uint value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt32WithStyle(numberStyles));
        public uint GetNextFromDataRow(out bool isAtEndOfAxis, ref uint value) => value = GetNextFromDataRowAs(ParseUInt32, out isAtEndOfAxis);
        public uint GetNextFromDataRow(out bool isAtEndOfAxis, ref uint value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt32WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region ULONG / UINT64
        public ulong GetCellAsULong(int rowIndex, int columnIndex) => GetCellAsUInt64(rowIndex, columnIndex);
        public ulong GetCellAsUInt64(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt64);
        public ulong GetCellAsULong(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsUInt64(rowIndex, columnIndex, numberStyles);
        public ulong GetCellAsUInt64(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt64WithStyle(numberStyles));
        public ulong GetDataCellAsULong(int rowIndex, int columnIndex) => GetDataCellAsUInt64(rowIndex, columnIndex);
        public ulong GetDataCellAsUInt64(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt64);
        public ulong GetDataCellAsULong(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsUInt64(rowIndex, columnIndex, numberStyles);
        public ulong GetDataCellAsUInt64(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseUInt64WithStyle(numberStyles));
        public ulong GetNextAsULong() => GetNextAsUInt64();
        public ulong GetNextAsUInt64() => GetNextAs(ParseUInt64);
        public ulong GetNextAsULong(NumberStyles numberStyles) => GetNextAsUInt64(numberStyles);
        public ulong GetNextAsUInt64(NumberStyles numberStyles) => GetNextAs(ParseUInt64WithStyle(numberStyles));
        public ulong GetNextAsULong(out bool isAtEndOfAxis) => GetNextAsUInt64(out isAtEndOfAxis);
        public ulong GetNextAsUInt64(out bool isAtEndOfAxis) => GetNextAs(ParseUInt64, out isAtEndOfAxis);
        public ulong GetNextAsULong(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsUInt64(out isAtEndOfAxis, numberStyles);
        public ulong GetNextAsUInt64(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseUInt64WithStyle(numberStyles), out isAtEndOfAxis);
        public ulong GetNextFromDataColumnAsULong() => GetNextFromDataColumnAsUInt64();
        public ulong GetNextFromDataColumnAsUInt64() => GetNextFromDataColumnAs(ParseUInt64);
        public ulong GetNextFromDataColumnAsULong(NumberStyles numberStyles) => GetNextFromDataColumnAsUInt64(numberStyles);
        public ulong GetNextFromDataColumnAsUInt64(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseUInt64WithStyle(numberStyles));
        public ulong GetNextFromDataColumnAsULong(out bool isAtEndOfAxis) => GetNextFromDataColumnAsUInt64(out isAtEndOfAxis);
        public ulong GetNextFromDataColumnAsUInt64(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt64, out isAtEndOfAxis);
        public ulong GetNextFromDataRowAsULong() => GetNextFromDataRowAsUInt64();
        public ulong GetNextFromDataRowAsUInt64() => GetNextFromDataRowAs(ParseUInt64);
        public ulong GetNextFromDataRowAsULong(NumberStyles numberStyles) => GetNextFromDataRowAsUInt64(numberStyles);
        public ulong GetNextFromDataRowAsUInt64(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt64WithStyle(numberStyles));
        public ulong GetNextFromDataRowAsULong(out bool isAtEndOfAxis) => GetNextFromDataRowAsUInt64(out isAtEndOfAxis);
        public ulong GetNextFromDataRowAsUInt64(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt64, out isAtEndOfAxis);
        public ulong GetNextFromDataRowAsULong(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsUInt64(out isAtEndOfAxis, numberStyles);
        public ulong GetNextFromDataRowAsUInt64(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt64WithStyle(numberStyles), out isAtEndOfAxis);

        public ulong GetDataCell(int rowIndex, int columnIndex, ref ulong value) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt64);
        public ulong GetDataCell(int rowIndex, int columnIndex, ref ulong value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt64WithStyle(numberStyles));
        public ulong GetCell(int rowIndex, int columnIndex, ref ulong value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt64);
        public ulong GetCell(int rowIndex, int columnIndex, ref ulong value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt64WithStyle(numberStyles));
        public ulong GetNext(ref ulong value) => value = GetNextAs(ParseUInt64);
        public ulong GetNext(ref ulong value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt64WithStyle(numberStyles));
        public ulong GetNext(out bool isAtEndOfAxis, ref ulong value) => value = GetNextAs(ParseUInt64, out isAtEndOfAxis);
        public ulong GetNext(out bool isAtEndOfAxis, ref ulong value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt64WithStyle(numberStyles), out isAtEndOfAxis);
        public ulong GetNextFromDataColumn(ref ulong value) => value = GetNextFromDataColumnAs(ParseUInt64);
        public ulong GetNextFromDataColumn(ref ulong value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt64WithStyle(numberStyles));
        public ulong GetNextFromDataColumn(out bool isAtEndOfAxis, ref ulong value) => value = GetNextFromDataColumnAs(ParseUInt64, out isAtEndOfAxis);
        public ulong GetNextFromDataColumn(out bool isAtEndOfAxis, ref ulong value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt64WithStyle(numberStyles), out isAtEndOfAxis);
        public ulong GetNextFromDataRow(ref ulong value) => value = GetNextFromDataRowAs(ParseUInt64);
        public ulong GetNextFromDataRow(ref ulong value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt64WithStyle(numberStyles));
        public ulong GetNextFromDataRow(out bool isAtEndOfAxis, ref ulong value) => value = GetNextFromDataRowAs(ParseUInt64, out isAtEndOfAxis);
        public ulong GetNextFromDataRow(out bool isAtEndOfAxis, ref ulong value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt64WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region UINT128
        public UInt128 GetCellAsUInt128(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt128);
        public UInt128 GetCellAsUInt128(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseUInt128WithStyle(numberStyles));
        public UInt128 GetDataCellAsUInt128(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt128);
        public UInt128 GetDataCellAsUInt128(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNextAsUInt128() => GetNextAs(ParseUInt128);
        public UInt128 GetNextAsUInt128(NumberStyles numberStyles) => GetNextAs(ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNextAsUInt128(out bool isAtEndOfAxis) => GetNextAs(ParseUInt128, out isAtEndOfAxis);
        public UInt128 GetNextAsUInt128(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseUInt128WithStyle(numberStyles), out isAtEndOfAxis);
        public UInt128 GetNextFromDataColumnAsUInt128() => GetNextFromDataColumnAs(ParseUInt128);
        public UInt128 GetNextFromDataColumnAsUInt128(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNextFromDataColumnAsUInt128(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt128, out isAtEndOfAxis);
        public UInt128 GetNextFromDataRowAsUInt128() => GetNextFromDataRowAs(ParseUInt128);
        public UInt128 GetNextFromDataRowAsUInt128(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNextFromDataRowAsUInt128(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt128, out isAtEndOfAxis);
        public UInt128 GetNextFromDataRowAsUInt128(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseUInt128WithStyle(numberStyles), out isAtEndOfAxis);

        public UInt128 GetDataCell(int rowIndex, int columnIndex, ref UInt128 value) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt128);
        public UInt128 GetDataCell(int rowIndex, int columnIndex, ref UInt128 value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt128WithStyle(numberStyles));
        public UInt128 GetCell(int rowIndex, int columnIndex, ref UInt128 value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt128);
        public UInt128 GetCell(int rowIndex, int columnIndex, ref UInt128 value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNext(ref UInt128 value) => value = GetNextAs(ParseUInt128);
        public UInt128 GetNext(ref UInt128 value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNext(out bool isAtEndOfAxis, ref UInt128 value) => value = GetNextAs(ParseUInt128, out isAtEndOfAxis);
        public UInt128 GetNext(out bool isAtEndOfAxis, ref UInt128 value, NumberStyles numberStyles) => value = GetNextAs(ParseUInt128WithStyle(numberStyles), out isAtEndOfAxis);
        public UInt128 GetNextFromDataColumn(ref UInt128 value) => value = GetNextFromDataColumnAs(ParseUInt128);
        public UInt128 GetNextFromDataColumn(ref UInt128 value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNextFromDataColumn(out bool isAtEndOfAxis, ref UInt128 value) => value = GetNextFromDataColumnAs(ParseUInt128, out isAtEndOfAxis);
        public UInt128 GetNextFromDataColumn(out bool isAtEndOfAxis, ref UInt128 value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseUInt128WithStyle(numberStyles), out isAtEndOfAxis);
        public UInt128 GetNextFromDataRow(ref UInt128 value) => value = GetNextFromDataRowAs(ParseUInt128);
        public UInt128 GetNextFromDataRow(ref UInt128 value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt128WithStyle(numberStyles));
        public UInt128 GetNextFromDataRow(out bool isAtEndOfAxis, ref UInt128 value) => value = GetNextFromDataRowAs(ParseUInt128, out isAtEndOfAxis);
        public UInt128 GetNextFromDataRow(out bool isAtEndOfAxis, ref UInt128 value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseUInt128WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region SBYTE / INT8
        public sbyte GetCellAsSByte(int rowIndex, int columnIndex) => GetCellAsInt8(rowIndex, columnIndex);
        public sbyte GetCellAsInt8(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt8);
        public sbyte GetCellAsSByte(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsInt8(rowIndex, columnIndex, numberStyles);
        public sbyte GetCellAsInt8(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt8WithStyle(numberStyles));
        public sbyte GetDataCellAsSByte(int rowIndex, int columnIndex) => GetDataCellAsInt8(rowIndex, columnIndex);
        public sbyte GetDataCellAsInt8(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt8);
        public sbyte GetDataCellAsSByte(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsInt8(rowIndex, columnIndex, numberStyles);
        public sbyte GetDataCellAsInt8(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseInt8WithStyle(numberStyles));
        public sbyte GetNextAsSByte() => GetNextAsInt8();
        public sbyte GetNextAsInt8() => GetNextAs(ParseInt8);
        public sbyte GetNextAsSByte(NumberStyles numberStyles) => GetNextAsInt8(numberStyles);
        public sbyte GetNextAsInt8(NumberStyles numberStyles) => GetNextAs(ParseInt8WithStyle(numberStyles));
        public sbyte GetNextAsSByte(out bool isAtEndOfAxis) => GetNextAsInt8(out isAtEndOfAxis);
        public sbyte GetNextAsInt8(out bool isAtEndOfAxis) => GetNextAs(ParseInt8, out isAtEndOfAxis);
        public sbyte GetNextAsSByte(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsInt8(out isAtEndOfAxis, numberStyles);
        public sbyte GetNextAsInt8(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseInt8WithStyle(numberStyles), out isAtEndOfAxis);
        public sbyte GetNextFromDataColumnAsSByte() => GetNextFromDataColumnAsInt8();
        public sbyte GetNextFromDataColumnAsInt8() => GetNextFromDataColumnAs(ParseInt8);
        public sbyte GetNextFromDataColumnAsSByte(NumberStyles numberStyles) => GetNextFromDataColumnAsInt8(numberStyles);
        public sbyte GetNextFromDataColumnAsInt8(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseInt8WithStyle(numberStyles));
        public sbyte GetNextFromDataColumnAsSByte(out bool isAtEndOfAxis) => GetNextFromDataColumnAsInt8(out isAtEndOfAxis);
        public sbyte GetNextFromDataColumnAsInt8(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt8, out isAtEndOfAxis);
        public sbyte GetNextFromDataRowAsSByte() => GetNextFromDataRowAsInt8();
        public sbyte GetNextFromDataRowAsInt8() => GetNextFromDataRowAs(ParseInt8);
        public sbyte GetNextFromDataRowAsSByte(NumberStyles numberStyles) => GetNextFromDataRowAsInt8(numberStyles);
        public sbyte GetNextFromDataRowAsInt8(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt8WithStyle(numberStyles));
        public sbyte GetNextFromDataRowAsSByte(out bool isAtEndOfAxis) => GetNextFromDataRowAsInt8(out isAtEndOfAxis);
        public sbyte GetNextFromDataRowAsInt8(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt8, out isAtEndOfAxis);
        public sbyte GetNextFromDataRowAsSByte(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsInt8(out isAtEndOfAxis, numberStyles);
        public sbyte GetNextFromDataRowAsInt8(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt8WithStyle(numberStyles), out isAtEndOfAxis);

        public sbyte GetDataCell(int rowIndex, int columnIndex, ref sbyte value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt8);
        public sbyte GetDataCell(int rowIndex, int columnIndex, ref sbyte value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt8WithStyle(numberStyles));
        public sbyte GetCell(int rowIndex, int columnIndex, ref sbyte value) => value = GetCellAs(rowIndex, columnIndex, ParseInt8);
        public sbyte GetCell(int rowIndex, int columnIndex, ref sbyte value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt8WithStyle(numberStyles));
        public sbyte GetNext(ref sbyte value) => value = GetNextAs(ParseInt8);
        public sbyte GetNext(ref sbyte value, NumberStyles numberStyles) => value = GetNextAs(ParseInt8WithStyle(numberStyles));
        public sbyte GetNext(out bool isAtEndOfAxis, ref sbyte value) => value = GetNextAs(ParseInt8, out isAtEndOfAxis);
        public sbyte GetNext(out bool isAtEndOfAxis, ref sbyte value, NumberStyles numberStyles) => value = GetNextAs(ParseInt8WithStyle(numberStyles), out isAtEndOfAxis);
        public sbyte GetNextFromDataColumn(ref sbyte value) => value = GetNextFromDataColumnAs(ParseInt8);
        public sbyte GetNextFromDataColumn(ref sbyte value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt8WithStyle(numberStyles));
        public sbyte GetNextFromDataColumn(out bool isAtEndOfAxis, ref sbyte value) => value = GetNextFromDataColumnAs(ParseInt8, out isAtEndOfAxis);
        public sbyte GetNextFromDataColumn(out bool isAtEndOfAxis, ref sbyte value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt8WithStyle(numberStyles), out isAtEndOfAxis);
        public sbyte GetNextFromDataRow(ref sbyte value) => value = GetNextFromDataRowAs(ParseInt8);
        public sbyte GetNextFromDataRow(ref sbyte value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt8WithStyle(numberStyles));
        public sbyte GetNextFromDataRow(out bool isAtEndOfAxis, ref sbyte value) => value = GetNextFromDataRowAs(ParseInt8, out isAtEndOfAxis);
        public sbyte GetNextFromDataRow(out bool isAtEndOfAxis, ref sbyte value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt8WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region SHORT / INT16
        public short GetCellAsShort(int rowIndex, int columnIndex) => GetCellAsInt16(rowIndex, columnIndex);
        public short GetCellAsInt16(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt16);
        public short GetCellAsShort(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsInt16(rowIndex, columnIndex, numberStyles);
        public short GetCellAsInt16(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt16WithStyle(numberStyles));
        public short GetDataCellAsShort(int rowIndex, int columnIndex) => GetDataCellAsInt16(rowIndex, columnIndex);
        public short GetDataCellAsInt16(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt16);
        public short GetDataCellAsShort(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsInt16(rowIndex, columnIndex, numberStyles);
        public short GetDataCellAsInt16(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseInt16WithStyle(numberStyles));
        public short GetNextAsShort() => GetNextAsInt16();
        public short GetNextAsInt16() => GetNextAs(ParseInt16);
        public short GetNextAsShort(NumberStyles numberStyles) => GetNextAsInt16(numberStyles);
        public short GetNextAsInt16(NumberStyles numberStyles) => GetNextAs(ParseInt16WithStyle(numberStyles));
        public short GetNextAsShort(out bool isAtEndOfAxis) => GetNextAsInt16(out isAtEndOfAxis);
        public short GetNextAsInt16(out bool isAtEndOfAxis) => GetNextAs(ParseInt16, out isAtEndOfAxis);
        public short GetNextAsShort(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsInt16(out isAtEndOfAxis, numberStyles);
        public short GetNextAsInt16(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseInt16WithStyle(numberStyles), out isAtEndOfAxis);
        public short GetNextFromDataColumnAsShort() => GetNextFromDataColumnAsInt16();
        public short GetNextFromDataColumnAsInt16() => GetNextFromDataColumnAs(ParseInt16);
        public short GetNextFromDataColumnAsShort(NumberStyles numberStyles) => GetNextFromDataColumnAsInt16(numberStyles);
        public short GetNextFromDataColumnAsInt16(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseInt16WithStyle(numberStyles));
        public short GetNextFromDataColumnAsShort(out bool isAtEndOfAxis) => GetNextFromDataColumnAsInt16(out isAtEndOfAxis);
        public short GetNextFromDataColumnAsInt16(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt16, out isAtEndOfAxis);
        public short GetNextFromDataRowAsShort() => GetNextFromDataRowAsInt16();
        public short GetNextFromDataRowAsInt16() => GetNextFromDataRowAs(ParseInt16);
        public short GetNextFromDataRowAsShort(NumberStyles numberStyles) => GetNextFromDataRowAsInt16(numberStyles);
        public short GetNextFromDataRowAsInt16(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt16WithStyle(numberStyles));
        public short GetNextFromDataRowAsShort(out bool isAtEndOfAxis) => GetNextFromDataRowAsInt16(out isAtEndOfAxis);
        public short GetNextFromDataRowAsInt16(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt16, out isAtEndOfAxis);
        public short GetNextFromDataRowAsShort(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsInt16(out isAtEndOfAxis, numberStyles);
        public short GetNextFromDataRowAsInt16(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt16WithStyle(numberStyles), out isAtEndOfAxis);

        public short GetDataCell(int rowIndex, int columnIndex, ref short value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt16);
        public short GetDataCell(int rowIndex, int columnIndex, ref short value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt16WithStyle(numberStyles));
        public short GetCell(int rowIndex, int columnIndex, ref short value) => value = GetCellAs(rowIndex, columnIndex, ParseInt16);
        public short GetCell(int rowIndex, int columnIndex, ref short value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt16WithStyle(numberStyles));
        public short GetNext(ref short value) => value = GetNextAs(ParseInt16);
        public short GetNext(ref short value, NumberStyles numberStyles) => value = GetNextAs(ParseInt16WithStyle(numberStyles));
        public short GetNext(out bool isAtEndOfAxis, ref short value) => value = GetNextAs(ParseInt16, out isAtEndOfAxis);
        public short GetNext(out bool isAtEndOfAxis, ref short value, NumberStyles numberStyles) => value = GetNextAs(ParseInt16WithStyle(numberStyles), out isAtEndOfAxis);
        public short GetNextFromDataColumn(ref short value) => value = GetNextFromDataColumnAs(ParseInt16);
        public short GetNextFromDataColumn(ref short value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt16WithStyle(numberStyles));
        public short GetNextFromDataColumn(out bool isAtEndOfAxis, ref short value) => value = GetNextFromDataColumnAs(ParseInt16, out isAtEndOfAxis);
        public short GetNextFromDataColumn(out bool isAtEndOfAxis, ref short value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt16WithStyle(numberStyles), out isAtEndOfAxis);
        public short GetNextFromDataRow(ref short value) => value = GetNextFromDataRowAs(ParseInt16);
        public short GetNextFromDataRow(ref short value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt16WithStyle(numberStyles));
        public short GetNextFromDataRow(out bool isAtEndOfAxis, ref short value) => value = GetNextFromDataRowAs(ParseInt16, out isAtEndOfAxis);
        public short GetNextFromDataRow(out bool isAtEndOfAxis, ref short value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt16WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region INT / INT32
        public int GetCellAsInt(int rowIndex, int columnIndex) => GetCellAsInt32(rowIndex, columnIndex);
        public int GetCellAsInt32(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt32);
        public int GetCellAsInt(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsInt32(rowIndex, columnIndex, numberStyles);
        public int GetCellAsInt32(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt32WithStyle(numberStyles));
        public int GetDataCellAsInt(int rowIndex, int columnIndex) => GetDataCellAsInt32(rowIndex, columnIndex);
        public int GetDataCellAsInt32(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt32);
        public int GetDataCellAsInt(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsInt32(rowIndex, columnIndex, numberStyles);
        public int GetDataCellAsInt32(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseInt32WithStyle(numberStyles));
        public int GetNextAsInt() => GetNextAsInt32();
        public int GetNextAsInt32() => GetNextAs(ParseInt32);
        public int GetNextAsInt(NumberStyles numberStyles) => GetNextAsInt32(numberStyles);
        public int GetNextAsInt32(NumberStyles numberStyles) => GetNextAs(ParseInt32WithStyle(numberStyles));
        public int GetNextAsInt(out bool isAtEndOfAxis) => GetNextAsInt32(out isAtEndOfAxis);
        public int GetNextAsInt32(out bool isAtEndOfAxis) => GetNextAs(ParseInt32, out isAtEndOfAxis);
        public int GetNextAsInt(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsInt32(out isAtEndOfAxis, numberStyles);
        public int GetNextAsInt32(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseInt32WithStyle(numberStyles), out isAtEndOfAxis);
        public int GetNextFromDataColumnAsInt() => GetNextFromDataColumnAsInt32();
        public int GetNextFromDataColumnAsInt32() => GetNextFromDataColumnAs(ParseInt32);
        public int GetNextFromDataColumnAsInt(NumberStyles numberStyles) => GetNextFromDataColumnAsInt32(numberStyles);
        public int GetNextFromDataColumnAsInt32(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseInt32WithStyle(numberStyles));
        public int GetNextFromDataColumnAsInt(out bool isAtEndOfAxis) => GetNextFromDataColumnAsInt32(out isAtEndOfAxis);
        public int GetNextFromDataColumnAsInt32(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt32, out isAtEndOfAxis);
        public int GetNextFromDataRowAsInt() => GetNextFromDataRowAsInt32();
        public int GetNextFromDataRowAsInt32() => GetNextFromDataRowAs(ParseInt32);
        public int GetNextFromDataRowAsInt(NumberStyles numberStyles) => GetNextFromDataRowAsInt32(numberStyles);
        public int GetNextFromDataRowAsInt32(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt32WithStyle(numberStyles));
        public int GetNextFromDataRowAsInt(out bool isAtEndOfAxis) => GetNextFromDataRowAsInt32(out isAtEndOfAxis);
        public int GetNextFromDataRowAsInt32(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt32, out isAtEndOfAxis);
        public int GetNextFromDataRowAsInt(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsInt32(out isAtEndOfAxis, numberStyles);
        public int GetNextFromDataRowAsInt32(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt32WithStyle(numberStyles), out isAtEndOfAxis);

        public int GetDataCell(int rowIndex, int columnIndex, ref int value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt32);
        public int GetDataCell(int rowIndex, int columnIndex, ref int value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt32WithStyle(numberStyles));
        public int GetCell(int rowIndex, int columnIndex, ref int value) => value = GetCellAs(rowIndex, columnIndex, ParseInt32);
        public int GetCell(int rowIndex, int columnIndex, ref int value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt32WithStyle(numberStyles));
        public int GetNext(ref int value) => value = GetNextAs(ParseInt32);
        public int GetNext(ref int value, NumberStyles numberStyles) => value = GetNextAs(ParseInt32WithStyle(numberStyles));
        public int GetNext(out bool isAtEndOfAxis, ref int value) => value = GetNextAs(ParseInt32, out isAtEndOfAxis);
        public int GetNext(out bool isAtEndOfAxis, ref int value, NumberStyles numberStyles) => value = GetNextAs(ParseInt32WithStyle(numberStyles), out isAtEndOfAxis);
        public int GetNextFromDataColumn(ref int value) => value = GetNextFromDataColumnAs(ParseInt32);
        public int GetNextFromDataColumn(ref int value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt32WithStyle(numberStyles));
        public int GetNextFromDataColumn(out bool isAtEndOfAxis, ref int value) => value = GetNextFromDataColumnAs(ParseInt32, out isAtEndOfAxis);
        public int GetNextFromDataColumn(out bool isAtEndOfAxis, ref int value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt32WithStyle(numberStyles), out isAtEndOfAxis);
        public int GetNextFromDataRow(ref int value) => value = GetNextFromDataRowAs(ParseInt32);
        public int GetNextFromDataRow(ref int value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt32WithStyle(numberStyles));
        public int GetNextFromDataRow(out bool isAtEndOfAxis, ref int value) => value = GetNextFromDataRowAs(ParseInt32, out isAtEndOfAxis);
        public int GetNextFromDataRow(out bool isAtEndOfAxis, ref int value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt32WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region LONG / INT64
        public long GetCellAsLong(int rowIndex, int columnIndex) => GetCellAsInt64(rowIndex, columnIndex);
        public long GetCellAsInt64(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt64);
        public long GetCellAsLong(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsInt64(rowIndex, columnIndex, numberStyles);
        public long GetCellAsInt64(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt64WithStyle(numberStyles));
        public long GetDataCellAsLong(int rowIndex, int columnIndex) => GetDataCellAsInt64(rowIndex, columnIndex);
        public long GetDataCellAsInt64(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt64);
        public long GetDataCellAsLong(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsInt64(rowIndex, columnIndex, numberStyles);
        public long GetDataCellAsInt64(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseInt64WithStyle(numberStyles));
        public long GetNextAsLong() => GetNextAsInt64();
        public long GetNextAsInt64() => GetNextAs(ParseInt64);
        public long GetNextAsLong(NumberStyles numberStyles) => GetNextAsInt64(numberStyles);
        public long GetNextAsInt64(NumberStyles numberStyles) => GetNextAs(ParseInt64WithStyle(numberStyles));
        public long GetNextAsLong(out bool isAtEndOfAxis) => GetNextAsInt64(out isAtEndOfAxis);
        public long GetNextAsInt64(out bool isAtEndOfAxis) => GetNextAs(ParseInt64, out isAtEndOfAxis);
        public long GetNextAsLong(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsInt64(out isAtEndOfAxis, numberStyles);
        public long GetNextAsInt64(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseInt64WithStyle(numberStyles), out isAtEndOfAxis);
        public long GetNextFromDataColumnAsLong() => GetNextFromDataColumnAsInt64();
        public long GetNextFromDataColumnAsInt64() => GetNextFromDataColumnAs(ParseInt64);
        public long GetNextFromDataColumnAsLong(NumberStyles numberStyles) => GetNextFromDataColumnAsInt64(numberStyles);
        public long GetNextFromDataColumnAsInt64(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseInt64WithStyle(numberStyles));
        public long GetNextFromDataColumnAsLong(out bool isAtEndOfAxis) => GetNextFromDataColumnAsInt64(out isAtEndOfAxis);
        public long GetNextFromDataColumnAsInt64(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt64, out isAtEndOfAxis);
        public long GetNextFromDataRowAsLong() => GetNextFromDataRowAsInt64();
        public long GetNextFromDataRowAsInt64() => GetNextFromDataRowAs(ParseInt64);
        public long GetNextFromDataRowAsLong(NumberStyles numberStyles) => GetNextFromDataRowAsInt64(numberStyles);
        public long GetNextFromDataRowAsInt64(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt64WithStyle(numberStyles));
        public long GetNextFromDataRowAsLong(out bool isAtEndOfAxis) => GetNextFromDataRowAsInt64(out isAtEndOfAxis);
        public long GetNextFromDataRowAsInt64(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt64, out isAtEndOfAxis);
        public long GetNextFromDataRowAsLong(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsInt64(out isAtEndOfAxis, numberStyles);
        public long GetNextFromDataRowAsInt64(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt64WithStyle(numberStyles), out isAtEndOfAxis);

        public long GetDataCell(int rowIndex, int columnIndex, ref long value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt64);
        public long GetDataCell(int rowIndex, int columnIndex, ref long value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt64WithStyle(numberStyles));
        public long GetCell(int rowIndex, int columnIndex, ref long value) => value = GetCellAs(rowIndex, columnIndex, ParseInt64);
        public long GetCell(int rowIndex, int columnIndex, ref long value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt64WithStyle(numberStyles));
        public long GetNext(ref long value) => value = GetNextAs(ParseInt64);
        public long GetNext(ref long value, NumberStyles numberStyles) => value = GetNextAs(ParseInt64WithStyle(numberStyles));
        public long GetNext(out bool isAtEndOfAxis, ref long value) => value = GetNextAs(ParseInt64, out isAtEndOfAxis);
        public long GetNext(out bool isAtEndOfAxis, ref long value, NumberStyles numberStyles) => value = GetNextAs(ParseInt64WithStyle(numberStyles), out isAtEndOfAxis);
        public long GetNextFromDataColumn(ref long value) => value = GetNextFromDataColumnAs(ParseInt64);
        public long GetNextFromDataColumn(ref long value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt64WithStyle(numberStyles));
        public long GetNextFromDataColumn(out bool isAtEndOfAxis, ref long value) => value = GetNextFromDataColumnAs(ParseInt64, out isAtEndOfAxis);
        public long GetNextFromDataColumn(out bool isAtEndOfAxis, ref long value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt64WithStyle(numberStyles), out isAtEndOfAxis);
        public long GetNextFromDataRow(ref long value) => value = GetNextFromDataRowAs(ParseInt64);
        public long GetNextFromDataRow(ref long value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt64WithStyle(numberStyles));
        public long GetNextFromDataRow(out bool isAtEndOfAxis, ref long value) => value = GetNextFromDataRowAs(ParseInt64, out isAtEndOfAxis);
        public long GetNextFromDataRow(out bool isAtEndOfAxis, ref long value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt64WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region INT128
        public Int128 GetCellAsInt128(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt128);
        public Int128 GetCellAsInt128(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseInt128WithStyle(numberStyles));
        public Int128 GetDataCellAsInt128(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt128);
        public Int128 GetDataCellAsInt128(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseInt128WithStyle(numberStyles));
        public Int128 GetNextAsInt128() => GetNextAs(ParseInt128);
        public Int128 GetNextAsInt128(NumberStyles numberStyles) => GetNextAs(ParseInt128WithStyle(numberStyles));
        public Int128 GetNextAsInt128(out bool isAtEndOfAxis) => GetNextAs(ParseInt128, out isAtEndOfAxis);
        public Int128 GetNextAsInt128(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseInt128WithStyle(numberStyles), out isAtEndOfAxis);
        public Int128 GetNextFromDataColumnAsInt128() => GetNextFromDataColumnAs(ParseInt128);
        public Int128 GetNextFromDataColumnAsInt128(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseInt128WithStyle(numberStyles));
        public Int128 GetNextFromDataColumnAsInt128(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt128, out isAtEndOfAxis);
        public Int128 GetNextFromDataRowAsInt128() => GetNextFromDataRowAs(ParseInt128);
        public Int128 GetNextFromDataRowAsInt128(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt128WithStyle(numberStyles));
        public Int128 GetNextFromDataRowAsInt128(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt128, out isAtEndOfAxis);
        public Int128 GetNextFromDataRowAsInt128(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseInt128WithStyle(numberStyles), out isAtEndOfAxis);

        public Int128 GetDataCell(int rowIndex, int columnIndex, ref Int128 value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt128);
        public Int128 GetDataCell(int rowIndex, int columnIndex, ref Int128 value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt128WithStyle(numberStyles));
        public Int128 GetCell(int rowIndex, int columnIndex, ref Int128 value) => value = GetCellAs(rowIndex, columnIndex, ParseInt128);
        public Int128 GetCell(int rowIndex, int columnIndex, ref Int128 value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseInt128WithStyle(numberStyles));
        public Int128 GetNext(ref Int128 value) => value = GetNextAs(ParseInt128);
        public Int128 GetNext(ref Int128 value, NumberStyles numberStyles) => value = GetNextAs(ParseInt128WithStyle(numberStyles));
        public Int128 GetNext(out bool isAtEndOfAxis, ref Int128 value) => value = GetNextAs(ParseInt128, out isAtEndOfAxis);
        public Int128 GetNext(out bool isAtEndOfAxis, ref Int128 value, NumberStyles numberStyles) => value = GetNextAs(ParseInt128WithStyle(numberStyles), out isAtEndOfAxis);
        public Int128 GetNextFromDataColumn(ref Int128 value) => value = GetNextFromDataColumnAs(ParseInt128);
        public Int128 GetNextFromDataColumn(ref Int128 value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt128WithStyle(numberStyles));
        public Int128 GetNextFromDataColumn(out bool isAtEndOfAxis, ref Int128 value) => value = GetNextFromDataColumnAs(ParseInt128, out isAtEndOfAxis);
        public Int128 GetNextFromDataColumn(out bool isAtEndOfAxis, ref Int128 value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseInt128WithStyle(numberStyles), out isAtEndOfAxis);
        public Int128 GetNextFromDataRow(ref Int128 value) => value = GetNextFromDataRowAs(ParseInt128);
        public Int128 GetNextFromDataRow(ref Int128 value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt128WithStyle(numberStyles));
        public Int128 GetNextFromDataRow(out bool isAtEndOfAxis, ref Int128 value) => value = GetNextFromDataRowAs(ParseInt128, out isAtEndOfAxis);
        public Int128 GetNextFromDataRow(out bool isAtEndOfAxis, ref Int128 value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseInt128WithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region FLOAT / SINGLE
        public float GetCellAsSingle(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseSingle);
        public float GetCellAsSingle(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseSingleWithStyle(numberStyles));
        public float GetDataCellAsSingle(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseSingle);
        public float GetDataCellAsSingle(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseSingleWithStyle(numberStyles));
        public float GetNextAsSingle() => GetNextAs(ParseSingle);
        public float GetNextAsSingle(NumberStyles numberStyles) => GetNextAs(ParseSingleWithStyle(numberStyles));
        public float GetNextAsSingle(out bool isAtEndOfAxis) => GetNextAs(ParseSingle, out isAtEndOfAxis);
        public float GetNextAsSingle(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseSingleWithStyle(numberStyles), out isAtEndOfAxis);
        public float GetNextFromDataColumnAsSingle() => GetNextFromDataColumnAs(ParseSingle);
        public float GetNextFromDataColumnAsSingle(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseSingleWithStyle(numberStyles));
        public float GetNextFromDataColumnAsSingle(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseSingle, out isAtEndOfAxis);
        public float GetNextFromDataRowAsSingle() => GetNextFromDataRowAs(ParseSingle);
        public float GetNextFromDataRowAsSingle(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseSingleWithStyle(numberStyles));
        public float GetNextFromDataRowAsSingle(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseSingle, out isAtEndOfAxis);
        public float GetNextFromDataRowAsSingle(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseSingleWithStyle(numberStyles), out isAtEndOfAxis);

        public float GetCellAsFloat(int rowIndex, int columnIndex) => GetCellAsSingle(rowIndex, columnIndex);
        public float GetCellAsFloat(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAsSingle(rowIndex, columnIndex, numberStyles);
        public float GetDataCellAsFloat(int rowIndex, int columnIndex) => GetDataCellAsSingle(rowIndex, columnIndex);
        public float GetDataCellAsFloat(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAsSingle(rowIndex, columnIndex, numberStyles);
        public float GetNextAsFloat() => GetNextAsSingle();
        public float GetNextAsFloat(NumberStyles numberStyles) => GetNextAsSingle(numberStyles);
        public float GetNextAsFloat(out bool isAtEndOfAxis) => GetNextAsSingle(out isAtEndOfAxis);
        public float GetNextAsFloat(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAsSingle(out isAtEndOfAxis, numberStyles);
        public float GetNextFromDataColumnAsFloat() => GetNextFromDataColumnAsSingle();
        public float GetNextFromDataColumnAsFloat(NumberStyles numberStyles) => GetNextFromDataColumnAsSingle(numberStyles);
        public float GetNextFromDataColumnAsFloat(out bool isAtEndOfAxis) => GetNextFromDataColumnAsSingle(out isAtEndOfAxis);
        public float GetNextFromDataRowAsFloat() => GetNextFromDataRowAsSingle();
        public float GetNextFromDataRowAsFloat(NumberStyles numberStyles) => GetNextFromDataRowAsSingle(numberStyles);
        public float GetNextFromDataRowAsFloat(out bool isAtEndOfAxis) => GetNextFromDataRowAsSingle(out isAtEndOfAxis);
        public float GetNextFromDataRowAsFloat(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAsSingle(out isAtEndOfAxis, numberStyles);

        public float GetDataCell(int rowIndex, int columnIndex, ref float value) => value = GetDataCellAs(rowIndex, columnIndex, ParseSingle);
        public float GetDataCell(int rowIndex, int columnIndex, ref float value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseSingleWithStyle(numberStyles));
        public float GetCell(int rowIndex, int columnIndex, ref float value) => value = GetCellAs(rowIndex, columnIndex, ParseSingle);
        public float GetCell(int rowIndex, int columnIndex, ref float value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseSingleWithStyle(numberStyles));
        public float GetNext(ref float value) => value = GetNextAs(ParseSingle);
        public float GetNext(ref float value, NumberStyles numberStyles) => value = GetNextAs(ParseSingleWithStyle(numberStyles));
        public float GetNext(out bool isAtEndOfAxis, ref float value) => value = GetNextAs(ParseSingle, out isAtEndOfAxis);
        public float GetNext(out bool isAtEndOfAxis, ref float value, NumberStyles numberStyles) => value = GetNextAs(ParseSingleWithStyle(numberStyles), out isAtEndOfAxis);
        public float GetNextFromDataColumn(ref float value) => value = GetNextFromDataColumnAs(ParseSingle);
        public float GetNextFromDataColumn(ref float value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseSingleWithStyle(numberStyles));
        public float GetNextFromDataColumn(out bool isAtEndOfAxis, ref float value) => value = GetNextFromDataColumnAs(ParseSingle, out isAtEndOfAxis);
        public float GetNextFromDataColumn(out bool isAtEndOfAxis, ref float value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseSingleWithStyle(numberStyles), out isAtEndOfAxis);
        public float GetNextFromDataRow(ref float value) => value = GetNextFromDataRowAs(ParseSingle);
        public float GetNextFromDataRow(ref float value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseSingleWithStyle(numberStyles));
        public float GetNextFromDataRow(out bool isAtEndOfAxis, ref float value) => value = GetNextFromDataRowAs(ParseSingle, out isAtEndOfAxis);
        public float GetNextFromDataRow(out bool isAtEndOfAxis, ref float value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseSingleWithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region DOUBLE
        public double GetCellAsDouble(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseDouble);
        public double GetCellAsDouble(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseDoubleWithStyle(numberStyles));
        public double GetDataCellAsDouble(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseDouble);
        public double GetDataCellAsDouble(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseDoubleWithStyle(numberStyles));
        public double GetNextAsDouble() => GetNextAs(ParseDouble);
        public double GetNextAsDouble(NumberStyles numberStyles) => GetNextAs(ParseDoubleWithStyle(numberStyles));
        public double GetNextAsDouble(out bool isAtEndOfAxis) => GetNextAs(ParseDouble, out isAtEndOfAxis);
        public double GetNextAsDouble(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseDoubleWithStyle(numberStyles), out isAtEndOfAxis);
        public double GetNextFromDataColumnAsDouble() => GetNextFromDataColumnAs(ParseDouble);
        public double GetNextFromDataColumnAsDouble(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseDoubleWithStyle(numberStyles));
        public double GetNextFromDataColumnAsDouble(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseDouble, out isAtEndOfAxis);
        public double GetNextFromDataRowAsDouble() => GetNextFromDataRowAs(ParseDouble);
        public double GetNextFromDataRowAsDouble(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseDoubleWithStyle(numberStyles));
        public double GetNextFromDataRowAsDouble(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseDouble, out isAtEndOfAxis);
        public double GetNextFromDataRowAsDouble(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseDoubleWithStyle(numberStyles), out isAtEndOfAxis);

        public double GetDataCell(int rowIndex, int columnIndex, ref double value) => value = GetDataCellAs(rowIndex, columnIndex, ParseDouble);
        public double GetDataCell(int rowIndex, int columnIndex, ref double value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseDoubleWithStyle(numberStyles));
        public double GetCell(int rowIndex, int columnIndex, ref double value) => value = GetCellAs(rowIndex, columnIndex, ParseDouble);
        public double GetCell(int rowIndex, int columnIndex, ref double value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseDoubleWithStyle(numberStyles));
        public double GetNext(ref double value) => value = GetNextAs(ParseDouble);
        public double GetNext(ref double value, NumberStyles numberStyles) => value = GetNextAs(ParseDoubleWithStyle(numberStyles));
        public double GetNext(out bool isAtEndOfAxis, ref double value) => value = GetNextAs(ParseDouble, out isAtEndOfAxis);
        public double GetNext(out bool isAtEndOfAxis, ref double value, NumberStyles numberStyles) => value = GetNextAs(ParseDoubleWithStyle(numberStyles), out isAtEndOfAxis);
        public double GetNextFromDataColumn(ref double value) => value = GetNextFromDataColumnAs(ParseDouble);
        public double GetNextFromDataColumn(ref double value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseDoubleWithStyle(numberStyles));
        public double GetNextFromDataColumn(out bool isAtEndOfAxis, ref double value) => value = GetNextFromDataColumnAs(ParseDouble, out isAtEndOfAxis);
        public double GetNextFromDataColumn(out bool isAtEndOfAxis, ref double value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseDoubleWithStyle(numberStyles), out isAtEndOfAxis);
        public double GetNextFromDataRow(ref double value) => value = GetNextFromDataRowAs(ParseDouble);
        public double GetNextFromDataRow(ref double value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseDoubleWithStyle(numberStyles));
        public double GetNextFromDataRow(out bool isAtEndOfAxis, ref double value) => value = GetNextFromDataRowAs(ParseDouble, out isAtEndOfAxis);
        public double GetNextFromDataRow(out bool isAtEndOfAxis, ref double value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseDoubleWithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region DECIMAL
        public decimal GetCellAsDecimal(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseDecimal);
        public decimal GetCellAsDecimal(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, ParseDecimalWithStyle(numberStyles));
        public decimal GetDataCellAsDecimal(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseDecimal);
        public decimal GetDataCellAsDecimal(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetDataCellAs(rowIndex, columnIndex, ParseDecimalWithStyle(numberStyles));
        public decimal GetNextAsDecimal() => GetNextAs(ParseDecimal);
        public decimal GetNextAsDecimal(NumberStyles numberStyles) => GetNextAs(ParseDecimalWithStyle(numberStyles));
        public decimal GetNextAsDecimal(out bool isAtEndOfAxis) => GetNextAs(ParseDecimal, out isAtEndOfAxis);
        public decimal GetNextAsDecimal(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextAs(ParseDecimalWithStyle(numberStyles), out isAtEndOfAxis);
        public decimal GetNextFromDataColumnAsDecimal() => GetNextFromDataColumnAs(ParseDecimal);
        public decimal GetNextFromDataColumnAsDecimal(NumberStyles numberStyles) => GetNextFromDataColumnAs(ParseDecimalWithStyle(numberStyles));
        public decimal GetNextFromDataColumnAsDecimal(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseDecimal, out isAtEndOfAxis);
        public decimal GetNextFromDataRowAsDecimal() => GetNextFromDataRowAs(ParseDecimal);
        public decimal GetNextFromDataRowAsDecimal(NumberStyles numberStyles) => GetNextFromDataRowAs(ParseDecimalWithStyle(numberStyles));
        public decimal GetNextFromDataRowAsDecimal(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseDecimal, out isAtEndOfAxis);
        public decimal GetNextFromDataRowAsDecimal(out bool isAtEndOfAxis, NumberStyles numberStyles) => GetNextFromDataRowAs(ParseDecimalWithStyle(numberStyles), out isAtEndOfAxis);

        public decimal GetDataCell(int rowIndex, int columnIndex, ref decimal value) => value = GetDataCellAs(rowIndex, columnIndex, ParseDecimal);
        public decimal GetDataCell(int rowIndex, int columnIndex, ref decimal value, NumberStyles numberStyles) => value = GetDataCellAs(rowIndex, columnIndex, ParseDecimalWithStyle(numberStyles));
        public decimal GetCell(int rowIndex, int columnIndex, ref decimal value) => value = GetCellAs(rowIndex, columnIndex, ParseDecimal);
        public decimal GetCell(int rowIndex, int columnIndex, ref decimal value, NumberStyles numberStyles) => value = GetCellAs(rowIndex, columnIndex, ParseDecimalWithStyle(numberStyles));
        public decimal GetNext(ref decimal value) => value = GetNextAs(ParseDecimal);
        public decimal GetNext(ref decimal value, NumberStyles numberStyles) => value = GetNextAs(ParseDecimalWithStyle(numberStyles));
        public decimal GetNext(out bool isAtEndOfAxis, ref decimal value) => value = GetNextAs(ParseDecimal, out isAtEndOfAxis);
        public decimal GetNext(out bool isAtEndOfAxis, ref decimal value, NumberStyles numberStyles) => value = GetNextAs(ParseDecimalWithStyle(numberStyles), out isAtEndOfAxis);
        public decimal GetNextFromDataColumn(ref decimal value) => value = GetNextFromDataColumnAs(ParseDecimal);
        public decimal GetNextFromDataColumn(ref decimal value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseDecimalWithStyle(numberStyles));
        public decimal GetNextFromDataColumn(out bool isAtEndOfAxis, ref decimal value) => value = GetNextFromDataColumnAs(ParseDecimal, out isAtEndOfAxis);
        public decimal GetNextFromDataColumn(out bool isAtEndOfAxis, ref decimal value, NumberStyles numberStyles) => value = GetNextFromDataColumnAs(ParseDecimalWithStyle(numberStyles), out isAtEndOfAxis);
        public decimal GetNextFromDataRow(ref decimal value) => value = GetNextFromDataRowAs(ParseDecimal);
        public decimal GetNextFromDataRow(ref decimal value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseDecimalWithStyle(numberStyles));
        public decimal GetNextFromDataRow(out bool isAtEndOfAxis, ref decimal value) => value = GetNextFromDataRowAs(ParseDecimal, out isAtEndOfAxis);
        public decimal GetNextFromDataRow(out bool isAtEndOfAxis, ref decimal value, NumberStyles numberStyles) => value = GetNextFromDataRowAs(ParseDecimalWithStyle(numberStyles), out isAtEndOfAxis);
        #endregion

        #region ENUM
        public TEnum GetCellAsEnum<TEnum>(int rowIndex, int columnIndex) where TEnum : struct, Enum => GetCellAs(rowIndex, columnIndex, ParseEnum<TEnum>(true) );
        public TEnum GetCellAsEnum<TEnum>(int rowIndex, int columnIndex, bool ignoreCase) where TEnum : struct, Enum => GetCellAs(rowIndex, columnIndex, ParseEnum<TEnum>(ignoreCase));
        public TEnum GetDataCellAsEnum<TEnum>(int rowIndex, int columnIndex) where TEnum : struct, Enum => GetDataCellAs(rowIndex, columnIndex, ParseEnum<TEnum>());
        public TEnum GetDataCellAsEnum<TEnum>(int rowIndex, int columnIndex, bool ignoreCase) where TEnum : struct, Enum => GetDataCellAs(rowIndex, columnIndex, ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNextAsEnum<TEnum>() where TEnum : struct, Enum => GetNextAs(ParseEnum<TEnum>());
        public TEnum GetNextAsEnum<TEnum>(bool ignoreCase) where TEnum : struct, Enum => GetNextAs(ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNextAsEnum<TEnum>(out bool isAtEndOfAxis) where TEnum : struct, Enum => GetNextAs(ParseEnum<TEnum>(), out isAtEndOfAxis);
        public TEnum GetNextAsEnum<TEnum>(out bool isAtEndOfAxis, bool ignoreCase) where TEnum : struct, Enum => GetNextAs(ParseEnum<TEnum>(ignoreCase), out isAtEndOfAxis);
        public TEnum GetNextFromDataColumnAsEnum<TEnum>() where TEnum : struct, Enum => GetNextFromDataColumnAs(ParseEnum<TEnum>());
        public TEnum GetNextFromDataColumnAsEnum<TEnum>(bool ignoreCase) where TEnum : struct, Enum => GetNextFromDataColumnAs(ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNextFromDataColumnAsEnum<TEnum>(out bool isAtEndOfAxis) where TEnum : struct, Enum => GetNextFromDataColumnAs(ParseEnum<TEnum>(), out isAtEndOfAxis);
        public TEnum GetNextFromDataRowAsEnum<TEnum>() where TEnum : struct, Enum => GetNextFromDataRowAs(ParseEnum<TEnum>());
        public TEnum GetNextFromDataRowAsEnum<TEnum>(bool ignoreCase) where TEnum : struct, Enum => GetNextFromDataRowAs(ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNextFromDataRowAsEnum<TEnum>(out bool isAtEndOfAxis) where TEnum : struct, Enum => GetNextFromDataRowAs(ParseEnum<TEnum>(), out isAtEndOfAxis);
        public TEnum GetNextFromDataRowAsEnum<TEnum>(out bool isAtEndOfAxis, bool ignoreCase) where TEnum : struct, Enum => GetNextFromDataRowAs(ParseEnum<TEnum>(ignoreCase), out isAtEndOfAxis);

        public TEnum GetDataCell<TEnum>(int rowIndex, int columnIndex, ref TEnum value) where TEnum : struct, Enum => value = GetDataCellAs(rowIndex, columnIndex, ParseEnum<TEnum>());
        public TEnum GetDataCell<TEnum>(int rowIndex, int columnIndex, ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetDataCellAs(rowIndex, columnIndex, ParseEnum<TEnum>(ignoreCase));
        public TEnum GetCell<TEnum>(int rowIndex, int columnIndex, ref TEnum value) where TEnum : struct, Enum => value = GetCellAs(rowIndex, columnIndex, ParseEnum<TEnum>());
        public TEnum GetCell<TEnum>(int rowIndex, int columnIndex, ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetCellAs(rowIndex, columnIndex, ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNext<TEnum>(ref TEnum value) where TEnum : struct, Enum => value = GetNextAs(ParseEnum<TEnum>());
        public TEnum GetNext<TEnum>(ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetNextAs(ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNext<TEnum>(out bool isAtEndOfAxis, ref TEnum value) where TEnum : struct, Enum => value = GetNextAs(ParseEnum<TEnum>(), out isAtEndOfAxis);
        public TEnum GetNext<TEnum>(out bool isAtEndOfAxis, ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetNextAs(ParseEnum<TEnum>(ignoreCase), out isAtEndOfAxis);
        public TEnum GetNextFromDataColumn<TEnum>(ref TEnum value) where TEnum : struct, Enum => value = GetNextFromDataColumnAs(ParseEnum<TEnum>());
        public TEnum GetNextFromDataColumn<TEnum>(ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetNextFromDataColumnAs(ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNextFromDataColumn<TEnum>(out bool isAtEndOfAxis, ref TEnum value) where TEnum : struct, Enum => value = GetNextFromDataColumnAs(ParseEnum<TEnum>(), out isAtEndOfAxis);
        public TEnum GetNextFromDataColumn<TEnum>(out bool isAtEndOfAxis, ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetNextFromDataColumnAs(ParseEnum<TEnum>(ignoreCase), out isAtEndOfAxis);
        public TEnum GetNextFromDataRow<TEnum>(ref TEnum value) where TEnum : struct, Enum => value = GetNextFromDataRowAs(ParseEnum<TEnum>());
        public TEnum GetNextFromDataRow<TEnum>(ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetNextFromDataRowAs(ParseEnum<TEnum>(ignoreCase));
        public TEnum GetNextFromDataRow<TEnum>(out bool isAtEndOfAxis, ref TEnum value) where TEnum : struct, Enum => value = GetNextFromDataRowAs(ParseEnum<TEnum>(), out isAtEndOfAxis);
        public TEnum GetNextFromDataRow<TEnum>(out bool isAtEndOfAxis, ref TEnum value, bool ignoreCase) where TEnum : struct, Enum => value = GetNextFromDataRowAs(ParseEnum<TEnum>(ignoreCase), out isAtEndOfAxis);
        #endregion

    }
}
