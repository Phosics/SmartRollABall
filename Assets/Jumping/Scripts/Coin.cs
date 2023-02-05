using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class Coin: MonoBehaviour
{

    public float speed = 1;
    public GameObject playGround;

    private float hight;
    private Vector3 startLocation;
    public bool goodCoin;

    private void Start()
    {
        SetStartLocation();
        hight = Random.Range(1.5f, 2.5f);
    }

    private void SetStartLocation()
    {
        startLocation = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(0f, 2f), Random.Range(-9.5f, 9.5f));
        transform.position = playGround.transform.position + startLocation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        float y = Mathf.PingPong((startLocation.y + Time.time) * speed, 1) * 2 - 1;
        float posX = transform.position.x;
        float posZ = transform.position.z;
        transform.position = new Vector3(posX, hight + y, posZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetStartLocation();
        }
    }
}
