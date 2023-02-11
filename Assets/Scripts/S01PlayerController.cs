using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Random = System.Random;

public class S01PlayerController : MonoBehaviour
{
    private const float Floor = 0.5f;

    private static readonly Random Rand = new();
    
    public float speed;
    public float jump;
    public ScoreManager scoreManager;
    public UnityEvent onLavaTouch;
    public S01PostProcessManager postProcessManager;
    
    private Rigidbody _rb;
    private float _movementX;
    private float _movementY;
    private float _movementZ;

    private Vector3 _startingPosition;

    public void Reset()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.position = _startingPosition;
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(_movementX, _movementZ, _movementY);
        _rb.AddForce(movement * speed);
        _movementZ = 0;
    }

    private void OnJump()
    {
        if (transform.position.y - Floor <= 0.5f)
        {
            _movementZ = jump;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup") || other.gameObject.CompareTag("Post Process Pickup"))
        {
            HandlePickup(other);
        } 
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            HandleObstacle();
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            onLavaTouch.Invoke();
        }
    }

    private void HandleObstacle()
    {
        _movementZ = jump * 3;
    }

    private void HandlePickup(Collider other)
    {
        other.gameObject.transform.position = new Vector3(Rand.Next(-9, 9), other.gameObject.transform.position.y, Rand.Next(-9, 9));
        AudioManager.Play("Pickup");
        scoreManager.Increase();

        if (other.gameObject.CompareTag("Post Process Pickup"))
        {
            postProcessManager.ActiveEffect();
        }
    }
}
