using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;

namespace Ability
{
    public enum OverrideAnimation
    {
        Default,//默认
        act_attack,// 攻击
        act_cast_ability,//施法
        act_run,//奔跑

    }
    public enum Attributes
    {
        None,

        MULTIPLE,//可重复
        PERMANENT,//死亡保持
        IGNORE_INVULNERABLE//无敌保持
    }
    public enum Properitiy
    {
        Absolute_no_damage_magical,//魔法攻击无效
        Absolute_no_damage_physical,//物理攻击无效
        attack_range_bonus,//攻击范围奖励

    }
    public enum State
    {
        attack_immune,//攻击免疫
        cannot_miss,//无法闪避
        magic_immune,//魔免

    }
    public interface IModifier
    {
    }



    [Serializable]
    public class Modifier : IModifier
    {
        public Attributes Attributes;
        public float Duration;//持续时间
        public EventTarget Target;
        public string ModifierName;

        public bool Passive;//默认拥有
        public string TextureName;//图标
        public bool IsBuff;//正面效果
        public bool IsDebuff;//负面效果
        public bool IsHidden;//隐藏图标
        public bool IsPurgable;//可净化
        public bool AllowIllusionDuplicate;//幻象可继承
        public bool IsAura;//作为光环
        public bool Orb;//法球
        public string EffectName;//特效名
        public EffectAttachType EffectAttachedType;//特效绑定位置
        public string StatusEffectName;// 状态特效
        public string StatusEffectPriority;// 状态特效优先级
        public OverrideAnimation OverrideAnimation;//覆盖动画
        public List<Properitiy> Properities;//属性
        public List<AbilityState> States;//状态
        public List<AbilityEvent> Events;//事件

    }



}