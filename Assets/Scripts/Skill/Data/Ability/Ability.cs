using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;

namespace Ability
{

    public class Ability
    {
        public Type baseclass;
        public string AbilityTextureName;//ͼ��
        public AbilityBehavior AbilityBehavior;
        public AbilityUnitTargetType AbilityUnitTargetType;
        public AbilityUnitTargetTeam AbilityUnitTargetTeam;
        public AbilityUnitDamageType AbilityUnitDamageType;
        public SpellImmunityType SpellImmunityType;
        public bool CastFilterRejectCaster;//���ͷ�����Ч
        public int FightRecapLevel;//ս���طŵȼ�	
        public AbilityType AbilityType;//��������	
        public string HotKeyOverride;//�ȼ�
        public int MaxLevel;//���ȼ�	
        public int RequiredLevel;//����ȼ�	
        public int LevelsBetweenUpgrades;//�������	
        public float[] AbilityCastPoint;//ʩ��ǰҡ	ʱ��
        public string AbilityCastAnimation;//ʩ������
        public float AnimationPlaybackRate;//������������	
        public float[] AbilityCooldown;//��ȴʱ��	
        public int AbilityManaCost;//ħ������	
        public float AbilityCastRange;//ʩ������	
        public float AbilityCastRangeBuffer;//ʩ�����뻺��	
        public float AbilityChannelTime;//����ʩ��ʱ��	
        public float AbilityChannelledManaCostPerSecond;//��ʩ��ÿ���ħ	
        public float AOERadius;//AOE��Χ	


      
        public List<AbilityEvent> events;  // �����¼�
    }

    public enum AbilityBehavior
    {
        IMMEDIATE=1<<1, // ����   ���� ��Ŀ�� �������ͼ�� ���̴��� ��Ŀ��ʱ ѡ��Ŀ�����̴���
        HIDDEN = 1 << 2, // ����
        PASSIVE = 1 << 3,// ���� ���� ѧϰʱ ��������
        NO_TARGET = 1 << 4,// ��Ŀ��
        UNIT_TARGET = 1 << 5,// Ŀ��
        POINT = 1 << 6,// �� 
        AOE = 1 << 7,// ��Χ 
        CHANNELLED = 1 << 8,// ����ʩ��
        NOT_LEARNABLE = 1 << 9,// ����ѧϰ 
        ITEM = 1 << 10,// ��Ʒ?
        TOGGLE = 1 << 11,// ����
        DIRECTIONAL=1<<12,// ���� 
        AUTOCAST = 1 << 13,// �Զ�ʩ��  ���� ѧϰ�������Զ�ʩ��ʱ ��������
        NOASSIST = 1 << 14,// �޸�������
        AURA = 1 << 15,// �⻷�����ã�
        ATTACK = 1 << 16,// ����  ???
        DONT_RESUME_MOVEMENT = 1 << 17,// ���ָ��ƶ�
        ROOT_DISABLES = 1 << 18,// �����޷��ͷ� ���� �������ͼ��ʱ�ж�
        UNRESTRICTED = 1 << 19,// ��������
        IGNORE_PSEUDO_QUEUE = 1 << 20,// ����Ч-�Զ�ʩ��
        IGNORE_CHANNEL = 1 << 21,// ʩ�������Ч
        DONT_CANCEL_MOVEMENT = 1 << 22,// ��Ӱ���ƶ� 
        DONT_ALERT_TARGET = 1 << 23,// ������Ŀ��
        DONT_RESUME_ATTACK = 1 << 24,// ���ָ�����
        NORMAL_WHEN_STOLEN = 1 << 25,// ͵ȡ����ǰҡ
        IGNORE_BACKSWING = 1 << 26,// ���Ӻ�ҡ
        RUNE_TARGET = 1 << 27,// ���Ŀ��
        DONT_CANCEL_CHANNEL = 1 << 28,// ��ȡ������ʩ��?
        OPTIONAL_UNIT_TARGET = 1 << 29,// ��ѡ��λĿ��?
        OPTIONAL_NO_TARGET = 1 << 30,// ��ѡ��Ŀ��? 
    }


    // Ŀ������
    public enum AbilityUnitTargetType
    {
        HERO,//
        BASIC,//����
        ALL,
        BUILDING,
        COURIER,//��ʹ
        CREEP,//Ұ��
        CUSTOM,//��ͨ 
        MECHANICAL,//��е(���Ƴ�)
        NONE,//
        OTHER,//
        TREE,//
    }

    // Ŀ�����
    public enum AbilityUnitTargetTeam
    {
        Default,
        Unit_target_team_both,//˫������
        Unit_target_team_ENEMY,
        Unit_target_team_Friendly,
        Unit_target_team_custom,//��ͨ
        Unit_target_team_none,//��
    }


    // �˺�����
    public enum AbilityUnitDamageType
    {
        Default,//
        Damage_Type_magical,//ħ������
        Damage_Type_Physical, // ������
        Damage_Type_pure,// �����˺�
    }


    // ������������
    public enum SpellImmunityType
    {
        Default,//
        spell_immunity_none,//��
        spell_immunity_allies_yes, // �������ڼ������ߵ��Ѿ�
        spell_immunity_allies_no,// ���������ڼ������ߵ��Ѿ�
        spell_immunity_enemies_yes, // �������ڼ������ߵĵо�
        spell_immunity_enemies_no,// ���������ڼ������ߵĵо�
    }

    // ��������
    public enum AbilityType
    {
        DEFAULT,//
        ABILITY_TYPE_BASIC,//��ͨ����
        ABILITY_TYPE_ULTIMATE, // �ռ�����
        ABILITY_TYPE_ATTRIBUTES, // �������Խ���
        ABILITY_TYPE_HIDDEN, // ��ɶ�ģ�
    }
}