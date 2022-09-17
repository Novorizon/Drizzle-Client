
using System;
using System.Collections.Generic;
using ECS;
using Unity.Mathematics;
using UnityEngine;

namespace Ability
{
    public  class AbilityComponent : IComponentData
    {
        public AbilityVO ability;
    }

    public class BuffComponent : IComponentData
    {
    }

    public class ProjectileComponent : IComponentData
    {
        public float3 start;
        public float3 toward;//直线

        public float3 end;//抛物线

        public Entity target;//跟踪

        public float speed;
        public float maxDistance;
        public Projectile projectile;

        public float timer;

        //public Action<EffectVO> Hit;
        //public EffectVO effect;
        public Dictionary<EventType, List<AbilityEvent>> events;
    }

    public class TrackingProjectileComponent : IComponentData
    {
        public float3 start;
        public Entity target;//跟踪
        public float speed;
        public float timer;

        public TrackingProjectile projectile;


        public Dictionary<EventType, List<AbilityEvent>> events;

    }


    public class ForeswingComponent : IComponentData
    {
        public float time;//施法前摇时间
        public bool stop;
        public float timer;//前摇时间计时器

        public string anim;
        public string sfx;
        public string asset;
    }

    public class ChannelingComponent : IComponentData
    {
        public float time;//施法前摇时间
        public bool stop;
        public float timer;//前摇时间计时器
    }

    public class CastComponent : IComponentData
    {
        public EffectVO effect;
    }

    public class BackswingComponent : IComponentData
    {
        public EffectVO effect;
    }
}
