using Common.Objects;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Common.Player
{
    public class AIPlayerAgent : Agent, IPlayerFunctions
    {
        [FormerlySerializedAs("playerParameters")]
        public PlayerManager playerManager;

        private Rigidbody _rb;
        private Vector3 _startingPosition;
        private bool _isGrounded;
        private bool _isOnWall;

        private PickUp _closestPickUp;

        private Transform _closestPickUpTransform;

        private bool _isClosestPickUpTransformInitialized;

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

            // Reset the pickup in a new random location
            if (playerManager.playGround.IsInTrainingMode())
            {
                playerManager.playGround.ResetPlayGround();
            }

            // Get the closest PickUp
            FindClosestPickUp();
        }

        public void ResetPlayer()
        {
            if (SceneSettings.useAI || Random.Range(0f, 1f) < 0.0f)
            {
                transform.position = _startingPosition;
            }
            else
            {
                transform.position = playerManager.playGround.FindSafeLocationForPlayer();
            }
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
                moveY += playerManager.jump;
                _isGrounded = false;
            }
            else if (_isOnWall && actions.ContinuousActions[2] > 0.5)
            {
                moveY += playerManager.jump / 4;
                _isOnWall = false;
            }

            // Calculate movement vector
            Vector3 moveXZ = new Vector3(actions.ContinuousActions[0], moveY, actions.ContinuousActions[1]);

            // Add force in the direction of the move vector
            _rb.AddForce(moveXZ * playerManager.speed);

            AddReward(-0.015f);
        }

        /// <summary>
        /// Collect vector observations from the environment
        /// </summary>
        /// <param name="sensor">The vector sensor</param>
        public override void CollectObservations(VectorSensor sensor)
        {
            // Observe the agent position - 3 observations
            sensor.AddObservation(transform.position.normalized);

            // Observe the agent velocity - 6 observations
            sensor.AddObservation(_rb.angularVelocity.normalized);
            sensor.AddObservation(_rb.velocity.normalized);

            // Observe a normalized vector pointing to the nearest pickup - 3 observations
            if (_isClosestPickUpTransformInitialized)
            {
                sensor.AddObservation(_closestPickUpTransform.position.normalized);
            }
            else
            {
                sensor.AddObservation(Vector2.zero);
            }
            // 12 total observations
        }

        /// <summary>
        /// Called when the agent's collide enters a trigger collider
        /// </summary>
        /// <param name="other">The trigger collider</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PickUp") || other.CompareTag("SpecialPickUp"))
            {
                if (playerManager.playGround.OnPickUp(other.gameObject.GetComponent<PickUp>()))
                {
                    AddReward(30f);
                    EndEpisode();
                    
                    if (!SceneSettings.useAI)
                    {
                        playerManager.playGround.OnPlayerExitBoundary();
                    }
                }

                if (other.gameObject.transform == _closestPickUpTransform)
                    AddReward(1f);
                else
                    AddReward(1f);

                FindClosestPickUp();
            }
            else if (other.CompareTag("Boundary"))
            {
                AddReward(-10f);
                _isClosestPickUpTransformInitialized = false;
                EndEpisode();
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                _rb.AddForce(new Vector3(0, playerManager.enemyCollisionJumpForce, 0) * playerManager.speed);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Wall") || collision.collider.CompareTag("MovingWall"))
            {
                _isOnWall = true;
            }
            else if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;   
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                _rb.AddForce(new Vector3(0, playerManager.enemyCollisionJumpForce, 0) * playerManager.speed);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("MovingWall"))
                _isOnWall = false;
            else if (other.gameObject.CompareTag("Ground"))
                _isGrounded = false;
        }

        private void FixedUpdate()
        {
            if (transform.position.y >= -3)
                return;
            
            _isClosestPickUpTransformInitialized = false;
            EndEpisode();
        }

        private void Update()
        {
            if (_isClosestPickUpTransformInitialized)
            {
                Debug.DrawLine(transform.position, _closestPickUpTransform.position, Color.red);
            }
        }

        private void FindClosestPickUp()
        {
            var position = transform.position;
            _closestPickUp = playerManager.playGround.FindClosestPickUp(position);

            if (_closestPickUp == null)
            {
                return;
            }

            _closestPickUpTransform = _closestPickUp.transform;
            _isClosestPickUpTransformInitialized = true;
        }
    }
}