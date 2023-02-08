using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed;
    public float playerJumpForce;
    public float enemyCollisionJumpForce;
    private float _movementX;
    private float _movementZ;
    private bool _isGrounded = false;

    public string jumpAudio;
    public string pickUpAudio;
    public string enemyHitAudio;

    public PlayGround playGround;

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
            playGround.OnPickUp(other.gameObject.GetComponent<PickUp>());
            AudioManager.Play(pickUpAudio);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Play(enemyHitAudio);
            _rb.AddForce(new Vector3(0, enemyCollisionJumpForce, 0) * speed);
        }
    }
}
