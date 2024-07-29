
using Cinemachine;
using DataBase;
using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LoadHero : SimpleCommand
    {
        public const string NAME = "LoadHero";
        public override void Execute(INotification notification)
        {
            LoadProfile();
        }

        public void LoadProfile()
        {
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            if (tableProxy == null)
                return;

            HeroProxy proxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            if (proxy == null)
                return;
            proxy.GetPrefs();

            QuestProxy questProxy = Facade.RetrieveProxy(QuestProxy.NAME) as QuestProxy;
            if (questProxy == null)
                return;

            int isCreated = PlayerPrefs.GetInt("CharacterCreated", 0);
            if (isCreated == 0)
            {
                DefaultData defaultData = tableProxy.GetData<DefaultData>(10001);
                if (defaultData == null)
                    return;
                proxy.SetData(defaultData);
                //questProxy.SetData(defaultData);
            }

            HeroVO data = proxy.GetData();
            if (data == null)
                return;

            ModelData model = tableProxy.GetData<ModelData>(data.modelId);
            if (model == null)
                return;

            ArchetypeProxy archetypeProxy = Facade.RetrieveProxy(ArchetypeProxy.NAME) as ArchetypeProxy;
            EntityArchetype archetype = archetypeProxy.GetData(ArchetypeProxy.Archetype.Hero);

            Entity hero = EntityManager.Create(archetype);

            EntityManager.AddComponentData<CopyInitialTransformFromGameObject>(hero);
            EntityManager.AddComponentData<CopyTransformToGameObject>(hero);
            EntityManager.AddComponentData<LocalToWorld>(hero);
            EntityManager.AddComponentData<Position>(hero).Value = data.position;
            EntityManager.AddComponentData<Rotation>(hero).Value = Quaternion.identity;
            EntityManager.AddComponentData<Scale>(hero).Value = Vector3.one;

            EntityManager.AddComponentData<Speed>(hero).Value = data.speed * 10;
            EntityManager.AddComponentData<MoveDirection>(hero).Value = Vector3.zero;
            EntityManager.AddComponentData<FaceDirection>(hero).Value = Vector3.forward;// data.forward;

            EntityManager.AddComponentData<PlayerController>(hero);
            //Debug.LogError(asset.name);


            Weapon weapon=EntityManager.AddComponentData<Weapon>(hero);
            weapon.attack = 10;
            weapon.angle = 60;
            weapon.duration = 1;
            weapon.timer = 0;
            weapon.fire = true;
            ResourceManager.Instance.LoadAssetAsync<GameObject>("Bullet", (asset, _) =>
            {
                if (asset != null)
                {
                    weapon.gameObject = GameObjectPool.Spawn(asset);
                }
            });

            EntityManager.LoadGameObject(hero, model.name, data.position, Quaternion.identity, (e, _) =>
            {
                //EntityManager.AddComponentData<PlayerController>(hero);

                var VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
                if (VirtualCamera != null)
                {
                    VirtualCamera.Follow = e.gameObject.transform;
                    VirtualCamera.LookAt = e.gameObject.transform;
                }
            });

            //SendNotification();//×°±¸

            data.entity = hero;
            data.guid = hero.GUID;

            //Entity entity = EntityManager.Create();
            //EntityManager.AddComponentData<Ability.AbilityComponent>(entity);

            //Entity buff = EntityManager.Create();
            //EntityManager.AddComponentData<Ability.BuffComponent>(buff);
        }
    }
}
