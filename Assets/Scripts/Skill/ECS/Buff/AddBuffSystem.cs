//using ECS;
//using PureMVC.Patterns.Facade;

//namespace Game
//{
//    public class AddBuffSystem : SystemBase<Skill, SkillTarget>
//    {
//        BuffProxy proxy;

//        protected override void OnCreate()
//        {
//            //proxy = Facade.RetrieveProxy(BuffProxy.NAME) as BuffProxy;
//        }

//        //技能Entity
//        protected override void OnUpdate(int index, Entity entity, Skill skill, SkillTarget target)
//        {
//            if (skill.buffs.Count == 0)
//                return;
//            //if (skill.targets.Length == 0)
//            //    return;
//            if ((skill.type & SkillType.Halo) > 0)
//                return;

//            for (int i = 0; i < target.targets.Count; i++)
//            {
//                EntityManager.TryGetEntity(target.targets[i], out Entity targetEntity);
//                if (targetEntity == null)
//                    continue;

//                BuffData data = proxy.GetBuffConfig(skill.buffs[i]);

//                //不是叠加型 也不是替换型
//                if ((data.type & BuffType.Overlap) == 0 && (data.type & BuffType.Replace) == 0)
//                    continue;

//                BuffComponent buffComponent = proxy.GetBuff(targetEntity.GUID);
//                if ((data.type & BuffType.Overlap) > 0) //叠加
//                {
//                    //增加buff效果
//                    Buff buff = proxy.InitBuff(skill.buffs[i]);
//                    buffComponent.buffs.Add(buff);
//                }
//                else if ((data.type & BuffType.Replace) > 0) //替换
//                {
//                    //增加buff效果
//                    Buff buff = buffComponent.buffs.Find(a => a.id == data.id);
//                    if (buff == null)
//                    {
//                        buff = proxy.InitBuff(skill.buffs[i]);
//                        buffComponent.buffs.Add(buff);
//                    }
//                    buff.timer = 0;
//                }
//            }

//            //if (track.state == TrackState.End)
//            //{
//            //    EntityManager.DestroyEntity(skill.skill.effect);
//            //    EntityManager.DestroyEntity(entity);

//            //    skill = new Skill();
//            //}
//            //targets.Clear();
//            skill.buffs.Clear();
//        }
//    }
//}
