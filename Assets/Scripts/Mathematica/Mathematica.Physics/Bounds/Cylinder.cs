
using Unity.Mathematics;

namespace Mathematica
{
    public struct Cylinder
    {
        public float3 Center { get; private set; }
        public float Radius { get; private set; }
        public float Height { get; private set; }
        public quaternion Orientation { get; private set; }
        public float3 Axis { get; private set; }

        public float Radius2 { get; private set; }
        public float3 Center1 { get; private set; }
        public float3 Center2 { get; private set; }
    }
}



