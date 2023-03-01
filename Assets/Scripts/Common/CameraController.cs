using Common.Player;
using UnityEngine;

namespace Common
{
    public class CameraController : MonoBehaviour
    {
        public PlayerManager playerManager;

        private Vector3 _offset;
    
        private void Start()
        {
            _offset = transform.position - playerManager.GetPlayerTransform().position;
        }
    
        private void LateUpdate()
        {
            transform.position = playerManager.GetPlayerTransform().position + _offset;
        }
    }
}
