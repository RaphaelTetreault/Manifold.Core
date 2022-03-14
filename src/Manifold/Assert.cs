using System;
using System.Collections;
using System.Collections.Generic;

namespace Manifold
{
    public static class Assert
    {
        public class AssertException : Exception
        {
            public AssertException() { }
            public AssertException(string message) : base(message) { }
            public AssertException(string message, Exception inner) : base(message, inner) { }
        }


        public static void IsTrue(bool value, string message = null)
        {
            if (value)
                return;

            if (string.IsNullOrEmpty(message))
            {
                throw new AssertException();
            }
            else
            {
                throw new AssertException(message);
            }
        }

        public static void IsFalse(bool value, string message = null)
            => IsTrue(!value, message);

        public static void ContainsNoNulls<T>(T ienumerable)
            where T : IEnumerable
        {
            foreach (var item in ienumerable)
            {
                IsTrue(item != null, $"Value null found in ienumerable type {nameof(T)}!");
            }
        }
    }
}
