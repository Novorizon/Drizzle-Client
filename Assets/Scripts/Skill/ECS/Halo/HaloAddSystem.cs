//using ECS;
//using PureMVC.Patterns.Facade;

//namespace Game
//{
//    public class HaloAddSystem : SystemBase<Skill>
//    {
//        HaloProxy proxy;

//        protected override void OnCreate()
//        {
//            proxy = Facade.RetrieveProxy(HaloProxy.NAME) as HaloProxy;
//        }

//        //技能Entity
//        protected override void OnUpdate(int index, Entity entity, Skill skill)
//        {
//            if (skill.halos.Count == 0)
//                return;

//            if ((skill.type & SkillType.Buff) > 0)
//                return;

              

//                BuffData data = proxy.GetHaloConfig(skill.halo);


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
