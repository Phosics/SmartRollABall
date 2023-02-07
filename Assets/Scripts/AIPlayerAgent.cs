using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIPlayerAgent : Agent
{
    public ScoreManager scoreManager;
    
    public float speed = 10f;
    public float jump = 35f;
    public WallsManager wallsManager;
    public Transform pickUpTransform;
    public Transform obstacleTransform;

    private Rigidbody _rb;
    private Vector3 _startingPosition;
    private float _distance;
    private bool _collideObstacle;

    public override void Initialize()
    {
        _rb = GetComponent<Rigidbody>();
        _startingPosition = transform.position;
    }
    
    public override void OnEpisodeBegin()
    {
        // Zero out velocities so that movement stops before a new episode begins
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        // Reset the starting position
        transform.position = _startingPosition;
        
        // Reset the score
        scoreManager.Reset();
        
        // Reset the pickup in a new random location
        ResetPickup();

        _distance = Vector3.Distance(transform.position, pickUpTransform.position);
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
            Vector3 moveY = new Vector3(0, 0, 0);

            if (transform.position.y - _startingPosition.y <= 0.1f &&  actions.DiscreteActions[0] == 1)
            {
                moveY.y += jump;
            }
            
            // Check for collision with the obstacle
            if (_collideObstacle)
            {
                moveY.y += jump * 2;
            }

            // Calculate movement vector
            Vector3 moveXZ = new Vector3(actions.ContinuousActions[0], moveY.y, actions.ContinuousActions[1]);
            
            // Add force in the direction of the move vector
            _rb.AddForce(moveXZ * speed);
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
            sensor.AddObservation(pickUpTransform.position.normalized);
            
            sensor.AddObservation(obstacleTransform.position.normalized);

            // 11 total observations
        }

        /// <summary>
        /// Called when the agent's collide enters a trigger collider
        /// </summary>
        /// <param name="other">The trigger collider</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickup"))
            {
                ResetPickup();

                if (scoreManager.Increase())
                {
                    AddReward(50);
                    EndEpisode();
                }

                AddReward(0.5f);
            }
            // else if (other.CompareTag("Boundary"))
            // {
            //     // Collided with the area boundary, give a negative reward and finish
            //     AddReward(-30f);
            //     EndEpisode();
            // } 
            else if (other.gameObject.CompareTag("Obstacle"))
            {
                _collideObstacle = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                _collideObstacle = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                // Collided with the wall, give a negative reward
                AddReward(-0.05f);
            }
            else if (collision.gameObject.CompareTag("Boundary"))
            {
                AddReward(-1f);
            }
        }
        
        private void FixedUpdate()
        {
            float newDistance = Vector3.Distance(transform.position, pickUpTransform.position);

            if (newDistance > _distance)
            {
                AddReward(-0.01f);
            }

            _distance = newDistance;
        }
        
        /// <summary>
        /// Called every frame
        /// </summary>
        private void Update()
        {
            Debug.DrawLine(transform.position, pickUpTransform.position, Color.red);
        }

        private void ResetPickup()
        {
            pickUpTransform.position = new Vector3(GetRandomX(), pickUpTransform.position.y, GetRandomZ());

        }

        private float GetRandomX()
        {
            return Random.Range(wallsManager.GetMinX(), wallsManager.GetMaxX());
        }
        
        private float GetRandomZ()
        {
            return Random.Range(wallsManager.GetMinZ(), wallsManager.GetMaxZ());
        }
}