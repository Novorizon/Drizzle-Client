
using Ability;
using ECS;
using Game;

namespace Game
{
    public class BuffManager
    {
        public static void UpdateBuff(Entity entity, Buff buff, int sign)
        {
            switch (buff.to)
            {
                case BuffTo.Attack:
                    {
                        Attack value = entity.GetOrAddComponentData<Attack>();
                        value.Value.UpdateBuff(buff.type, buff.value, sign);
                    }
                    break;

                case BuffTo.Armor:
                    {
                        Armor value = entity.GetOrAddComponentData<Armor>();
                        value.Value.UpdateBuff(buff.type, buff.value, sign);
                    }
                    break;
                case BuffTo.Speed:
                    {
                        Speed value = entity.GetOrAddComponentData<Speed>();
                        //value.Value.UpdateBuff(buff.type, buff.value, sign);
                    }
                    break;
                case BuffTo.HP:
                    {
                        HP value = entity.GetOrAddComponentData<HP>();
                        value.Value.UpdateBuff(buff.type, buff.value, sign);
                    }
                    break;

                case BuffTo.MagicImmunity:
                    {
                        MagicImmunity value = buff.entity.GetOrAddComponentData<MagicImmunity>();
                        value.Value = sign > 0 ? true : false;
                    }
                    break;
                case BuffTo.Invincible:
                    {
                        Invincible value = buff.entity.GetOrAddComponentData<Invincible>();
                        value.Value = sign > 0 ? true : false;
                    }
                    break;
                case BuffTo.Visible://需要同步到gameobject的透明度，仇恨值（？),其他玩家的可见列表
                    {
                        Visible value = buff.entity.GetOrAddComponentData<Visible>();
                        value.Value.UpdateBuff(buff.type, buff.value, sign);
                    }
                    break;
                case BuffTo.Stun:
                    {
                        StunComponent value = buff.entity.GetOrAddComponentData<StunComponent>();
                        //value.Value = sign > 0 ? true : false;
                    }
                    break;

            }
        }

        //public static void AddBuff(Buff buff)
        //{
        //    switch (buff.to)
        //    {
        //        case BuffTo.Attack:
        //            {
        //                Attack value = buff.entity.GetOrAddComponentData<Attack>();
        //                value.Value.AddBuff(buff.type, buff.value);
        //            }
        //            break;

        //        case BuffTo.Armor:
        //            {
        //                Armor value = buff.entity.GetOrAddComponentData<Armor>();
        //                value.Value.AddBuff(buff.type, buff.value);
        //            }
        //            break;
        //        case BuffTo.Speed:
        //            {
        //                Speed value = buff.entity.GetOrAddComponentData<Speed>();
        //                value.Value.AddBuff(buff.type, buff.value);
        //            }
        //            break;
        //        case BuffTo.HP:
        //            {
        //                HP value = buff.entity.GetOrAddComponentData<HP>();
        //                value.Value.AddBuff(buff.type, buff.value);
        //            }
        //            break;

        //        case BuffTo.MagicImmunity:
        //            {
        //                MagicImmunity value = buff.entity.GetOrAddComponentData<MagicImmunity>();
        //                value.Value.AddBuff(buff.type, buff.value);
        //            }
        //            break;
        //        case BuffTo.Invincible:
        //            {
        //                Invincible value = buff.entity.GetOrAddComponentData<Invincible>();
        //                value.Value.AddBuff(buff.type, buff.value);
        //            }
        //            break;

        //    }
        //}

    }
}