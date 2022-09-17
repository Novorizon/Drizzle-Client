using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    // 添加特效
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
        AttachHitloc,//附着受伤点
        AttachOrigin,//附着 目标
        AttachAttack,//附着 攻击
    }
}