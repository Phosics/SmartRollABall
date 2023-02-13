using UnityEngine;

namespace Common
{
    public class WallsManager : MonoBehaviour
    {
        public GameObject southWall;
        public GameObject northWall;
        public GameObject eastWall;
        public GameObject westWall;

        public float GetMinX(bool full = false)
        {
            var min = westWall.transform.position.x;
        
            return !full ? GetValue(min, false) : min;
        }
    
        public float GetMaxX(bool full = false)
        {
            var max = eastWall.transform.position.x;
        
            return !full ? GetValue(max, true) : max;
        }
    
        public float GetMinZ(bool full = false)
        {
            var min = southWall.transform.position.z;
        
            return !full ? GetValue(min, false) : min;
        }
    
        public float GetMaxZ(bool full = false)
        {
            var max = northWall.transform.position.z;
        
            return !full ? GetValue(max, true) : max;
        }

        private static float GetValue(float value, bool reverse)
        {
            if (reverse)
            {
                return value - 1;
            }
        
            return value + 1;
        }

        public Vector3 RandomLocation(float height = 0)
        {
            return new Vector3(Random.Range(GetMinX(), GetMaxX()), height, Random.Range(GetMinZ(), GetMaxZ()));
        }
    }
}
