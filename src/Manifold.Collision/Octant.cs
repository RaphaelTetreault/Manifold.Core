using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifold.Geometry
{
    internal class Octant<T>
    {
        // FIELDS
        private int currentPoint = 0;
        private bool isSubivided = false;
        private readonly Octree<T> octree;
        private readonly Octant<T>?[] octants = new Octant<T>[8];
        private readonly Point3Value<T>[] points;


        // CONSTRUCTORS
        public Octant(Octree<T> octree, int capacity)
            : this(octree, new AABB(), capacity)
        {
        }

        public Octant(Octree<T> octree, AABB bounds, int capacity)
        {
            this.octree = octree;
            Bounds = bounds;

            // Allocate once
            points = new Point3Value<T>[capacity];
            for (int i = 0; i < capacity; i++)
                points[i] = new Point3Value<T>();
        }


        // PROPERTIES
        /// <summary>
        /// 
        /// </summary>
        internal AABB Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Capacity => points.Length;

        /// <summary>
        /// 
        /// </summary>
        public bool IsAtCapacity => currentPoint == points.Length;


        // METHODS
        /// <summary>
        /// 
        /// </summary>
        public void ClearState()
        {
            // Clear bounds
            Bounds = new AABB();

            // Clear points state
            foreach (var point in points)
                point.ResetState();
            currentPoint = 0;

            // Clear octants references
            for (int i = 0; i < octants.Length; i++)
                octants[i] = null;
            isSubivided = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="bounds"></param>
        public void CollectWithin(List<T> list, AABB bounds)
        {
            bool overlaps = Bounds.Overlaps(bounds);
            if (overlaps)
            {
                // Check to see if points are inside the provided bounds
                for (int i = 0; i < currentPoint; i++)
                {
                    var point = points[i];
                    bool containsItem = bounds.ContainsPoint(point.X, point.Y, point.Z);
                    if (containsItem)
                        list.Add(point.Value);
                }

                // If we have octants, try to collect any points
                if (isSubivided)
                {
                    for (int i = 0; i < octants.Length; i++)
                        octants[i].CollectWithin(list, bounds);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Insert(float x, float y, float z, T value)
        {
            bool containsPoint = Bounds.ContainsPoint(x, y, z);
            if (!containsPoint)
            {
                return false;
            }
            else if (IsAtCapacity)
            {
                foreach (var octant in octants)
                {
                    bool success = octant.Insert(x, y, z, value);
                    if (success)
                        return true;
                }
                return false;
            }
            else // Insert item in self
            {
                // Copy values over
                points[currentPoint].Set(x, y, z, value);
                currentPoint++;

                if (IsAtCapacity)
                    Subdivide();

                return true;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void Subdivide()
        {
            var subdividedBounds = AABB.SubdivideBounds(Bounds, 2, 2, 2);
            for (int i = 0; i < 8; i++)
            {
                var bounds = subdividedBounds[i];
                var octant = octree.GetCachedOctant();
                octant.Bounds = bounds;
                octants[i] = octant;
            }
            isSubivided = true;
        }
        
    }
}
