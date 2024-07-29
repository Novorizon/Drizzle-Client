using DataBase;
using ECS;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game
{
    public class StageProxy : Proxy
    {

        public new static string NAME = typeof(StageProxy).FullName;

        Dictionary<int, StageData> datas;

        public StageProxy() : base(NAME) { }

        public enum Archetype
        {
            Hero ,
            Bullet ,
        }
        public override void OnRegister()
        {
            datas = new Dictionary<int, StageData>();
          
        }

        public override void OnRemove()
        {
        }




        public StageData GetData(int id)
        {
            datas.TryGetValue(id, out StageData data);
            return data;
        }


        public void SetData(EntityArchetype data)
        {

        }

    }
}
