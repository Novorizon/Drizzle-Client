using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{

    public struct Triangle
    {
        public static readonly int EDGE = 3;
        public float3[] points;
        public float3 normal;

        /// ����������
        public float3 centroid;

        public Triangle(float3 a, float3 b, float3 c)
        {
            points = new float3[EDGE];

            points[0] = a;
            points[1] = b;
            points[2] = c;

            normal = math.cross(a, b);

            centroid.x = (a.x + b.x + c.x) / 3;
            centroid.y = (a.y + b.y + c.y) / 3;
            centroid.z = (a.z + b.z + c.z) / 3;
        }

        public Triangle(float3[] p)
        {
            points = new float3[3];

            normal = math.cross(p[0], p[1]);
            for (int i = 0; i < EDGE; i++)
            {
                points[i] = p[i];
            }

            centroid.x = (p[0].x + p[1].x + p[2].x) / 3;
            centroid.y = (p[0].y + p[1].y + p[2].y) / 3;
            centroid.z = (p[0].z + p[1].z + p[2].z) / 3;
        }
    }
}

