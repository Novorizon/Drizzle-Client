using Ability;
using ECS;
using Game;
using Game.Input;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;
using UnityEngine.InputSystem;
using EntityManager = ECS.EntityManager;

namespace Game
{
    public class BuffMediator : Mediator
    {
        new public static string NAME = typeof(BuffMediator).FullName;


        public BuffMediator(object viewComponent) : base(NAME, viewComponent) { }

        private float inputValue;
        bool waitTarget;
        bool waitPoint;


        HeroProxy heroProxy = null;
        AbilityProxy abilityProxy = null;
        AbilityVO ability = null;
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

            // 创建AbilityComponent
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

                        // 需要目标
                        if (ability.targetType > TargetType.None)
                        {
                            //目标是施法者本身
                            if(ability.targetTeam== TargetTeam.Caster)
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
            }
        }
    }
}