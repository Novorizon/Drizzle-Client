using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Mathematica;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Geometry
    {
        public static bool IsConvex(Polygon polygon)
        {
            for (int i = 0; i < polygon.edges.Length; i++)
            {
                if (mathEx.cross(polygon.edges[i], polygon.edges[(i + 1) % polygon.edges.Length]) * mathEx.cross(polygon.edges[(i + 1) % polygon.edges.Length], polygon.edges[(i + 2) % polygon.edges.Length]) <= 0)
                    return false;
            }
            return true;
        }


        public static bool IsOverlap(Circular c1, Circular c2)
        {
            float dis = math.distancesq(c1.center, c2.center);
            if (dis <= (c1.radius + c2.radius) * (c1.radius + c2.radius))
            {
                return true;
            }
            return false;
        }

        //点到a,b表示的直线的距离
        public static float PointToLineDistance(float2 point, float2 a, float2 b)
        {
            float2 m = a - point;
            float2 n = a - b;
            return PointToLineDistance(new float3(point.x, point.y, 0), new float3(m.x, m.y,0), new float3(n.x, n.y, 0));
        }



        /// 二次贝塞尔
        public static float2 Bezier2(float2 p0, float2 p1, float2 p2, float t)
        {
            return (1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2);
        }

        /// 三次贝塞尔
        public static float2 Bezier3(float2 p0, float2 p1, float2 p2, float2 p3, float t)
        {
            return (1 - t) * ((1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2)) + t * ((1 - t) * ((1 - t) * p1 + t * p2) + t * ((1 - t) * p2 + t * p3));
        }
    }
}
