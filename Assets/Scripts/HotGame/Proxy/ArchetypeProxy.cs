using ECS;
using PureMVC.Patterns.Proxy;
using System.Collections.Generic;

namespace HotGame
{
    public class ArchetypeProxy : Proxy
    {

        public new static string NAME = typeof(ArchetypeProxy).FullName;

        Dictionary<Archetype, EntityArchetype> datas;

        public ArchetypeProxy() : base(NAME) { }

        public enum Archetype
        {
            Hero ,
            Bullet,
            NPC,
        }
        public override void OnRegister()
        {
            datas = new Dictionary<Archetype, EntityArchetype>
            {
                { Archetype.Hero, ArchetypeData.Hero },
                { Archetype.Bullet, ArchetypeData.Bullet },
                { Archetype.NPC, ArchetypeData.NPC }
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
