
using ECS;
using Game;
using PureMVC.Interfaces;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace Ability
{
    public class ForeswingSystem : SystemBase<AbilityComponent, ForeswingComponent>
    {
        private IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnUpdate(int index, Entity entity, AbilityComponent Ability, ForeswingComponent Foreswing)
        {
            float dt = Time.DeltaTime;

            AbilityVO vo = Ability.ability;
            vo.foreswingTimer += dt;

            if (vo.foreswingTimer >= vo.foreswing)
            {
                if (vo.channel == 0)// ����ʱ��Ϊ0 ת����˲��
                {
                    Entity next = EntityManager.Create();
                    EntityManager.AddComponentData<CastComponent>(next);
                    // ˲������
                }
                else// ת��������
                {
                    Entity next = EntityManager.Create();
                    EntityManager.AddComponentData<ChannelingComponent>(next);
                    //��������
                }

                EnvetInfo EnvetInfo = new EnvetInfo
                {
                    type = EventType.SpellStart,
                    events = vo.events,
                    caster = vo.caster,
                    target = vo.target
                };

                SendNotification(GameConsts.ABILITY_EVENT, EnvetInfo);

                EntityManager.RemoveComponentData<ForeswingComponent>(entity);
            }
        }
    }


}
