using ECS;
using System;
using Unity.Mathematics;

namespace Game
{
    [Serializable]
    public class Velocity : IComponentData
    {
        public float3 Value;
    }
}
