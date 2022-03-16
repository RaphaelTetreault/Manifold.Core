namespace Manifold.IO
{
    public static class PaddingUtility
    {
        public static int Align(int value, int alignment) => (int)AlignSigned(value, alignment);
        public static long Align(long value, long alignment) => AlignSigned(value, alignment);
        public static uint Align(uint value, uint alignment) => (uint)AlignUnsigned(value, alignment);
        public static ulong Align(ulong value, ulong alignment) => AlignUnsigned(value, alignment);

        private static long AlignSigned(long value, long alignment)
        {
            if (value < 0)
                throw new ArgumentException($"Argument `{nameof(value)}` cannot be negative.");
            if (alignment < 1)
                throw new ArgumentException($"Argument `{nameof(alignment)}` must be greater than 1.");

            // Get number of bytes needed to aligment
            var diff = value % alignment;
            // If delta is 0, return 0; else length to align is (alignment - diff)
            var length = diff == 0 ? 0 : alignment - diff;
            return length;
        }

        private static ulong AlignUnsigned(ulong value, ulong alignment)
        {
            if (alignment < 1)
                throw new ArgumentException($"Argument `{nameof(alignment)}` must be greater than 1.");

            // Get number of bytes needed to aligment
            var diff = value % alignment;
            // If delta is 0, return 0; else length to align is (alignment - diff)
            var length = diff == 0 ? 0 : alignment - diff;
            return length;
        }
    }
}
