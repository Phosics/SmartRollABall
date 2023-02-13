
using UnityEngine;

public class CoinPickUp : Common.PickUp
{
    [Space(5)]
    [Header("Starting Location")] 
    public float startXFromWestWall;
    public float startZFromSouthWall;
    
    public void SetStartLocation(Common.WallsManager wallsManager)
    {
        SetLocation(new Vector3(wallsManager.GetMinX(true) + startXFromWestWall, DefaultHeight, 
            wallsManager.GetMinZ(true) + startZFromSouthWall));
    }
}