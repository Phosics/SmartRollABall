using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class S02PlayerController : MonoBehaviour
{
    private readonly Random _r = new Random();
    
    // Movement
    private Rigidbody _rb;
    public float speed;
    public float playerJumpForce;
    public float enemyCollisionJumpForce;
    private float _movementX;
    private float _movementZ;

    public AudioClip jumpAudio;
    public AudioClip pickUpAudio;
    public AudioClip spiderHitAudio;
    public AudioClip playerFallAudio;
    private AudioSource _audio;

    public S02ScoreManager s02ScoreManager;

    public UnityEvent playerFallEvent;
    public UnityEvent playerPickUpEvent;
    public UnityEvent playerSpecialPickUpEvent;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    private void OnMove(InputValue movementValue)
    {
        /*Vector2 movementVector = movementValue.Get<Vector2>();
        _movementX = movementVector.x;
        _movementZ = movementVector.y;*/
    }

    [ContextMenu("Test Jump")]
    private void OnJump()
    {
        if (!(transform.position.y < 1)) 
            return;

        _audio.PlayOneShot(jumpAudio);
        /*_rb.AddForce(new Vector3(0, playerJumpForce, 0) * speed);*/
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -2)
        {
            /*playerFallEvent.Invoke();
            _audio.PlayOneShot(playerFallAudio);*/
        }

        _rb.AddForce(new Vector3(_movementX, 0, _movementZ) * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            OnPickUp(other.gameObject);
            /*playerPickUpEvent.Invoke();*/
        }
        
        if (other.gameObject.CompareTag("SpecialPickUp"))
        {
            OnPickUp(other.gameObject);
            /*playerSpecialPickUpEvent.Invoke();*/
        }

        if (other.gameObject.CompareTag("Spider"))
        {
            _audio.PlayOneShot(spiderHitAudio);
            _rb.AddForce(new Vector3(0, enemyCollisionJumpForce, 0) * speed);
        }
    }

    private void OnPickUp(GameObject pickUpObject)
    {
        _audio.PlayOneShot(pickUpAudio);
        pickUpObject.transform.SetLocalPositionAndRotation(RandomPointInField(), pickUpObject.transform.rotation);
        s02ScoreManager.IncreaseScore();
    }

    private Vector3 RandomPointInField() =>
        new Vector3((float)_r.NextDouble() * 19 - 9.5f, 0.5f, (float)_r.NextDouble() * 19 - 9.5f);
}
