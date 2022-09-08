using ECS;
using System;
using Unity.Mathematics;

namespace Game
{
    [Serializable]
    public class MoveDirection : IComponentData
    {
        public float3 Value;
    }

    [Serializable]
    public struct Toward : IComponentData
    {
        public float3 Value;
    }
}
