
using System;

namespace Game 
{
    [Serializable]
    public class HaloData
    {
        public int id;

        public string  effect;//buff����ʾЧ��
        public BuffType type;
        public BuffTo to; //buff���õ�����

        public float value;//

        public float time;//����ʱ��
        public float interval;//�����buff�ļ��ʱ��
    }
}

