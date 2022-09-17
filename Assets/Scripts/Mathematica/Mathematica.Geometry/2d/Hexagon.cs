using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{
    //Regular Hexagon
    public struct Hexagon
    {
        public static readonly int EDGE = 6;
        public static readonly float INEX = 0.86602540378445f;//ÄÚ¾¶:Íâ¾¶
        public float2[] points;
        public float2[] edges;
        public float2[] normals;

        public float2 a;
        public float2 b;
        public float2 c;
        public float2 d;
        public float2 e;
        public float2 f;

        public float2 center;
        public float exradius;
        public float inradius;

        public Polygon polygon;

        public Hexagon(float2 a, float2 b, float2 c, float2 d, float2 e, float2 f)
        {
            points = new float2[EDGE];
            edges = new float2[EDGE];
            normals = new float2[EDGE];

            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
            points[0] = a;
            points[1] = b;
            points[2] = c;
            points[3] = d;
            points[4] = e;
            points[5] = f;

            for (int i = 0; i < points.Length; i++)
            {
                edges[i] = points[(i + 1) % 4] - points[i];
                normals[i] = new float2(edges[i].y, -edges[i].x);
            }

            center.x = (a.x + c.x + e.x) / 3;
            center.y = (a.y + c.y + e.y) / 3;
            exradius = math.distance(center, a);
            inradius = INEX * exradius;

            polygon = new Polygon(points);
        }

        public Hexagon(float2[] p)
        {
            points = new float2[EDGE];
            edges = new float2[EDGE];
            normals = new float2[EDGE];
            a = p[0];
            b = p[1];
            c = p[2];
            d = p[3];
            e = p[4];
            f = p[5];

            for (int i = 0; i < EDGE; i++)
            {
                points[i] = p[i];
                edges[i] = p[(i + 1) % p.Length] - p[i];
                normals[i] = new float2(edges[i].y, -edges[i].x);
            }
            center.x = (a.x + c.x + e.x) / 3;
            center.y = (a.y + c.y + e.y) / 3;
            exradius = math.distance(center, a);
            inradius = INEX * exradius;

            polygon = new Polygon(points);
        }
    }
}

