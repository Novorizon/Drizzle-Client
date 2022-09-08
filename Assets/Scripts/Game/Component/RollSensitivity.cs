using Cinemachine;
using ECS;
using UnityEngine;

namespace Game
{
    public class RollSensitivity : IComponentData
    {
        [Range(0.1f, 1f)]
        public float Value;
    }
    public class VirtualCamera : IComponentData
    {
        public CinemachineVirtualCamera Value;
    }
}
