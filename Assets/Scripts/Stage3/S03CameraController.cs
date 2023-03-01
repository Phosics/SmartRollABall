using Common.Player;
using UnityEngine;

namespace Stage3
{
    public class S03CameraController : MonoBehaviour
    {
        public PlayerManager playerManager;
        
        // Start is called before the first frame update
        private void Start()
        {
            transform.position = playerManager.GetPlayerTransform().position + new Vector3(0, 5, -10);
        }

        // Update is called once per frame
        private void Update()
        {
            transform.position = playerManager.GetPlayerTransform().position + new Vector3(0, 5, -10);
        }
    } 
}