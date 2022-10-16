using System;
using System.Text;

namespace Manifold.IO
{
    /// <summary>
    /// Simple wrapper class for string to encode and decode a C-style null-terminated
    /// string in SHIFT_JIS format.
    /// </summary>
    [Serializable]
    public sealed class ShiftJisCString : CString
    {
        // CONSTANTS
        public const int shiftJisCodepage = 932;
        public static readonly Encoding shiftJis = Encoding.GetEncoding(shiftJisCodepage);

        // PROPERTIES
        public override Encoding Encoding
        {
            get
            {
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                return shiftJis;
            }
        }

        // CONSTRUCTORS
        public ShiftJisCString() : base() { }
        public ShiftJisCString(string value) : base(value) { }

        // OPERATORS
        public static implicit operator string(ShiftJisCString cstr) => cstr.Value;
        public static implicit operator ShiftJisCString(string str) => new(str);

    }
}
