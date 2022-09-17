
using ECS;
using PureMVC.Interfaces;
using System.Collections.Generic;
using UnityEngine;
namespace Ability
{
    public class BuffSystem : SystemBase<AbilityComponent>
    {
        private IFacade Facade => PureMVC.Patterns.Facade.Facade.GetInstance(() => new PureMVC.Patterns.Facade.Facade());
        BuffProxy proxy = null;
        protected override void OnCreate()
        {
            proxy = Facade.RetrieveProxy(BuffProxy.NAME) as BuffProxy;
        }

        protected override void OnUpdate(int index, Entity entity, AbilityComponent component)
        {

            List<BuffVO> buffs = proxy.Datas;
            if (buffs == null || buffs.Count == 0)
                return;

            for (int i = buffs.Count - 1; i >= 0; i--)
            {
                BuffVO vo = buffs[i];

                float dt = Time.DeltaTime;


                if (vo.state == BuffState.Added)
                {
                    if (vo.layer.HasFlag(BuffLayer.Continuous))
                        continue;

                    if (vo.intervalTimer >= vo.interval)
                    {
                        vo.intervalTimer = 0;

                        vo.value += vo.effect.value;//累加 重置时需要
                        EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
                        effectProxy.Play(vo.effect);
                    }
                    vo.intervalTimer += dt;


                    if (vo.timer >= vo.duration)
                    {
                        vo.state = BuffState.Removed;
                    }
                    vo.timer += dt;
                }



                if (vo.state == BuffState.Removed)
                {
                    if (proxy == null)
                        proxy = Facade.RetrieveProxy(BuffProxy.NAME) as BuffProxy;

                    proxy.RemoveBuff(vo);
                }



            }
        }
    }


}
