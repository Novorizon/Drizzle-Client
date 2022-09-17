using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{
    public struct Sector
    {
        public static readonly int VERTEX = 2;//Ô²»¡¶Ëµã
        public float3 center;
        public float radius;
        public float radius2;
        public float3[] points;

        public Sector(float3 center, float radius, float3[] p)
        {
            points = new float3[VERTEX];

            this.center = center;
            this.radius = radius;
            radius2 = radius * radius;
            points = p;
        }

        public Sector(float3 center, float radius, float3 a, float3 b)
        {
            points = new float3[VERTEX];

            this.center = center;
            this.radius = radius;
            radius2 = radius * radius;
            points[0] = a;
            points[1] = b;
        }


        public override int GetHashCode()
        {
            return center.GetHashCode();
        }

    }
}

