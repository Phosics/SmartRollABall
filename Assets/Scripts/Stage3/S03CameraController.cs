using Common.Player;
using UnityEngine;

namespace Stage3
{
    public class S03CameraController : MonoBehaviour
    {
        public PlayerManager playerManager;
        
        private void Start()
        {
            transform.position = playerManager.GetPlayerTransform().position + new Vector3(0, 5, -10);
        }
        
        private void Update()
        {
            transform.position = playerManager.GetPlayerTransform().position + new Vector3(0, 5, -10);
        }
    } 
}