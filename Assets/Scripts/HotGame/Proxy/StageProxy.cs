using DataBase;
using ECS;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HotGame
{
    public class StageProxy : Proxy
    {

        public new static string NAME = typeof(StageProxy).FullName;

        Dictionary<int, StageVO> datas;

        public StageProxy() : base(NAME) { }


        public override void OnRegister()
        {
            datas = new Dictionary<int, StageVO>();
          
        }

        public override void OnRemove()
        {
        }




        public StageVO GetData(int id)
        {
            datas.TryGetValue(id, out StageVO data);
            return data;
        }


        public void SetData(StageVO data) => datas[data.id] = data;

        public void UpdateState(int id,StageState state)
        {
            StageVO data = GetData(id);
            if (data == null)
                return;

            data.state = state;
        }

        public StageState GetState(int id)
        {
            StageVO data = GetData(id);
            if (data == null)
                return StageState.None;

            return data.state;
        }
    }
}
