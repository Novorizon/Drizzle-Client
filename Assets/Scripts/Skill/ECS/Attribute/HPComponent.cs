
using System;
using ECS;

namespace Game
{
    [Serializable]
    public struct HP : IComponentData
    {
        public Attribute Value;
    }

}
