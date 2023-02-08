using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float speed = 1;
    public bool isMoving = false;
    private float defaultHeight = 0.5f;

    public void SetStartLocation(Vector3 newLocation)
    {
        newLocation.y = isMoving ? Random.Range(1.5f, 2.5f) : defaultHeight;
        transform.position = newLocation;
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        var y = isMoving ? Mathf.PingPong((transform.position.y + Time.time) * speed, 1) * 2 - 1: transform.position.y;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
