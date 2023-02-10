using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jumping : Enemy
{
    public UnityEvent miniJumping;
    private Vector3 startLocation;
    private Vector2 move;

    private void Start()
    {
        startLocation = Vector3.zero;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + move.x, 0.025f, transform.position.z + move.y);
    }

    public override void SetStartLocation(Vector3 position)
    {
        startLocation = new Vector3(Random.Range(-9f, 9f), 0.025f, Random.Range(-9f, 9f));
        transform.position = position + startLocation;
        SetMove();
    }

    private void SetMove()
    {
        move = new Vector2(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (miniJumping != null)
            {
                miniJumping.Invoke();
            }
            SetMove();
        }        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetMove();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            SetMove();
        }
    }
}
