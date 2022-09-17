
using ECS;
using PureMVC.Patterns.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ability
{
    public class BuffProxy : Proxy
    {
        public static new string NAME = typeof(BuffProxy).FullName;

        //buff id,BuffData
        private readonly Dictionary<int, BuffData> datas = new Dictionary<int, BuffData>();
        private readonly Dictionary<Entity, List<BuffVO>> vos = new Dictionary<Entity, List<BuffVO>>();

        List<BuffVO> buffs = new List<BuffVO>();


        public BuffProxy() : base(NAME)
        {

        }

        public override void OnRegister()
        {

        }

        public List<BuffVO> Datas => buffs;




        public void AddBuff(BuffVO vo)
        {

            if (vo == null)
                return;

            vo.value = vo.effect.value;

            if (vo.layer.HasFlag(BuffLayer.Cumulate))// 叠加
            {
                buffs.Add(vo);

                vos[vo.target].Add(vo);
            }
            else// 替换
            {
                if (vos.TryGetValue(vo.target, out var data))
                {
                    BuffVO buff0 = data.Find(b => b.id == vo.id);
                    if (buff0 != null)
                    {
                        buff0.state = BuffState.Added;
                        buff0.timer = 0;
                        buff0.intervalTimer = 0;
                    }
                    else
                    {
                        data.Add(vo);
                    }
                }

                BuffVO buff = buffs.Find(b => b.target == vo.target && b.id == vo.id);
                if (buff != null)
                {
                    buff.state = BuffState.Added;
                    buff.timer = 0;
                    buff.intervalTimer = 0;
                }
                else
                {
                    buffs.Add(vo);
                }
            }
        }


        public void RemoveBuff(BuffVO vo)
        {
            if (vo == null)
                return;

            // 重置属性
            if (vo.layer.HasFlag(BuffLayer.Reset))
            {
                EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;
                vo.effect.value = -vo.value;
                effectProxy.Play(vo.effect);
            }

            if (vos.TryGetValue(vo.target, out var data))
            {
                BuffVO buff0 = data.Find(b => b.id == vo.id);
                if (buff0 != null)
                {
                    data.Remove(buff0);
                    buff0=null;
                }

                // 该entity没有任何buff了，移除该entity
                if (data.Count == 0)
                {
                    vos.Remove(vo.target);
                }
            }

            buffs.Remove(vo);
        }



        public BuffData GetData(int id)
        {
            datas.TryGetValue(id, out BuffData data);
            return data;
        }

    }
}
