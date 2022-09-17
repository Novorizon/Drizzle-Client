
//using Game;
//using Unity.Mathematics;

//namespace Unity.Entities
//{
//    public struct BuffManager
//    {
//        readonly static EntityManager EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//        public static void AddBuff<T>(BuffClip buff) where T : struct, IComponentData, IBuffData
//        {
//            if (EntityManager.HasComponent<T>(buff.entity))
//            {
//                var value = EntityManager.GetComponentData<T>(buff.entity);
//                value.AddBuff(buff.type, buff.value);
//                EntityManager.SetComponentData(buff.entity, value);
//            }
//        }

//        public static void AddBuff(BuffClip buff)
//        {
//            if (buff.to == BuffTo.Attack)
//            {
//                if (EntityManager.HasComponent<Attack>(buff.entity))
//                {
//                    var value = EntityManager.GetComponentData<Attack>(buff.entity);
//                    value.Value.AddBuff(buff.type, buff.value);
//                    EntityManager.SetComponentData(buff.entity, value);
//                }
//            }
//            if (buff.to == BuffTo.Armor)
//            {
//                if (EntityManager.HasComponent<Armor>(buff.entity))
//                {
//                    var value = EntityManager.GetComponentData<Armor>(buff.entity);
//                    value.Value.AddBuff(buff.type, buff.value);
//                    EntityManager.SetComponentData(buff.entity, value);
//                }
//            }
//            if (buff.to == BuffTo.Speed)
//            {
//                EntityManager EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//                if (EntityManager.HasComponent<Speed>(buff.entity))
//                {
//                    var value = EntityManager.GetComponentData<Speed>(buff.entity);
//                    value.Value.AddBuff(buff.type, buff.value);
//                    EntityManager.SetComponentData(buff.entity, value);
//                }
//                //AddBuff<Speed>(buff);
//            }
//            if (buff.to == BuffTo.HP)
//            {
//                var value = EntityManager.GetComponentData<HP>(buff.entity);
//                value.Value.AddBuff(buff.type, buff.value);
//                EntityManager.SetComponentData(buff.entity, value);
//            }
//            if (buff.to == BuffTo.MagicImmunity)
//            {
//                EntityManager EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//                if (EntityManager.HasComponent<MagicImmunity>(buff.entity))
//                {
//                    EntityManager.SetComponentData(buff.entity, new MagicImmunity() { Value = true });
//                }
//            }
//            if (buff.to == BuffTo.Invincible)
//            {
//                EntityManager EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//                if (EntityManager.HasComponent<Invincible>(buff.entity))
//                {
//                    EntityManager.SetComponentData(buff.entity, new Invincible() { Value = true });
//                }
//            }

//        }

//        public static void RemoveBuff<T>(BuffClip buff) where T : struct, IComponentData, IBuffData
//        {
//            if (EntityManager.HasComponent<T>(buff.entity))
//            {
//                var value = EntityManager.GetComponentData<T>(buff.entity);
//                value.RemoveBuff(buff.type, buff.value);
//            }
//        }

//        public static void RemoveBuff(BuffClip buff)
//        {
//            if (buff.to == BuffTo.Attack)
//            {
//                if (EntityManager.HasComponent<Attack>(buff.entity))
//                {
//                    var value = EntityManager.GetComponentData<Attack>(buff.entity);
//                    value.Value.RemoveBuff(buff.type, buff.value);
//                    EntityManager.SetComponentData(buff.entity, value);
//                }
//            }
//            if (buff.to == BuffTo.Armor)
//            {
//                if (EntityManager.HasComponent<Armor>(buff.entity))
//                {
//                    var value = EntityManager.GetComponentData<Armor>(buff.entity);
//                    value.Value.RemoveBuff(buff.type, buff.value);
//                    EntityManager.SetComponentData(buff.entity, value);
//                }
//            }
//            if (buff.to == BuffTo.Speed)
//            {
//                EntityManager EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//                if (EntityManager.HasComponent<Speed>(buff.entity))
//                {
//                    var value = EntityManager.GetComponentData<Speed>(buff.entity);
//                    value.Value.RemoveBuff(buff.type, buff.value);
//                    EntityManager.SetComponentData(buff.entity, value);
//                }
//                //AddBuff<Speed>(buff);
//            }
//            if (buff.to == BuffTo.HP)
//            {
//                if (EntityManager.HasComponent<HP>(buff.entity))
//                {
//                    var value = EntityManager.GetComponentData<HP>(buff.entity);
//                    value.Value.RemoveBuff(buff.type, buff.value);
//                    EntityManager.SetComponentData(buff.entity, value);
//                }
//            }
//            if (buff.to == BuffTo.MagicImmunity)
//            {
//                EntityManager EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//                if (EntityManager.HasComponent<MagicImmunity>(buff.entity))
//                {
//                    EntityManager.SetComponentData(buff.entity, new MagicImmunity() { Value = false });
//                    //清除其他buff
//                    //buff列表 遍历 移除
//                }
//            }
//            if (buff.to == BuffTo.Invincible)
//            {
//                EntityManager EntityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
//                if (EntityManager.HasComponent<Invincible>(buff.entity))
//                {
//                    EntityManager.SetComponentData(buff.entity, new Invincible() { Value = false });
//                }
//            }

//        }
//    }
//}