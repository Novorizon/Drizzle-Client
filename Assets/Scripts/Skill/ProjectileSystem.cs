
using ECS;
using PureMVC.Interfaces;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
namespace Ability
{
    public class ProjectileSystem : SystemBase<ProjectileComponent, Position>
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

        protected override void OnUpdate(int index, Entity entity, ProjectileComponent projectile,Position position)
        {
            if (proxy == null)
                proxy = Facade.RetrieveProxy(AbilityProxy.NAME) as AbilityProxy;


            bool collided = false;
            switch (projectile.projectile)
            {
                case Projectile.Linear:
                    position.Value += projectile.toward * projectile.speed * Time.DeltaTime;
                    if (projectile.target==null)//直线无目标  月骑的箭 胖子的钩
                    {
                        //碰撞检测 任何目标类型的单位
                    }
                    else
                    {
                        // 与目标碰撞检测
                    }

                    break;

                case Projectile.Parabola:

                    position.Value +=-(projectile.toward * projectile.toward)*projectile.speed*Time.DeltaTime+30;
                    projectile.timer += Time.DeltaTime;

                    float time = (projectile.end.x - projectile.start.x) / projectile.speed;
                    if (projectile.timer>= time)
                    {
                        collided = true;
                    }

                    break;
                case Projectile.Tracking:
                    if(!projectile.target.IsAlive)
                    {
                        EntityManager.RemoveComponentData<ProjectileComponent>(entity);
                        return;
                    }

                    Position pos= EntityManager.GetComponentData<Position>(projectile.target);
                    projectile.toward =math.lerp( math.normalize(pos.Value - position.Value),position.Value,0.5f);
                    position.Value += projectile.toward * projectile.speed * Time.DeltaTime;
                    // 与目标碰撞检测

                    break;
            }


            if (collided)
            {
                //projectile.Hit.Invoke(projectile.effect);
            }
        }


        private void OnHitTarget(AbilityVO vo)
        {

        }
    }


}
