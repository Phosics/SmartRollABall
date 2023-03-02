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

        public void UpdateCurrentPlayer()
        {
            if (SceneSettings.useAI || playGround.IsInTrainingMode())
            {
                _currentPlayer = aiPlayer;
                _currentPlayerGameObject = aiPlayer.gameObject;
            }
            else
            {
                _currentPlayer = manualPlayer;
                _currentPlayerGameObject = manualPlayer.gameObject;
            }
        }

        public void ResetPlayer()
        {
            _currentPlayer.ResetPlayer();
        }

        public Transform GetPlayerTransform()
        {
            if (_currentPlayerGameObject != null)
            {
                return _currentPlayerGameObject.transform;
            }

            return manualPlayer.transform;
        }
    }
}