using System;
using Common;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIPlayerAgent : Agent
{
    [Header("Player Attributes")]
    public float speed = 10f;
    public float jump = 20f;
    public float enemyCollisionJumpForce = 40f;
    
    [Space(5)]
    [Header("Other Objects")]
    public PlayGround playGround;

    private Rigidbody _rb;
    private Vector3 _startingPosition;
    private bool _isGrounded;
    
    private PickUp _closestPickUp;
    private Transform _closestPickUpTransform;
    // private float _distance;
    private float _minDistance;
    private float _maxDistance;
    private bool _isClosestPickUpTransformInitialized;
    //private bool _collideObstacle;
    
    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _startingPosition = transform.position;
        _isClosestPickUpTransformInitialized = false;
    }
    
    public override void OnEpisodeBegin()
    {
        // Zero out velocities so that movement stops before a new episode begins
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        // Reset the starting position
        if (Random.Range(0f, 1f) < 0.5f)
            transform.position = _startingPosition;
        else
            transform.position = playGround.wallsManager.RandomLocation();

        // Reset the pickup in a new random location
        playGround.ResetPlayGround();
        
        // Get the closest PickUp
        FindClosestPickUp();
    }

    /// <summary>
    /// Called when an action is received from either the player input or the neural network.
    ///
    /// actions[i] represents:
    /// Index 0: move vector x (+1 = right,     -1 = left)
    /// Index 2: move vector z (+1 = forward,   -1 = backward)
    /// Index 3: should jump (1 or 0)
    /// </summary>
    /// <param name="actions">The actions to take</param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveY = 0;

        if (_isGrounded && actions.ContinuousActions[2] > 0.5)
        {
            moveY += jump;
            AddReward(-0.01f);
        }
            
        // Check for collision with the obstacle
        // if (_collideObstacle)
        // {
        //     moveY.y += jump * 2;
        // }

        // Calculate movement vector
        Vector3 moveXZ = new Vector3(actions.ContinuousActions[0], moveY, actions.ContinuousActions[1]);
        
        // Add force in the direction of the move vector
        _rb.AddForce(moveXZ * speed);

        AddReward(-0.001f);
    }
        
    /// <summary>
    /// Collect vector observations from the environment
    /// </summary>
    /// <param name="sensor">The vector sensor</param>
    public override void CollectObservations(VectorSensor sensor)
    {
        // Observe the agent position - 3 observations
        sensor.AddObservation(transform.position.normalized);
        
        // Observe the agent velocity - 2 observations
        sensor.AddObservation(_rb.angularVelocity.normalized);
        sensor.AddObservation(_rb.velocity.normalized);

        // Observe a normalized vector pointing to the nearest pickup - 3 observations
        sensor.AddObservation(_closestPickUpTransform.position.normalized);

        // 8 total observations
    }

    /// <summary>
    /// Called when the agent's collide enters a trigger collider
    /// </summary>
    /// <param name="other">The trigger collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            if (playGround.OnPickUp(other.gameObject.GetComponent<PickUp>()))
            {
                AddReward(10f);
                Debug.Log("Win!");
                EndEpisode();
            }

            if (other.gameObject.transform == _closestPickUpTransform)
            {
                Debug.Log("Right pickup");
                AddReward(1f);
            }
            else
            {
                Debug.Log("Wrong pickup");
                AddReward(0.1f);
            }
            FindClosestPickUp();
            Debug.Log("Total Picked");
        }
        else if (other.CompareTag("Boundary"))
        {
            AddReward(-10f);
            Debug.Log("Loss!");
            _isClosestPickUpTransformInitialized = false;
            EndEpisode();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            _rb.AddForce(new Vector3(0, enemyCollisionJumpForce, 0) * speed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //_collideObstacle = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            // Collided with the wall, give a negative reward
            // AddReward(-0.01f);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            _rb.AddForce(new Vector3(0, enemyCollisionJumpForce, 0) * speed);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    private float distanceIgnoeY(Vector3 pos1, Vector3 pos2)
    {
        var newDistance = pos1 - pos2;
        newDistance.y = 0;
        return newDistance.magnitude;
    }

    private void FixedUpdate()
    {
        if (!_isClosestPickUpTransformInitialized)
        {
            return;
        }

        // var newDistance = Vector3.Distance(transform.position, _closestPickUpTransform.position);
        var distanceToTarget = distanceIgnoeY(transform.position, _closestPickUpTransform.position);
        if (distanceToTarget < _minDistance)
        {
            AddReward(0.01f * (1 + Mathf.Pow(_minDistance - distanceToTarget, 2)));
            _minDistance = distanceToTarget;

        }
        else if (distanceToTarget > _maxDistance)
        {
            AddReward(-0.01f * (1 + Mathf.Pow(distanceToTarget - _maxDistance, 2)));
            _maxDistance = distanceToTarget;
        }
        else
        {
            _minDistance = 0.9f * _minDistance + 0.1f * distanceToTarget;
            _maxDistance = 0.9f * _maxDistance + 0.1f * distanceToTarget;
        }

        //_distance = distanceToTarget;
    }

    /// <summary>
    /// Called every frame
    /// </summary>
    private void Update()
    {
        Debug.DrawLine(transform.position, _closestPickUpTransform.position, Color.red);
    }
    
    private void FindClosestPickUp()
    {
        var position = transform.position;
        _closestPickUp = playGround.FindClosestPickUp(position);
        _closestPickUpTransform = _closestPickUp.transform;
        //_distance = Vector3.Distance(position, _closestPickUpTransform.position);
        _minDistance = _maxDistance = distanceIgnoeY(position, _closestPickUpTransform.position);
        _isClosestPickUpTransformInitialized = true;
    }
}