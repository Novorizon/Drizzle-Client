using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    [Serializable]
    public class TrackingProjectile : EventOperation, IEventOperation
    {
        public EventTarget Target;
        public string EffectName;

        public bool dispersable; // �ɱ���ɢ
        public bool Dodgeable; // �ɱ�����
        public bool ProvidesVision; // �ṩ��Ұ ������ ����֮�� ����
        public float VisionRadius; //��Ұ�뾶 
        public float MoveSpeed; //  
    }
}