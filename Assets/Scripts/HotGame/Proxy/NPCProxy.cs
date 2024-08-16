using Database;
using DataBase;
using ECS;
using HotGame;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class NpcProxy : Proxy
    {

        public new static string NAME = typeof(NpcProxy).FullName;

        Dictionary<int, NpcVO> datas;//entity id

        public NpcProxy() : base(NAME) { }

        public override void OnRegister()
        {
            datas = new Dictionary<int, NpcVO>();
        }

        public override void OnRemove()
        {
        }





        public NpcVO GetData(int id)
        {
            datas.TryGetValue(id, out NpcVO data);
            return data;
        }

        public Dictionary<int, NpcVO> GetDatas() => datas;

        public void SetData(NpcVO data) => datas[data.id] = data;



        public void ClearData()
        {
            datas.Clear();
        }

        public bool HasNPC(int id) => datas.ContainsKey(id);



        //public void SetAttribute(int id, Ability.Attribute attribute, float value)
        //{
        //    if (!datas.TryGetValue(id, out NPCVO vo))
        //        return;

        //    switch (attribute)
        //    {
        //        case Ability.Attribute.Attack:
        //            vo.attack += (int)value;
        //            break;
        //        case Ability.Attribute.Defence:
        //            vo.defence += (int)value;
        //            break;
        //        case Ability.Attribute.Speed:
        //            vo.speed += (int)value;
        //            break;
        //        case Ability.Attribute.Health:
        //            vo.health += (int)value;
        //            break;
        //    }
        //}

        public void SetImmunity(int id, float value)
        {
            if (!datas.TryGetValue(id, out NpcVO vo))
                return;

            vo.isImmunity = value > 0 ? true : false;
        }

        public void SetInvincible(int id, float value)
        {
            if (!datas.TryGetValue(id, out NpcVO vo))
                return;

            vo.isInvincible = value > 0 ? true : false;
        }
    }
}
