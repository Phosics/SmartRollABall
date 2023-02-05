using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    public S02ObjectivePool s02ObjectivePool;
    public S02SpiderGenerator s02SpidersPool;
    
    private Rigidbody _rb;

    private Vector3 CurrentPosition => _rb.transform.position;
    private Vector3 CurrentVelocity => _rb.velocity;

    private S02Rotator _closestObjective;
    private Vector3 DirectionToClosestObjective => _closestObjective.transform.position - CurrentPosition;
    
    private S02SpiderController _closestS02Spider;
    private Vector3 DirectionToClosestSpider => _closestS02Spider.transform.position - CurrentPosition;
    
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

        s02SpidersPool.ResetSpiders();
        UpdateNearestObjectives();
    }

    /// <summary>
    /// 0: move x
    /// 1: move z
    /// 2: move y (should have it?)
    /// </summary>
    /// <param name="actions"></param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector3 move = new Vector3(actions.ContinuousActions[0], 0 /* actions.ContinuousActions[2]*/,
            actions.ContinuousActions[1]);
        _rb.AddForce(move * moveForce);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (_closestS02Spider == null || _closestObjective == null)
        {
            sensor.AddObservation(new float[12]);
            return;
        }
        sensor.AddObservation(CurrentPosition); // 3
        sensor.AddObservation(CurrentVelocity); // 3
        sensor.AddObservation(DirectionToClosestObjective); // 3
        sensor.AddObservation(DirectionToClosestSpider); // 3
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        float moveZ = 0;
        if (Input.GetKey(KeyCode.UpArrow)) moveZ = 1;
        else if (Input.GetKey(KeyCode.DownArrow)) moveZ = -1;
        
        float moveX = 0;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1;
        else if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1;

        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = moveX;
        continuousActionsOut[1] = moveZ;
    }
    
    private void FixedUpdate()
    {
        if(_closestObjective != null)
            Debug.DrawLine(_closestObjective.transform.position, transform.position, Color.green);
        if(_closestS02Spider != null)
            Debug.DrawLine(_closestS02Spider.transform.position, transform.position, Color.red);
        
        if (transform.position.y < 0.1 && transform.position.y > 2)
        {
            AddReward(-5);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") || other.gameObject.CompareTag("SpecialPickUp"))
        {
            AddReward(1);
            UpdateNearestObjectives();
        }

        if (other.gameObject.CompareTag("Spider"))
        {
            AddReward(-0.8f);
            UpdateNearestObjectives();
        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            // for when a spider hits you to the air, we want to recalculate nearest objectives
            UpdateNearestObjectives();
            
        }
        if (collision.collider.CompareTag("Wall"))
        {
            AddReward(-1);
        }
    }

    private void MoveToSafeRandomPosition()
    {
        bool foundSafePosition = false;
        int attemptsRemaining = 100;

        Vector3 potentialPosition = Vector3.zero;
        while (!foundSafePosition && attemptsRemaining > 0)
        {
            attemptsRemaining--;
            potentialPosition = RandomPointInField();
            Collider[] colliders = Physics.OverlapSphere(potentialPosition, 0.05f);
            foundSafePosition = colliders.Length == 0;
        }
        
        Debug.Assert(foundSafePosition, "could not fins safe position to spawn");

        _rb.transform.localPosition = potentialPosition;
    }

    private void UpdateNearestObjectives()
    {
        S02Rotator closestO = null;
        foreach (S02Rotator objective in s02ObjectivePool.Objectives)
        {
            if (closestO == null ||
                Vector3.Distance(objective.transform.position, CurrentPosition)
                < Vector3.Distance(closestO.transform.position, CurrentPosition))
                closestO = objective;
        }

        _closestObjective = closestO;

        S02SpiderController closestS = null;
        foreach (S02SpiderController spider in s02SpidersPool.Spiders)
        {
            if (closestS == null ||
                Vector3.Distance(spider.transform.position, CurrentPosition)
                < Vector3.Distance(closestS.transform.position, CurrentPosition))
                closestS = spider;
        }

        _closestS02Spider = closestS;
    }
    
    private Vector3 RandomPointInField() =>
        new Vector3((float)_r.NextDouble() * 19 - 9.5f, 0.5f, (float)_r.NextDouble() * 19 - 9.5f);
}
