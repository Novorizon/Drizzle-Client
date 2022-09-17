using Cinemachine;
using DataBase;
using ECS;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;

namespace Game
{
    public class LoadNPCCommand : SimpleCommand
    {
        public const string NAME = "LoadNPCCommand";
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

            ResourceManager.Instance.LoadAssetAsync<GameObject>("FemaleCharacter", (asset, _) =>
            {
                GameObject go = GameObject.Instantiate(asset);

                go.transform.position = new Vector3(300,0,436);
                go.transform.forward = Vector3.forward;

                Entity hero = EntityManager.Create(go);
                EntityManager.AddComponentData<LocalToWorld>(hero);
                EntityManager.AddComponentData<Position>(hero).Value = go.transform.position;
                EntityManager.AddComponentData<Rotation>(hero).Value = Quaternion.identity;
                EntityManager.AddComponentData<Scale>(hero).Value = Vector3.one;
                EntityManager.AddComponentData<CopyToTransformComponent>(hero);

                EntityManager.AddComponentData<Speed>(hero).Value =default;
                EntityManager.AddComponentData<MoveDirection>(hero).Value =Vector3.forward;
                EntityManager.AddComponentData<FaceDirection>(hero).Value = Vector3.forward;

                EntityManager.AddComponentData<EntityLayer>(hero).Value= EntityLayerMask.Npc| EntityLayerMask.Friend| EntityLayerMask.Interactive| EntityLayerMask.Select;
                EntityManager.AddComponentData<Selected>(hero);
                //Debug.LogError(asset.name);

            });



        }
    }
}
