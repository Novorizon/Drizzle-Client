using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{

    public struct Triangle2D
    {
        public static readonly int EDGE = 3;
        public float2[] points;
        public float2[] edges;
        public float2[] normals;

        public float2 a;
        public float2 b;
        public float2 c;
        /// 三角形重心
        public float2 centroid;

        public Polygon polygon;
        public Triangle2D(float2 a, float2 b, float2 c)
        {
            points = new float2[EDGE];
            edges = new float2[EDGE];
            normals = new float2[EDGE];

            this.a = a;
            this.b = b;
            this.c = c;
            points[0] = a;
            points[1] = b;
            points[2] = c;

            for (int i = 0; i < EDGE; i++)
            {
                edges[i] = points[(i + 1) % 3] - points[i];
                normals[i] = new float2(edges[i].y, -edges[i].x);
            }

            centroid.x = (a.x + b.x + c.x) / 3;
            centroid.y = (a.y + b.y + c.y) / 3;
            polygon = new Polygon(points);
        }

        public Triangle2D(float2[] p)
        {
            points = new float2[3];
            edges = new float2[3];
            normals = new float2[3];

            a = p[0];
            b = p[1];
            c = p[2];

            for (int i = 0; i < EDGE; i++)
            {
                points[i] = p[i];
                edges[i] = p[(i + 1) % 3] - p[i];
                normals[i] = new float2(edges[i].y, -edges[i].x);
            }

            centroid.x = (a.x + b.x + c.x) / 3;
            centroid.y = (a.y + b.y + c.y) / 3;
            polygon = new Polygon(points);
        }
    }
}

