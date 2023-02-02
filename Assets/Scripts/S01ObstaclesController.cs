using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class S01ObstaclesController : MonoBehaviour
{
    public List<GameObject> obstacles;
    
    private readonly List<Vector3> _obstaclesStartingPositions = new ();

    public void Reset()
    {
        var i = 0;
        
        foreach (var obstacle in obstacles)
        {
            obstacle.transform.position = _obstaclesStartingPositions[i];
            obstacle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obstacle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            i++;
        }
    }
    
    private void Start()
    {
        foreach (var obstacle in obstacles)
        {
            _obstaclesStartingPositions.Add(obstacle.transform.position);
        }
    }
}
