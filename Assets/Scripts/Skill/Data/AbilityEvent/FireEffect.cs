using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    // ´¥·¢ÌØÐ§
    [Serializable]
    public class FireEffect : IEventOperation
    {
        public string EffectName;
        public EffectAttachType EffectAttachType;
        public EventTarget Target;
        public List<string> ControlPoints;
        public List<string> ControlPointEntities;

    }
}