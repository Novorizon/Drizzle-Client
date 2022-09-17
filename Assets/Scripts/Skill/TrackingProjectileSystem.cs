
using ECS;
using Game;
using PureMVC.Interfaces;
using Unity.Mathematics;
namespace Ability
{
    public class TrackingProjectileSystem : SystemBase<ProjectileComponent, Position>
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

        protected override void OnUpdate(int index, Entity entity, ProjectileComponent projectile, Position position)
        {
            if (proxy == null)
                proxy = Facade.RetrieveProxy(AbilityProxy.NAME) as AbilityProxy;


            bool collided = false;
            if (!projectile.target.IsAlive)
            {
                EntityManager.RemoveComponentData<ProjectileComponent>(entity);
                return;
            }

            Position pos = EntityManager.GetComponentData<Position>(projectile.target);
            projectile.toward = math.lerp(math.normalize(pos.Value - position.Value), position.Value, 0.5f);
            position.Value += projectile.toward * projectile.speed * Time.DeltaTime;
            // ÓëÄ¿±êÅö×²¼ì²â


            if (collided)
            {

                EnvetInfo EnvetInfo = new EnvetInfo
                {
                    type = EventType.ProjectileHitUnit,
                    events = projectile.events,
                    target = projectile.target
                };

                SendNotification(GameConsts.ABILITY_EVENT, EnvetInfo);
            }
        }
    }


}
