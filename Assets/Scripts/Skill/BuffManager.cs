//using UnityEngine;
//using MVC;
//using System.Collections.Generic;
//using Game;

//namespace Ability
//{
//    //[UpdateInGroup]
//    public class BuffManager : SingletonBehaviour<BuffManager>
//    {

//        List<BuffVO> buffs = new List<BuffVO>();


//        public void Play(AbilityData data, GameObject target = null)
//        {
//            if (data == null)
//                return;

//            if (data.targetType > TargetType.None && target == null)
//                return;



//        }

//        private void Update()
//        {
//            for (int i = 0; i < buffs.Count; i++)
//            {
//                BuffVO vo = buffs[i];

//                float dt = Time.deltaTime;
//                if (vo.id == 0)
//                {
//                    buffs.RemoveAt(i);//TODO
//                    return;
//                }



//                if (vo.timer >= vo.duration)
//                {
//                    buffs.RemoveAt(i);//TODO
//                    return;
//                }


//                vo.intervalTimer += dt;

//                if (vo.intervalTimer >= vo.interval)
//                {
//                    vo.intervalTimer = 0;

//                    EffectProxy effectProxy = Facade.RetrieveProxy(EffectProxy.NAME) as EffectProxy;

//                    EffectVO effect = new EffectVO(vo.effect);
//                    effect.target = vo.target;
//                    effectProxy.Play(effect);
//                }



//            }
//        }
//    }
//}

