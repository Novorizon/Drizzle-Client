using Ability;
using ECS;

namespace Game
{
    public class StunSystem : SystemBase<Speed, StunComponent>
    {
        protected override void OnUpdate(int index, Entity entity, Speed speed, StunComponent stun)
        {
            //ӦΪ�ƶ���� move Ϊ0
            //if (stun.Value)
            //    speed.Value = 0;
        }
    }
}
