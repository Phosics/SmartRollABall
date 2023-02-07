using UnityEngine;

public class S01ObstacleController : MonoBehaviour
{
    public int speed;

    private static readonly int[] ForcesArray = {-1, 1};

    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        _rb.AddForce(GetRandomOneOrMinusOne() * speed, 0, GetRandomOneOrMinusOne() * speed);
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

    private int GetRandomOneOrMinusOne()
    {
        return ForcesArray[Random.Range(0, 2)];
    }
}
