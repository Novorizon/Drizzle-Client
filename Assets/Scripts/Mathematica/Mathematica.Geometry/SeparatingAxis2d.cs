using Mathematica;
using Unity.Mathematics;

using UnityEngine;


namespace Mathematica
{
    public static partial class Geometry
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

        /// SeparatingAxisTest
        public static bool SeparatingAxisTest(Polygon g0, Polygon g1)
        {
            for (int i = 0; i < g0.normals.Length; i++)
            {
                float2 extremePoints0 = new float2();
                float2 extremePoints1 = new float2();
                for (int j = 0; j < g0.points.Length; j++)
                {
                    extremePoints0 = ExtremeProjectPoint(g0.normals[i], g0.points);
                }
                for (int j = 0; j < g1.points.Length; j++)
                {
                    extremePoints1 = ExtremeProjectPoint(g1.normals[i], g1.points);
                }
                if (!IsOverlap(extremePoints0, extremePoints1))
                    return false;
            }

            return true;
        }

        public static bool SeparatingAxisTest(Polygon polygon, Circular circular)
        {
            float2 extremePoints0 = new float2();
            float2 extremePoints1 = new float2();
            for (int i = 0; i < polygon.normals.Length; i++)
            {
                for (int j = 0; j < polygon.points.Length; j++)
                {
                    extremePoints0 = ExtremeProjectPoint(polygon.normals[i], polygon.points);
                }
                extremePoints1 = ExtremeProjectPoint(polygon.normals[i], circular);

                if (!IsOverlap(extremePoints0, extremePoints1))
                    return false;
            }

            float2 axis = MinDistanceAxis(polygon, circular);
            for (int i = 0; i < polygon.points.Length; i++)
            {
                extremePoints0 = ExtremeProjectPoint(axis, polygon.points);
            }
            extremePoints1 = ExtremeProjectPoint(axis, circular);
            if (!IsOverlap(extremePoints0, extremePoints1))
                return false;


            return true;
        }

        public static float2 ExtremeProjectPoint(float2 axis, float2[] points)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            for (int i = 0; i < points.Length; i++)
            {
                var p = math.dot(axis, points[i]);
                min = math.min(p, min);
                max = math.max(p, max);
            }
            return new float2(min, max);
        }

        public static float2 ExtremeProjectPoint(float2 axis, Circular circular)
        {
            float2 a = circular.center + circular.center * math.normalize(axis);
            float2 b = circular.center - circular.center * math.normalize(axis);
            float p1 = math.dot(axis, a);
            float p2 = math.dot(axis, b);
            float min = math.min(p1, p2);
            float max = math.max(p1, p2);

            return new float2(min, max);
        }


        public static float2 MinDistanceAxis(Polygon polygon, Circular circular)
        {
            float distance = float.MaxValue;
            float d = float.MaxValue;
            int id = -1;
            for (int i = 0; i < polygon.points.Length; i++)
            {
                d = math.distance(circular.center, polygon.points[i]);
                if (distance > d)
                {
                    distance = d;
                    id = i;
                }
            }
            float2 axis = circular.center - polygon.points[id];
            for (int i = 0; i < polygon.edges.Length; i++)
            {
                float2 m = polygon.points[i] - circular.center;
                float2 n = polygon.edges[i];
                float3 M = new float3(m.x, m.y,0);
                float3 N = new float3(n.x, n.y, 0);
                d = math.length(math.cross(M, N)) / math.length(N);
                if (distance > d)
                {
                    distance = d;
                    axis = polygon.edges[i];
                }
            }


            return axis;
        }


    }
}
