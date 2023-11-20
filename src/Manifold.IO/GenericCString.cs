using System.Text;

namespace Manifold.IO
{
    public class GenericCString : CString
    {
        // PROPERTIES
        public override Encoding Encoding { get; }

        // CONSTRUCTORS
        public GenericCString(Encoding encoding) : base()
        {
            Encoding = encoding;
        }
        public GenericCString(Encoding encoding, string value) : base(value)
        {
            Encoding = encoding;
        }

        // OPERATORS
        public static implicit operator string(GenericCString cstr) => cstr.Value;
    }
}
