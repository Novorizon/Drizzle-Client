using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;

namespace Ability
{

    public class Ability
    {
        public Type baseclass;
        public string AbilityTextureName;//图标
        public AbilityBehavior AbilityBehavior;
        public AbilityUnitTargetType AbilityUnitTargetType;
        public AbilityUnitTargetTeam AbilityUnitTargetTeam;
        public AbilityUnitDamageType AbilityUnitDamageType;
        public SpellImmunityType SpellImmunityType;
        public bool CastFilterRejectCaster;//对释放者无效
        public int FightRecapLevel;//战斗回放等级	
        public AbilityType AbilityType;//技能类型	
        public string HotKeyOverride;//热键
        public int MaxLevel;//最大等级	
        public int RequiredLevel;//需求等级	
        public int LevelsBetweenUpgrades;//升级间隔	
        public float[] AbilityCastPoint;//施法前摇	时间
        public string AbilityCastAnimation;//施法动作
        public float AnimationPlaybackRate;//动画播放速率	
        public float[] AbilityCooldown;//冷却时间	
        public int AbilityManaCost;//魔法消耗	
        public float AbilityCastRange;//施法距离	
        public float AbilityCastRangeBuffer;//施法距离缓冲	
        public float AbilityChannelTime;//持续施法时间	
        public float AbilityChannelledManaCostPerSecond;//续施法每秒耗魔	
        public float AOERadius;//AOE范围	


      
        public List<AbilityEvent> events;  // 技能事件
    }

    public enum AbilityBehavior
    {
        IMMEDIATE=1<<1, // 立即   —— 无目标 点击技能图标 立刻创建 有目标时 选中目标立刻创建
        HIDDEN = 1 << 2, // 隐藏
        PASSIVE = 1 << 3,// 被动 —— 学习时 创建技能
        NO_TARGET = 1 << 4,// 无目标
        UNIT_TARGET = 1 << 5,// 目标
        POINT = 1 << 6,// 点 
        AOE = 1 << 7,// 范围 
        CHANNELLED = 1 << 8,// 持续施法
        NOT_LEARNABLE = 1 << 9,// 不可学习 
        ITEM = 1 << 10,// 物品?
        TOGGLE = 1 << 11,// 开关
        DIRECTIONAL=1<<12,// 方向 
        AUTOCAST = 1 << 13,// 自动施法  —— 学习或设置自动施法时 创建技能
        NOASSIST = 1 << 14,// 无辅助网格
        AURA = 1 << 15,// 光环（无用）
        ATTACK = 1 << 16,// 法球  ???
        DONT_RESUME_MOVEMENT = 1 << 17,// 不恢复移动
        ROOT_DISABLES = 1 << 18,// 定身无法释放 —— 点击技能图标时判断
        UNRESTRICTED = 1 << 19,// 无视限制
        IGNORE_PSEUDO_QUEUE = 1 << 20,// 总有效-自动施法
        IGNORE_CHANNEL = 1 << 21,// 施法打断有效
        DONT_CANCEL_MOVEMENT = 1 << 22,// 不影响移动 
        DONT_ALERT_TARGET = 1 << 23,// 不惊醒目标
        DONT_RESUME_ATTACK = 1 << 24,// 不恢复攻击
        NORMAL_WHEN_STOLEN = 1 << 25,// 偷取保持前摇
        IGNORE_BACKSWING = 1 << 26,// 无视后摇
        RUNE_TARGET = 1 << 27,// 神符目标
        DONT_CANCEL_CHANNEL = 1 << 28,// 不取消持续施法?
        OPTIONAL_UNIT_TARGET = 1 << 29,// 可选单位目标?
        OPTIONAL_NO_TARGET = 1 << 30,// 可选无目标? 
    }


    // 目标类型
    public enum AbilityUnitTargetType
    {
        HERO,//
        BASIC,//基本
        ALL,
        BUILDING,
        COURIER,//信使
        CREEP,//野怪
        CUSTOM,//普通 
        MECHANICAL,//机械(已移除)
        NONE,//
        OTHER,//
        TREE,//
    }

    // 目标队伍
    public enum AbilityUnitTargetTeam
    {
        Default,
        Unit_target_team_both,//双方队伍
        Unit_target_team_ENEMY,
        Unit_target_team_Friendly,
        Unit_target_team_custom,//普通
        Unit_target_team_none,//无
    }


    // 伤害类型
    public enum AbilityUnitDamageType
    {
        Default,//
        Damage_Type_magical,//魔法攻击
        Damage_Type_Physical, // 物理攻击
        Damage_Type_pure,// 纯粹伤害
    }


    // 技能免疫类型
    public enum SpellImmunityType
    {
        Default,//
        spell_immunity_none,//无
        spell_immunity_allies_yes, // 可以用于技能免疫的友军
        spell_immunity_allies_no,// 不可以用于技能免疫的友军
        spell_immunity_enemies_yes, // 可以用于技能免疫的敌军
        spell_immunity_enemies_no,// 不可以用于技能免疫的敌军
    }

    // 技能类型
    public enum AbilityType
    {
        DEFAULT,//
        ABILITY_TYPE_BASIC,//普通技能
        ABILITY_TYPE_ULTIMATE, // 终极技能
        ABILITY_TYPE_ATTRIBUTES, // 用于属性奖励
        ABILITY_TYPE_HIDDEN, // 干啥的？
    }
}