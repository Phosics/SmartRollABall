using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float speed = 1;
    public bool isMoving = False;
    

    private float hight = 0.5;
    private Vector3 startLocation;

    public bool goodCoin;

    [HideInInspector]
    public PlayGround playGround;
    

    private void Start()
    {
        //SetStartLocation();
        startLocation = Vector3.zero;
        if (isMoving) {
            hight = Random.Range(1.5f, 2.5f);
        }
    }

    public void SetStartLocation(Vector3 startLocation)
    {
        startLocation.y = hight;
        transform.position = startLocation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        float y = isMoving ? Mathf.PingPong((startLocation.y + Time.time) * speed, 1) * 2 - 1: hight;
        float posX = transform.position.x;
        float posZ = transform.position.z;
        transform.position = new Vector3(posX, hight + y, posZ);
    }

    private void OnTriggerEnter(Collider other) // move to player
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playGround.OnCoinCollected(this, goodCoin);
        }
    }
}
