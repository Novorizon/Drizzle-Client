
using System;
using System.Collections;

namespace Game
{
    //定义
    //蓄力：随着蓄力时间长短，伤害随之变化
    //施法吟唱：技能开始，CD触发——吟唱——技能伤害。若吟唱期间被打断，CD仍然需要重新计时
    //施法前摇：技能开始，CD不触发——前摇——技能伤害，CD触发。若前摇期间被打断，CD不需要需要重新计时
    //施法前摇：技能发动之后（可能一次性发动，可能持续施法）,进行的一些动作
    //如果同时有以上阶段，则依次处理

    //控制： 眩晕stun，捆绑，冰冻等，被控制目标，会有以下效果：不能移动，不能施法，施法被打断，移动速度降低

    //一个技能最多加一个光环
    //光环的目标是施法者本人
    public enum SkillType
    {
        None,
        LockTarget,
        Buff,
        Halo,
    }


    //单次攻击：Debuff | Plus（-）
    //持续掉血：Debuff | Plus（-） | Interval
    //单次回血：Buff | Plus |  
    //持续回血：Buff | Plus | Interval
    //回蓝：Buff | Plus | Continuous | Reset
    //提升速度：Buff | Plus | Reset
    //吸血：Buff | Plus | ？
    //暴击：Buff | Plus | Reset
    //魔免：Buff | MagicImmunity 
    //无敌：Buff | Invincible 
    //光环类：Halo TODO：多人效果
    //隐身：Buff | Plus（-） | Continuous | Reset
    [Serializable]
    public enum BuffType : ulong
    {
        None = 1,
        Buff = None << 1,
        Debuff = None << 2,
        Hybrid = None << 3,//混合

        Plus = None << 4,
        Mul = None << 5,

        MagicImmunity = None << 11,
        Invincible = None << 12,

        Once = None << 21,//一次型  一般用作单次伤害 
        Continuous = None << 22,//持续型  一般用作一段时间内持续改变某种属性，对应间歇型 
        Interval = None << 23,//间断型  一般用作一段时间内 间隔一段时间改变某种属性
        //Permanent = None << 22,//永久型 移除buff也不会移除效果
        //Once,Continuous ,Interval三者不能同时出现，但至少有一个

        Overlap = None << 23,//叠加
        Replace = None << 24,//以新换旧，新的效果取代旧的

        Halo = None << 31,//光环

        Reset = None << 32,//删除buff以后需要重置改变的属性
    }

    public enum BuffTo
    {
        None = 0,
        Attack,
        Armor = Attack << 1,
        Speed = Attack << 2,
        HP = Attack << 3,
        WarningRange = Attack << 4,
        AttackRange = Attack << 5,

        MagicImmunity = Attack << 10,
        Invincible = Attack << 11,
        Visible = Attack << 12,
        Stun = Attack << 13,

    }
    public enum BuffState
    {
        None,
        Added,
    }
    public enum SkillState
    {
        None,

        Foreswing,//前摇
        Cast,
        Channeling,
        Backswing,
        End,
    }
}
