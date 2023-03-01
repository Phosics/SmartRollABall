using UnityEngine;

namespace Common.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Players")] 
        public PlayerController manualPlayer;
        public AIPlayerAgent aiPlayer;
        
        [Space(5)]
        [Header("Player Attributes")]
        public float speed = 10f;
        public float jump = 20f;
        public float enemyCollisionJumpForce = 40f;
        
        [Space(5)]
        [Header("Player Audios")]
        public string jumpAudio;
        public string pickUpAudio;
        public string enemyHitAudio;
        
        [Space(5)]
        [Header("Other Objects")]
        public PlayGround playGround;

        private IPlayerFunctions _currentPlayer;
        private GameObject _currentPlayerGameObject;

        private void Awake()
        {
            if (!SceneSettings.useAI)
            {
                _currentPlayer = manualPlayer;
                _currentPlayerGameObject = manualPlayer.gameObject;
            }
            else
            {
                _currentPlayer = aiPlayer;
                _currentPlayerGameObject = aiPlayer.gameObject;
            }
        }

        public void ResetPlayer()
        {
            _currentPlayer.ResetPlayer();
        }

        public Transform GetPlayerTransform()
        {
            return _currentPlayerGameObject.transform;
        }
    }
}