using DataBase;
using ECS;
using Game;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HotGame
{
    public class LoadWeaponData
    {
        public int id;
        public GameObject dummy;
        public string description;
    }
    public class LoadWeapon : SimpleCommand
    {
        public const string NAME = "LoadWeapon";
        public override void Execute(INotification notification)
        {
            if (notification.Body == null)
                return;

            LoadWeaponData data = (LoadWeaponData)notification.Body;
            int id = data.id;
            GameObject dummy = data.dummy;

            if (id == 0)
                return;

            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            WeaponData weaponData = tableProxy.GetData<WeaponData>(id);


            if (weaponData == null)
                return;


            Weapon weapon = new Weapon();
            weapon.id = weaponData.id;
            weapon.trackCount = weaponData.trackCount;
            weapon.id = weaponData.id;
            weapon.id = weaponData.id;

            Spawn spawn = new Spawn();
            spawn.interval = weaponData.interval;
            spawn.duration = -1;
            spawn.state = SpawnState.Start;

            ResourceManager.Instance.LoadAssetAsync<GameObject>(weaponData.asset, (asset, _) =>
            {

                GameObject gameObject = GameObject.Instantiate(asset);
                gameObject.transform.parent = dummy.transform;
                gameObject.transform.localPosition = default;
                Entity entity = EntityManager.Create(gameObject);

                //×Óµ¯
                ResourceManager.Instance.LoadAssetAsync<GameObject>(weaponData.bulletAsset, (asset, _) =>
                {
                    GameObject bullet = GameObjectPool.Spawn(asset);

                    WeaponVO weaponVO = new WeaponVO();
                    weaponVO.id = weapon.id;
                    weaponVO.bullet = bullet;
                    WeaponProxy weaponProxy = Facade.RetrieveProxy(WeaponProxy.NAME) as WeaponProxy;
                    weaponProxy.SetData(weaponVO);

                    EntityManager.AddComponentData(entity, weapon);
                    EntityManager.AddComponentData(entity, spawn);
                });
            });

        }
        private void OnLoaded(GameObject asset, object userdata)
        {

            dynamic parameters = userdata;
            GameObject dummy = parameters.data.dummy;
            WeaponData weaponData = parameters.weaponData;
        }
    }
}
