using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    [Serializable]
    public class ProjectileHitUnit : EventOperation, IEventOperation
    {
        public EventTarget Target;
        public float Duration;
    }
}