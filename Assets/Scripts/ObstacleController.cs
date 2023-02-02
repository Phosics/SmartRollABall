using UnityEngine;
using Random = System.Random;

public class ObstacleController : MonoBehaviour
{
    public int speed;

    private static readonly int[] ForcesArray = {-1, 1};
    
    private static readonly Random Rand = new();

    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        _rb.AddForce(getRandomOneOrMinusOne() * speed, 0, getRandomOneOrMinusOne() * speed);
    }
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.gameObject.CompareTag("Wall"))
    //     {
    //         return;
    //     }
    //
    //     _rb.transform.position = new Vector3(Rand.Next(-9, 9), _rb.transform.position.y, Rand.Next(-9, 9));
    // }

    private int getRandomOneOrMinusOne()
    {
        return ForcesArray[Rand.Next(2)];
    }
}
