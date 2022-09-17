using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    [Serializable]
    public class Stun : EventOperation, IEventOperation
    {
        public EventTarget Target;
        public float Duration;
    }
}