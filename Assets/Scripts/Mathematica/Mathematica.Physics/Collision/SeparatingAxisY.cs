
using Unity.Mathematics;
using System;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Physics
    {
        internal static bool SeparatingAxisTestY(AABB a, OBB b)
        {
            if (a.Center.y - a.BevelRadius.y >= b.Center.y + b.BevelRadius.y)
                return false;
            if (a.Center.y + a.BevelRadius.y <= b.Center.y - b.BevelRadius.y)
                return false;

            float2 points0 = ExtremeProjectPoint(AABB.Normals[0], a.Points);
            float2 points1 = ExtremeProjectPoint(AABB.Normals[0], b.Points);
            if (!IsOverlap(points0, points1))
                return false;

            points0 = ExtremeProjectPoint(AABB.Normals[2], a.Points);
            points1 = ExtremeProjectPoint(AABB.Normals[2], b.Points);
            if (!IsOverlap(points0, points1))
                return false;

            points0 = ExtremeProjectPoint(b.Normals[0], a.Points);
            points1 = ExtremeProjectPoint(b.Normals[0], b.Points);
            if (!IsOverlap(points0, points1))
                return false;

            points0 = ExtremeProjectPoint(b.Normals[2], a.Points);
            points1 = ExtremeProjectPoint(b.Normals[2], b.Points);
            if (!IsOverlap(points0, points1))
                return false;

            return true;
        }


        /// SeparatingAxisTest  OBB OBB
        internal static bool SeparatingAxisTestY(OBB a, OBB b)
        {
            if (a.Center.y - a.BevelRadius.y >= b.Center.y + b.BevelRadius.y)
                return false;
            if (a.Center.y + a.BevelRadius.y <= b.Center.y - b.BevelRadius.y)
                return false;

            float2 points0 = ExtremeProjectPoint(a.Normals[0], a.Points);
            float2 points1 = ExtremeProjectPoint(a.Normals[0], b.Points);
            if (!IsOverlap(points0, points1))
                return false;

            points0 = ExtremeProjectPoint(a.Normals[2], a.Points);
            points1 = ExtremeProjectPoint(a.Normals[2], b.Points);
            if (!IsOverlap(points0, points1))
                return false;

            points0 = ExtremeProjectPoint(b.Normals[0], a.Points);
            points1 = ExtremeProjectPoint(b.Normals[0], b.Points);
            if (!IsOverlap(points0, points1))
                return false;

            points0 = ExtremeProjectPoint(b.Normals[2], a.Points);
            points1 = ExtremeProjectPoint(b.Normals[2], b.Points);
            if (!IsOverlap(points0, points1))
                return false;

            return true;
        }

        internal static bool SeparatingAxisTestY(AABB a, Capsule b)
        {

            float up = a.Center.y + a.BevelRadius.y;
            float down = a.Center.y - a.BevelRadius.y;

            if (down > b.Center1.y + b.Radius)
            {
                return false;
            }
            else if (down > b.Center1.y && down <= b.Center1.y + b.Radius)
            {
                float3 p = b.Center1 - a.Center;

                float3 v = math.max(p, -p);
                float3 u = math.max(v - a.BevelRadius, float3.zero);
                return math.length(u) < b.Radius;

            }
            else if (up < b.Center2.y - b.Radius)
            {
                return false;
            }
            else if (up > b.Center2.y - b.Radius && up <= b.Center2.y)
            {
                float3 p = b.Center2 - a.Center;

                float3 v = math.max(p, -p);
                float3 u = math.max(v - a.BevelRadius, float3.zero);
                return math.length(u) < b.Radius;

            }
            else
            {
                float2 CenterA = new float2(a.Center.x, a.Center.z);
                float2 CenterB = new float2(b.Center.x, b.Center.z);
                float2 BevelRadius = new float2(a.BevelRadius.x, a.BevelRadius.z);
                float2 p = CenterB - CenterA;

                float2 v = math.max(p, -p);
                float2 u = math.max(v - BevelRadius, float2.zero);
                return math.length(u) < b.Radius;

            }
        }

        internal static bool SeparatingAxisTestY(OBB a, Capsule b)
        {

            float up = a.Center.y + a.BevelRadius.y;
            float down = a.Center.y - a.BevelRadius.y;

            if (down > b.Center1.y + b.Radius)
            {
                return false;
            }
            else if (down > b.Center1.y && down <= b.Center1.y + b.Radius)
            {
                float3 p = b.Center1 - a.Center;

                float3 v = math.max(p, -p);
                float3 u = math.max(v - a.BevelRadius, float3.zero);
                return math.length(u) < b.Radius;

            }
            else if (up < b.Center2.y - b.Radius)
            {
                return false;
            }
            else if (up > b.Center2.y - b.Radius && up <= b.Center2.y)
            {
                float3 p = b.Center2 - a.Center;

                float3 v = math.max(p, -p);
                float3 u = math.max(v - a.BevelRadius, float3.zero);
                return math.length(u) < b.Radius;

            }
            else
            {
                float2 CenterA = new float2(a.Center.x, a.Center.z);
                float2 CenterB = new float2(b.Center.x, b.Center.z);
                float2 BevelRadius = new float2(a.BevelRadius.x, a.BevelRadius.z);
                float2 p = CenterB - CenterA;

                float2 v = math.max(p, -p);
                float2 u = math.max(v - BevelRadius, float2.zero);
                return math.length(u) < b.Radius;

            }
        }

        internal static bool SeparatingAxisTestY(Capsule capsule, Sphere sphere)
        {
            float topS = sphere.Center.y + sphere.Radius;
            float bottomS = sphere.Center.y - sphere.Radius;
            float topC = capsule.Center1.y + capsule.Radius;
            float bottomC = capsule.Center2.y - capsule.Radius;
            if (topS < bottomC || bottomS > topC)
            {
                return false;
            }
            else if (bottomS < topC && bottomS > capsule.Center1.y)
            {
                return math.distancesq(sphere.Center, capsule.Center1) <= (capsule.Radius2 + sphere.Radius) * (capsule.Radius2 + sphere.Radius);

            }
            else if (topS < capsule.Center2.y && topS > capsule.Center1.y)
            {
                return math.distancesq(sphere.Center, capsule.Center2) <= (capsule.Radius2 + sphere.Radius) * (capsule.Radius2 + sphere.Radius);

            }
            else
            {
                float2 CenterA = new float2(sphere.Center.x, sphere.Center.z);
                float2 CenterB = new float2(capsule.Center.x, capsule.Center.z);
                return math.distancesq(CenterA, CenterB) <= (sphere.Radius + capsule.Radius) * (sphere.Radius + capsule.Radius);
            }
        }

    }
}
