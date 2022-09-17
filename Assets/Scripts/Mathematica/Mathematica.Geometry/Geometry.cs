using System;
using Unity.Mathematics;

namespace Mathematica
{
    public enum GeometryType
    {
        Polygon = 1,
        Triangle = 2,
        Rectangle = 3,
        Hexagon = 4,
        Circular = 5
    }

    public static partial class Geometry
    {
        //�㵽�߶εľ���
        public static float PointToSegmentDistance(float3 point, Segment a)
        {
            return PointToSegmentDistance(point, a.start, a.end);
        }

        //�㵽ֱ�ߵľ���
        public static float PointToLineDistance(float3 point, Line a)
        {
            return PointToSegmentDistance(point, a.point, a.point + a.direction);
        }

        //�㵽�߶εľ���
        public static float PointToPlaneDistance(float3 point, Plane a)
        {
            return PointToPlaneDistance(point, a.point, a.normal);
        }


        /// ���α�����
        public static float3 Bezier2(float3 p0, float3 p1, float3 p2, float t)
        {
            return (1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2);
        }

        /// ���α�����
        public static float3 Bezier3(float3 p0, float3 p1, float3 p2, float3 p3, float t)
        {
            return (1 - t) * ((1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2)) + t * ((1 - t) * ((1 - t) * p1 + t * p2) + t * ((1 - t) * p2 + t * p3));
        }


        public static bool IsConvex(float2[] points)
        {
            int Length = points.Length;
            for (int i = 0; i < Length; i++)
            {
                if (mathEx.cross(points[i], points[(i + 1) % Length]) * mathEx.cross(points[(i + 1) % Length], points[(i + 2) % Length]) <= 0)
                    return false;
            }
            return true;
        }


        public static bool IsConvex(float3[] points)
        {
            int Length = points.Length;
            for (int i = 0; i < Length; i++)
            {
                if (math.cross(points[i], points[(i + 1) % Length]).y * math.cross(points[(i + 1) % Length], points[(i + 2) % Length]).y <= 0)
                    return false;
            }
            return true;
        }
    }

}

