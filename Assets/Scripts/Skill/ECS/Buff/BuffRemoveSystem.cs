using ECS;
using Game;

namespace Game
{

}
//public class RemoveBuffSystem1 : SystemBaseFacade
//{
//    protected override void OnUpdate()
//    {
//        return;
//        Entities
//            .ForEach((ref Buff buff, in Timer timer, in Entity entity) =>
//            {
//                if (timer.Value >= buff.time)
//                {
//                    BuffManager.RemoveBuff(buff);
//                    buff.state = BuffState.None;
//                    if (EntityManager.HasComponent<Span>(entity))
//                    {
//                        EntityManager.SetComponentData(entity, new Span { Value = 0 });
//                        ////删除玩家身上buff  TODO：简化流程
//                        //var Parent = EntityManager.GetComponentData<Parent>(entity);
//                        //var buffs = EntityManager.GetBuffer<Buff>(Parent.Value);
//                        //for (int i = 0; i < buffs.Length; i++)
//                        //{
//                        //    if (buffs[i].buff.id == buff.id)
//                        //    {
//                        //        buffs.RemoveAt(i);
//                        //        break;
//                        //    }
//                        //}
//                    }
//                }
//            })
//            .WithStructuralChanges()
//            .WithoutBurst()
//            .Run();
//    }
//}


public class RemoveBuffSystem : SystemBase<BuffComponent>
{
    protected override void OnUpdate(int index, Entity entity, BuffComponent buffs)
    {
        for (int i = 0; i < buffs.buffs.Count; ++i)
        {
            Buff buff = buffs.buffs[i];

            if (buff.timer >= buff.time)
            {
                buffs.buffs.RemoveAt(i);
                if ((buff.type & BuffType.Reset) > 0)
                {
                    BuffManager.UpdateBuff(entity, buff, -1);
                }
                continue;
            }
        }
    }
}

