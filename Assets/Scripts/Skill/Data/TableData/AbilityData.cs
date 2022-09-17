using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    //�˺� ���� ѣ�� ���ԣ����� ���� �ٶȡ����� ħ�� �޵�
    public enum Type
    {
        None,
        Damage,
        Heal,
        Attribute,
        Stun,//ѣ��״̬����Ŀ�겻����Ӧ�κβٿ�
        Root,//���ƣ��ֳƶ�����Ŀ�겻��Ӧ�ƶ����󣬵��ǿ���ִ��ĳЩ��������ʩ��ĳЩ����
        Silence, //��Ĭ����Ŀ���ֹʩ�ż���
        Invisible, //���������ɱ������˿���
        Immunity,
        Invincible,//�޵С����������ܵ����е��˺���Ч��Ӱ��
    }

    // ����
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
        ABILITY_BEHAVIOR_ATTACK, //��ͨ
        ABILITY_BEHAVIOR_ACTIVE,// ����
        ABILITY_BEHAVIOR_AUTOCAST, //�Զ�
        ABILITY_BEHAVIOR_PASSIVE,// ����



        DOTA_ABILITY_BEHAVIOR_POINT,//��Ҫ���
        DOTA_ABILITY_BEHAVIOR_AOE,// AOE�͵ļ�����Ҫ��һ��POINT����UNIT
        DOTA_ABILITY_BEHAVIOR_UNIT_TARGET,// ��ҪĿ��
    }


    // Ŀ������
    public enum TargetType
    {
        None,//����Ŀ��
        Player,//���
        NPC,
        BUILDING,
        TREE,
        ALL
    }

    // Ŀ����Ӫ
    public enum TargetTeam
    {
        Any,
        Caster,//ʩ�����Լ�
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

        Foreswing,//ǰҡ
        Cast,
        Channeling,
        Backswing,
        End,
    }

    public enum Projectile
    {
        None,
        Parabola , //�����ߣ� �̶���ص�
        Linear,// ���� ����ɼ� 
        Tracking,// ׷��Ŀ��

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

        // ʩ������
        public float foreswing;//ʩ��ǰҡʱ��
        public bool foreswingStop;

        public float channel;//ʩ������ʱ��
        public float interval;//���ʱ��
        public bool channelStop;

        public float backswing;//ʩ����ҡʱ�䣬
        public bool backswingStop;

        // ������Ϊ�����������Զ�����ͨ������ 
        public Behavior behavior;

        // ����Ŀ�꣺�κε�λ �ѵ� �Ѿ� �е� �о� ���е�λ
        public TargetTeam targetTeam;

        // Ŀ�����ͣ�����Ҫ�� hero NPC ���� ��ľ ���С� ������Ҫʱ
        public TargetType targetType;

        // ��Ҫ����� ��Ҫ��Ŀ�꣬ 


        // ���ܷ�Χ:   ���� ��״ �Ƕ�  �Ƿ�AOE ���Ŀ������
        public Range range;
        public float distance;
        public float angle;
        public bool isAOE;
        public int maxTargets;

        // ��Ч���ԣ� �ٶ� �����о��� Ͷ��������
        public float speed;
        public float maxDistance;
        public Projectile projectile;

        // ����������Ŀ��״̬���ȼ� Ѫ���� �Ƿ�����ħ�� �޵�
        public int limitLevel;
        public int limitHP;
        public bool ignoreImmunity;
        public bool ignoreInvincible;

        // ����Ч��
        public List<AbilityEvent> events;
        public Dictionary<AbilityState, List<Effect>> effects;


        //�Ƿ����buff
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

        public float value; //Ч����ֵ  ��������� todo
        public bool dispersable; // �ɱ���ɢ

        public Type type;//Ч������
        public Attribute attribute; //���Ч�������� �ټ����������

        public AbilityState state;//��ʲô�׶δ���
    }


    //  Ч�� ����ʱ�� ���ʱ�� 
    [Serializable]
    public class Buff
    {
        public int id;
        public string name;
        public string description;

        public float duration;// ����ʱ��
        public float interval; // ���ʱ��
        public Effect effect;

    }
}