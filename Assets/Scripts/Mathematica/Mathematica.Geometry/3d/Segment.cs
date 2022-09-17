
using Unity.Mathematics;
using Mathematica;
using System;

namespace Mathematica
{
    public struct Segment
    {
        public float3 start { get; private set; }
        public float3 end { get; private set; }

        public Segment(float3 start, float3 end)
        {
            this.start = start;
            this.end = end;
        }
    }
}