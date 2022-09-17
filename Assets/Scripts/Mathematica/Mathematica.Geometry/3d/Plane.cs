using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{
    public struct Plane
    {
        public float3 point;
        public float3 normal;

        public Plane(float3 point, float3 normal)
        {
            this.point = point;
            this.normal = normal;
        }
    }
}

