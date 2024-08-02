using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using Game;
using Unity.Mathematics;
using UnityEngine;

namespace DataBase
{
    public class WeaponData : TableData
    {
        public int id;
        public string name;
        public string description { get; set; }

        public string asset;//武器模型
        public string effectAsset; //武器特效
        public string fireEffecAsset; //武器射击特效
        public float scale = 1;

        public string bulletAsset;//子弹模型
        public string bulletEffectAsset;//子弹伤害特效

        public WeaponType type = WeaponType.Single;

        public float interval;//射击间隔
        public int trackCount = 1;//弹道数量，每波子弹数量
        public float angle;//多子弹 攻击范围角度

        public float speed;//子弹飞行速度
        public float lifetime;//子弹存在时间

        //public WeaponAttackShape shape;
        //public float range;//子弹攻击距离=speed*lifetime


        private bool Updated;

        public WeaponData()
        {
            Updated = false;
        }

        public void UpdateHeroToLevel()
        {
            if (!Updated) //this must not be called more than once, otherwise something is wrong
            {
                Updated = true;
            }
        }

        public WeaponData Clone()
        {
            WeaponData newClone = new WeaponData();
            //deep copy
            newClone.id = id;
            newClone.name = name;
            return newClone;
        }
    }
}