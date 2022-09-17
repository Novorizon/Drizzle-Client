using Mathematica;
using System;
using Unity.Mathematics;

namespace Mathematica
{

    public struct Circular
    {
        public float2 center;
        public float radius;
        public float radius2;
        public Circular(float2 center, float radius)
        {

            this.center = center;
            this.radius = radius;
            radius2 = radius * radius;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (center.GetHashCode() * 397) ^ radius.GetHashCode();
            }
        }

    }
}

