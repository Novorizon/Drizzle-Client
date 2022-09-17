
using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{
    public struct Ray
    {
        public float3 origin { get; set; }
        public float3 direction { get; set; }

        public Ray(float3 origin, float3 direction)
        {
            this.origin = origin;
            this.direction = direction;
        }

        public float3 GetPoint(float distance)
        {
            return origin + distance * direction;
        }

        public override string ToString()
        {
            return origin.ToString() + " " + direction.ToString();
        }
    }
}