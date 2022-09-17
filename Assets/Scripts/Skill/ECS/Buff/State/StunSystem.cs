using Ability;
using ECS;

namespace Game
{
    public class StunSystem : SystemBase<Speed, StunComponent>
    {
        protected override void OnUpdate(int index, Entity entity, Speed speed, StunComponent stun)
        {
            //应为移动组件 move 为0
            //if (stun.Value)
            //    speed.Value = 0;
        }
    }
}
