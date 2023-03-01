using Common.Effects;
using Common.Objects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Common.Player
{
    public class PlayerController : MonoBehaviour, IPlayerFunctions
    {
        public PlayerManager playerManager;

        private Rigidbody _rb;
        private float _movementX;
        private float _movementZ;
        private bool _isGrounded;
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

        private void OnMove(InputValue movementValue)
        {
            var movementVector = movementValue.Get<Vector2>();
            _movementX = movementVector.x;
            _movementZ = movementVector.y;
        }
    
        private void OnJump()
        {
            if (!_isGrounded) 
                return;

            AudioManager.Play(playerManager.jumpAudio);
            _rb.AddForce(new Vector3(0, playerManager.jump, 0) * playerManager.speed);
        }

        private void FixedUpdate()
        {
            _rb.AddForce(new Vector3(_movementX, 0, _movementZ) * playerManager.speed);
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;   
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Play(playerManager.enemyHitAudio);
                _rb.AddForce(new Vector3(0, playerManager.enemyCollisionJumpForce, 0) * playerManager.speed);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
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
                AudioManager.Play(playerManager.enemyHitAudio);
                _rb.AddForce(new Vector3(0, playerManager.enemyCollisionJumpForce, 0) * playerManager.speed);
            }
            else if (other.gameObject.CompareTag("Boundary"))
            {
                playerManager.playGround.OnPlayerExitBoundary(); 
            }
        }
    }
}
