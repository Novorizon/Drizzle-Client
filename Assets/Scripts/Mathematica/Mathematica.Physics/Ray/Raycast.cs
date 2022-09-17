
using Unity.Mathematics;
using Unity.Mathematics;

namespace Mathematica
{
    public static partial class Physics
    {
        public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
        {
            hitInfo = new RaycastHit();
            float dis = 0;
            while (dis < maxDistance)
            {
                //hitInfo.point = geometry.IsOverlap(new AABB(), ray.origin);
                //if (!hitInfo.point .Equals( new float3(float.MinValue, float.MinValue, float.MinValue)))
                //{
                //    return true;
                //}
                dis += 0.1f;
                ray.origin += dis * ray.direction;
            }
            return false;
        }

        public static bool Raycast(float3 origin, float3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
        {
            hitInfo = new RaycastHit();
            float dis = 0;
            while (dis < maxDistance)
            {
                //hitInfo.point = geometry.IsOverlap(new Cuboid(), origin);
                //if (hitInfo.point != new float3(float.MinValue, float.MinValue, float.MinValue))
                //{
                //    return true;
                //}
                dis +=0.1f;
                origin += dis * direction;
            }
            return false;
        }
    }
}