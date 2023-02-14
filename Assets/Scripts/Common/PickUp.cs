using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class PickUp : MonoBehaviour
    {
        [Header("Y Axis Movement")]
        public bool isMoving;
        public float moveSpeed;
        private float Height = 0;

        protected const float DefaultHeight = 0.5f;

        public void SetLocation(Vector3 newLocation)
        {
            newLocation.y = isMoving ? Random.Range(1.5f, 2.5f) : DefaultHeight;
            Height = newLocation.y;
            transform.position = newLocation;
        }

        private void Update()
        {
            var pickUpPosition = transform.position;
        
            transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
            transform.position = new Vector3(pickUpPosition.x, CalculateHeight(pickUpPosition), pickUpPosition.z);
        }

        private float CalculateHeight(Vector3 position)
        {
            return isMoving ? Mathf.Lerp(Height - 1, Height + 1, Mathf.PingPong(Time.time / moveSpeed, 1)) : position.y;
        }
    }
}
