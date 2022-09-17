//using Unity.Mathematics;
//using UnityEngine;
//using ECS;
//using MVC;
//using PureMVC.Interfaces;
//using Ability;
//using System.Collections.Generic;

//namespace Game
//{
//    //[UpdateInGroup]
//    public class AbilityManager : SingletonBehaviour<AbilityManager>
//    {
//        private IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());

//        List<AbilityVO> Abilities = new List<AbilityVO>();


//        public void Play(Entity caster, List<Entity> targets, AbilityData data)
//        {
//            if (data == null)
//                return;


//            AbilityVO vo = new AbilityVO(data);
//            vo.caster = caster;
//            vo.target = targets;
//            Abilities.Add(vo);
//        }

//        private void Update()
//        {
//            for (int i = 0; i < Abilities.Count; i++)
//            {
//                AbilityVO vo = Abilities[i];

//                float dt = Time.deltaTime;
//                if (vo.id == 0)
//                    return;

//                if (vo.targetType > TargetType.None && vo.target == null)
//                    vo.state = State.End;


//                if (vo.state == State.End)
//                {
//                    Abilities.RemoveAt(i);//TODO
//                    return;
//                }


//                if (vo.state == State.None)
//                {
//                    vo.state = State.Foreswing;


//                    //
//                    //if (vo.effects.TryGetValue(State.Foreswing, out List<Ability.Effect> channels))
//                    //{
//                    //    EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
//                    //    for (int j = 0; j < channels.Count; j++)
//                    //    {
//                    //        EffectVO effect = new EffectVO(channels[j]);
//                    //        effectProxy.Play(vo.targets, effect);
//                    //    }
//                    //}

//                    if (vo.foreswingTimer >= vo.foreswing)
//                    {
//                        if (vo.channel == 0)
//                        {
//                            vo.state = State.Cast;
//                        }
//                        else
//                        {
//                            vo.state = State.Channeling;
//                        }
//                    }
//                    vo.foreswingTimer += dt;
//                }


//                if (vo.state == State.Channeling)
//                {
//                    if (vo.channelTimer >= vo.channel)
//                    {
//                        vo.state = State.Cast;
//                    }
//                    else
//                    {
//                        vo.channelTimer += dt;
//                        if (vo.channelStop)
//                        {
//                            //vo.caster.GetOrAddComponentData<MoveDirection>().Value = 0;
//                            //面向目标
//                        }


//                        //可能有多次
//                        vo.intervalTimer += dt;
//                        if (vo.intervalTimer >= vo.interval)
//                        {
//                            vo.intervalTimer = 0;

//                            if (vo.effects.TryGetValue(State.Channeling, out List<Ability.Effect> channels))
//                            {
//                                EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
//                                for (int j = 0; j < channels.Count; j++)
//                                {
//                                    EffectVO effect = new EffectVO(channels[j]);
//                                    effectProxy.Play(vo.target, effect);
//                                }
//                            }
//                        }
//                    }
//                }


//                if (vo.state == State.Cast)
//                {
//                    if (vo.effects.TryGetValue(State.Cast, out List<Ability.Effect> channels))
//                    {
//                        EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
//                        for (int j = 0; j < channels.Count; j++)
//                        {
//                            EffectVO effect = new EffectVO(channels[j]);
//                            effectProxy.Play(vo.target, effect);
//                        }
//                    }
//                }

//                if (vo.state == State.Backswing)
//                {
//                    if (vo.effects.TryGetValue(State.Backswing, out List<Ability.Effect> channels))
//                    {
//                        EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
//                        for (int j = 0; j < channels.Count; j++)
//                        {
//                            EffectVO effect = new EffectVO(channels[j]);
//                            effectProxy.Play(vo.target, effect);
//                        }
//                    }
//                }
//            }
//        }
//    }
//}

