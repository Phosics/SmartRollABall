using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Common
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Attributes")]
        public float speed;
        public float playerJumpForce;
        public float enemyCollisionJumpForce;
    
        [Space(5)]
        [Header("Player Audios")]
        public string jumpAudio;
        public string pickUpAudio;
        public string enemyHitAudio;

        [Space(5)]
        [Header("Other Objects")]
        public PlayGround playGround;

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

            AudioManager.Play(jumpAudio);
            _rb.AddForce(new Vector3(0, playerJumpForce, 0) * speed);
        }

        private void FixedUpdate()
        {
            _rb.AddForce(new Vector3(_movementX, 0, _movementZ) * speed);
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;   
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Play(enemyHitAudio);
                _rb.AddForce(new Vector3(0, enemyCollisionJumpForce, 0) * speed);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
                _isGrounded = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                AudioManager.Play(pickUpAudio);
                playGround.OnPickUp(other.gameObject.GetComponent<PickUp>());
            }
            else if (other.gameObject.CompareTag("SpecialPickUp"))
            {
                AudioManager.Play(pickUpAudio);
                playGround.OnSpecialPickUp(other.gameObject.GetComponent<PickUp>());
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Play(enemyHitAudio);
                _rb.AddForce(new Vector3(0, enemyCollisionJumpForce, 0) * speed);
            }
            else if (other.gameObject.CompareTag("Boundary"))
            {
                playGround.OnPlayerExitBoundary(); 
            }
        }
    }
}
