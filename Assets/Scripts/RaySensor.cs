using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySensor : MonoBehaviour
{
    private Quaternion _startingRotation;
    private Vector3 _offset;
    
    // Start is called before the first frame update
    public GameObject player;
    //public GameObject cube;

    private void Awake()
    {
        _startingRotation = new Quaternion(transform.rotation.x, transform.rotation.y,
            transform.rotation.z, transform.rotation.w);

        _offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.SetPositionAndRotation(player.transform.position + _offset, _startingRotation);

        //transform.rotation = cube.transform.rotation;
        // transform.position = player.transform.position;
        // transform.rotation = new Quaternion(cube.transform.rotation.x, player.transform.rotation.y, cube.transform.rotation.z, cube.transform.rotation.w);

        //transform.position = player.transform.position;
    }
}
