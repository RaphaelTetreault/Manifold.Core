using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifold.Geometry
{
    internal class Readonly3DTreeNode<T>
    {
        private Readonly3DTreeNode<T>? parent;
        private readonly Readonly3DTree<T> tree;
        private readonly Point3Value<T> point = new Point3Value<T>();
        private readonly Readonly3DTreeNode<T>?[] children = new Readonly3DTreeNode<T>[2];

        internal Readonly3DTreeNode<T>? ChildLesserThan { get => children[0]; private set => children[0] = value; }
        internal Readonly3DTreeNode<T>? ChildGreaterThan { get => children[1]; private set => children[1] = value; }
        internal bool IsLeafNode => ChildGreaterThan is null && ChildLesserThan is null;
        //internal bool IsGreaterThan { get; set; }

        public Readonly3DTreeNode(Readonly3DTree<T> tree)
        {
            this.tree = tree;
        }




        public void Insert(float x, float y, float z, T value)
        {
            // TODO: explain this wizardry (cyclic)
            var compareFunction = tree.CompareComponentValue.GetAndMoveNext();
            bool greaterThan = compareFunction(point, x, y, z);

            if (greaterThan)
            {
                if (ChildGreaterThan is null)
                {
                    ChildGreaterThan = tree.GetCachedNode();
                    ChildGreaterThan.parent = this;
                    ChildGreaterThan.point.Set(x, y, z, value);
                }
                else
                {
                    ChildGreaterThan.Insert(x, y, z, value);
                }
            }
            else // lesser than
            {
                if (ChildLesserThan is null)
                {
                    ChildLesserThan = tree.GetCachedNode();
                    ChildLesserThan.parent = this;
                    ChildLesserThan.point.Set(x, y, z, value);
                }
                else
                {
                    ChildLesserThan.Insert(x, y, z, value);
                }
            }
        }


        public void Search(float x, float y, float z)
        {
            int index = tree.FunctionIndex.PostIncrement();
            // Find which child to search first and last.
            // First: child on correct side of boundary. We must always check it.
            // Last : child on other side of boundary. We may or may not need to check it.
            var compareFunction = tree.CompareComponentValue[index];
            bool greaterThan = compareFunction(point, x, y, z);
            var branch1 = greaterThan ? ChildGreaterThan : ChildLesserThan;
            var branch2 = greaterThan ? ChildLesserThan : ChildGreaterThan;

            // Search downward.
            // This recusion stops when there is not child branch greater/lesser than.
            branch1?.Search(x, y, z);

            // Calculate distance. If this is the closest point so far, record.
            float squareDistanceToPoint = GetSquareDistance(point, x, y, z);
            if (squareDistanceToPoint < tree.CurrentBestSquareDistance)
            {
                tree.CurrentBestSquareDistance = squareDistanceToPoint;
                tree.CurrentBestPoint = point;
            }
            // Check to see how far this point is from the splitting boundary.
            var squareFunction = tree.SquareBoundary[index];
            float squareDistanceToMedian = squareFunction(parent.point, x, y, z);
            // If we are closer to boundary than point, check other side of boundary.
            bool doCheckOtherBranch = squareDistanceToMedian < squareDistanceToPoint;
            if (doCheckOtherBranch)
            {
                // Move function pointer back one index
                tree.FunctionIndex.PostDecrement();
                branch2?.Search(x, y, z);
            }

            // Move upward one node
            // Upon returning from function, move function pointer back one index
            tree.FunctionIndex.PostDecrement();
        }

        public void Visit()
        {
            ChildLesserThan?.Visit();
            Console.WriteLine(point.Value);
            ChildGreaterThan?.Visit();
        }


        private float GetSquareDistance(Point3Value<T> p, float x, float y, float z)
        {
            float xsqr = (p.X - x) * (p.X - x);
            float ysqr = (p.Y - y) * (p.Y - y);
            float zsqr = (p.Z - z) * (p.Z - z);
            float sum = xsqr + ysqr + zsqr;
            return sum;
        }

    }
}
