using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Facade;
using Unity.Mathematics;
using UnityEngine;
namespace Game
{
    public class Bullet : IComponentData
    {
        public int id;
        public string name;
    }

    public class WeaponFireSystem : SystemBase<Weapon, Spawn>
    {
        //public void SendNotification(string notificationName, object body = null, string type = null)
        //{
        //    Facade.SendNotification(notificationName, body, type);
        //}

        protected IFacade Facade { get { return PureMVC.Patterns.Facade.Facade.GetInstance(() => new Facade()); } }


        protected override void OnUpdate(int index, Entity entity, Weapon weapon, Spawn spawn)
        {

            if (spawn.state == SpawnState.Start)
            {
                //获得子弹原型
                ArchetypeProxy proxy = Facade.RetrieveProxy(ArchetypeProxy.NAME) as ArchetypeProxy;
                EntityArchetype archetype = proxy.GetData(ArchetypeProxy.Archetype.Bullet);

                float deltaAngle = weapon.angle / (weapon.trackCount - 1);
                float startAngle = -weapon.angle / 2;

                //通过原型创建子弹
                //EntityManager.Create(archetype, weapon.trackCount, out Entity[] bullets);

                for (int i = 0; i < weapon.trackCount; i++)
                {
                    //Entity bullet = bullets[i];

                    //通过原型创建子弹
                    GameObject gameObject = GameObjectPool.Spawn(weapon.gameObject);
                    Entity bullet = EntityManager.Create(gameObject, archetype);

                    EntityManager.GetComponentData<Translation>(bullet).Value = weapon.position;
                    EntityManager.GetComponentData<Scale>(bullet).Value = weapon.scale;
                    EntityManager.GetComponentData<Speed>(bullet).Value = weapon.bulletSpeed;
                    EntityManager.GetComponentData<LifeTime>(bullet).Value = weapon.bulletLifeTime;

                    float currentAngle = startAngle + i * deltaAngle;
                    quaternion rotation = math.mul(weapon.rotation, quaternion.RotateY(math.radians(currentAngle)));
                    EntityManager.GetComponentData<Rotation>(bullet).Value = rotation;

                    float3 forward = new float3(0, 0, 1);
                    float3 direction = math.mul(rotation, forward);
                    EntityManager.GetComponentData<MoveDirection>(bullet).Value = direction;

                    //碰撞检测
                    //EntityManager.AddComponentData(bullet, new Sphere() { Center = float3.zero, Radius = 0.1f });
                    //EntityManager.AddComponentData(bullet, new CollideEffect() { effectId = weapon.bulletEffectId, effectSpan = weapon.bulletEffectSpan });
                    //EntityManager.AddComponentData(bullet, new Damageable());
                    //EntityManager.AddComponentData(bullet, new Collide() { type = CollideType.Bullet, collidedEntity = new FixedList512<Entity>(), collidedType = new FixedList512<CollideType>() });
                    SphereCollider collider= gameObject.GetComponent<SphereCollider>();
                    collider.enabled = true;
                    collider.isTrigger = true;
                }

            }
        }

    }
}