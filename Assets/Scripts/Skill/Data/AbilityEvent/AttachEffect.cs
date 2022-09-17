using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    // �����Ч
    [Serializable]
    public class AttachEffect : IEventOperation
    {
        public string EffectName;
        public EffectAttachType EffectAttachType;
        public EventTarget Target;
        public List<string> ControlPoints;
        public List<string> ControlPointEntities;

    }

    public enum EffectAttachType
    {
        Default,
        AttachHitloc,//�������˵�
        AttachOrigin,//���� Ŀ��
        AttachAttack,//���� ����
    }
}