
using Unity.Mathematics;
namespace Mathematica
{
    public struct Capsule : Bounds
    {
        public float3 Center { get; private set; }
        public float Radius { get; private set; }
        public float Height { get; private set; }
        public quaternion Orientation { get; private set; }
        public float3 Axis { get; private set; }

        public float Radius2 { get; private set; }
        public float3 Center1 { get; private set; }
        public float3 Center2 { get; private set; }

        float3 axisOrigin;
        public AABB Bounds { get; private set; }

        public Capsule(float3 center, float radius, float height, quaternion rotation, float3 axisOrigin)
        {
            Center = center;
            Radius = radius;
            Height = height;
            Orientation = rotation;
            Axis = math.mul(rotation, axisOrigin);
            this.axisOrigin = axisOrigin;

            Radius2 = radius * radius;
            Center1 = center + math.normalize(Axis) * (height / 2 - radius);
            Center2 = center - math.normalize(Axis) * (height / 2 - radius);
            Bounds = new AABB();
        }

        public void Update(float3 center, quaternion rotation = default)
        {
            Center = center;
            Orientation = rotation;
            Axis = math.normalize(math.mul(rotation, axisOrigin));
            Center1 = center + Axis * (Height / 2 - Radius);
            Center2 = center - Axis * (Height / 2 - Radius);
        }
    }
}

