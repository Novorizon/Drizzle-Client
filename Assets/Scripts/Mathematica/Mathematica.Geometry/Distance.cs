using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Mathematica;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Geometry
    {
        //点到a,b表示的直线的距离
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="a">直线上一点</param>
        /// <param name="b">直线上另一点</param>
        /// <returns></returns>
        public static float PointToLineDistance(float3 point, float3 a, float3 b)
        {
            float3 m = a - point;
            float3 n = a - b;
            if (n .Equals( float3.zero))
                return float.MinValue;

            float d = math.length(math.cross(m, n)) / math.length(n);
            return d;
        }

        // Returns the squared distance between point  and segment ab .Chapter 5 Basic Primitive Tests
        public static float PointToSegmentDistanceSq(float3 point, float3 a, float3 b)
        {
            float3 ab = b - a;
            float3 ac = point - a;
            float3 bc = point - b;
            float e = math.dot(ac, ab);
            // Handle cases where c projects outside ab
            if (e <= 0) return math.dot(ac, ac);
            float f = math.dot(ab, ab);
            if (e >= f) return math.dot(bc, bc);
            // Handle cases where c projects onto ab
            return math.dot(ac, ac) - e * e / f;
        }


        //点到a,b线段的距离
        public static float PointToSegmentDistance(float3 point, float3 a, float3 b)
        {
            float3 m1 = a - point;
            float3 m2 = b - point;
            float3 n = a - b;
            if (n.Equals(float3.zero))
                return float.NaN;

            if (math.dot(m1, a) < 0)
                return math.length(m1);
            if (math.dot(m2, a) > 0)
                return math.length(m2);
            return math.length(math.cross(m1, n)) / math.length(n);
        }

        //点到平面的距离
        public static float PointToPlaneDistance(float3 point, float3 pointPlane, float3 normal)
        {
            float3 vector = point - pointPlane;
            float3 prj = math.project(vector, normal);

            return math.length(prj);
        }
    }
}
