
using PureMVC.Patterns.Proxy;
using Game;
using System.Collections.Generic;
using System.Linq;
using ECS;
using UnityEngine;
using Unity.Mathematics;

namespace Ability
{
    public class AbilityProxy : Proxy
    {
        public static new string NAME = typeof(AbilityProxy).FullName;

        private readonly Dictionary<int, AbilityData> datas = new Dictionary<int, AbilityData>();
        private readonly Dictionary<int, AbilityVO> vos = new Dictionary<int, AbilityVO>();
        List<AbilityVO> abilities = new List<AbilityVO>();

        List<AbilityVO> heroAbilities = new List<AbilityVO>();

        public AbilityProxy() : base(NAME)
        {

        }

        public override void OnRegister()
        {
            AbilityVO vo = new AbilityVO();
            vo.id = 1001;
            vo.name = "Test";
            vo.behavior = Behavior.ABILITY_BEHAVIOR_ACTIVE;
            vo.targetType = TargetType.ALL;
            vo.targetTeam = TargetTeam.Caster;
            vo.state = AbilityState.None;
            vo.range = Range.Sphere;
            vo.channel = 3;
            vo.effects = new Dictionary<AbilityState, List<Effect>>();

            Effect effect = new Effect();
            effect.id = 2001;
            effect.attribute = Attribute.Speed;
            effect.type = Type.Attribute;
            effect.value = 1;
            effect.state = AbilityState.Channeling;

            vo.effects.Add(effect.state, new List<Effect>() { effect });

            heroAbilities.Add(vo);
        }


        public List<AbilityVO> Datas => abilities;
        public AbilityVO HeroAbility(int i) => heroAbilities[i];

        public void Play(AbilityVO data, Entity caster, Entity target = null)
        {
            if (data == null)
                return;

            abilities.Add(data);

            Entity entity = EntityManager.Create();
            ForeswingComponent component = EntityManager.AddComponentData<ForeswingComponent>(entity);
            component.time = data.foreswing;
            // 前摇动画 音效 特效等

            EnvetInfo EnvetInfo = new EnvetInfo
            {
                type = EventType.AbilityStart,
                events = data.events,
                caster = caster,
                target = target
            };

            SendNotification(GameConsts.ABILITY_EVENT, EnvetInfo);
            //OnAbilityEvent(EventType.AbilityStart, data.events, caster,target);
        }

        public void Play(Entity caster, AbilityVO data, Vector3 center)
        {
            if (data == null)
                return;

            abilities.Add(data);
        }



        public void Play(Ability ability, Entity caster, Entity target)
        {
            switch(ability.AbilityBehavior)
            {
                case AbilityBehavior.IMMEDIATE:

                    break;
            }
        }



        public void DoBehaviour(Ability ability, Entity caster, Entity target )
        {
            switch (ability.AbilityBehavior)
            {
                case AbilityBehavior.IMMEDIATE:

                    break;
            }
        }



    }
}
