using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    //伤害 治疗 眩晕 属性（工具 防御 速度……） 魔免 无敌
    public enum Type
    {
        None,
        Damage,
        Heal,
        Attribute,
        Stun,//眩晕状态——目标不再响应任何操控
        Root,//缠绕，又称定身——目标不响应移动请求，但是可以执行某些操作，如施放某些技能
        Silence, //沉默——目标禁止施放技能
        Invisible, //隐身——不可被其他人看见
        Immunity,
        Invincible,//无敌——几乎不受到所有的伤害和效果影响
    }

    // 属性
    public enum Attribute
    {
        None,
        Attack,
        Defence,
        Speed,
        Health,


    }

    public enum Behavior
    {
        ABILITY_BEHAVIOR_ATTACK, //普通
        ABILITY_BEHAVIOR_ACTIVE,// 主动
        ABILITY_BEHAVIOR_AUTOCAST, //自动
        ABILITY_BEHAVIOR_PASSIVE,// 被动



        DOTA_ABILITY_BEHAVIOR_POINT,//需要点地
        DOTA_ABILITY_BEHAVIOR_AOE,// AOE型的技能需要有一个POINT或是UNIT
        DOTA_ABILITY_BEHAVIOR_UNIT_TARGET,// 需要目标
    }


    // 目标类型
    public enum TargetType
    {
        None,//不需目标
        Player,//玩家
        NPC,
        BUILDING,
        TREE,
        ALL
    }

    // 目标阵营
    public enum TargetTeam
    {
        Any,
        Caster,//施法者自己
        Friend,
        TEAM_Friend,
        ENEMY,
        TEAM_ENEMY,
        ALL,
    }

    public enum Range
    {
        Line,
        Sphere,
        Fan,
        Triangle,
        Rectangle

    }

    public enum AbilityState
    {
        None,

        Foreswing,//前摇
        Cast,
        Channeling,
        Backswing,
        End,
    }

    public enum Projectile
    {
        None,
        Parabola , //抛物线， 固定落地点
        Linear,// 线性 例如飞箭 
        Tracking,// 追踪目标

    }


    [Serializable]
    public class AbilityData
    {
        public int id;
        public string name;
        public string description;
        public string icon;

        public float cd;
        public float mana;
        public int maxLevel;

        // 施法过程
        public float foreswing;//施法前摇时间
        public bool foreswingStop;

        public float channel;//施法吟唱时间
        public float interval;//间隔时间
        public bool channelStop;

        public float backswing;//施法后摇时间，
        public bool backswingStop;

        // 技能行为：主动被动自动，普通攻击， 
        public Behavior behavior;

        // 作用目标：任何单位 友单 友军 敌单 敌军 所有单位
        public TargetTeam targetTeam;

        // 目标类型：不需要， hero NPC 建筑 树木 所有。 当不需要时
        public TargetType targetType;

        // 需要点地面 需要点目标， 


        // 技能范围:   距离 形状 角度  是否AOE 最大目标数量
        public Range range;
        public float distance;
        public float angle;
        public bool isAOE;
        public int maxTargets;

        // 特效属性： 速度 最大飞行距离 投射物类型
        public float speed;
        public float maxDistance;
        public Projectile projectile;

        // 技能条件：目标状态（等级 血量） 是否无视魔免 无敌
        public int limitLevel;
        public int limitHP;
        public bool ignoreImmunity;
        public bool ignoreInvincible;

        // 技能效果
        public List<AbilityEvent> events;
        public Dictionary<AbilityState, List<Effect>> effects;


        //是否带有buff
        public List<Buff> buffs;


        public bool Has(Behavior behavior)=> (this.behavior & behavior) > 0;
        public bool Has(TargetTeam team)
        {
            return (this.targetTeam & team) > 0;
        }
    }


    //  
    [Serializable]
    public class Effect
    {
        public int id;
        public string name;
        public string description;

        public float value; //效果数值  如果是属性 todo
        public bool dispersable; // 可被驱散

        public Type type;//效果类型
        public Attribute attribute; //如果效果是属性 再检查属性类型

        public AbilityState state;//在什么阶段触发
    }


    //  效果 持续时长 间隔时长 
    [Serializable]
    public class Buff
    {
        public int id;
        public string name;
        public string description;

        public float duration;// 持续时间
        public float interval; // 间隔时间
        public Effect effect;

    }
}