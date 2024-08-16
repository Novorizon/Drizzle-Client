using DataBase;
using ECS;
using Game;
using Game.Input;
using MVC;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

namespace HotGame
{
    public class LoadBuildingData
    {
        public int id;
        public GameObject dummy;
        public bool isBuild;
    }
    public class LoadBuilding : SimpleCommand
    {
        public const string NAME = "LoadBuilding";
        private static bool canPlaceBuilding = false;
        LoadBuildingData data = new LoadBuildingData();
        public override void Execute(INotification notification)
        {
            if (notification.Body == null)
                return;

            data = (LoadBuildingData)notification.Body;
            int id = data.id;
            bool isBuild = data.isBuild;

            if (isBuild)
            {
                GameInput.Controller.Default.Press.performed += OnPress;
                GameInput.Controller.Default.MousePosition.performed += OnMouseMoved;

            }
            else
            {
                GameInput.Controller.Default.Press.performed -= OnPress;
                GameInput.Controller.Default.MousePosition.performed -= OnMouseMoved;

            }

        }

        private void OnMouseMoved(InputAction.CallbackContext context)
        {
            // 将屏幕坐标转换为世界坐标
            Vector2 mousePosition = GameInput.Controller.Default.MousePosition.ReadValue<Vector2>();
            Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, MainCamera.camera.nearClipPlane);
            Vector3 worldPosition = MainCamera.camera.ScreenToWorldPoint(screenPosition);

            // 检测是否能在该位置放置建筑物
            canPlaceBuilding = CanBuild(worldPosition);

            if (canPlaceBuilding)
            {
                Debug.Log("可以在该位置建造建筑物: " + worldPosition);
            }
            else
            {
                Debug.Log("无法在该位置建造建筑物: " + worldPosition);
            }
        }


        private void OnPress(InputAction.CallbackContext context)
        {
            if (context.performed && canPlaceBuilding)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, MainCamera.camera.nearClipPlane);
                Vector3 worldPosition = MainCamera.camera.ScreenToWorldPoint(screenPosition);

                // 执行建造操作
                Build(worldPosition);
                //Instantiate(buildingPrefab, worldPosition, Quaternion.identity);
                Debug.Log("建造建筑物在位置: " + worldPosition);

                GameInput.Controller.Default.Press.performed -= OnPress;
                GameInput.Controller.Default.MousePosition.performed -= OnMouseMoved;
            }
            else
            {
                Debug.Log("无法在该位置建造建筑物 ");

            }
        }

        private bool CanBuild(Vector3 worldPosition)
        {
            // 例如检测地形、碰撞等
            return false;
        }

        private void Build(Vector3 worldPosition)
        {
            TableProxy tableProxy = Facade.RetrieveProxy(TableProxy.NAME) as TableProxy;
            BuildingData buildingData = tableProxy.GetData<BuildingData>(data.id);


            if (buildingData == null)
                return;


            ResourceManager.Instance.LoadAssetAsync<GameObject>(buildingData.asset, (asset, _) =>
            {
                GameObject building = GameObjectPool.Spawn(asset);

                GameObject dummy = building.gameObject;
                LoadWeaponData loadWeaponData = new LoadWeaponData();
                loadWeaponData.id = 1001;
                loadWeaponData.dummy = dummy;


                //更新地图信息
                //更新流场寻路

                //加载武器
                SendNotification(LoadWeapon.NAME, loadWeaponData);
            });
        }
    }
}
