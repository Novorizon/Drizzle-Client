using System;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Geometry
    {

        /// Determine the signed angle between two vectors, with normal 'n' as the rotation axis.
        /// ��������֮��ļнǣ��з���. ˳ʱ��Ϊ�� [0,180] ��ʱ��Ϊ��(0,180)
        public static float AngleSigned(float3 from, float3 to, float3 n)
        {
            return math.atan2(math.dot(n, math.cross(from, to)), math.dot(from, to)) * mathEx.Rad2Deg;
        }

        /// Determine the signed angle between two vectors
        /// ��������֮��ļнǣ��޷��� [0,180]
        public static float kEpsilon =0.00001f;
        public static float kEpsilonNormalSqrt = 1e-15F;
        public static float Angle(float3 from, float3 to)
        {
            float denominator = math.sqrt(math.lengthsq(from) * math.lengthsq(to));
            if (denominator < kEpsilonNormalSqrt)
                return 0F;

            float dot = math.clamp(math.dot(from, to) / denominator, -1, 1);
            //float dot = math.dot(v1, v2);
            //if (dot == 0)
            //    return 0;
            return math.acos(dot) * mathEx.Rad2Deg;
        }


        //��point��OA��OB��ɵļн�����
        public static bool IsPointInAngle(float3 point, float3 O, float3 A, float3 B)
        {
            float3 p = point - O;
            float3 a = A - O;
            float3 b = B - O;
            float dot1 = math.dot(a, p);
            float dot2 = math.dot(b, p);

            return dot1 * dot2 <= 0;
        }

        //����ͬһ����������ɵļн�����
        public static bool IsPointInBetweenTheRays(float3 point, Ray ray1, Ray ray2)
        {
            float3 p = point - ray1.origin;
            float dot1 = math.dot(ray1.direction, p);
            float dot2 = math.dot(ray2.direction, p);

            return dot1 * dot2 <= 0;
        }
    }

}

