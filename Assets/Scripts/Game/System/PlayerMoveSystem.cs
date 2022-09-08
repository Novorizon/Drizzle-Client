using ECS;
using UnityEngine;
namespace Game
{
    [UpdateAfter(typeof(ControllerMoveSystem))]
    public class PlayerMoveSystem : SystemBase<Position, MoveDirection, Speed,PlayerController>
    {
        protected override void OnUpdate(int index, Entity entity, Position position, MoveDirection toward, Speed speed, PlayerController player)
        {
            var dt = Time.DeltaTime;
            position.Value = position.Value + toward.Value * speed.Value * dt;
        }

    }
}