using UnityEngine;

namespace Common
{
    public class CameraController : TrainLogicable
    {
        public GameObject player;

        private Vector3 _offset;
    
        private void Start()
        {
            if (trainingMode)
                return;
        
            _offset = transform.position - player.transform.position;
        }
    
        private void LateUpdate()
        {
            if (trainingMode)
                return;
        
            transform.position = player.transform.position + _offset;
        }
    }
}
