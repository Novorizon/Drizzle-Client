using Game.Protobuffer;
using Google.Protobuf;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Game
{
    public class SkillProxy : Proxy
    {
        public static new string NAME = typeof(SkillProxy).FullName;

        private readonly Dictionary<int, SkillVO> datas = new Dictionary<int, SkillVO>();
        private readonly Dictionary<int, SkillVO> vos = new Dictionary<int, SkillVO>();

        public SkillProxy() : base(NAME)
        {

        }

        public override void OnRegister()
        {

        }

        public void Set(SkillVO vo)
        {
            if (datas.ContainsKey(vo.id))
            {
                datas[vo.id] = vo;
            }
            else
            {
                datas.Add(vo.id, vo);
            }
        }

        public SkillVO Get(int Key)
        {
            datas.TryGetValue(Key, out SkillVO vo);
            return vo;
        }
    }
}
