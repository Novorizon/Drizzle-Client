using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{
    // жнаф
    [Serializable]
    public class Heal : IEventOperation
    {
        public EventTarget Target;
        public int HealAmount;
    }
}