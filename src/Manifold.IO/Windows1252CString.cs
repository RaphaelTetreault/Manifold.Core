using System.Text;

namespace Manifold.IO;

public class Windows1252CString : CString
{
    // CONSTANTS
    public static readonly Encoding windows1252 = Encoding.GetEncoding(codepage: 1252);

    // PROPERTIES
    public override Encoding Encoding => windows1252;

    // CONSTRUCTORS
    public Windows1252CString() : base() { }
    public Windows1252CString(string value) : base(value) { }

    // OPERATORS
    public static implicit operator string(Windows1252CString cstr) => cstr.Value;
    public static implicit operator Windows1252CString(string str) => new(str);
}
