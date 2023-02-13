using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = System.Random;

/*features:
1. distances from the objectives
2. distances from the spiders
3. position in the board
4. direction to nearest objective
5. current velocity vector

rewards:
1. -0.01 every time delta
2. -1 for every crash into spider
3. -100 for touching the border
4. +1 for every objective taken*/

public class S02PlayerAgent : Agent
{
    public float moveForce = 5f;
    
    public PlayGround playGround;
    
    private Rigidbody _rb;

    private Vector3 CurrentPosition => _rb.transform.position;
    private Vector3 CurrentVelocity => _rb.velocity;

    private PickUp _closestPickUp;
    private Vector3 DirectionToClosestPickUp => _closestPickUp.transform.position - CurrentPosition;
    
    private Enemy _closestEnemy;
    private Vector3 DirectionToClosestEnemy => _closestEnemy.transform.position - CurrentPosition;
    
    private readonly Random _r = new Random();
    
    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        MoveToSafeRandomPosition();

        playGround.ResetPlayGround();
        UpdateNearestPickUp();
    }

    /// <summary>
    /// 0: move x
    /// 1: move z
    /// 2: move y (should have it?)
    /// </summary>
    /// <param name="actions"></param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        /*Vector3 move = new Vector3(actions.ContinuousActions[0], 0 /* actions.ContinuousActions[2]
           actions.ContinuousActions[1]);
        _rb.AddForce(move * moveForce);*/
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        /*if (_closestEnemy == null || _closestPickUp == null)
        {
            sensor.AddObservation(new float[12]);
            return;
        }
        sensor.AddObservation(CurrentPosition); // 3
        sensor.AddObservation(CurrentVelocity); // 3
        sensor.AddObservation(DirectionToClosestPickUp); // 3
        sensor.AddObservation(DirectionToClosestEnemy); // 3*/
    }

    private void FixedUpdate()
    {
        /*if(_closestPickUp != null)
            Debug.DrawLine(_closestPickUp.transform.position, transform.position, Color.green);
        if(_closestEnemy != null)
            Debug.DrawLine(_closestEnemy.transform.position, transform.position, Color.red);
        
        if (transform.position.y < 0.1 && transform.position.y > 2)
        {
            AddReward(-5);
        }*/
    }
    
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("PickUp") || other.gameObject.CompareTag("SpecialPickUp"))
        {
            AddReward(1);
            UpdateNearestPickUp();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            AddReward(-0.8f);
            UpdateNearestPickUp();
        }*/
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.collider.CompareTag("Ground"))
        {
            // for when a spider hits you to the air, we want to recalculate nearest objectives
            UpdateNearestPickUp();
            
        }
        if (collision.collider.CompareTag("Wall"))
        {
            AddReward(-1);
        }*/
    }

    private void MoveToSafeRandomPosition()
    {
        bool foundSafePosition = false;
        int attemptsRemaining = 100;

        Vector3 potentialPosition = Vector3.zero;
        while (!foundSafePosition && attemptsRemaining > 0)
        {
            attemptsRemaining--;
            potentialPosition = playGround.wallsManager.RandomLocation();
            var colliders = Physics.OverlapSphere(potentialPosition, 0.05f);
            foundSafePosition = colliders.Length == 0;
        }
        
        Debug.Assert(foundSafePosition, "could not find safe position to spawn");

        _rb.transform.localPosition = potentialPosition;
    }

    private void UpdateNearestPickUp()
    {
        PickUp closestO = null;
        foreach (var objective in playGround.PickUps)
        {
            if (closestO == null ||
                Vector3.Distance(objective.transform.position, CurrentPosition)
                < Vector3.Distance(closestO.transform.position, CurrentPosition))
                closestO = objective;
        }

        _closestPickUp = closestO;

        Enemy closestS = null;
        foreach (var enemy in playGround.Enemies)
        {
            if (closestS == null ||
                Vector3.Distance(enemy.transform.position, CurrentPosition)
                < Vector3.Distance(closestS.transform.position, CurrentPosition))
                closestS = enemy;
        }

        _closestEnemy = closestS;
    }
}
