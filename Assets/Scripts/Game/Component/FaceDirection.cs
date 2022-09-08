using System;
using Unity.Mathematics;
using ECS;

namespace Game
{
    [Serializable]
    public class FaceDirection : IComponentData
    {
        public float3 Value;
    }
}
