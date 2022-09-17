//using PureMVCFramework.Entity;

//namespace Game
//{
//    public class SkillAddHaloSystem : ComponentSystem<Skill, Halo>
//    {
//        protected override void OnUpdate(int index, Entity entity, Skill skill, Halo halo)
//        {
//            if (skill.sh == 0)
//                return;

//            if ((skill.type & SkillType.Halo) == 0)
//                return;

//            for (int i = 0; i < skill.buffs.length; i++)
//            {
//                if (BuffProxy.Has(skill.skill.buffs[i]))
//                {
//                    BuffClip buff = BuffProxy.GetBuff(skill.skill.buffs[i]);
//                    halo.buffs.Add(buff);
//                }
//            }
//            skill = new Skill();

//        }
//    }
//}
