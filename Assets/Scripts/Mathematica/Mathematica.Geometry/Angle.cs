using System;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Geometry
    {

        /// Determine the signed angle between two vectors, with normal 'n' as the rotation axis.
        /// 两个向量之间的夹角，有符号. 顺时针为正 [0,180] 逆时针为正(0,180)
        public static float AngleSigned(float3 from, float3 to, float3 n)
        {
            return math.atan2(math.dot(n, math.cross(from, to)), math.dot(from, to)) * mathEx.Rad2Deg;
        }

        /// Determine the signed angle between two vectors
        /// 两个向量之间的夹角，无符号 [0,180]
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


        //点point在OA、OB组成的夹角内外
        public static bool IsPointInAngle(float3 point, float3 O, float3 A, float3 B)
        {
            float3 p = point - O;
            float3 a = A - O;
            float3 b = B - O;
            float dot1 = math.dot(a, p);
            float dot2 = math.dot(b, p);

            return dot1 * dot2 <= 0;
        }

        //点在同一起点的射线组成的夹角内外
        public static bool IsPointInBetweenTheRays(float3 point, Ray ray1, Ray ray2)
        {
            float3 p = point - ray1.origin;
            float dot1 = math.dot(ray1.direction, p);
            float dot2 = math.dot(ray2.direction, p);

            return dot1 * dot2 <= 0;
        }
    }

}

