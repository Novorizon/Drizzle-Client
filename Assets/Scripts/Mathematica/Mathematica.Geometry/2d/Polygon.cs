using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{

    public struct Polygon
    {
        public static int EDGE;
        public float2[] points;
        public float2[] edges;
        public float2[] normals;
        public Polygon(float2[] p)
        {
            EDGE = p.Length;

            points = new float2[EDGE];
            edges = new float2[EDGE];
            normals = new float2[EDGE];
            for (int i = 0; i < EDGE; i++)
            {
                points[i] = p[i];
                edges[i] = p[(i + 1) % EDGE] - p[i];
                normals[i] = new float2(edges[i].y, -edges[i].x);
            }
        }
    }
}

