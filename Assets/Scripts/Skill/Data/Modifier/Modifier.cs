using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;
using ECS;

namespace Ability
{
    public enum OverrideAnimation
    {
        Default,//Ĭ��
        act_attack,// ����
        act_cast_ability,//ʩ��
        act_run,//����

    }
    public enum Attributes
    {
        None,

        MULTIPLE,//���ظ�
        PERMANENT,//��������
        IGNORE_INVULNERABLE//�޵б���
    }
    public enum Properitiy
    {
        Absolute_no_damage_magical,//ħ��������Ч
        Absolute_no_damage_physical,//��������Ч
        attack_range_bonus,//������Χ����

    }
    public enum State
    {
        attack_immune,//��������
        cannot_miss,//�޷�����
        magic_immune,//ħ��

    }
    public interface IModifier
    {
    }



    [Serializable]
    public class Modifier : IModifier
    {
        public Attributes Attributes;
        public float Duration;//����ʱ��
        public EventTarget Target;
        public string ModifierName;

        public bool Passive;//Ĭ��ӵ��
        public string TextureName;//ͼ��
        public bool IsBuff;//����Ч��
        public bool IsDebuff;//����Ч��
        public bool IsHidden;//����ͼ��
        public bool IsPurgable;//�ɾ���
        public bool AllowIllusionDuplicate;//����ɼ̳�
        public bool IsAura;//��Ϊ�⻷
        public bool Orb;//����
        public string EffectName;//��Ч��
        public EffectAttachType EffectAttachedType;//��Ч��λ��
        public string StatusEffectName;// ״̬��Ч
        public string StatusEffectPriority;// ״̬��Ч���ȼ�
        public OverrideAnimation OverrideAnimation;//���Ƕ���
        public List<Properitiy> Properities;//����
        public List<AbilityState> States;//״̬
        public List<AbilityEvent> Events;//�¼�

    }



}