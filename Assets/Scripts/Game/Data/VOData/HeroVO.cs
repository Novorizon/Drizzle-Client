using System.Collections.Generic;
using Mono.Data.Sqlite;
using ECS;
using UnityEngine;

namespace Game
{
    public class HeroVO
    {
        public ulong guid;
        public Entity entity;
        public int id;                                        // 
        public string name;                                     // 
        public string modelPath;                                     // 
        public int modelId;                                       // 

        public int gender;                                       // ?ȼ?
        public int level;                                // 

        public int attack;                                      // 
        public int defence;                                      // 

        //public Attribute speed;                                      // 
        public float speed;                                      // 
        public int health;                                       // 
        public int age;                                      // 

        public int strength;                         // 
        public int agility;                                  // 
        public int intelligence;                           // 


        public Vector3 position;                                       // ?ȼ?
        public Vector3 forward;                                // 


        public bool isImmunity;
        public bool isInvincible;
        public HeroVO()
        {
           
        }


        public HeroVO Clone()
        {
            HeroVO newClone = new HeroVO();
            return newClone;
        }
    }
}