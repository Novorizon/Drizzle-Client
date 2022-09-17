using Database;
using DataBase;
using ECS;
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

        Dictionary<int, NPCData> datas;//entity id

        Dictionary<ulong, NPCVO> vos;

        public NpcProxy() : base(NAME) { }

        public override void OnRegister()
        {
            datas = new Dictionary<int, NPCData>();
        }

        public override void OnRemove()
        {
        }





        public NPCData GetData(int id)
        {
            datas.TryGetValue(id, out NPCData data);
            return data;
        }

        public Dictionary<int, NPCData> GetDatas() => datas;

        public void SetData(NPCData data) => datas[data.id] = data;

        public void SetVO(NPCVO vo)
        {
            vos.Add(vo.guid, vo);
        }



        public NPCVO GetVO(ulong id)
        {
            vos.TryGetValue(id, out NPCVO vo);
            return vo;
        }

        public void ClearData()
        {
            datas.Clear();
        }
        public void ClearVO(List<string> keys)
        {
            PlayerPrefs.DeleteAll();

        }

        public bool HasNPC(ulong id) => vos.ContainsKey(id);



        public void SetAttribute(ulong id, Ability.Attribute attribute, float value)
        {
            if (!vos.TryGetValue(id, out NPCVO vo))
                return;

            switch (attribute)
            {
                case Ability.Attribute.Attack:
                    vo.attack += (int)value;
                    break;
                case Ability.Attribute.Defence:
                    vo.defence += (int)value;
                    break;
                case Ability.Attribute.Speed:
                    vo.speed += (int)value;
                    break;
                case Ability.Attribute.Health:
                    vo.health += (int)value;
                    break;
            }
        }

        public void SetImmunity(ulong id, float value)
        {
            if (!vos.TryGetValue(id, out NPCVO vo))
                return;

            vo.isImmunity = value > 0 ? true : false;
        }

        public void SetInvincible(ulong id, float value)
        {
            if (!vos.TryGetValue(id, out NPCVO vo))
                return;

            vo.isInvincible = value > 0 ? true : false;
        }
    }
}
