using ECS;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ability
{

    public class AbilityVO
    {
        public int id;
        public string name;
        public string description;
        public string icon;


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


        public Dictionary<EventType, List<AbilityEvent>> events;// 技能事件

        public Entity caster;
        public Entity target;//目标可能是entity
        ///===============================


















        public float cd;
        public float mana;
        public int level;
        public int maxLevel;

        // 施法过程
        public float foreswing;//施法前摇时间
        public float foreswingTimer;//前摇时间计时器

        public float channel;//施法吟唱时间
        public float interval;//间隔时间
        public float channelTimer;//吟唱时间计时器
        public float intervalTimer;//间隔时间计时器

        public float backswing;//施法后摇时间，
        public float backswingTimer;//后摇时间计时器

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

        public float speed;
        public float maxDistance;
        public Projectile projectile;

        // 技能条件：目标状态（等级 血量） 是否无视魔免 无敌
        public int limitLevel;
        public int limitHP;
        public bool ignoreImmunity;
        public bool ignoreInvincible;

        // 技能效果
        public Dictionary<AbilityState, List<Effect>> effects;


        //是否带有buff
        public List<Buff> buffs;

        public Vector3 center;//目标可能是地面
        public AbilityState state;

        public AbilityVO() { }

        public AbilityVO(AbilityData data)
        {
            this.id = data. id;
            this.name = data.name;
            this.description = data.description;
            this.icon = data.icon;
            this.cd = data.cd;
            this.mana = data.mana;
            this.maxLevel = data.maxLevel;
            this.foreswing = data.foreswing;
            this.foreswingTimer = 0;
            this.channel = data.channel;
            this.interval = data.interval;
            this.channelTimer = 0;
            this.intervalTimer = 0;
            this.backswing = data.backswing;
            this.backswingTimer = 0;
            this.behavior = data.behavior;
            this.targetTeam = data.targetTeam;
            this.targetType = data.targetType;
            this.range = data.range;
            this.distance = data.distance;
            this.angle = data.angle;
            this.isAOE = data.isAOE;
            this.maxTargets = data.maxTargets;
            this.limitLevel = data.limitLevel;
            this.limitHP = data.limitHP;
            this.ignoreImmunity = data.ignoreImmunity;
            this.ignoreInvincible = data.ignoreInvincible;

            this.effects = data.effects;
            this.buffs = data.buffs;

            this.state =AbilityState.None;
        }
    }
}
