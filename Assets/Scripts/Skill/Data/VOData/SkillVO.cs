using ECS;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class SkillVO
    {
        public ulong guid;

        public int id;

        public float cd;
        public float mana;
        public SkillType type;

        public float channel;//施法吟唱时间
        public bool channelStop;

        public float foreswing;//施法前摇时间
        public bool foreswingStop;

        public float duration;//施法时间
        public bool durationStop;

        public float backswing;//施法后摇时间，
        public bool backswingStop;

        public EntityLayerMask supposeLayer;//目标类型 队友 自己 敌人 
        //public RangeShape shape;//攻击范围形状
        public float distance; //攻击范围半径
        public float angle;  //攻击范围角度

        public bool needTarget;//指向型 

        public int maxTargets;//目标数量

        public Entity castEffect;
        public Entity effect;
        public Entity targetEffect;
        public List<int> buffs;//施加的buff
    }
}
