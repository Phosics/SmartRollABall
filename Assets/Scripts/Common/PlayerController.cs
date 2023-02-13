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

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
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
                _isGrounded = true;
            
            if (collision.gameObject.CompareTag("Boundary"))
                playGround.OnPlayerExitBoundary();
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

            if (other.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Play(enemyHitAudio);
                _rb.AddForce(new Vector3(0, enemyCollisionJumpForce, 0) * speed);
            }
        }
    }
}
