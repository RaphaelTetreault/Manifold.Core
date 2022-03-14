using Manifold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifold.IO
{
    public static class AssertBin
    {

        public static void ValidateReferencePointer(object reference, IPointer pointer)
        {
            bool referenceIsNull = reference == null;
            // There is an issue if one of the two are set, but when both are the same, no issue
            bool invalidState = referenceIsNull ^ pointer.IsNull;

            var msg = referenceIsNull
                ? $"Reference is null but pointer is not! Ptr(0x{pointer.Address:x8})"
                : $"Reference exists but pointer is null!";

            Assert.IsFalse(invalidState, msg);
        }


        public static void ReferencePointer(IBinaryAddressable reference, IPointer pointer)
        {
            // Validates reference-pointer null/instance connection
            ValidateReferencePointer(reference, pointer);

            // Validates that pointer and reference share the same address
            if (reference != null)
            {
                var refPtr = reference.GetPointer();
                var isSamePointer = pointer.Address == refPtr.address;
                const string msg = "Reference's pointer and supplied pointer do not match!";
                Assert.IsTrue(isSamePointer, msg);
            }
        }

        public static void ReferencePointer(IBinaryAddressable[] reference, ArrayPointer pointer)
        {
            // Validates reference-pointer null/instance connection
            ValidateReferencePointer(reference, pointer);

            // Validates that pointer and reference share the same address
            if (reference != null && reference.Length > 0)
            {
                var refPtr = reference[0].GetPointer();
                var isSamePointer = pointer.address == refPtr.address;
                Assert.IsTrue(isSamePointer);
            }
        }

    }
}
