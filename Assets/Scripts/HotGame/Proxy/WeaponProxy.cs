using DataBase;
using Game.Protobuffer;
using PureMVC.Patterns.Proxy;
using System.Collections.Generic;
using UnityEngine;
using ECS;
using HotGame;

namespace Game
{
    public class WeaponProxy : Proxy
    {

        public new static string NAME = typeof(WeaponProxy).FullName;

        private Dictionary<int, WeaponVO> datas;

        public WeaponProxy() : base(NAME) { }

        public override void OnRegister()
        {
            datas = new Dictionary<int, WeaponVO>();
        }

        public override void OnRemove()
        {
        }


        public WeaponVO GetData(int id)
        {
            datas.TryGetValue(id, out WeaponVO data);
            return data;
        }

        public Dictionary<int, WeaponVO> Datas() => datas;

        public void SetData(WeaponVO data) => datas[data.id] = data;
    }
}
