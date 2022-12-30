namespace Manifold.Geometry
{
    public struct AABB
    {
        public float minX;
        public float minY;
        public float minZ;
        public float maxX;
        public float maxY;
        public float maxZ;

        public float ExtentX => maxX - minX;
        public float ExtentY => maxY - minY;
        public float ExtentZ => maxZ - minZ;

        public (float x, float y, float z) Center()
        {
            float halfExtentX = ExtentX * 0.5f;
            float halfExtentY = ExtentY * 0.5f;
            float halfExtentZ = ExtentZ * 0.5f;
            float x = minX + halfExtentX;
            float y = minY + halfExtentY;
            float z = minZ + halfExtentZ;
            return (x, y, z);
        }

        public bool ContainsPoint(float x, float y, float z)
        {
            bool insideBoundsX = (x >= minX) && (x < maxX);
            bool insideBoundsY = (y >= minY) && (y < maxY);
            bool insideBoundsZ = (z >= minZ) && (z < maxZ);
            bool containsPoint = insideBoundsX && insideBoundsY && insideBoundsZ;
            return containsPoint;
        }

        public static AABB CreateFromMinAndExtent(float minX, float minY, float minZ, float extent)
        {
            var aabb = new AABB()
            {
                minX = minX,
                minY = minY,
                minZ = minZ,
                maxX = minX + extent,
                maxY = minY + extent,
                maxZ = minZ + extent,
            };
            return aabb;
        }

        public static AABB CreateFromMinAndExtent(float minX, float minY, float minZ, float extentX, float extentY, float extentZ)
        {
            var aabb = new AABB()
            {
                minX = minX,
                minY = minY,
                minZ = minZ,
                maxX = minX + extentX,
                maxY = minY + extentY,
                maxZ = minZ + extentZ,
            };
            return aabb;
        }

        public static AABB CreateFromMaxAndExtent(float maxX, float maxY, float maxZ, float extent)
        {
            var aabb = new AABB()
            {
                minX = maxX - extent,
                minY = maxY - extent,
                minZ = maxZ - extent,
                maxX = maxX,
                maxY = maxY,
                maxZ = maxZ,
            };
            return aabb;
        }

        public static AABB CreateFromMaxAndExtent(float maxX, float maxY, float maxZ, float extentX, float extentY, float extentZ)
        {
            var aabb = new AABB()
            {
                minX = maxX - extentX,
                minY = maxY - extentY,
                minZ = maxZ - extentZ,
                maxX = maxX,
                maxY = maxY,
                maxZ = maxZ,
            };
            return aabb;
        }

        public static AABB CreateFromCenterAndExtent(float x, float y, float z, float extent)
        {
            float halfExtent = extent * 0.5f;
            var aabb = new AABB()
            {
                minX = x - halfExtent,
                minY = y - halfExtent,
                minZ = z - halfExtent,
                maxX = x + halfExtent,
                maxY = y + halfExtent,
                maxZ = z + halfExtent,
            };
            return aabb;
        }

        public static AABB CreateFromCenterAndExtent(float x, float y, float z, float extentX, float extentY, float extentZ)
        {
            float halfExtentX = extentX * 0.5f;
            float halfExtentY = extentY * 0.5f;
            float halfExtentZ = extentZ * 0.5f;
            var aabb = new AABB()
            {
                minX = x - halfExtentX,
                minY = y - halfExtentY,
                minZ = z - halfExtentZ,
                maxX = x + halfExtentX,
                maxY = y + halfExtentY,
                maxZ = z + halfExtentZ,
            };
            return aabb;
        }

        public static AABB CreateFromCenterAndHalfExtent(float x, float y, float z, float halfExtent)
        {
            var aabb = new AABB()
            {
                minX = x - halfExtent,
                minY = y - halfExtent,
                minZ = z - halfExtent,
                maxX = x + halfExtent,
                maxY = y + halfExtent,
                maxZ = z + halfExtent,
            };
            return aabb;
        }

        public static AABB CreateFromCenterAndHalfExtent(float x, float y, float z, float halfExtentX, float halfExtentY, float halfExtentZ)
        {
            var aabb = new AABB()
            {
                minX = x - halfExtentX,
                minY = y - halfExtentY,
                minZ = z - halfExtentZ,
                maxX = x + halfExtentX,
                maxY = y + halfExtentY,
                maxZ = z + halfExtentZ,
            };
            return aabb;
        }

        public static AABB[] SubdivideBounds(AABB bounds, int divX, int divY, int divZ)
        {
            int count = divX * divY * divZ;
            var subdivisions = new AABB[count];
            float extentsX = bounds.ExtentX / divX;
            float extentsY = bounds.ExtentY / divY;
            float extentsZ = bounds.ExtentZ / divZ;

            // Iterate over 3D space, subdiving bounds
            for (int z = 0; z < divZ; z++)
            {
                int indexZ = z * divY * divX;
                float minZ = bounds.minZ + (extentsZ * z);
                for (int y = 0; y < divY; y++)
                {
                    int indexYZ = (y * divX) + indexZ;
                    float minY = bounds.minY + (extentsY * y);
                    for (int x = 0; x < divX; x++)
                    {
                        int indexXYZ = x + indexYZ;
                        float minX = bounds.minX + (extentsX * x);
                        //
                        subdivisions[indexXYZ] = CreateFromMinAndExtent(minX, minY, minZ, extentsX, extentsY, extentsZ);
                    }
                }
            }
            return subdivisions;
        }

        public bool IsValid()
        {
            bool xValid = minX < maxX;
            bool yValid = minY < maxY;
            bool zValid = minZ < maxZ;
            bool isValid = xValid && yValid && zValid;
            return isValid;
        }

        public bool Overlaps(AABB bounds)
        {
            // If min is greater than max, no overlap
            bool outsideMinX = minX >= bounds.maxX;
            bool outsideMinY = minY >= bounds.maxY;
            bool outsideMinZ = minZ >= bounds.maxZ;
            // If max is lesser than min, no overlap
            bool outsideMaxX = maxX <= bounds.minX;
            bool outsideMaxY = maxY <= bounds.minY;
            bool outsideMaxZ = maxZ <= bounds.minZ;

            // 
            bool doesOverlap =
                !(outsideMinX || outsideMinY || outsideMinZ ||
                  outsideMaxX || outsideMaxY || outsideMaxZ );

            return doesOverlap;
        }

    }
}
