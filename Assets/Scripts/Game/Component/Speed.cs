using ECS;
using System;

namespace Game
{
    [Serializable]
    public class Speed : IComponentData
    {
        public float Value;
        //public Attribute Value;
        //public void AddBuff(BuffEffect type, float value)
        //{
        //    if (type == BuffEffect.Plus)
        //        Value.valuePlus += value;
        //    else if (type == BuffEffect.Multi)
        //        Value.valueMul += value;
        //}

        //public void RemoveBuff(BuffEffect type, float value)
        //{
        //    if (type == BuffEffect.Plus)
        //        Value.valuePlus -= value;
        //    else if (type == BuffEffect.Multi)
        //        Value.valueMul -= value;
        //}
    }
}
