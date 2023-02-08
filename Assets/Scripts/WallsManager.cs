using UnityEngine;

public class WallsManager : MonoBehaviour
{
    public GameObject southWall;
    public GameObject northWall;
    public GameObject eastWall;
    public GameObject westWall;

    public float GetMinX()
    {
        return GetValue(westWall.transform.position.x, false);
    }
    
    public float GetMaxX()
    {
        return GetValue(eastWall.transform.position.x, true);
    }
    
    public float GetMinZ()
    {
        return GetValue(southWall.transform.position.z, false);
    }
    
    public float GetMaxZ()
    {
        return GetValue(northWall.transform.position.z, true);
    }

    private float GetValue(float value, bool reverse)
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
