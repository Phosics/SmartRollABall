using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySensor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject cube;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = cube.transform.rotation;
        transform.position = player.transform.position;
        transform.rotation = new Quaternion(cube.transform.rotation.x, player.transform.rotation.y, cube.transform.rotation.z, cube.transform.rotation.w);

        //transform.position = player.transform.position;
    }
}
