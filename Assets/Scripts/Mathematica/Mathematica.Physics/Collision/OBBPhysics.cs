using Mathematica;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Physics
    {
        internal static bool IsOverlap(OBB aabb, float3 point)
        {
            float3 test0 = point - aabb.PointNormals[0];
            for (int i = 0; i < 3; i++)
            {
                float3 test1 = point - aabb.PointNormals[i + 1];
                float3 n = aabb.Normals[i];

                if (math.dot(test0, n) * math.dot(test1, n) > 0)
                    return false;
            }
            return true;
        }

        public static float3 CollidePoint(OBB obb, Sphere sphere)
        {
            float3 p = sphere.Center - obb.Center;
            p =math.mul( obb.Orientation ,p);

            float3 v = math.max(p, -p);
            float3 u = math.max(v - obb.BevelRadius, float3.zero);

            float dis = math.length(u);
            if (dis <= sphere.Radius)
            {
                return sphere.Center - sphere.Radius * math.normalize(u);
            }
            return new float3(float.MinValue, float.MinValue, float.MinValue);
        }
    }
}
