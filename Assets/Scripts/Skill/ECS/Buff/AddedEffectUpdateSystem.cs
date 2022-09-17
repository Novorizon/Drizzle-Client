//using ECS;
//using System.Collections.Generic;

//namespace Game
//{
//    //[UpdateInGroup(typeof(MainThreadSystemGroup))]
//    public class AddedEffectUpdateSystem : SystemBaseFacade
//    {
//        protected override void OnUpdate()
//        {

//            Entities
//                .ForEach((ref AddedEffect addedEffect) =>
//                {
//                    if (addedEffect.effects.Count() > 0)
//                    {
//                        var e = addedEffect.effects.GetEnumerator();
//                        while (e.MoveNext())
//                        {
//                            var current = e.Current;
//                            if (current.Value != Entity.Null)
//                            {
//                                if (!EntityManager.HasComponent<Span>(current.Value))
//                                {
//                                    addedEffect.effects.Remove(current.Key);
//                                }
//                            }
//                            else
//                            {
//                                addedEffect.effects.Remove(current.Key);
//                            }
//                        }
//                    }
//                })
//                .WithStructuralChanges()
//                .WithoutBurst()
//                .Run();
//        }
//    }
//}
