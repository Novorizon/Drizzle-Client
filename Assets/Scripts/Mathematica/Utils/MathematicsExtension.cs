using System;
using Unity.Mathematics;

namespace Unity.Mathematics
{
    public static partial class mathEx
    {
        public const float Deg2Rad = (float)Math.PI / 180f;

        public const float Rad2Deg = 57.29578f;
        public static float cross(float2 x, float2 y) { return x.x * y.y - x.y * y.x; }


        //public static quaternion in
    }
}
