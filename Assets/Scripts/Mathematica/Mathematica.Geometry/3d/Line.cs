
using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{
    public struct Line
    {
        public float3 point { get; set; }
        public float3 direction { get; set; }

        public Line(float3 point, float3 direction)
        {
            this.point = point;
            this.direction = direction;
        }
    }
}