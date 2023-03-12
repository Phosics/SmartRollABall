using Common.Objects;
using UnityEngine;

namespace Stage1
{
    public class EnemyBall : Enemy
    {
        public int speed;
    
        [Space(2)]
        [Header("Starting location")]
        public float startXFromWestWall;
        public float startZFromSouthWall;

        private static readonly int[] ForcesArray = {-1, 1};

        private const float StartY = 0.5f;
    
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void SetStartLocation(WallsManager wallsManager)
        {
            SetLocation(new Vector3(wallsManager.GetMinX(true) + startXFromWestWall, StartY, 
                wallsManager.GetMinZ(true) + startZFromSouthWall));
        }
    
        public override void SetLocation(Vector3 location)
        {
            transform.position = location;
        }
    
        private void FixedUpdate()
        {
            _rb.AddForce(GetRandomOneOrMinusOne() * speed, 0, GetRandomOneOrMinusOne() * speed);
        }

        private static int GetRandomOneOrMinusOne()
        {
            return ForcesArray[Random.Range(0, 2)];
        }
    }

}
