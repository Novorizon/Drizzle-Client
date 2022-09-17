
using Unity.Mathematics;
using System;
using Unity.Burst;
namespace Mathematica
{
    public static partial class Physics
    {
        //每个点 在法线axis上的投影点
        internal static float2 ExtremeProjectPoint(float3 axis, float3[] points)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            for (int i = 0; i < points.Length; i++)
            {
                float p = math.dot(axis, points[i]);
                min = math.min(p, min);
                max = math.max(p, max);
            }
            return new float2(min, max);
        }
        [BurstCompile]
        //端点 在自己法线axis上的投影点
        internal static float2 ExtremeProjectPoint(float3 axis, float3 min, float3 max)
        {
            float a = math.dot(axis, min);
            float b = math.dot(axis, max);
            float MIN = math.min(a, b);
            float MAX = math.max(a, b);

            return new float2(MIN, MAX);
        }

        internal static bool IsOverlap(float2 point0, float2 point1)
        {
            if (point0.x > point1.y || point0.y < point1.x)
                return false;
            return true;
        }


        /// SeparatingAxisTest  AABB AABB
        internal static bool SeparatingAxisTest(AABB a, AABB b)
        {
            float2 extremePoints0;
            float2 extremePoints1;
            for (int i = 0; i < AABB.NORMAL; i++)
            {
                extremePoints0 = ExtremeProjectPoint(AABB.Normals[i], a.Min, a.Max);
                extremePoints1 = ExtremeProjectPoint(AABB.Normals[i], b.Points);
                if (!IsOverlap(extremePoints0, extremePoints1))
                    return false;
            }

            for (int i = 0; i < AABB.NORMAL; i++)
            {
                extremePoints0 = ExtremeProjectPoint(AABB.Normals[i], b.Min, b.Max);
                extremePoints1 = ExtremeProjectPoint(AABB.Normals[i], a.Points);
                if (!IsOverlap(extremePoints0, extremePoints1))
                    return false;
            }

            return true;
        }




        /// SeparatingAxisTest  AABB OBB
        internal static bool SeparatingAxisTest(AABB aabb, OBB obb)
        {
            float2 extremePoints0;
            float2 extremePoints1;
            for (int i = 0; i < AABB.NORMAL; i++)
            {
                extremePoints0 = ExtremeProjectPoint(AABB.Normals[i], aabb.Points);
                extremePoints1 = ExtremeProjectPoint(AABB.Normals[i], obb.Points);
                if (!IsOverlap(extremePoints0, extremePoints1))
                    return false;
            }

            for (int i = 0; i < OBB.NORMAL; i++)
            {
                extremePoints0 = ExtremeProjectPoint(obb.Normals[i], aabb.Points);
                extremePoints1 = ExtremeProjectPoint(obb.Normals[i], obb.Points);
                if (!IsOverlap(extremePoints0, extremePoints1))
                    return false;
            }

            for (int i = 0; i < AABB.NORMAL; i++)
            {
                for (int j = 0; j < OBB.NORMAL; j++)
                {
                    float3 n = math.cross(AABB.Normals[i], obb.Normals[j]);
                    extremePoints0 = ExtremeProjectPoint(n, aabb.Points);
                    extremePoints1 = ExtremeProjectPoint(n, obb.Points);
                    if (!IsOverlap(extremePoints0, extremePoints1))
                        return false;
                }
            }

            return true;
        }


        /// SeparatingAxisTest  AABB Sphere
        internal static bool SeparatingAxisTest(AABB aabb, Sphere sphere)
        {
            float3 p = sphere.Center - aabb.Center;

            float3 v = math.max(p, -p);
            float3 u = math.max(v - aabb.BevelRadius, float3.zero);
            return math.length(u) < sphere.Radius;
        }


        /// SeparatingAxisTest  OBB OBB
        internal static bool SeparatingAxisTest(OBB a, OBB b)
        {
            for (int i = 0; i < OBB.NORMAL; i++)
            {
                float2 extremePoints0 = ExtremeProjectPoint(a.Normals[i], a.Points);
                float2 extremePoints1 = ExtremeProjectPoint(a.Normals[i], b.Points);
                if (!IsOverlap(extremePoints0, extremePoints1))
                    return false;
            }

            for (int i = 0; i < OBB.NORMAL; i++)
            {
                float2 extremePoints0 = ExtremeProjectPoint(b.Normals[i], a.Points);
                float2 extremePoints1 = ExtremeProjectPoint(b.Normals[i], b.Points);
                if (!IsOverlap(extremePoints0, extremePoints1))
                    return false;
            }

            for (int i = 0; i < OBB.NORMAL; i++)
            {
                for (int j = 0; j < OBB.NORMAL; j++)
                {
                    float3 n = math.cross(a.Normals[i], b.Normals[j]);
                    float2 extremePoints0 = ExtremeProjectPoint(n, a.Points);
                    float2 extremePoints1 = ExtremeProjectPoint(n, b.Points);
                    if (!IsOverlap(extremePoints0, extremePoints1))
                        return false;
                }
            }

            return true;
        }

        /// SeparatingAxisTest  OBB Sphere
        internal static bool SeparatingAxisTest(OBB obb, Sphere sphere)
        {
            float3 p = sphere.Center - obb.Center;
            p = math.mul(math.inverse(obb.Orientation) , p);

            float3 v = math.max(p, -p);
            float3 u = math.max(v - obb.BevelRadius, float3.zero);
            return math.length(u) < sphere.Radius;
        }

        internal static bool SeparatingAxisTest(AABB aabb, Capsule capsule)
        {
            float3 p = capsule.Center1 - aabb.Center;
            float3 v = math.max(p, -p);
            float3 u = math.max(v - aabb.BevelRadius, float3.zero);
            if (math.length(u) < capsule.Radius)
                return true;

            p = capsule.Center2 - aabb.Center;
            v = math.max(p, -p);
            u = math.max(v - aabb.BevelRadius, float3.zero);
            if (math.length(u) < capsule.Radius)
                return true;

            //顶点 轴的距离
            for (int i = 0; i < AABB.VERTEX; i++)
            {
                if (math.dot(aabb.Points[i] - capsule.Center1, capsule.Center2 - capsule.Center1) >= 0 && math.dot(aabb.Points[i] - capsule.Center2, capsule.Center1 - capsule.Center2) >= 0)
                {
                    float dis = Geometry.PointToLineDistance(aabb.Points[i], capsule.Center1, capsule.Center2);
                    if (dis < capsule.Radius)
                        return true;
                }
            }

            for (int i = 0; i < AABB.NORMAL; i++)
            {
                float3 radius = math.cross(capsule.Axis, AABB.Normals[i]) * capsule.Radius;
                float3[] seg = new float3[2] { capsule.Center1 + radius, capsule.Center2 + radius };

                for (int j = 0; j < AABB.triangles.Length; j = j + 3)
                {
                    float3[] tri = new float3[3] { aabb.Points[AABB.triangles[j]], aabb.Points[AABB.triangles[j + 1]], aabb.Points[AABB.triangles[j + 2]] };
                    float3 hit =new float3( float.MinValue, float.MinValue, float.MinValue);
                    SpatialRelationship sr = Geometry.SegmentHitTriangle(tri, seg, hit);
                    if (sr == SpatialRelationship.INTERSECTING)
                        return true;
                }
            }


            return false;
        }

        internal static bool SeparatingAxisTest(OBB obb, Capsule capsule)
        {
            float3 p = capsule.Center1 - obb.Center;
            p = math.mul( math.inverse(obb.Orientation) ,p);
            float3 v = math.max(p, -p);
            float3 u = math.max(v - obb.BevelRadius, float3.zero);
            if (math.length(u) < capsule.Radius)
                return true;

            p = capsule.Center2 - obb.Center;
            p = math.mul( math.inverse(obb.Orientation) , p);
            v = math.max(p, -p);
            u = math.max(v - obb.BevelRadius, float3.zero);
            if (math.length(u) < capsule.Radius)
                return true;

            //顶点 轴的距离
            for (int i = 0; i < OBB.VERTEX; i++)
            {
                if (math.dot(obb.Points[i] - capsule.Center1, capsule.Center2 - capsule.Center1) >= 0 && math.dot(obb.Points[i] - capsule.Center2, capsule.Center1 - capsule.Center2) >= 0)
                {
                    float dis = Geometry.PointToLineDistance(obb.Points[i], capsule.Center1, capsule.Center2);
                    if (dis < capsule.Radius)
                        return true;
                }
            }

            for (int i = 0; i < OBB.NORMAL; i++)
            {
                float3 radius = math.cross(capsule.Axis, obb.Normals[i]) * capsule.Radius;
                float3[] seg = new float3[2] { capsule.Center1 + radius, capsule.Center2 + radius };

                for (int j = 0; j < OBB.Triangles.Length; j = j + 3)
                {
                    float3[] tri = new float3[3] { obb.Points[OBB.Triangles[j]], obb.Points[OBB.Triangles[j + 1]], obb.Points[OBB.Triangles[j + 2]] };
                    float3 hit = new float3(float.MinValue, float.MinValue, float.MinValue);
                    SpatialRelationship sr = Geometry.SegmentHitTriangle(tri, seg, hit);
                    if (sr == SpatialRelationship.INTERSECTING)
                        return true;
                }
            }

            return false;
        }

        internal static bool SeparatingAxisTest(Capsule capsule, Sphere sphere)
        {
            //判断Sphere与中心点1的距离
            if (math.distancesq(sphere.Center, capsule.Center1) <= capsule.Radius2)
                return true;
            //判断Sphere与中心点2的距离
            if (math.distancesq(sphere.Center, capsule.Center2) <= capsule.Radius2)
                return true;

            //Sphere端点模拟分离轴
            float3 axis = math.cross(capsule.Axis, sphere.Center - capsule.Center1);
            axis = math.cross(capsule.Axis, axis);
            axis = math.normalize(axis);
            float3 radius1 = axis * sphere.Radius;
            float3 radius2 = axis * capsule.Radius;
            float2 extremePoints0 = ExtremeProjectPoint(axis, new float3[] { sphere.Center - radius1, sphere.Center + radius1 });
            float2 extremePoints1 = ExtremeProjectPoint(axis, new float3[] { capsule.Center1 - radius2, capsule.Center2 + radius2 });
            if (!IsOverlap(extremePoints0, extremePoints1))
                return false;

            axis = math.normalize(capsule.Center1 - sphere.Center);
            radius1 = axis * sphere.Radius;
            radius2 = axis * capsule.Radius;
            extremePoints0 = ExtremeProjectPoint(axis, new float3[] { sphere.Center - radius1, sphere.Center + radius1 });
            extremePoints1 = ExtremeProjectPoint(axis, new float3[] { capsule.Center1 - radius2, capsule.Center2 + radius2 });
            if (!IsOverlap(extremePoints0, extremePoints1))
                return false;

            axis = math.normalize(capsule.Center1 - sphere.Center);
            radius1 = axis * sphere.Radius;
            radius2 = axis * capsule.Radius;
            extremePoints0 = ExtremeProjectPoint(axis, new float3[] { sphere.Center - radius1, sphere.Center + radius1 });
            extremePoints1 = ExtremeProjectPoint(axis, new float3[] { capsule.Center1 - radius2, capsule.Center2 + radius2 });
            if (!IsOverlap(extremePoints0, extremePoints1))
                return false;

            return true;
        }
        internal static bool intersectSegmentSphere(float3 p, float3 d, float3 s_c, float r, float t)
        {
            float tmax = math.length(d * d);
            float3 m = p - s_c;
            float b = math.length(m * d);
            float c = math.length(m * m - r * r);
            if (tmax > 0.0f)
            {
                tmax = math.sqrt(tmax);
                d = d / tmax;
            }
            else
            {
                if (c > 0.0f)
                    return false;
                else
                {
                    t = 0;
                    //q = p;
                    return true;
                }
            }
            //Exit if r's origin outside s (c > 0) and r pointing away from s (b > 0)
            if (c > 0.0f && b > 0.0f) return false;
            float discr = b * b - c;
            // A negative discriminant corresponds to ray missing sphere.
            if (discr < 0.0f) return false;
            // Ray now found to intersect sphere, compute smallest t value of
            // intersection.
            t = -b - math.sqrt(discr);
            // If t > tmax then sphere is missed.
            if (t > tmax)
                return false;
            // If t is negative then segment started inside sphere, so clamp t to
            // zero.
            if (t < 0.0f) t = 0.0f;
            // Calculate intersection point.
            //q = p + d * t;
            // Set t to the interval 0 <= t <= tmax.
            t = t / tmax;
            return true;
        }


        internal static bool intersectSegmentCapsule(float3 sa, float3 sb, float3 p, float3 q, float r, float t)
        {
            float3 d = q - p, m = sa - p, n = sb - sa;
            float md = math.length(m * d);
            float nd = math.length(n * d);
            float dd = math.length(d * d);
            // Test if segment fully outside either endcap of capsule.
            if (md < 0.0f && md + nd < 0.0f)
            {
                // Segment outside 'p'.
                return intersectSegmentSphere(sa, n, p, r, t);
            }
            if (md > dd && md + nd > dd)
            {
                // Segment outside 'q'
                return intersectSegmentSphere(sa, n, q, r, t);
            }
            float nn = math.length(n * n);
            float mn = math.length(m * n);
            float a = dd * nn - nd * nd;
            float k = math.length(m * m - r * r);
            float c = dd * k - md * md;
            if (math.abs(a) < float.Epsilon)
            {
                // Segment runs parallel to cylinder axis.
                if (c > 0) return false; // 'a' and thus the segment lies outside cyl.
                                         // Now known that segment intersects cylinder. Figure out how.
                if (md < 0)
                    // Intersect against 'p' endcap.
                    intersectSegmentSphere(sa, n, p, r, t);
                else if (md > dd)
                    // Intersect against 'q' encap.
                    intersectSegmentSphere(sa, n, q, r, t);
                else t = 0.0f; // 'a' lies inside cylinder.
                return true;
            }
            float b = dd * mn - nd * md;
            float discr = b * b - a * c;
            if (discr < 0.0f) return false; // No real roots; no intersection.

            t = (-b - math.sqrt(discr)) / a;
            float t0 = t;
            if (md + t * nd < 0.0f)
            {
                // Intersection outside cylinder on 'p' side;
                return intersectSegmentSphere(sa, n, p, r, t);
            }
            else if (md + t * nd > dd)
            {
                // Intsection outside cylinder on 'q' side.
                return intersectSegmentSphere(sa, n, q, r, t);
            }
            t = t0;
            // Intersection if segment intersects cylinder between end-caps.
            return t >= 0.0f && t <= 1.0f;
        }

    }
}
