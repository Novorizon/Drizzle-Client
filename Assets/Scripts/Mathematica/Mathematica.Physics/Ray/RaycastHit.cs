
using Mathematica;
using System;
using Unity.Mathematics;
using Unity.Mathematics;

namespace Mathematica
{
    public struct RaycastHit
    {
        public long id { get;  set; }
        public float3 point { get; set; }
        public Bounds bounds { get; set; }
        //public float distance { get; set; }
    }
}