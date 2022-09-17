
using Unity.Mathematics;
using System;

namespace Mathematica
{
    [Serializable]
    public struct OBB : Bounds
    {
        public static readonly int EDGE = 12;
        public static readonly int FACE = 6;
        public static readonly int VERTEX = 8;
        public static readonly int NORMAL = 3;
        public static readonly int[] Triangles = new int[36] { 0, 1, 5, 0, 4, 5, 2, 3, 7, 2, 6, 7, 0, 3, 7, 0, 4, 7, 1, 2, 6, 1, 5, 6, 0, 1, 2, 0, 2, 3, 4, 5, 6, 4, 6, 7 };

        public float3 Center { get; private set; }
        //public float3 Size { get; private set; }
        public float3 BevelRadius { get; private set; }
        public quaternion Orientation { get; private set; }
        public AABB Bounds { get; private set; }
        public float3 Min { get; private set; }
        public float3 Max { get; private set; }

        public float3[] Points { get; private set; }//上面左前 左后 右后 右前 下面左前 左后 右后 右前 
        public float3[] PointNormals { get; private set; }//用于判断和点的位置关系
        public float3[] Normals { get; private set; }//x轴 左右法线，y轴上下法线，z轴前后法线}

        float3[] points;
        float3 center;

        public OBB(float3 min, float3 max, quaternion rotation)
        {
            points = new float3[VERTEX];
            Points = new float3[VERTEX];
            Normals = new float3[NORMAL];
            PointNormals = new float3[4];

            Min = min;
            Max = max;

            Center = (min + max) / 2;
            center = Center;
            BevelRadius = (max - min) / 2;
            Orientation = rotation;

            Points[0] = points[0] = new float3(Min.x, Max.y, Max.z);
            Points[1] = points[1] = new float3(Min.x, Max.y, Min.z);
            Points[2] = points[2] = new float3(Max.x, Max.y, Min.z);
            Points[3] = points[3] = Max;
            Points[4] = points[4] = new float3(Min.x, Min.y, Max.z);
            Points[5] = points[5] = Min;
            Points[6] = points[6] = new float3(Max.x, Min.y, Min.z);
            Points[7] = points[7] = new float3(Max.x, Min.y, Max.z);

            Normals[0] = math.mul(rotation, math.right());
            Normals[1] = math.mul(rotation ,math.up());
            Normals[2] = math.mul(rotation , math.forward());

            PointNormals[0] = Points[0];
            PointNormals[1] = Points[3];
            PointNormals[2] = Points[4];
            PointNormals[3] = Points[1];
            Bounds = new AABB(Min, Max);
        }

        public void Update(float3 center, float3 forward, float3 up)
        {
            Center = center;
            Orientation = quaternion.LookRotation(forward, up);

            Min =new float3(float.MaxValue, float.MaxValue, float.MaxValue);
            Max = new float3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < VERTEX; i++)
            {
                Points[i] =math.mul( Orientation , (points[i] - this.center) )+ center;
                Min = math.min(Min, Points[i]);
                Max = math.max(Max, Points[i]);
            }

            Normals[0] = math.mul(Orientation , math.right());
            Normals[1] = math.mul(Orientation , math.up());
            Normals[2] = math.mul(Orientation , math.forward());

            PointNormals[0] = Points[0];
            PointNormals[1] = Points[3];
            PointNormals[2] = Points[4];
            PointNormals[3] = Points[1];
            Bounds = new AABB(Min, Max);
        }

        public void Update(float3 center, quaternion rotation)
        {
            Center = center;
            Orientation = rotation;

            Min =new float3(float.MaxValue, float.MaxValue, float.MaxValue);
            Max = new float3(float.MinValue, float.MinValue, float.MinValue);
            for (int i = 0; i < VERTEX; i++)
            {
                Points[i] = math.mul(Orientation , (points[i] - this.center)) + center;
                Min = math.min(Min, Points[i]);
                Max = math.max(Max, Points[i]);
            }

            Normals[0] = math.mul(Orientation , math.right());
            Normals[1] = math.mul(Orientation , math.up());
            Normals[2] = math.mul(Orientation , math.forward());

            PointNormals[0] = Points[0];
            PointNormals[1] = Points[3];
            PointNormals[2] = Points[4];
            PointNormals[3] = Points[1];

            Bounds = new AABB(Min, Max);
        }
    }
}

