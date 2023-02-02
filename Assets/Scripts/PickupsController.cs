using System.Collections.Generic;
using UnityEngine;

public class PickupsController : MonoBehaviour
{
    public List<GameObject> pickUps;

    private readonly List<Vector3> _pickUpsStartingPositions = new ();
    private readonly List<Quaternion> _pickUpsStartingRotations = new ();
    
    public void Reset()
    {
        var i = 0;
        
        foreach (var pickUp in pickUps)
        {
            pickUp.transform.position = _pickUpsStartingPositions[i];
            pickUp.transform.rotation = _pickUpsStartingRotations[i];
            i++;
        }
    }
    
    private void Start()
    {
        foreach (var pickUp in pickUps)
        {
            _pickUpsStartingPositions.Add(pickUp.transform.position);
            _pickUpsStartingRotations.Add(pickUp.transform.rotation);
        }
    }
}
