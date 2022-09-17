using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    [Serializable]
    public class LinearProjectile
    {
        public EventTarget Target;
        public string EffectName;
        public float MoveSpeed; //  
        public float StartRadius; // 
        public float EndRadius; // 
        public float FixedDistance; // 
        public float StartPosition; // 
        public TargetTeams TargetTeams; // 
        public TargetType TargetType; // 

        public bool HasFrontalCone; // 前锥？？？
        public bool ProvidesVision; // 提供视野 ：导弹 精灵之火 吞噬
        public float VisionRadius; //视野半径 
    }

    public enum TargetTeams
    {
        Default,
        None,
        TeamBoth,
        TeamEnemy,//
        TeamFriendly,//
        TeamCustom,//普通队伍
    }
}