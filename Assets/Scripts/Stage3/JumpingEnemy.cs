using Common.Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Stage3
{
    public class JumpingEnemy : Enemy
    {
        public UnityEvent miniJumping;
        private float startYLocation;
        private Vector2 move;

        private void Start()
        {
            startYLocation = transform.position.y;
        }

        void FixedUpdate()
        {
            transform.position = new Vector3(transform.position.x + move.x, startYLocation+0.025f, transform.position.z + move.y);
        }

        public override void SetLocation(Vector3 position)
        {
            position.y = startYLocation;
            transform.position = position;
            SetMove();
        }

        private void SetMove()
        {
            move = new Vector2(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
            transform.rotation = Quaternion.identity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (miniJumping != null)
                {
                    miniJumping.Invoke();
                }
                SetMove();
            }        
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                SetMove();
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                SetMove();
            }
        }
    }
}
