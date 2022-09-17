
using Unity.Mathematics;
using System;

namespace Mathematica
{
    [Serializable]
    public struct Sphere : Bounds
    {
        public float3 Center { get; private set; }
        public float Radius { get; private set; }
        public float Radius2 { get; private set; }
        public AABB Bounds { get; private set; }
        float3 R;

        public Sphere(float3 center, float radius)
        {
            Center = center;
            Radius = radius;
            Radius2 = radius * radius;
            R = new float3(Radius, Radius, Radius);
            Bounds = new AABB(Center - R, Center + R);
        }

        public void Update(float3 center)
        {
            Center = center;
            Bounds = new AABB(Center - R, Center + R);
        }
    }


}

