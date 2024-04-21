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
        private static readonly ParseType<float> ParseFloat = float.Parse;
        private static readonly ParseType<double> ParseDouble = double.Parse;
        private static readonly ParseType<decimal> ParseDecimal = decimal.Parse;
        private static readonly ParseType<DateTime> ParseDateTime = DateTime.Parse;

        private static ParseType<byte> ParseUInt8WithStyle(NumberStyles numberStyles) => (string str) => { return byte.Parse(str, numberStyles); };
        private static ParseType<ushort> ParseUInt16WithStyle(NumberStyles numberStyles) => (string str) => { return ushort.Parse(str, numberStyles); };
        private static ParseType<uint> ParseUInt32WithStyle(NumberStyles numberStyles) => (string str) => { return uint.Parse(str, numberStyles); };
        private static ParseType<ulong> ParseUInt64WithStyle(NumberStyles numberStyles) => (string str) => { return ulong.Parse(str, numberStyles); };
        private static ParseType<UInt128> ParseUInt128WithStyle(NumberStyles numberStyles) => (string str) => { return UInt128.Parse(str, numberStyles); };
        private static ParseType<sbyte> ParseInt8WithStyle(NumberStyles numberStyles) => (string str) => { return sbyte.Parse(str, numberStyles); };
        private static ParseType<short> ParseInt16WithStyle(NumberStyles numberStyles) => (string str) => { return short.Parse(str, numberStyles); };
        private static ParseType<int> ParseInt32WithStyle(NumberStyles numberStyles) => (string str) => { return int.Parse(str, numberStyles); };
        private static ParseType<long> ParseInt64WithStyle(NumberStyles numberStyles) => (string str) => { return long.Parse(str, numberStyles); };
        private static ParseType<Int128> ParseInt128WithStyle(NumberStyles numberStyles) => (string str) => { return Int128.Parse(str, numberStyles); };
        private static ParseType<float> ParseFloatWithStyle(NumberStyles numberStyles) => (string str) => { return float.Parse(str, numberStyles); };
        private static ParseType<double> ParseDoubleWithStyle(NumberStyles numberStyles) => (string str) => { return double.Parse(str, numberStyles); };
        private static ParseType<decimal> ParseDecimalWithStyle(NumberStyles numberStyles) => (string str) => { return decimal.Parse(str, numberStyles); };
        private static ParseType<DateTime> ParseDateTimeWithStyle(IFormatProvider dateTimeFormatProvider) => (string str) => { return DateTime.Parse(str, dateTimeFormatProvider); };


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
        public bool GetNextFromDataRow(ref bool value) => value = GetNextFromDataRowAs(ParseBool);
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

        // TODO: organize the rest of this code...



        //public ushort GetCellAsUShort(int rowIndex, int columnIndex) => GetCellAsUInt16(rowIndex, columnIndex);
        public uint GetCellAsUInt(int rowIndex, int columnIndex) => GetCellAsUInt32(rowIndex, columnIndex);
        public ulong GetCellAsULong(int rowIndex, int columnIndex) => GetCellAsUInt64(rowIndex, columnIndex);
        //public ushort GetCellAsUInt16(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt16);
        public uint GetCellAsUInt32(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt32);
        public ulong GetCellAsUInt64(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt64);
        public UInt128 GetCellAsUInt128(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseUInt128);
        public sbyte GetCellAsSByte(int rowIndex, int columnIndex) => GetCellAsInt8(rowIndex, columnIndex);
        public short GetCellAsShort(int rowIndex, int columnIndex) => GetCellAsInt16(rowIndex, columnIndex);
        public int GetCellAsInt(int rowIndex, int columnIndex) => GetCellAsInt32(rowIndex, columnIndex);
        public long GetCellAsLong(int rowIndex, int columnIndex) => GetCellAsInt64(rowIndex, columnIndex);
        public sbyte GetCellAsInt8(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt8);
        public short GetCellAsInt16(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt16);
        public int GetCellAsInt32(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt32);
        public long GetCellAsInt64(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt64);
        public Int128 GetCellAsInt128(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseInt128);
        public float GetCellAsFloat(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseFloat);
        //public float GetCellAsFloat(int rowIndex, int columnIndex, NumberStyles numberStyles) => GetCellAs(rowIndex, columnIndex, (string str) => { return float.Parse(str, numberStyles); });
        public double GetCellAsDouble(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseDouble);
        public decimal GetCellAsDecimal(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseDecimal);
        //public DateTime GetCellAsDateTime(int rowIndex, int columnIndex) => GetCellAs(rowIndex, columnIndex, ParseDateTime);


        // GET DATA CELL AS
        //public ushort GetDataCellAsUShort(int rowIndex, int columnIndex) => GetDataCellAsUInt16(rowIndex, columnIndex);
        public uint GetDataCellAsUInt(int rowIndex, int columnIndex) => GetDataCellAsUInt32(rowIndex, columnIndex);
        public ulong GetDataCellAsULong(int rowIndex, int columnIndex) => GetDataCellAsUInt64(rowIndex, columnIndex);
        //public ushort GetDataCellAsUInt16(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt16);
        public uint GetDataCellAsUInt32(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt32);
        public ulong GetDataCellAsUInt64(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt64);
        public UInt128 GetDataCellAsUInt128(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseUInt128);
        public sbyte GetDataCellAsSByte(int rowIndex, int columnIndex) => GetDataCellAsInt8(rowIndex, columnIndex);
        public short GetDataCellAsShort(int rowIndex, int columnIndex) => GetDataCellAsInt16(rowIndex, columnIndex);
        public int GetDataCellAsInt(int rowIndex, int columnIndex) => GetDataCellAsInt32(rowIndex, columnIndex);
        public long GetDataCellAsLong(int rowIndex, int columnIndex) => GetDataCellAsInt64(rowIndex, columnIndex);
        public sbyte GetDataCellAsInt8(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt8);
        public short GetDataCellAsInt16(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt16);
        public int GetDataCellAsInt32(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt32);
        public long GetDataCellAsInt64(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt64);
        public Int128 GetDataCellAsInt128(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseInt128);
        public float GetDataCellAsFloat(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseFloat);
        public double GetDataCellAsDouble(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseDouble);
        public decimal GetDataCellAsDecimal(int rowIndex, int columnIndex) => GetDataCellAs(rowIndex, columnIndex, ParseDecimal);

        // GET NEXT AS
        //public ushort GetNextAsUShort() => GetNextAsUInt16();
        public uint GetNextAsUInt() => GetNextAsUInt32();
        public ulong GetNextAsULong() => GetNextAsUInt64();
        //public ushort GetNextAsUInt16() => GetNextAs(ParseUInt16);
        public uint GetNextAsUInt32() => GetNextAs(ParseUInt32);
        public ulong GetNextAsUInt64() => GetNextAs(ParseUInt64);
        public UInt128 GetNextAsUInt128() => GetNextAs(ParseUInt128);
        public sbyte GetNextAsSByte() => GetNextAsInt8();
        public short GetNextAsShort() => GetNextAsInt16();
        public int GetNextAsInt() => GetNextAsInt32();
        public long GetNextAsLong() => GetNextAsInt64();
        public sbyte GetNextAsInt8() => GetNextAs(ParseInt8);
        public short GetNextAsInt16() => GetNextAs(ParseInt16);
        public int GetNextAsInt32() => GetNextAs(ParseInt32);
        public long GetNextAsInt64() => GetNextAs(ParseInt64);
        public Int128 GetNextAsInt128() => GetNextAs(ParseInt128);
        public float GetNextAsFloat() => GetNextAs(ParseFloat);
        public double GetNextAsDouble() => GetNextAs(ParseDouble);
        public decimal GetNextAsDecimal() => GetNextAs(ParseDecimal);

        // GET NEXT AS (with out bool)
        //public ushort GetNextAsUShort(out bool isAtEndOfAxis) => GetNextAsUInt16(out isAtEndOfAxis);
        public uint GetNextAsUInt(out bool isAtEndOfAxis) => GetNextAsUInt32(out isAtEndOfAxis);
        public ulong GetNextAsULong(out bool isAtEndOfAxis) => GetNextAsUInt64(out isAtEndOfAxis);
        //public ushort GetNextAsUInt16(out bool isAtEndOfAxis) => GetNextAs(ParseUInt16, out isAtEndOfAxis);
        public uint GetNextAsUInt32(out bool isAtEndOfAxis) => GetNextAs(ParseUInt32, out isAtEndOfAxis);
        public ulong GetNextAsUInt64(out bool isAtEndOfAxis) => GetNextAs(ParseUInt64, out isAtEndOfAxis);
        public UInt128 GetNextAsUInt128(out bool isAtEndOfAxis) => GetNextAs(ParseUInt128, out isAtEndOfAxis);
        public sbyte GetNextAsSByte(out bool isAtEndOfAxis) => GetNextAsInt8(out isAtEndOfAxis);
        public short GetNextAsShort(out bool isAtEndOfAxis) => GetNextAsInt16(out isAtEndOfAxis);
        public int GetNextAsInt(out bool isAtEndOfAxis) => GetNextAsInt32(out isAtEndOfAxis);
        public long GetNextAsLong(out bool isAtEndOfAxis) => GetNextAsInt64(out isAtEndOfAxis);
        public sbyte GetNextAsInt8(out bool isAtEndOfAxis) => GetNextAs(ParseInt8, out isAtEndOfAxis);
        public short GetNextAsInt16(out bool isAtEndOfAxis) => GetNextAs(ParseInt16, out isAtEndOfAxis);
        public int GetNextAsInt32(out bool isAtEndOfAxis) => GetNextAs(ParseInt32, out isAtEndOfAxis);
        public long GetNextAsInt64(out bool isAtEndOfAxis) => GetNextAs(ParseInt64, out isAtEndOfAxis);
        public Int128 GetNextAsInt128(out bool isAtEndOfAxis) => GetNextAs(ParseInt128, out isAtEndOfAxis);
        public float GetNextAsFloat(out bool isAtEndOfAxis) => GetNextAs(ParseFloat, out isAtEndOfAxis);
        public double GetNextAsDouble(out bool isAtEndOfAxis) => GetNextAs(ParseDouble, out isAtEndOfAxis);
        public decimal GetNextAsDecimal(out bool isAtEndOfAxis) => GetNextAs(ParseDecimal, out isAtEndOfAxis);

        //
        //public ushort GetNextFromDataColumnAsUShort() => GetNextFromDataColumnAsUInt16();
        public uint GetNextFromDataColumnAsUInt() => GetNextFromDataColumnAsUInt32();
        public ulong GetNextFromDataColumnAsULong() => GetNextFromDataColumnAsUInt64();
        //public ushort GetNextFromDataColumnAsUInt16() => GetNextFromDataColumnAs(ParseUInt16);
        public uint GetNextFromDataColumnAsUInt32() => GetNextFromDataColumnAs(ParseUInt32);
        public ulong GetNextFromDataColumnAsUInt64() => GetNextFromDataColumnAs(ParseUInt64);
        public UInt128 GetNextFromDataColumnAsUInt128() => GetNextFromDataColumnAs(ParseUInt128);
        public sbyte GetNextFromDataColumnAsSByte() => GetNextFromDataColumnAsInt8();
        public short GetNextFromDataColumnAsShort() => GetNextFromDataColumnAsInt16();
        public int GetNextFromDataColumnAsInt() => GetNextFromDataColumnAsInt32();
        public long GetNextFromDataColumnAsLong() => GetNextFromDataColumnAsInt64();
        public sbyte GetNextFromDataColumnAsInt8() => GetNextFromDataColumnAs(ParseInt8);
        public short GetNextFromDataColumnAsInt16() => GetNextFromDataColumnAs(ParseInt16);
        public int GetNextFromDataColumnAsInt32() => GetNextFromDataColumnAs(ParseInt32);
        public long GetNextFromDataColumnAsInt64() => GetNextFromDataColumnAs(ParseInt64);
        public Int128 GetNextFromDataColumnAsInt128() => GetNextFromDataColumnAs(ParseInt128);
        public float GetNextFromDataColumnAsFloat() => GetNextFromDataColumnAs(ParseFloat);
        public double GetNextFromDataColumnAsDouble() => GetNextFromDataColumnAs(ParseDouble);
        public decimal GetNextFromDataColumnAsDecimal() => GetNextFromDataColumnAs(ParseDecimal);
        // 
        //public ushort GetNextFromDataColumnAsUShort(out bool isAtEndOfAxis) => GetNextFromDataColumnAsUInt16(out isAtEndOfAxis);
        public uint GetNextFromDataColumnAsUInt(out bool isAtEndOfAxis) => GetNextFromDataColumnAsUInt32(out isAtEndOfAxis);
        public ulong GetNextFromDataColumnAsULong(out bool isAtEndOfAxis) => GetNextFromDataColumnAsUInt64(out isAtEndOfAxis);
        //public ushort GetNextFromDataColumnAsUInt16(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt16, out isAtEndOfAxis);
        public uint GetNextFromDataColumnAsUInt32(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt32, out isAtEndOfAxis);
        public ulong GetNextFromDataColumnAsUInt64(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt64, out isAtEndOfAxis);
        public UInt128 GetNextFromDataColumnAsUInt128(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseUInt128, out isAtEndOfAxis);
        public sbyte GetNextFromDataColumnAsSByte(out bool isAtEndOfAxis) => GetNextFromDataColumnAsInt8(out isAtEndOfAxis);
        public short GetNextFromDataColumnAsShort(out bool isAtEndOfAxis) => GetNextFromDataColumnAsInt16(out isAtEndOfAxis);
        public int GetNextFromDataColumnAsInt(out bool isAtEndOfAxis) => GetNextFromDataColumnAsInt32(out isAtEndOfAxis);
        public long GetNextFromDataColumnAsLong(out bool isAtEndOfAxis) => GetNextFromDataColumnAsInt64(out isAtEndOfAxis);
        public sbyte GetNextFromDataColumnAsInt8(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt8, out isAtEndOfAxis);
        public short GetNextFromDataColumnAsInt16(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt16, out isAtEndOfAxis);
        public int GetNextFromDataColumnAsInt32(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt32, out isAtEndOfAxis);
        public long GetNextFromDataColumnAsInt64(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt64, out isAtEndOfAxis);
        public Int128 GetNextFromDataColumnAsInt128(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseInt128, out isAtEndOfAxis);
        public float GetNextFromDataColumnAsFloat(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseFloat, out isAtEndOfAxis);
        public double GetNextFromDataColumnAsDouble(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseDouble, out isAtEndOfAxis);
        public decimal GetNextFromDataColumnAsDecimal(out bool isAtEndOfAxis) => GetNextFromDataColumnAs(ParseDecimal, out isAtEndOfAxis);

        //
        //public ushort GetNextFromDataRowAsUShort() => GetNextFromDataRowAsUInt16();
        public uint GetNextFromDataRowAsUInt() => GetNextFromDataRowAsUInt32();
        public ulong GetNextFromDataRowAsULong() => GetNextFromDataRowAsUInt64();
        //public ushort GetNextFromDataRowAsUInt16() => GetNextFromDataRowAs(ParseUInt16);
        public uint GetNextFromDataRowAsUInt32() => GetNextFromDataRowAs(ParseUInt32);
        public ulong GetNextFromDataRowAsUInt64() => GetNextFromDataRowAs(ParseUInt64);
        public UInt128 GetNextFromDataRowAsUInt128() => GetNextFromDataRowAs(ParseUInt128);
        public sbyte GetNextFromDataRowAsSByte() => GetNextFromDataRowAsInt8();
        public short GetNextFromDataRowAsShort() => GetNextFromDataRowAsInt16();
        public int GetNextFromDataRowAsInt() => GetNextFromDataRowAsInt32();
        public long GetNextFromDataRowAsLong() => GetNextFromDataRowAsInt64();
        public sbyte GetNextFromDataRowAsInt8() => GetNextFromDataRowAs(ParseInt8);
        public short GetNextFromDataRowAsInt16() => GetNextFromDataRowAs(ParseInt16);
        public int GetNextFromDataRowAsInt32() => GetNextFromDataRowAs(ParseInt32);
        public long GetNextFromDataRowAsInt64() => GetNextFromDataRowAs(ParseInt64);
        public Int128 GetNextFromDataRowAsInt128() => GetNextFromDataRowAs(ParseInt128);
        public float GetNextFromDataRowAsFloat() => GetNextFromDataRowAs(ParseFloat);
        public double GetNextFromDataRowAsDouble() => GetNextFromDataRowAs(ParseDouble);
        public decimal GetNextFromDataRowAsDecimal() => GetNextFromDataRowAs(ParseDecimal);
        // 
        //public ushort GetNextFromDataRowAsUShort(out bool isAtEndOfAxis) => GetNextFromDataRowAsUInt16(out isAtEndOfAxis);
        public uint GetNextFromDataRowAsUInt(out bool isAtEndOfAxis) => GetNextFromDataRowAsUInt32(out isAtEndOfAxis);
        public ulong GetNextFromDataRowAsULong(out bool isAtEndOfAxis) => GetNextFromDataRowAsUInt64(out isAtEndOfAxis);
        //public ushort GetNextFromDataRowAsUInt16(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt16, out isAtEndOfAxis);
        public uint GetNextFromDataRowAsUInt32(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt32, out isAtEndOfAxis);
        public ulong GetNextFromDataRowAsUInt64(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt64, out isAtEndOfAxis);
        public UInt128 GetNextFromDataRowAsUInt128(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseUInt128, out isAtEndOfAxis);
        public sbyte GetNextFromDataRowAsSByte(out bool isAtEndOfAxis) => GetNextFromDataRowAsInt8(out isAtEndOfAxis);
        public short GetNextFromDataRowAsShort(out bool isAtEndOfAxis) => GetNextFromDataRowAsInt16(out isAtEndOfAxis);
        public int GetNextFromDataRowAsInt(out bool isAtEndOfAxis) => GetNextFromDataRowAsInt32(out isAtEndOfAxis);
        public long GetNextFromDataRowAsLong(out bool isAtEndOfAxis) => GetNextFromDataRowAsInt64(out isAtEndOfAxis);
        public sbyte GetNextFromDataRowAsInt8(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt8, out isAtEndOfAxis);
        public short GetNextFromDataRowAsInt16(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt16, out isAtEndOfAxis);
        public int GetNextFromDataRowAsInt32(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt32, out isAtEndOfAxis);
        public long GetNextFromDataRowAsInt64(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt64, out isAtEndOfAxis);
        public Int128 GetNextFromDataRowAsInt128(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseInt128, out isAtEndOfAxis);
        public float GetNextFromDataRowAsFloat(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseFloat, out isAtEndOfAxis);
        public double GetNextFromDataRowAsDouble(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseDouble, out isAtEndOfAxis);
        public decimal GetNextFromDataRowAsDecimal(out bool isAtEndOfAxis) => GetNextFromDataRowAs(ParseDecimal, out isAtEndOfAxis);


        /////////

        //public ushort GetCell(int rowIndex, int columnIndex, ref ushort value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt16);
        public uint GetCell(int rowIndex, int columnIndex, ref uint value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt32);
        public ulong GetCell(int rowIndex, int columnIndex, ref ulong value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt64);
        public UInt128 GetCell(int rowIndex, int columnIndex, ref UInt128 value) => value = GetCellAs(rowIndex, columnIndex, ParseUInt128);
        public sbyte GetCell(int rowIndex, int columnIndex, ref sbyte value) => value = GetCellAs(rowIndex, columnIndex, ParseInt8);
        public short GetCell(int rowIndex, int columnIndex, ref short value) => value = GetCellAs(rowIndex, columnIndex, ParseInt16);
        public int GetCell(int rowIndex, int columnIndex, ref int value) => value = GetCellAs(rowIndex, columnIndex, ParseInt32);
        public long GetCell(int rowIndex, int columnIndex, ref long value) => value = GetCellAs(rowIndex, columnIndex, ParseInt64);
        public Int128 GetCell(int rowIndex, int columnIndex, ref Int128 value) => value = GetCellAs(rowIndex, columnIndex, ParseInt128);
        public float GetCell(int rowIndex, int columnIndex, ref float value) => value = GetCellAs(rowIndex, columnIndex, ParseFloat);
        public double GetCell(int rowIndex, int columnIndex, ref double value) => value = GetCellAs(rowIndex, columnIndex, ParseDouble);
        public decimal GetCell(int rowIndex, int columnIndex, ref decimal value) => value = GetCellAs(rowIndex, columnIndex, ParseDecimal);

        // GET DATA CELL AS
        //public ushort GetDataCell(int rowIndex, int columnIndex, ref ushort value) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt16);
        public uint GetDataCell(int rowIndex, int columnIndex, ref uint value) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt32);
        public ulong GetDataCell(int rowIndex, int columnIndex, ref ulong value) => value = GetDataCellAs(rowIndex, columnIndex, ParseUInt64);
        public sbyte GetDataCell(int rowIndex, int columnIndex, ref sbyte value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt8);
        public short GetDataCell(int rowIndex, int columnIndex, ref short value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt16);
        public int GetDataCell(int rowIndex, int columnIndex, ref int value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt32);
        public long GetDataCell(int rowIndex, int columnIndex, ref long value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt64);
        public Int128 GetDataCell(int rowIndex, int columnIndex, ref Int128 value) => value = GetDataCellAs(rowIndex, columnIndex, ParseInt128);
        public float GetDataCell(int rowIndex, int columnIndex, ref float value) => value = GetDataCellAs(rowIndex, columnIndex, ParseFloat);
        public double GetDataCell(int rowIndex, int columnIndex, ref double value) => value = GetDataCellAs(rowIndex, columnIndex, ParseDouble);
        public decimal GetDataCell(int rowIndex, int columnIndex, ref decimal value) => value = GetDataCellAs(rowIndex, columnIndex, ParseDecimal);

        // GET NEXT AS
        //public ushort GetNext(ref ushort value) => value = GetNextAs(ParseUInt16);
        public uint GetNext(ref uint value) => value = GetNextAs(ParseUInt32);
        public ulong GetNext(ref ulong value) => value = GetNextAs(ParseUInt64);
        public UInt128 GetNext(ref UInt128 value) => value = GetNextAs(ParseUInt128);
        public sbyte GetNext(ref sbyte value) => value = GetNextAs(ParseInt8);
        public short GetNext(ref short value) => value = GetNextAs(ParseInt16);
        public int GetNext(ref int value) => value = GetNextAs(ParseInt32);
        public long GetNext(ref long value) => value = GetNextAs(ParseInt64);
        public Int128 GetNext(ref Int128 value) => value = GetNextAs(ParseInt128);
        public float GetNext(ref float value) => value = GetNextAs(ParseFloat);
        public double GetNext(ref double value) => value = GetNextAs(ParseDouble);
        public decimal GetNext(ref decimal value) => value = GetNextAs(ParseDecimal);

        // GET NEXT AS (with out bool)

        //public ushort GetNext(out bool isAtEndOfAxis, ref ushort value) => value = GetNextAs(ParseUInt16, out isAtEndOfAxis);
        public uint GetNext(out bool isAtEndOfAxis, ref uint value) => value = GetNextAs(ParseUInt32, out isAtEndOfAxis);
        public ulong GetNext(out bool isAtEndOfAxis, ref ulong value) => value = GetNextAs(ParseUInt64, out isAtEndOfAxis);
        public UInt128 GetNext(out bool isAtEndOfAxis, ref UInt128 value) => value = GetNextAs(ParseUInt128, out isAtEndOfAxis);
        public sbyte GetNext(out bool isAtEndOfAxis, ref sbyte value) => value = GetNextAs(ParseInt8, out isAtEndOfAxis);
        public short GetNext(out bool isAtEndOfAxis, ref short value) => value = GetNextAs(ParseInt16, out isAtEndOfAxis);
        public int GetNext(out bool isAtEndOfAxis, ref int value) => value = GetNextAs(ParseInt32, out isAtEndOfAxis);
        public long GetNext(out bool isAtEndOfAxis, ref long value) => value = GetNextAs(ParseInt64, out isAtEndOfAxis);
        public Int128 GetNext(out bool isAtEndOfAxis, ref Int128 value) => value = GetNextAs(ParseInt128, out isAtEndOfAxis);
        public float GetNext(out bool isAtEndOfAxis, ref float value) => value = GetNextAs(ParseFloat, out isAtEndOfAxis);
        public double GetNext(out bool isAtEndOfAxis, ref double value) => value = GetNextAs(ParseDouble, out isAtEndOfAxis);
        public decimal GetNext(out bool isAtEndOfAxis, ref decimal value) => value = GetNextAs(ParseDecimal, out isAtEndOfAxis);

        //
        //public ushort GetNextFromDataColumn(ref ushort value) => value = GetNextFromDataColumnAs(ParseUInt16);
        public uint GetNextFromDataColumn(ref uint value) => value = GetNextFromDataColumnAs(ParseUInt32);
        public ulong GetNextFromDataColumn(ref ulong value) => value = GetNextFromDataColumnAs(ParseUInt64);
        public UInt128 GetNextFromDataColumn(ref UInt128 value) => value = GetNextFromDataColumnAs(ParseUInt128);
        public sbyte GetNextFromDataColumn(ref sbyte value) => value = GetNextFromDataColumnAs(ParseInt8);
        public short GetNextFromDataColumn(ref short value) => value = GetNextFromDataColumnAs(ParseInt16);
        public int GetNextFromDataColumn(ref int value) => value = GetNextFromDataColumnAs(ParseInt32);
        public long GetNextFromDataColumn(ref long value) => value = GetNextFromDataColumnAs(ParseInt64);
        public Int128 GetNextFromDataColumn(ref Int128 value) => value = GetNextFromDataColumnAs(ParseInt128);
        public float GetNextFromDataColumn(ref float value) => value = GetNextFromDataColumnAs(ParseFloat);
        public double GetNextFromDataColumn(ref double value) => value = GetNextFromDataColumnAs(ParseDouble);
        public decimal GetNextFromDataColumn(ref decimal value) => value = GetNextFromDataColumnAs(ParseDecimal);
        // 
        //public ushort GetNextFromDataColumn(out bool isAtEndOfAxis, ref ushort value) => value = GetNextFromDataColumnAs(ParseUInt16, out isAtEndOfAxis);
        public uint GetNextFromDataColumn(out bool isAtEndOfAxis, ref uint value) => value = GetNextFromDataColumnAs(ParseUInt32, out isAtEndOfAxis);
        public ulong GetNextFromDataColumn(out bool isAtEndOfAxis, ref ulong value) => value = GetNextFromDataColumnAs(ParseUInt64, out isAtEndOfAxis);
        public UInt128 GetNextFromDataColumn(out bool isAtEndOfAxis, ref UInt128 value) => value = GetNextFromDataColumnAs(ParseUInt128, out isAtEndOfAxis);
        public sbyte GetNextFromDataColumn(out bool isAtEndOfAxis, ref sbyte value) => value = GetNextFromDataColumnAs(ParseInt8, out isAtEndOfAxis);
        public short GetNextFromDataColumn(out bool isAtEndOfAxis, ref short value) => value = GetNextFromDataColumnAs(ParseInt16, out isAtEndOfAxis);
        public int GetNextFromDataColumn(out bool isAtEndOfAxis, ref int value) => value = GetNextFromDataColumnAs(ParseInt32, out isAtEndOfAxis);
        public long GetNextFromDataColumn(out bool isAtEndOfAxis, ref long value) => value = GetNextFromDataColumnAs(ParseInt64, out isAtEndOfAxis);
        public Int128 GetNextFromDataColumn(out bool isAtEndOfAxis, ref Int128 value) => value = GetNextFromDataColumnAs(ParseInt128, out isAtEndOfAxis);
        public float GetNextFromDataColumn(out bool isAtEndOfAxis, ref float value) => value = GetNextFromDataColumnAs(ParseFloat, out isAtEndOfAxis);
        public double GetNextFromDataColumn(out bool isAtEndOfAxis, ref double value) => value = GetNextFromDataColumnAs(ParseDouble, out isAtEndOfAxis);
        public decimal GetNextFromDataColumn(out bool isAtEndOfAxis, ref decimal value) => value = GetNextFromDataColumnAs(ParseDecimal, out isAtEndOfAxis);

        //
        //public ushort GetNextFromDataRow(ref ushort value) => value = GetNextFromDataRowAs(ParseUInt16);
        public uint GetNextFromDataRow(ref uint value) => value = GetNextFromDataRowAs(ParseUInt32);
        public ulong GetNextFromDataRow(ref ulong value) => value = GetNextFromDataRowAs(ParseUInt64);
        public UInt128 GetNextFromDataRow(ref UInt128 value) => value = GetNextFromDataRowAs(ParseUInt128);
        public sbyte GetNextFromDataRow(ref sbyte value) => value = GetNextFromDataRowAs(ParseInt8);
        public short GetNextFromDataRow(ref short value) => value = GetNextFromDataRowAs(ParseInt16);
        public int GetNextFromDataRow(ref int value) => value = GetNextFromDataRowAs(ParseInt32);
        public long GetNextFromDataRow(ref long value) => value = GetNextFromDataRowAs(ParseInt64);
        public Int128 GetNextFromDataRow(ref Int128 value) => value = GetNextFromDataRowAs(ParseInt128);
        public float GetNextFromDataRow(ref float value) => value = GetNextFromDataRowAs(ParseFloat);
        public double GetNextFromDataRow(ref double value) => value = GetNextFromDataRowAs(ParseDouble);
        public decimal GetNextFromDataRow(ref decimal value) => value = GetNextFromDataRowAs(ParseDecimal);
        // 
        //public ushort GetNextFromDataRow(out bool isAtEndOfAxis, ref ushort value) => value = GetNextFromDataRowAs(ParseUInt16, out isAtEndOfAxis);
        public uint GetNextFromDataRow(out bool isAtEndOfAxis, ref uint value) => value = GetNextFromDataRowAs(ParseUInt32, out isAtEndOfAxis);
        public ulong GetNextFromDataRow(out bool isAtEndOfAxis, ref ulong value) => value = GetNextFromDataRowAs(ParseUInt64, out isAtEndOfAxis);
        public UInt128 GetNextFromDataRow(out bool isAtEndOfAxis, ref UInt128 value) => value = GetNextFromDataRowAs(ParseUInt128, out isAtEndOfAxis);
        public sbyte GetNextFromDataRow(out bool isAtEndOfAxis, ref sbyte value) => value = GetNextFromDataRowAs(ParseInt8, out isAtEndOfAxis);
        public short GetNextFromDataRow(out bool isAtEndOfAxis, ref short value) => value = GetNextFromDataRowAs(ParseInt16, out isAtEndOfAxis);
        public int GetNextFromDataRow(out bool isAtEndOfAxis, ref int value) => value = GetNextFromDataRowAs(ParseInt32, out isAtEndOfAxis);
        public long GetNextFromDataRow(out bool isAtEndOfAxis, ref long value) => value = GetNextFromDataRowAs(ParseInt64, out isAtEndOfAxis);
        public Int128 GetNextFromDataRow(out bool isAtEndOfAxis, ref Int128 value) => value = GetNextFromDataRowAs(ParseInt128, out isAtEndOfAxis);
        public float GetNextFromDataRow(out bool isAtEndOfAxis, ref float value) => value = GetNextFromDataRowAs(ParseFloat, out isAtEndOfAxis);
        public double GetNextFromDataRow(out bool isAtEndOfAxis, ref double value) => value = GetNextFromDataRowAs(ParseDouble, out isAtEndOfAxis);
        public decimal GetNextFromDataRow(out bool isAtEndOfAxis, ref decimal value) => value = GetNextFromDataRowAs(ParseDecimal, out isAtEndOfAxis);

    }
}
