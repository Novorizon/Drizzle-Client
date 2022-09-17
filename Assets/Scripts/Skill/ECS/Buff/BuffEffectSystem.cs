namespace Game
{
    //    public class BuffEffectSystem : ComponentSystem<BuffEffect, AddedEffect>
    //    {
    //        protected override void OnUpdate(int index, Entity entity, BuffEffect translation, AddedEffect toward)
    //        {
    //            translation.Value += toward.Value * speed.Value * World.TimePerFrame;
    //        }
    //    }
}

//    public class BuffEffectSystem : SystemBaseFacade
//    {
//        protected override void OnUpdate()
//        {

//            Entities
//                .ForEach((ref BuffEffect buffEffects, ref AddedEffect effects, in Entity entity) =>
//                {
//                    if (buffEffects.effects.Length > 0)
//                    {
//                        //var e = buffEffects.GetEnumerator();
//                        //while (e.MoveNext())
//                        //{
//                        //    var current = e.Current;
//                        //    var buffEffect = current.effect;
//                        //    if (buffEffect.entity != Entity.Null)
//                        //    {
//                        //        //相同特效只有一个
//                        //        if (effects.effects.ContainsKey(buffEffect.id))
//                        //        {
//                        //            var effect = effects.effects[buffEffect.id];
//                        //            EntityManager.SetComponentData(effect, new Span() { Value = buffEffect.time });
//                        //        }
//                        //        else
//                        //        {
//                        //            var effect = CreateEffect(EntityManager, buffEffect.entity, entity, buffEffect.time);
//                        //            effects.effects.Add(buffEffect.id, effect);
//                        //        }
//                        //    }
//                        //}
//                        for (int i = 0; i < buffEffects.effects.Length; ++i)
//                        {
//                            var buffEffect = buffEffects.effects[i];
//                            if (buffEffect.entity != Entity.Null)
//                            {
//                                //相同特效只有一个
//                                if (effects.effects.ContainsKey(buffEffect.id))
//                                {
//                                    var effectEntity = effects.effects[buffEffect.id];
//                                    if (EntityManager.HasComponent<Span>(effectEntity))
//                                    {
//                                        EntityManager.SetComponentData(effectEntity, new Span() { Value = 0 });
//                                    }
//                                    //EntityManager.SetComponentData(effect, new Span() { Value = buffEffect.time });
//                                }
//                                var effect = CreateEffect(EntityManager, buffEffect, entity, buffEffect.time);
//                                effects.effects.Add(buffEffect.id, effect);
//                            }
//                        }
//                        buffEffects.effects.Clear();
//                    }
//                })
//                .WithStructuralChanges()
//                .WithoutBurst()
//                .Run();

//            //var commandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
//            //Entities
//            //    .ForEach((int entityInQueryIndex, ref DynamicBuffer<BuffEffect> buffEffect, ref AddedEffect effects, in Entity entity) =>
//            //    {
//            //        if (buffEffect.Length > 0)
//            //        {
//            //            for (int i = 0; i < buffEffect.Length; ++i)
//            //            {
//            //                if (buffEffect[i].effect.entity != Entity.Null)
//            //                {
//            //                    var effect = commandBuffer.Instantiate(entityInQueryIndex, buffEffect[i].effect.entity);
//            //                    commandBuffer.AddComponent(entityInQueryIndex, effect, new LocalToWorld());
//            //                    commandBuffer.AddComponent(entityInQueryIndex, effect, new Translation());
//            //                    commandBuffer.AddComponent(entityInQueryIndex, effect, new Rotation());
//            //                    commandBuffer.AddComponent(entityInQueryIndex, effect, new LocalToParent());
//            //                    commandBuffer.AddComponent(entityInQueryIndex, effect, new Parent { Value = entity });
//            //                    commandBuffer.AddComponent(entityInQueryIndex, effect, new Span() { Value = buffEffect[i].effect.time });

//            //                    effects.effects.Add(buffEffect[i].effect.id, effect);
//            //                }
//            //            }
//            //            buffEffect.Clear();
//            //        }
//            //    })
//            //    .WithoutBurst()
//            //    .ScheduleParallel();

//            //m_EntityCommandBufferSystem.AddJobHandleForProducer(Dependency);
//        }
//        private static Entity CreateEffect(EntityManager EntityManager, Effect effect, Entity entity, float time)
//        {
//            var effectEntity = EntityManager.Instantiate(effect.entity);
//            EntityManager.AddComponentData(effectEntity, new LocalToWorld());
//            EntityManager.AddComponentData(effectEntity, new Translation());
//            EntityManager.AddComponentData(effectEntity, new Rotation());
//            EntityManager.AddComponentData(effectEntity, new LocalToParent());
//            EntityManager.AddComponentData(effectEntity, new Parent { Value = entity });
//            EntityManager.AddComponentData(effectEntity, new Span() { Value = time });
//            EntityManager.AddComponentData(effectEntity, effect);

//            return effectEntity;
//        }
//    }


//}
