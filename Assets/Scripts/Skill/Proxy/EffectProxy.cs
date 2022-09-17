
using PureMVC.Patterns.Proxy;
using Game;
using System.Collections.Generic;
using System.Linq;
using ECS;

namespace Ability
{
    public class EffectProxy : Proxy
    {
        public static new string NAME = typeof(EffectProxy).FullName;

        private readonly Dictionary<int, Effect> datas = new Dictionary<int, Effect>();
        private readonly Dictionary<int, EffectVO> vos = new Dictionary<int, EffectVO>();


        public EffectProxy() : base(NAME)
        {

        }

        public override void OnRegister()
        {

        }


        public void OnHit(EffectVO vo)
        {
            Play(vo);
        }

        public EffectVO GetVO(int Key)
        {
            vos.TryGetValue(Key, out EffectVO vo);
            return vo;
        }

        public void Play(Entity target, EffectVO vo)
        {
            if (target == null)
                return;

            vo.target = target;
            Play(vo);
        }

        public void Play(List<Entity> targets, EffectVO vo)
        {
            if (targets == null || targets.Count == 0)
                return;

            for (int i = 0; i < targets.Count; i++)
            {
                vo.target = targets[i];
                Play(vo);
            }
        }

        public void Play(EffectVO vo)
        {
            if (vo.target == null)
                return;

            switch (vo.type)
            {
                case Type.Damage:
                case Type.Heal:
                case Type.Attribute:
                    SetAttribute(vo);
                    break;

                case Type.Stun:
                    SetStun(vo);
                    break;

                case Type.Immunity:
                    SetImmunity(vo);
                    break;

                case Type.Invincible:
                    SetInvincible(vo);
                    break;
            }
        }


        public void SetAttribute(EffectVO vo)
        {
            HeroProxy heroProxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            if (vo.target.GUID == heroProxy.Guid)
            {
                heroProxy.SetAttribute(vo.attribute, vo.value);

                //动画 特效
                return;
            }

            NpcProxy npcProxy = Facade.RetrieveProxy(NpcProxy.NAME) as NpcProxy;
            npcProxy.SetAttribute(vo.target.GUID, vo.attribute, vo.value);
            //动画 特效
        }


        public void SetStun(EffectVO vo)
        {
            //EntityManager.GetComponentData<MoveDirection>(vo.target).Value = new Unity.Mathematics.float3(0);

            //动画 特效
        }

        public void SetImmunity(EffectVO vo)
        {
            HeroProxy heroProxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            if (vo.target.GUID == heroProxy.Guid)
            {
                heroProxy.Immunity = vo.value > 0 ? true : false;

                //动画 特效
                return;
            }

            NpcProxy npcProxy = Facade.RetrieveProxy(NpcProxy.NAME) as NpcProxy;
            npcProxy.SetImmunity(vo.target.GUID, vo.value);
            //动画 特效
        }

        public void SetInvincible(EffectVO vo)
        {
            HeroProxy heroProxy = Facade.RetrieveProxy(HeroProxy.NAME) as HeroProxy;
            if (vo.target.GUID == heroProxy.Guid)
            {
                heroProxy.Invincible = vo.value > 0 ? true : false;

                //动画 特效
                return;
            }

            NpcProxy npcProxy = Facade.RetrieveProxy(NpcProxy.NAME) as NpcProxy;
            npcProxy.SetInvincible(vo.target.GUID, vo.value);
            //动画 特效
        }




    }
}
