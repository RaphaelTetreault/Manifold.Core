namespace Manifold.Geometry
{
    /// <summary>
    /// An generic octree optimized for minimal allocations that can reference a generic value <typeparamref name="T"/>
    /// in relation to a 3D point (XYZ).
    /// </summary>
    /// <typeparam name="T">The type of value to store as reference with a 3D point.</typeparam>
    public class Octree<T>
    {
        // FIELDS
        private int currentOctant;
        private readonly AABB bounds;
        private readonly Octant<T> rootOctant;
        private readonly Octant<T>[] octants;
        private readonly List<T> collectCacheList;


        // CONSTRUCTORS
        /// <summary>
        /// Create an octree to store values of type <typeparamref name="T"/> referenced by a 3D point (XYZ)
        /// within the specified <paramref name="bounds"/>.
        /// </summary>
        /// <param name="bounds">The bounds for this entire octree.</param>
        /// <param name="octantPointCapacity">How many points each octant can hold before subdividing.</param>
        /// <param name="octantCacheSize">How many octants this octree uses in total.</param>
        /// <param name="collectCacheList">
        ///     The temporary list used when querying each octant to collect <typeparamref name="T"/> elements.
        ///     Cached so not to be allocating a new list each query each frame.
        ///     Optimally, list capacity should be set in a way that will not grow.
        /// </param>
        public Octree(AABB bounds, int octantPointCapacity, int octantCacheSize, List<T> collectCacheList)
        {
            this.collectCacheList = collectCacheList;

            this.bounds = bounds;
            rootOctant = new Octant<T>(this, bounds, octantPointCapacity);

            // Allocate octants once
            octants = new Octant<T>[octantCacheSize];
            for (int i = 0; i < octantCacheSize; i++)
                octants[i] = new Octant<T>(this, octantPointCapacity);
        }


        public int CurrentOctant => currentOctant;
        public AABB Bounds => bounds;



        // METHODS
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public T[] CollectWithin(AABB bounds)
        {
            collectCacheList.Clear();
            rootOctant.CollectWithin(collectCacheList, bounds);
            return collectCacheList.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        internal Octant<T> GetCachedOctant()
        {
            bool hasOctants = currentOctant < octants.Length;
            if (!hasOctants)
                throw new IndexOutOfRangeException();

            var octant = octants[currentOctant];
            currentOctant++;

            return octant;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="value"></param>
        public void Insert(float x, float y, float z, T value)
        {
            rootOctant.Insert(x, y, z, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetState()
        {
            // Clear refs, but maintain bounding box
            rootOctant.ClearState();
            rootOctant.Bounds = bounds;

            // Clear state for all cached octants
            foreach (var octant in octants)
                octant.ClearState();
            currentOctant = 0;
        }

    }
}