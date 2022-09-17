
using ECS;
using PureMVC.Interfaces;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace Ability
{
    public class AbilitySystem : SystemBase<AbilityComponent>
    {
        public delegate void HitTargetHandler(AbilityVO vo);
        public event HitTargetHandler HitTarget;

        private IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());
        AbilityProxy proxy = null;
        protected override void OnCreate()
        {
            base.OnCreate();
            proxy = Facade.RetrieveProxy(AbilityProxy.NAME) as AbilityProxy;
        }

        protected override void OnUpdate(int index, Entity entity, AbilityComponent AbilityComponent)
        {
            if (proxy == null)
                proxy = Facade.RetrieveProxy(AbilityProxy.NAME) as AbilityProxy;

            List<AbilityVO> Abilities = proxy.Datas;
            if (Abilities == null || Abilities.Count == 0)
                return;

            for (int i = Abilities.Count - 1; i >= 0; i--)
            {
                AbilityVO vo = Abilities[i];

                float dt = Time.DeltaTime;
                if (vo.id == 0)
                    return;

                // ��ҪĿ�� ��û��Ŀ�꣬�Ƴ� ����
                if (vo.targetType > TargetType.None && vo.target == null)
                    vo.state = AbilityState.End;

                //����
                if (vo.state == AbilityState.End)
                {
                    // ���õ�λ״̬  movedirection

                    Abilities.RemoveAt(i);//TODO
                    return;
                }

                //��ʼ
                if (vo.state == AbilityState.None)
                {

                    vo.state = AbilityState.Foreswing;

                    // ǰҡ����

                    // ������Ч
                }

                //ǰҡ
                if (vo.state == AbilityState.Foreswing)
                {
                    if (vo.foreswingTimer >= vo.foreswing)
                    {
                        if (vo.channel == 0)// ����ʱ��Ϊ0 ֱ��ת����˲��
                        {
                            vo.state = AbilityState.Cast;
                            // ˲������
                        }
                        else// ת��������
                        {
                            vo.state = AbilityState.Channeling;
                            //��������
                        }
                    }
                    vo.foreswingTimer += dt;

                }

                // ����
                if (vo.state == AbilityState.Channeling)
                {
                    if (vo.channelTimer >= vo.channel)
                    {
                        vo.state = AbilityState.Cast;
                        // ˲������
                    }
                    else
                    {
                        vo.channelTimer += dt;
                        //����Ŀ��


                        //�����ж��
                        vo.intervalTimer += dt;
                        if (vo.intervalTimer >= vo.interval)
                        {
                            vo.intervalTimer = 0;

                            if (vo.effects.TryGetValue(AbilityState.Channeling, out List<Effect> channels))
                            {
                                EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
                                for (int j = 0; j < channels.Count; j++)
                                {
                                    EffectVO effect = new EffectVO(channels[j]);

                                    List<Entity> targets = new List<Entity>();
                                    if (vo.target != null)
                                    {
                                        targets.Add(vo.target);

                                    }

                                    //�����AOE ��Ҫ����targetΪ���ĵķ�Χ�ڵ�����Ŀ��
                                    if (vo.isAOE)
                                    {
                                        if (vo.behavior.HasFlag(Behavior.DOTA_ABILITY_BEHAVIOR_POINT))// ��λ�ò���
                                        {
                                            Vector3 center = vo.center;

                                        }
                                        else// ��targetΪ���Ĳ���
                                        {
                                            Vector3 center = vo.target.gameObject.transform.position;

                                        }

                                        //����

                                    }
                                    effectProxy.Play(targets, effect);
                                }
                            }
                        }
                    }
                }

                //˲��
                if (vo.state == AbilityState.Cast)
                {
                    if (vo.effects.TryGetValue(AbilityState.Cast, out List<Effect> effects))
                    {
                        EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
                        for (int j = 0; j < effects.Count; j++)
                        {
                            EffectVO effect = new EffectVO(effects[j]);
                            List<Entity> targets = new List<Entity>();
                            targets.Add(vo.target);

                       
                            if(vo.projectile== Projectile.None)//������Ч
                            {
                                if (vo.isAOE)
                                {
                                    Vector3 center = vo.target.gameObject.transform.position;
                                    //����

                                }
                                effectProxy.Play(targets, effect);
                            }
                            else     //����Ͷ����
                            {
                                float3 start = vo.caster.gameObject.transform.position;
                                Entity projectile = EntityManager.Create();
                                EntityManager.AddComponentData<LocalToWorld>(projectile);
                                EntityManager.AddComponentData<Position>(projectile).Value= start;
                                EntityManager.AddComponentData<Rotation>(projectile).Value= vo.caster.gameObject.transform.rotation;
                                EntityManager.AddComponentData<Scale>(projectile).Value=1;
                                EntityManager.AddComponentData<CopyTransformToGameObject>(projectile);
                                ProjectileComponent component = EntityManager.AddComponentData<ProjectileComponent>(projectile);
                                component.start = EntityManager.GetComponentData<Position>(vo.caster).Value;
                                component.maxDistance = vo.maxDistance;
                                //component.Hit = effectProxy.OnHit;
                                //component.effect = effect;

                                switch (vo.projectile)
                                {
                                    case Projectile.Parabola:
                                        component.end = vo.center;
                                        component.toward = component.end - start;
                                        break;
                                    case Projectile.Linear:
                                        if(vo.targetType== TargetType.None)
                                        {
                                            component.end = vo.center;
                                            component.toward = component.end - start;
                                        }
                                        else
                                        {
                                            component.target = vo.target;
                                            Position end = EntityManager.GetComponentData<Position>(vo.target);
                                            component.toward = end.Value - start;
                                        }
                                        break;
                                    case Projectile.Tracking:
                                        component.target = vo.target;
                                        break;
                                }

                                EntityManager.LoadGameObject(projectile, "",vo.caster.gameObject.transform.position, vo.caster.gameObject.transform.rotation, (e, _) =>
                                {

                                });

                            }

                        }
                    }
                    vo.state = AbilityState.Backswing;
                    // ��ҡ����
                }

                //��ҡ
                if (vo.state == AbilityState.Backswing)
                {
                    vo.state = AbilityState.End;
                }
            }
        }


    }


}
