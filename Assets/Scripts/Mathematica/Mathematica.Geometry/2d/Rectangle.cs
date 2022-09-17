using Mathematica;
using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Mathematica
{
    [Serializable]
    public struct Rectangle
    {
        public static readonly int EDGE = 4;
        public float2[] points;
        public float2[] edges;
        public float2[] normals;

        public float2 a;
        public float2 b;
        public float2 c;
        public float2 d;

        public float2 bevelRadius;
        public float2 centroid;
        public quaternion orientation;

        public Polygon polygon;


        public Rectangle(float2 a, float2 b, float2 c, float2 d, quaternion rotation = default)
        {
            points = new float2[EDGE];
            edges = new float2[EDGE];
            normals = new float2[EDGE];

            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            points[0] = a;
            points[1] = b;
            points[2] = c;
            points[3] = d;

            for (int i = 0; i < EDGE; i++)
            {
                edges[i] = points[(i + 1) % 4] - points[i];
                normals[i] = new float2(edges[i].y, -edges[i].x);
            }

            centroid.x = (a.x + b.x + c.x + d.y) / 4;
            centroid.y = (a.y + b.y + c.y + d.y) / 4;
            bevelRadius = math.abs(points[0] - centroid);
            orientation = rotation;
            polygon = new Polygon(points);
        }


        public Rectangle(float2[] p, quaternion rotation = default)
        {
            points = new float2[4];
            edges = new float2[4];
            normals = new float2[4];

            a = p[0];
            b = p[1];
            c = p[2];
            d = p[3];

            points[0] = a;
            points[1] = a;
            points[2] = a;
            points[3] = d;

            for (int i = 0; i < EDGE; i++)
            {
                edges[i] = points[(i + 1) % 4] - points[i];
                normals[i] = new float2(edges[i].y, -edges[i].x);
            }

            centroid.x = (a.x + b.x + c.x + d.y) / 4;
            centroid.y = (a.y + b.y + c.y + d.y) / 4;
            bevelRadius = math.abs(points[0] - centroid);
            orientation = rotation;
            polygon = new Polygon(points);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = a.GetHashCode();
                hashCode = (hashCode * 397) ^ b.GetHashCode();
                hashCode = (hashCode * 397) ^ c.GetHashCode();
                hashCode = (hashCode * 397) ^ d.GetHashCode();
                return hashCode;
            }
        }


    }
}