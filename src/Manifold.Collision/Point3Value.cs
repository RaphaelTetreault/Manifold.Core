using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manifold.Geometry
{
    public class Point3Value<T>
    {
        private float x;
        private float y;
        private float z;
        private T value;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }
        public T Value { get => value; set => this.value = value; }

        public void ResetState()
        {
            x = 0;
            y = 0;
            z = 0;
            value = default(T);
        }

        public void Set(float x, float y, float z, T value)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.value = value;
        }
    }
}
