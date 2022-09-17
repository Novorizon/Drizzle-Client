
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
        public float3 toward;//ֱ��

        public float3 end;//������

        public Entity target;//����

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
        public Entity target;//����
        public float speed;
        public float timer;

        public TrackingProjectile projectile;


        public Dictionary<EventType, List<AbilityEvent>> events;

    }


    public class ForeswingComponent : IComponentData
    {
        public float time;//ʩ��ǰҡʱ��
        public bool stop;
        public float timer;//ǰҡʱ���ʱ��

        public string anim;
        public string sfx;
        public string asset;
    }

    public class ChannelingComponent : IComponentData
    {
        public float time;//ʩ��ǰҡʱ��
        public bool stop;
        public float timer;//ǰҡʱ���ʱ��
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
