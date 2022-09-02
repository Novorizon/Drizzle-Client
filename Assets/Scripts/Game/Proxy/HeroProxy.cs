using DataBase;
using Game.Protobuffer;
using PureMVC.Patterns.Proxy;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HeroProxy : Proxy
    {

        public new static string NAME = typeof(HeroProxy).FullName;

        private HeroVO data;
        NetProxy netProxy = null;
        HandlerProxy handlerProxy = null;

        public HeroProxy() : base(NAME) { }

        public override void OnRegister()
        {
            netProxy = Facade.RetrieveProxy(NetProxy.NAME) as NetProxy;

            handlerProxy = Facade.RetrieveProxy(HandlerProxy.NAME) as HandlerProxy;
            handlerProxy.RegisterHandler(typeof(S2C_PlayerInfo_Ack), HandlePlayerInfoAck);

            data = new HeroVO();
            GetPrefs();
        }

        public override void OnRemove()
        {
        }


        protected void HandlePlayerInfoAck(object data)
        {
            S2C_PlayerInfo_Ack ack = data as S2C_PlayerInfo_Ack;
            if (ack == null || ack.Err > 0)
                return;

            this.data.id = ack.Id;
            this.data.name = ack.Name;
            //this.data.modelPath = ack.modelPath;
            this.data.modelId = ack.ModelId;
            this.data.gender = ack.Gender;
            this.data.level = ack.Level;
            this.data.speed = ack.Speed;
            this.data.health = ack.Health;
            this.data.age = ack.Age;
            this.data.strength = ack.Strength;
            this.data.agility = ack.Agility;
            this.data.intelligence = ack.Intelligence;
            this.data.position = ack.Position.Vector3();
            this.data.forward = ack.Forward.Vector3();

            SendNotification(LoadHeroCommand.NAME);

        }

        public void GetPrefs()
        {
            data.UID = PlayerPrefs.GetInt("UID", 0);//
            data.modelId = PlayerPrefs.GetInt("ModelId", 1000);//
            data.position = PlayerPrefsX.GetVector3("Position", default);//
        }

        public void SetPrefs()
        {
            PlayerPrefs.SetInt("ModelId", data.modelId);//
            PlayerPrefs.SetInt("ModelId", data.modelId);//
            PlayerPrefsX.SetVector3("Position", data.position);//
        }



        public HeroVO GetData() => data;


        public void SetData(DefaultData defaultData)
        {
            data.id = defaultData.id;
            data.name = defaultData.name;
            data.modelPath = defaultData.modelPath;
            data.modelId = defaultData.modelId;
            data.gender = defaultData.gender;
            data.level = defaultData.level;
            data.speed = defaultData.speed;
            data.health = defaultData.health;
            data.age = defaultData.age;
            data.strength = defaultData.strength;
            data.agility = defaultData.agility;
            data.intelligence = defaultData.intelligence;
            data.position = defaultData.position;
            data.forward = defaultData.forward;
        }

        public void SetData(HeroVO vo)
        {

        }

        public void ClearPlayerPrefs(string key)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteKey(key);

        }
        public void ClearPlayerPrefs(List<string> keys)
        {
            PlayerPrefs.DeleteAll();

        }
    }
}
