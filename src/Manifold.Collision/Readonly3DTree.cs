using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifold.Geometry
{
    public class Readonly3DTree<T>
    {
        // Caching function pointers in cyclic
        internal delegate bool CompareComponent(Point3Value<T> p, float x, float y, float z);
        internal readonly ReadOnlyCyclicList<CompareComponent> CompareComponentValue
            = new ReadOnlyCyclicList<CompareComponent>(new CompareComponent[] { CompareGreaterThanX, CompareGreaterThanY, CompareGreaterThanZ });

        internal delegate float SquareBoundaryDistance(Point3Value<T> p, float x, float y, float z);
        internal readonly ReadOnlyCyclicList<SquareBoundaryDistance> SquareBoundary
            = new ReadOnlyCyclicList<SquareBoundaryDistance>(new SquareBoundaryDistance[] { SquareX, SquareY, SquareZ });

        private int currentCacheNode = 0;
        private readonly Readonly3DTreeNode<T> root;
        private readonly Readonly3DTreeNode<T>[] cache;
        //private readonly Point3Value<T>[] points;

        internal float CurrentBestSquareDistance { get; set; }
        internal Point3Value<T>? CurrentBestPoint { get; set; }
        internal ReadonlyCyclicIndex FunctionIndex { get; } = new ReadonlyCyclicIndex(3);


        public Readonly3DTree(int cacheSize)
        {
            root = new Readonly3DTreeNode<T>(this);

            cache = new Readonly3DTreeNode<T>[cacheSize];
            for (int i = 0; i < cacheSize; i++)
                cache[i] = new Readonly3DTreeNode<T>(this);

            //points = new Point3Value<T>[cacheSize];
            //for (int i = 0; i < cacheSize; i++)
            //    points[i] = new Point3Value<T>();
        }



        private static bool CompareGreaterThanX(Point3Value<T> p, float x, float y, float z) => (p.X > x);
        private static bool CompareGreaterThanY(Point3Value<T> p, float x, float y, float z) => (p.Y > y);
        private static bool CompareGreaterThanZ(Point3Value<T> p, float x, float y, float z) => (p.Z > z);
        private static float SquareX(Point3Value<T> p, float x, float y, float z) => (x - p.X) * (x - p.X);
        private static float SquareY(Point3Value<T> p, float x, float y, float z) => (y - p.Y) * (y - p.Y);
        private static float SquareZ(Point3Value<T> p, float x, float y, float z) => (z - p.Z) * (z - p.Z);


        internal Readonly3DTreeNode<T> GetCachedNode()
        {
            var element = cache[currentCacheNode];
            currentCacheNode++;
            return element;
        }

        public void Insert(float x, float y, float z, T value)
        {
            FunctionIndex.CurrentIndex = 0;
            root.Insert(x, y, z, value);
        }

        public Point3Value<T>? Search(float x, float y, float z)
        {
            CurrentBestPoint = null;
            CurrentBestSquareDistance = float.PositiveInfinity;
            FunctionIndex.CurrentIndex = 0;
            root.Search(x, y, z);
            return CurrentBestPoint;
        }




    }
}
