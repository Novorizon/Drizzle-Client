//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.Entities;
//using Unity.Mathematics;
//using Unity.Rendering;
//using Unity.Transforms;
//using UnityEngine;

//namespace Game
//{

//    [UpdateInGroup(typeof(JobSystemGroup))]
//    public class BuffUpdateSystem : SystemBaseFacade
//    {
//        protected override void OnUpdate()
//        {
//            var proxy = Facade.RetrieveProxy(BuffProxy.NAME) as BuffProxy;
//            float dt = Time.DeltaTime;
//            Entities
//                .ForEach((ref DynamicBuffer<Buff> buffs , ref Halo halo, in Entity entity) =>
//                {
//                    for (int i = 0; i < buffs.Length; ++i)
//                    {
//                        Buff buff = buffs[i];
//                        buff.buff.timer += dt;

//                        if ((buffs[i].buff.type & BuffType.Halo) > 0)//Halo
//                        {

//                            buffs.RemoveAt(i);
//                        }
//                        if ((buffs[i].buff.type & BuffType.Continuous) > 0)//持续型buff
//                        {

//                        }
//                        else //间歇型buff
//                        {
//                            buff.buff.intervalTimer += dt;
//                            if (buff.buff.intervalTimer > buff.buff.interval)
//                            {
//                                BuffManager.AddBuff(buff.buff);
//                                buff.buff.intervalTimer = 0;
//                            }
//                        }

//                        buffs[i] = buff;
//                    }
//                })
//                .WithStructuralChanges()
//                .WithoutBurst()
//                .Run();
//        }
//    }
//}
