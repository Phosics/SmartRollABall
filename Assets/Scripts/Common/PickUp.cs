using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class PickUp : MonoBehaviour
    {
        [Header("Y Axis Movement")]
        public bool isMoving;
        public float moveSpeed;

        protected const float DefaultHeight = 0.5f;

        public void SetLocation(Vector3 newLocation)
        {
            newLocation.y = isMoving ? Random.Range(1.5f, 2.5f) : DefaultHeight;
            transform.position = newLocation;
        }

        private void Update()
        {
            var pickUpTransform = transform;
            var pickUpPosition = pickUpTransform.position;
        
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
            pickUpTransform.position = new Vector3(pickUpPosition.x, CalculateHeight(pickUpPosition), pickUpPosition.z);
        }

        private float CalculateHeight(Vector3 position)
        {
            return isMoving ? Mathf.PingPong((position.y + Time.time) * moveSpeed, 1) * 2 - 1 : position.y;
        }
    }
}
