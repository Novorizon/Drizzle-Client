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


        public Dictionary<EventType, List<AbilityEvent>> events;// �����¼�

        public Entity caster;
        public Entity target;//Ŀ�������entity
        ///===============================


















        public float cd;
        public float mana;
        public int level;
        public int maxLevel;

        // ʩ������
        public float foreswing;//ʩ��ǰҡʱ��
        public float foreswingTimer;//ǰҡʱ���ʱ��

        public float channel;//ʩ������ʱ��
        public float interval;//���ʱ��
        public float channelTimer;//����ʱ���ʱ��
        public float intervalTimer;//���ʱ���ʱ��

        public float backswing;//ʩ����ҡʱ�䣬
        public float backswingTimer;//��ҡʱ���ʱ��

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

        public float speed;
        public float maxDistance;
        public Projectile projectile;

        // ����������Ŀ��״̬���ȼ� Ѫ���� �Ƿ�����ħ�� �޵�
        public int limitLevel;
        public int limitHP;
        public bool ignoreImmunity;
        public bool ignoreInvincible;

        // ����Ч��
        public Dictionary<AbilityState, List<Effect>> effects;


        //�Ƿ����buff
        public List<Buff> buffs;

        public Vector3 center;//Ŀ������ǵ���
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
