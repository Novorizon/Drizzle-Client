//using Unity.Collections;
//using Unity.Collections.LowLevel.Unsafe;
//using Unity.Entities;
//using Unity.Mathematics;

//namespace Game
//{
//    public class SkillDetectorTargetSystem : SystemBaseFacade
//    {
//        protected override void OnUpdate()
//        {
//            var proxy = Facade.RetrieveProxy(EntityQueryProxy.NAME) as EntityQueryProxy;
//            var entities = proxy.DamageableEntities;
//            var matrixAll = proxy.MatrixForDamageables;
//            var layers = proxy.LayersForDamageables;

//            Entities
//                .ForEach((Entity entity, ref DynamicBuffer<DamageableTarget> targets, ref Skill skill, ref Track track, ref FaceDirection face, ref Translation translation) =>
//                {
//                    targets.Clear();
//                    if (skill.skill.id == 0)
//                        return;

//                    //光环暂时设计为只能有施法者给自己添加光环 技能目标即为施法者 唯一的光环载体
//                    if ((skill.skill.type & SkillType.Halo) > 0)
//                        return;

//                    if (track.state != TrackState.End)
//                    {
//                        if ((skill.skill.type & SkillType.UpdateTarget) == 0)
//                        {
//                            return;
//                        }
//                    }


//                    if ((skill.skill.type & SkillType.LockTarget) > 0 && skill.skill.maxTargets == 1)
//                    {
//                        targets.Add(new DamageableTarget { Value = skill.target, distanceSquare = 0 });
//                    }
//                    else
//                    {
//                        //查找skill范围内的目标
//                        UnsafeList<EntityDistance> distances = new UnsafeList<EntityDistance>(64, Allocator.Temp, NativeArrayOptions.ClearMemory);
//                        for (int i = 0; i < entities.length; i++)
//                        {
//                            if (entity == entities[i])
//                                continue;

//                            if ((skill.skill.supposeLayer & layers[i].Value) == 0)
//                                continue;

//                            var distanceSquare = math.distancesq(translation.Value, matrixAll[i].Position);
//                            if (distanceSquare <= skill.skill.distance * skill.skill.distance)
//                            {
//                                if (skill.skill.rangeShape == RangeShapeEnum.RANGEENUM_SPHERE)
//                                {
//                                    distances.Add(new EntityDistance() { id = i, distance = distanceSquare });

//                                }
//                                else if (skill.skill.rangeShape == RangeShapeEnum.RANGEENUM_SECTOR)
//                                {
//                                    var vec = matrixAll[i].Position - translation.Value;
//                                    var cos = math.dot(math.normalize(face.Value), math.normalize(vec));
//                                    if (cos > math.cos(skill.skill.angle))
//                                    {
//                                        distances.Add(new EntityDistance() { id = i, distance = distanceSquare });
//                                    }
//                                }
//                            }
//                        }
//                        for (int i = 0; i < distances.length; i++)
//                        {
//                            targets.Add(new DamageableTarget { Value = entities[distances[i].id], distanceSquare = distances[i].distance });
//                        }
//                    }


//                })
//                //.WithoutBurst()
//                .ScheduleParallel();
//        }
//    }
//}
