using ECS;

namespace Game
{

    public class BuffUpdateSystem : SystemBase<BuffComponent, MagicImmunity, Invincible>
    {
        protected override void OnUpdate(int index, Entity entity, BuffComponent buffs, MagicImmunity immunity, Invincible invincible)
        {
            if (invincible.Value)
            {
                Buff buff = buffs.buffs.Find(a => a.to == BuffTo.Invincible);
                buffs.buffs.Clear();
                buffs.buffs.Add(buff);
            }
            else if (immunity.Value)
            {
                Buff buff = buffs.buffs.Find(a => a.to == BuffTo.MagicImmunity);
                buffs.buffs.Clear();
                buffs.buffs.Add(buff);
            }

            for (int i = 0; i < buffs.buffs.Count; i++)
            {
                Buff buff = buffs.buffs[i];

                float dt = Time.DeltaTime;

                buff.timer += dt;

                if ((buff.type & BuffType.Continuous) > 0)//持续型buff
                {

                }
                else //间歇型buff
                {
                    buff.intervalTimer += dt;
                    if (buff.intervalTimer >= buff.interval)
                    {
                        BuffManager.UpdateBuff(entity, buff, 1);
                        buff.intervalTimer = 0;
                    }
                }
            }
        }
    }
}
