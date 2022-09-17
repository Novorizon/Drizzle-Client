using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;

namespace Ability
{

    public class EnvetInfo
    {
        public EventType type;
        public Dictionary<EventType, List<AbilityEvent>> events;
        public Entity caster;
        public Entity target;
    }
    public enum OperationType
    {
        None,

        ApplyModifier,//
        RemoveModifier,
        FireEffect,
        LinearProjectile,
        Stun,
        TrackingProjectile,
        ProjectileHitUnit,
        AttachEffect,
    }
    public interface IEventOperation
    {
    }
    public  class EventOperation
    {
        public event EventHandler<EventArgs> OperationCompleted;
        public event EventHandler<EventArgs> OperationFailed;
        public event EventHandler<EventArgs> OperationCancelled;
        public event EventHandler<EventArgs> OperationAborted;
        public OperationType type;
    }

    [Serializable]
    public class AbilityEvent
    {
        public EventType EventType;//�¼����� 
        public List<EventOperation> Operations;//����

    }



    [Serializable]
    public class ApplyModifier : EventOperation, IEventOperation
    {
        public EventTarget Target;
        public string ModifierName;
        public float Duration;

    }

    [Serializable]
    public class RemoveModifier : IEventOperation
    {
        public EventTarget Target;
        public string ModifierName;
    }


    [Serializable]
    public class EventTarget
    {
        public EventTargetType EventTargetType;
        public GroupUnits EffectAttachType;
    }


    public enum EventTargetType
    {
        None,

        Caster,//ǰҡ
        Target,
        Point,
        Attacker,
        GroupUnits,
    }

    public enum EventType
    {
        AbilityStart,
        SpellStart,//��ʼʩ��
        Attack,//����
        Attacked,//������
        AbilityExcuted,//�ͷż���
        ProjectileHitUnit,//
    }


}