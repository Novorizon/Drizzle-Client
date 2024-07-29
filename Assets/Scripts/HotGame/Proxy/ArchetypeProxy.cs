using DataBase;
using ECS;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game
{
    public class ArchetypeProxy : Proxy
    {

        public new static string NAME = typeof(ArchetypeProxy).FullName;

        Dictionary<Archetype, EntityArchetype> datas;

        public ArchetypeProxy() : base(NAME) { }

        public enum Archetype
        {
            Hero ,
            Bullet ,
        }
        public override void OnRegister()
        {
            datas = new Dictionary<Archetype, EntityArchetype>
            {
                { Archetype.Hero, ArchetypeData.Hero },
                { Archetype.Bullet, ArchetypeData.Bullet }
            };
        }

        public override void OnRemove()
        {
        }




        public EntityArchetype GetData(Archetype id)
        {
            datas.TryGetValue(id, out EntityArchetype data);
            return data;
        }


        public void SetData(EntityArchetype data)
        {

        }

    }
}
