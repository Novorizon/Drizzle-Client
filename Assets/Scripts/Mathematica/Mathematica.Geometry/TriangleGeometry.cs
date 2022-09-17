using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Mathematica;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Geometry
    {
        /// 三角形是否相邻  to check
        public static bool IsCoincide(Triangle a, Triangle b)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a.points[i].Equals(b.points[(j + 1) % 3]) && a.points[(i + 1) % 3].Equals(b.points[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// 三角形是否相邻   to check
        public static Tuple<bool, float3, float3> CoincideTriangle(Triangle TriangleA, Triangle TriangleB)
        {
            bool isCoincide = false;
            float3 left = new float3();
            float3 right = new float3();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Equals(TriangleA.points[i], TriangleB.points[(j + 1) % 3]) && Equals(TriangleA.points[(i + 1) % 3], TriangleB.points[j]))
                    {
                        left = TriangleA.points[i];
                        right = TriangleB.points[j];
                        isCoincide = true;
                        break;
                    }
                }
                if (isCoincide)
                    break;
            }

            if (isCoincide)
            {
                float3 l = left - TriangleA.centroid;
                float3 r = right - TriangleA.centroid;
                float angle = AngleSigned(l, r, math.up());
                if (angle < 0)
                {
                    float3 p = left;
                    left = right;
                    right = p;
                }
            }
            return new Tuple<bool, float3, float3>(isCoincide, left, right);
        }
    }
}
