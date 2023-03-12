using Common.Effects;
using Common.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common.Player
{
    public class PlayerController : MonoBehaviour, IPlayerFunctions
    {
        public PlayerManager playerManager;

        private Rigidbody _rb;
        private float _movementX;
        private float _movementZ;
        private bool _isGrounded;
        private bool _isOnWall;
        private Vector3 _startingPosition;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _startingPosition = transform.position;
        }

        public void ResetPlayer()
        {
            transform.position = _startingPosition;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        public void OnMove(InputValue movementValue)
        {
            var movementVector = movementValue.Get<Vector2>();
            _movementX = movementVector.x;
            _movementZ = movementVector.y;
        }
    
        public void OnJump()
        {
            if (!_isGrounded && !_isOnWall) 
                return;
            
            AudioManager.Play(playerManager.jumpAudio, playerManager.playGround.IsInTrainingMode());

            var jump = playerManager.jump;

            if (_isOnWall)
            {
                jump /= 4;
            }

            _rb.AddForce(new Vector3(0, jump, 0) * playerManager.speed);
        }

        private void FixedUpdate()
        {
            _rb.AddForce(new Vector3(_movementX, 0, _movementZ) * playerManager.speed);
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
                AudioManager.Play(playerManager.enemyHitAudio, playerManager.playGround.IsInTrainingMode());
                _rb.AddForce(new Vector3(0, playerManager.enemyCollisionJumpForce, 0) * playerManager.speed);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("MovingWall"))
                _isOnWall = false;
            if (collision.gameObject.CompareTag("Ground"))
                _isGrounded = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PickUp") || other.gameObject.CompareTag("SpecialPickUp"))
            {
                playerManager.playGround.OnPickUp(other.gameObject.GetComponent<PickUp>());
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Play(playerManager.enemyHitAudio, playerManager.playGround.IsInTrainingMode());
                _rb.AddForce(new Vector3(0, playerManager.enemyCollisionJumpForce, 0) * playerManager.speed);
            }
            else if (other.gameObject.CompareTag("Boundary"))
            {
                playerManager.playGround.OnPlayerExitBoundary(); 
            }
        }
    }
}
