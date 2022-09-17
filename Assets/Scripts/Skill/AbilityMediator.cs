using Ability;
using ECS;
using Game;
using Game.Input;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using EntityManager = ECS.EntityManager;
using EventType = Ability.EventType;

namespace Game
{
    public class AbilityMediator : Mediator
    {
        new public static string NAME = typeof(AbilityMediator).FullName;


        public AbilityMediator(object viewComponent) : base(NAME, viewComponent) { }

        private float inputValue;
        bool waitTarget;
        bool waitPoint;


        HeroProxy heroProxy = null;
        AbilityProxy abilityProxy = null;
        AbilityVO ability = null;

        public delegate void ForeswingHandler(AbilityVO vo);
        public event ForeswingHandler Foreswing;
        public override void OnRegister()
        {
            base.OnRegister();
            waitTarget = false;
            waitPoint = false;

            heroProxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            abilityProxy = Facade.RetrieveProxy(AbilityProxy.NAME) as AbilityProxy;

            if (Application.isMobilePlatform)
            {
                GameInput.Controller.Default.VirtualPadLeft.performed += PressStart;
                GameInput.Controller.Default.VirtualPadLeft.canceled += PressCancel;
            }
            else
            {
                GameInput.Controller.Default.Press.started -= PressStart;
                GameInput.Controller.Default.Press.canceled += PressCancel;
            }

            Foreswing += OnForeswing;
            // 创建AbilityComponent
        }

        private void OnForeswing(AbilityVO vo)
        {
        }

        private void PressStart(InputAction.CallbackContext context)
        {
        }

        private void PressCancel(InputAction.CallbackContext context)
        {
            if (!waitTarget && !waitPoint)
                return;
            inputValue = context.ReadValue<float>();

            if (inputValue == 0)
                return;

            SendNotification(GameConsts.ABILITY_READY);
            inputValue = 0;
        }

        void GetTarget()
        {
            if (waitTarget)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    EntityManager.TryGetEntity(hit.collider.gameObject, out Entity target);
                    if (target == null)
                        return;

                    SendNotification(GameConsts.ABILITY_TARGET_READY, target);
                }

            }
            else if (waitPoint)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, LayerMask.NameToLayer("Ground")))
                {


                    SendNotification(GameConsts.ABILITY_POINT_READY, hit.point);
                }

            }

        }

        public override string[] ListNotificationInterests()
        {
            return new string[]
            {
                GameConsts.ABILITY_CAST,
                GameConsts.ABILITY_READY,
                GameConsts.ABILITY_TARGET_READY,
            };
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case GameConsts.ABILITY_CAST:
                    {
                        waitTarget = false;
                        waitPoint = false;
                        ability = (AbilityVO)notification.Body;
                        if (ability == null)
                            return;


                        // 定身无法释放
                        if (ability.AbilityBehavior.HasFlag(AbilityBehavior.ROOT_DISABLES))
                        {
                            EntityManager.GetComponentData<RootComponent>(ability.caster);
                        }
                        // 辅助网格
                        if (!ability.AbilityBehavior.HasFlag(AbilityBehavior.NOASSIST))
                        {

                        }

                        // 需要目标
                        if (ability.targetType > TargetType.None)
                        {
                            //目标是施法者本身
                            if (ability.targetTeam == TargetTeam.Caster)
                            {
                                ability.target = heroProxy.Entity;
                                abilityProxy.Play(ability, heroProxy.Entity);
                                return;
                            }

                            waitTarget = true;
                            return;

                        }

                        // 需要点击地面
                        if (ability.behavior.HasFlag(Behavior.DOTA_ABILITY_BEHAVIOR_POINT))
                        {
                            waitPoint = true;
                            return;

                        }

                        // 不需要目标 也不需要点击地面
                        abilityProxy.Play(ability, heroProxy.Entity);
                    }
                    break;

                case GameConsts.ABILITY_READY:
                    {
                        GetTarget();
                        Foreswing(ability);
                    }
                    break;

                case GameConsts.ABILITY_TARGET_READY:
                    {
                        waitTarget = false;
                        Entity target = (Entity)notification.Body;
                        if (target == null)
                            return;

                        abilityProxy.Play(ability, heroProxy.Entity, target);
                    }
                    break;

                case GameConsts.ABILITY_POINT_READY:
                    {
                        waitPoint = false;
                        Vector3 point = (Vector3)notification.Body;

                        abilityProxy.Play(heroProxy.Entity, ability, point);
                    }
                    break;

                case GameConsts.ABILITY_EVENT:
                    {
                        EnvetInfo EnvetInfo = (EnvetInfo)notification.Body;

                        OnAbilityEvent(EnvetInfo);
                    }
                    break;
            }
        }


        public void OnAbilityEvent(EnvetInfo EnvetInfo)
        {
            EventType type = EnvetInfo.type;
            Dictionary<EventType, List<AbilityEvent>> Events = EnvetInfo.events;
            Entity caster = EnvetInfo.caster;
            Entity target = EnvetInfo.target;

            if (target == null || !target.IsAlive)
                return;

            if (Events.TryGetValue(type, out List<AbilityEvent> events))
            {
                for (int i = 0; i < events.Count; i++)
                {
                    AbilityEvent ae = events[i];
                    for (int j = 0; j < ae.Operations.Count; j++)
                    {

                        EventOperation op = ae.Operations[j];
                        switch (op.type)
                        {
                            case OperationType.Stun:
                                OnStun(op as Stun, target);
                                break;
                            case OperationType.ApplyModifier:
                                OnStun(op as Stun, target);
                                break;
                            case OperationType.TrackingProjectile:

                                OnTrackingProjectile(op as TrackingProjectile, caster, target, Events);
                                break;
                            case OperationType.ProjectileHitUnit:

                                OnProjectileHitUnit(caster, target, Events);
                                break;
                        }
                        //OnOperation(ae.Operations[j], caster,target);
                    }
                }
            }
        }


        public void OnAbilityEvent(EventType type, Dictionary<EventType, List<AbilityEvent>> Events, Entity caster, Entity target)
        {
            if (Events.TryGetValue(type, out List<AbilityEvent> events))
            {
                for (int i = 0; i < events.Count; i++)
                {
                    AbilityEvent ae = events[i];
                    for (int j = 0; j < ae.Operations.Count; j++)
                    {
                        EventOperation op = ae.Operations[j];
                        switch (op.type)
                        {
                            case OperationType.Stun:
                                OnStun(op as Stun, target);
                                break;
                            case OperationType.ApplyModifier:
                                OnStun(op as Stun, target);
                                break;
                            case OperationType.TrackingProjectile:
                                OnTrackingProjectile(op as TrackingProjectile, caster, target, Events);
                                break;

                            case OperationType.ProjectileHitUnit:
                                OnProjectileHitUnit(caster, target, Events);
                                break;
                        }
                        //OnOperation(ae.Operations[j], caster,target);
                    }
                }
            }
        }

        public void OnStun(Stun stun, Entity target)
        {
            if (target == null || !target.IsAlive)
                return;

            EntityManager.AddComponentData<StunComponent>(target).Value = stun.Duration;
        }

        // 增加buff
        public void OnApplyModifier(ApplyModifier op, Entity target)
        {
            if (target == null || !target.IsAlive)
                return;

        }


        public void OnTrackingProjectile(TrackingProjectile tp, Entity caster, Entity target, Dictionary<EventType, List<AbilityEvent>> Events)
        {
            float3 start = caster.gameObject.transform.position;
            Entity projectile = EntityManager.Create();
            EntityManager.AddComponentData<LocalToWorld>(projectile);
            EntityManager.AddComponentData<Position>(projectile).Value = start;
            EntityManager.AddComponentData<Rotation>(projectile).Value = caster.gameObject.transform.rotation;
            EntityManager.AddComponentData<Scale>(projectile).Value = 1;
            EntityManager.AddComponentData<CopyTransformToGameObject>(projectile);

            TrackingProjectileComponent component = EntityManager.AddComponentData<TrackingProjectileComponent>(projectile);
            component.start = EntityManager.GetComponentData<Position>(caster).Value;
            component.target = target;
            component.speed = tp.MoveSpeed;
            component.events = Events;

            EntityManager.LoadGameObject(projectile, "", caster.gameObject.transform.position, caster.gameObject.transform.rotation, (e, _) =>
            {

            });

        }

        public void OnProjectileHitUnit(Entity caster, Entity target, Dictionary<EventType, List<AbilityEvent>> Events)
        {
            OnAbilityEvent(EventType.ProjectileHitUnit, Events, caster, target);
        }
    }
}