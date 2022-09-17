using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    // ´¥·¢ÌØÐ§
    [Serializable]
    public class Damage : IEventOperation
    {
        public EventTarget Target;
        public DamageType DamageType;
        public int damage;
        public int MinDamage;
        public int MaxDamage;
        public int CurrentHealthPercentBasedDamage;
        public int MaxHealthPercentBasedDamage;

    }

    public enum DamageType
    {
        Magical,
        Physics,//
        Pure,//
    }
}