using UnityEngine;

namespace Common.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Players")] 
        public PlayerController manualPlayer;
        public AIPlayerContainer aiPlayerContainer;
        
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

        private bool _isCurrentPlayerInitialized = false;

        public void UpdateCurrentPlayer()
        {
            if (SceneSettings.useAI || playGround.IsInTrainingMode())
            {
                var aiPlayerAgent = aiPlayerContainer.aiPlayers[SceneSettings.brain];
                
                _currentPlayer = aiPlayerAgent;
                _currentPlayerGameObject = aiPlayerAgent.gameObject;
            }
            else
            {
                _currentPlayer = manualPlayer;
                _currentPlayerGameObject = manualPlayer.gameObject;
            }
            
            _isCurrentPlayerInitialized = true;
        }

        public void ResetPlayer()
        {
            _currentPlayer.ResetPlayer();
        }

        public Transform GetPlayerTransform()
        {
            return _isCurrentPlayerInitialized ? _currentPlayerGameObject.transform : manualPlayer.transform;
        }

        public void SetActiveAIPlayer(bool active)
        {
            for (var i = 0; i < aiPlayerContainer.aiPlayers.Length; i++)
            {
                aiPlayerContainer.aiPlayers[i].gameObject.SetActive(i == SceneSettings.brain && active);
            }
        }

        public void EndEpisode()
        {
            aiPlayerContainer.aiPlayers[SceneSettings.brain].EndEpisode();
        }
    }
}