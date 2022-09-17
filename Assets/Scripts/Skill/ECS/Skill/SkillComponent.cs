using System;
using System.Collections.Generic;
using ECS;
namespace Game
{

    //[Serializable]
    //public class Ability
    //{
    //    public int id;
    //    public SkillType type;
    //    public List<int> buffs;//施加的buff
    //    public List<int> halos;//施加的halos
    //    //public int halo;//施加的halos

    //}


    [Serializable]
    public class Skill : IComponentData
    {
        public Entity caster;
        public SkillType type;
        public List<int> buffs;//施加的buff
        public List<int> halos;//施加的halos
        //public int halo;//施加的halos

    }

    public class SkillComponent : IComponentData
    {

        public Skill skill;

        public int id;

        public float cd;
        public float mana;
        public SkillType type;


        public float foreswing;//施法前摇时间
        public float foreswingTimer;//计时器
        public bool foreswingStop;

        public float channelTime;//施法吟唱时间
        public float channelTimer;//吟唱时间计时器
        public float interval;//间隔时间
        public float intervalTimer;//间隔时间计时器

        public bool channelStop;

        public float backswing;//施法后摇时间，
        public bool backswingStop;
        public float backswingTimer;//计时器

        public EntityLayerMask supposeLayer;//目标类型 队友 自己 敌人 
        //public RangeShape shape;//攻击范围形状
        public float distance; //攻击范围半径
        public float angle;  //攻击范围角度

        public bool needTarget;//指向型 

        public int maxTargets;//目标数量


        public Entity caster;
        public Entity target;//已有指定目标
        public SkillState state;

        public Entity castEffect;
        public Entity effect;
        public Entity targetEffect;

    }
    [Serializable]
    public class SkillTarget : IComponentData
    {
        public List<ulong> targets;//施加的buff

    }

}
