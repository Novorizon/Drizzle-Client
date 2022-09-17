
using ECS;
using PureMVC.Interfaces;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace Ability
{
    public class ChannelingSystem : SystemBase<AbilityComponent, ChannelingComponent>
    {
        private IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());
        protected override void OnCreate()
        {
            base.OnCreate();
        }

        protected override void OnUpdate(int index, Entity entity, AbilityComponent Ability, ChannelingComponent component)
        {
            float dt = Time.DeltaTime;

            AbilityVO vo = Ability.ability;
            vo.channelTimer += dt;

            if (vo.channelTimer >= vo.channel)
            {
                Entity next = EntityManager.Create();
                EntityManager.AddComponentData<CastComponent>(next);
                // 瞬发动画
            }
            else
            {
                //需要静止
                //vo.caster.GetOrAddComponentData<MoveDirection>().Value = 0;            

                if (true)//需要面向目标
                {

                }


                //多次间隔施法
                vo.intervalTimer += dt;
                if (vo.intervalTimer >= vo.interval)
                {
                    vo.intervalTimer = 0;

                    if (vo.effects.TryGetValue(AbilityState.Channeling, out List<Effect> effects))
                    {
                        EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
                        for (int j = 0; j < effects.Count; j++)
                        {
                            EffectVO effect = new EffectVO(effects[j]);

                            List<Entity> targets = new List<Entity>();
                            if (vo.target != null)
                            {
                                targets.Add(vo.target);

                            }

                            //如果是AOE 需要查找target为中心的范围内的所有目标
                            if (vo.isAOE)
                            {
                                if (vo.behavior.HasFlag(Behavior.DOTA_ABILITY_BEHAVIOR_POINT))// 按位置查找
                                {
                                    Vector3 center = vo.center;

                                }
                                else// 按target为中心查找
                                {
                                    Vector3 center = vo.target.gameObject.transform.position;

                                }

                                //查找

                            }
                            effectProxy.Play(targets, effect);
                        }
                    }
                }
            }
        }

    }


}
