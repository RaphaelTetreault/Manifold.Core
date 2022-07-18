using System.Text;

namespace Manifold.IO
{
    /// <summary>
    /// Simple wrapper class for string to encode and decode a C-style null-terminated
    /// string in ASCII format.
    /// </summary>
    public class AsciiCString : CString
    {
        // CONSTANTS
        private static readonly Encoding ascii = Encoding.ASCII;

        // PROPERTIES
        public override Encoding Encoding => ascii;

        // CONSTRUCTORS
        public AsciiCString() : base() { }
        public AsciiCString(string value) : base(value) { }

        // OPERATORS
        public static implicit operator string(AsciiCString cstr) => cstr.Value;
        public static implicit operator AsciiCString(string str) => new(str);
    }
}
