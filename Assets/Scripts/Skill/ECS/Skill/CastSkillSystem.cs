using Unity.Mathematics;
using UnityEngine;
using ECS;


namespace Game
{
    public class CastSkillSystem : SystemBase<SkillComponent, MoveDirection, FaceDirection, Position>
    {


        protected override void OnUpdate(int index, Entity entity, SkillComponent skill, MoveDirection move, FaceDirection face, Position translation)
        {
            float dt = Time.DeltaTime;
            if (skill.id == 0)
                return;

            if ((skill.type & SkillType.LockTarget) > 0 && skill.target == null)
            {
                skill.state = SkillState.End;
            }

            if (skill.state == SkillState.End)
            {
                EntityManager.RemoveComponentData<SkillComponent>(entity);
                return;
            }


            if (skill.state == SkillState.None)
            {
                skill.state = SkillState.Foreswing;

                if (skill.foreswingTimer >= skill.foreswing)
                {
                    if (skill.channelTime == 0)
                    {
                        skill.state = SkillState.Cast;
                    }
                    else
                    {
                        skill.state = SkillState.Channeling;
                    }
                }
                skill.foreswingTimer += dt;
            }


            if (skill.state == SkillState.Channeling)
            {
                if (skill.channelTimer >= skill.channelTime)
                {
                    skill.state = SkillState.Cast;
                }
                else
                {
                    skill.channelTimer += dt;
                    if (skill.channelStop)
                    {
                        move.Value = float3.zero;
                        //面向目标
                        if (skill.needTarget)
                        {
                            Position pos = EntityManager.GetComponentData<Position>(skill.target);
                            face.Value = math.normalize(pos.Value - translation.Value);
                        }
                    }

                    skill.intervalTimer += dt;

                    //引导型可能有多次
                    if (skill.intervalTimer >= skill.interval)
                    {
                        skill.intervalTimer = 0;
                        //生成技能entity，组件
                        //skill entity 到达目标（entity 或者落点）时，才对目标增加buff或者Halo
                        var pos = translation.Value + math.up();

                        var skillEntity = EntityManager.Create();
                        EntityManager.AddComponentData<LocalToWorld>(skillEntity);
                        EntityManager.AddComponentData<Position>(skillEntity).Value = pos;
                        EntityManager.AddComponentData<Rotation>(skillEntity).Value = quaternion.identity;
                        EntityManager.AddComponentData<Scale>(skillEntity).Value = 1;

                        EntityManager.AddComponentData<MoveDirection>(skillEntity).Value = move.Value;
                        EntityManager.AddComponentData<Speed>(skillEntity).Value =1;
                        EntityManager.AddComponentData<FaceDirection>(skillEntity).Value = face.Value;

                        //创建技能GameObject
                        if (false)
                        {
                            EntityManager.LoadGameObject(skillEntity, "", pos, Quaternion.identity, (e, _) =>
                            {
                                
                            });

                        }

                        Skill skill1 = EntityManager.AddComponentData<Skill>(skillEntity);
                        skill1 = skill.skill;
                        if (skill.needTarget)
                        {
                            SkillTarget target = EntityManager.AddComponentData<SkillTarget>(skillEntity);
                            if (skill.target != null)
                                target.targets.Add(skill.target.GUID);
                            else
                            {

                            }
                        }
                        //EntityManager.AddComponentData<LocalToWorld>(skillEntity, new Speed() { Value = new AttributeParam(10) });
                        //EntityManager.AddComponentData<LocalToWorld>(skillEntity, new SkillTag());
                        //EntityManager.AddComponentData<LocalToWorld>(skillEntity, new Track()
                        //{
                        //    start = pos,
                        //    end = pos + 10 * face.Value,
                        //    speed = 1,
                        //    state = TrackState.None,
                        //    type = TrackType.Straight,
                        //    height = 1
                        //});//轨迹组件
                        //EntityManager.AddBuffer<DamageableTarget>(skillEntity);


                        //生成技能特效，组件
                        //var effectEntity = EntityManager.Instantiate(skill.skill.effect);
                        //EntityManager.AddComponentData(effectEntity, new LocalToWorld());
                        //EntityManager.AddComponentData(effectEntity, new Translation());
                        //EntityManager.AddComponentData(effectEntity, new Translation());
                        //EntityManager.AddComponentData(effectEntity, new Rotation());
                        //EntityManager.AddComponentData(effectEntity, new LocalToParent());
                        //EntityManager.AddComponentData(effectEntity, new Parent { Value = skillEntity });

                        //skill.skill.effect = effectEntity;
                        //EntityManager.AddComponentData(skillEntity, skill);
                    }
                }

            }



            if (skill.state == SkillState.Backswing)
            {
                skill.backswingTimer += dt;

                if (skill.backswingTimer < skill.backswing)
                {
                    if (skill.backswingStop)
                        move.Value = float3.zero;
                }
                else
                {
                    skill.state = SkillState.End;
                }
            }
        }
    }
}


