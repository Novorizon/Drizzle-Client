using Mathematica;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Physics
    {
        internal static bool IsOverlap(Sphere sphere, float3 point) { return math.distancesq(point, sphere.Center) <= sphere.Radius2; }

        internal static bool IsOverlap(Sphere a, Sphere b) { return math.distancesq(a.Center, b.Center) < (a.Radius + b.Radius) * (a.Radius + b.Radius); }


        public static float3 CollidePoint(Sphere sphere, AABB aabb)
        {
            float3 p = sphere.Center - aabb.Center;

            float3 v = math.max(p, -p);
            float3 u = math.max(v - aabb.BevelRadius, float3.zero);
            if (math.length(u) <= sphere.Radius)
            {
                return sphere.Center - sphere.Radius * math.normalize(u);
            }
            return new float3(float.NaN, float.NaN, float.NaN);
        }

        public static float3 CollidePoint(Sphere sphere, OBB obb)
        {
            float3 p = sphere.Center - obb.Center;
            p =math.mul( obb.Orientation , p);

            float3 v = math.max(p, -p);
            float3 u = math.max(v - obb.BevelRadius, float3.zero);
            if (math.length(u) <= sphere.Radius)
            {
                return sphere.Center - sphere.Radius * math.normalize(u);
            }
            return new float3(float.NaN, float.NaN, float.NaN);
        }
    }
}
