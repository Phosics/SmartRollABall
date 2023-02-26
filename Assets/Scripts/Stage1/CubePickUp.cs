using Common;
using UnityEngine;

namespace Stage1
{
    public class CubePickUp : PickUp
    {
        [Space(5)]
        [Header("Starting Location")] 
        public float startXFromWestWall;
        public float startZFromSouthWall;

        public void SetStartLocation(WallsManager wallsManager)
        {
            SetLocation(new Vector3(wallsManager.GetMinX(true) + startXFromWestWall, DefaultHeight, 
                wallsManager.GetMinZ(true) + startZFromSouthWall));
        }
    }
}