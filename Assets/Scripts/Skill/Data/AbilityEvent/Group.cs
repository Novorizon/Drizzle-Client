using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

namespace Ability
{

    public enum GroupCenter
    {
        Caster,
        Target,
        Point,
        Attacker,
        Unit,
        Projectile
    }

    public enum GroupAction
    {
        Radius,
        Line,
        Script,
    }

    [Serializable]
    public class GroupUnits
    {
        public TargetType TargetType;
        public TargetTeam TargetTeam;
        public GroupCenter GroupCenter;
        public GroupAction GroupAction;

        public float Radius;
    }

}