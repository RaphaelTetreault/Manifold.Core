using System;

namespace Manifold.Text.Tables
{
    public interface ITableInterface
    {
        public string ReadNext();
        public T ReadNext<T>(Func<string, T> parse);

        // No awareness of newline 
        // No awareness of headers? 
        public string WriteNext(string value);
        public string WriteNext<T>(T value);
    }
}
