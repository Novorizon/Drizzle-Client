using ECS;
using System;

namespace Game
{
    [Serializable]
    public struct Attribute : IComponentData, IBuffData
    {
        public enum AttributeType
        {
            Attack,                                   // 
            Defence,                                      // 

            Speed,                                     // 
            Health,                                      // 
            Age,                                    // 

            Strength,                        // 
            Agility,                                  // 
            Intelligence,                          // 
        }

        public enum UpdateType
        {
            Add,
            Plus
        }
        public float value;
        public float Plus;
        public float Mul;

        public Attribute(float value)
        {
            this.value = value;
            Plus = 0;
            Mul = 0;
        }

        public float Value
        {
            get => (value + Plus) * (1 + Mul);
        }

        public void Update(float value, UpdateType type)
        {
            if (type == UpdateType.Add)
                Plus += value;
            else
                Mul += value;
        }

        public void AddBuff(BuffType type, float value)
        {
            if ((type & BuffType.Plus) > 0)
                Plus += value;
            else if ((type & BuffType.Mul) > 0)
                Mul += value;
        }

        public void UpdateBuff(BuffType type, float value, int sign)
        {
            if ((type & BuffType.Plus) > 0)
                Plus += value * sign;
            else if ((type & BuffType.Mul) > 0)
                Mul += value * sign;
        }
    }

    public interface IBuffData
    {
        void AddBuff(BuffType type, float value);
        void UpdateBuff(BuffType type, float value, int sign);

    }
    [Serializable]
    public struct Attack : IComponentData
    {
        public Attribute Value;
    }

    [Serializable]
    public struct Armor : IComponentData
    {
        public Attribute Value;
    }

    [Serializable]
    public struct AttackRange : IComponentData
    {
        public Attribute Value;
    }

    [Serializable]
    public struct MagicImmunity : IComponentData
    {
        public bool Value;
    }

    [Serializable]
    public struct Invincible : IComponentData
    {
        public bool Value;
    }

    [Serializable]
    public struct Visible : IComponentData
    {
        public Attribute Value;
    }

}
