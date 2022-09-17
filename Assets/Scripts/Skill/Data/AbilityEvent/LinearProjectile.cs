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

        public bool HasFrontalCone; // ǰ׶������
        public bool ProvidesVision; // �ṩ��Ұ ������ ����֮�� ����
        public float VisionRadius; //��Ұ�뾶 
    }

    public enum TargetTeams
    {
        Default,
        None,
        TeamBoth,
        TeamEnemy,//
        TeamFriendly,//
        TeamCustom,//��ͨ����
    }
}