using System;
using System.IO;

namespace Manifold.IO
{
    public interface ITableSerializable
    {
        public void Deserialize(XTable table); // TODO: contrain further, interface?
        public void Serialize(XTable table);
    }

    public interface ITableXXX
    {
        // TODO: write many types no parse passed (ie: just contraints?)
        public string ReadNext();
        public T ReadNext<T>(Func<string,T> parse);

        // No awareness of newline
        // No awareness of headers?
        public string WriteNext(string value);
        public string WriteNext<T>(T value);
    }
}
