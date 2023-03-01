using System.Collections.Generic;
using Common.Effects;
using Common.Menus;
using Common.Objects;
using Common.Player;
using UnityEngine;

namespace Common
{
    public class PlayGround : TrainLogicable
    {
        [Header("Managers")]
        public MenuManager menuManager;

        [Space(5)]
        [Header("Effects")]
        public PostProcessingEffector postProcessingEffector;
        public ParticlesEffector particlesEffector;
        
        [HideInInspector]
        public WallsManager wallsManager;
        public PlayerManager playerManager;
        
        private ScoreManager _scoreManager;
        private TrainingManager _trainingManager;
        private bool _isPp = true;
        
        public List<PickUp> PickUps { get; private set; }
        public List<Enemy> Enemies { get; protected set; }
        public List<MovingWall> MovingWalls { get; protected set; }
        
        public virtual void ResetPlayGround()
        {
            _scoreManager.Reset();

            ResetMovingWalls();
            ResetPickUps();
            ResetEnemies();
            playerManager.ResetPlayer();
        }

        public PickUp FindClosestPickUp(Vector3 location)
        {
            PickUp closestPickUp = null;
            float closestDistance = 0;

            foreach (var pickUp in PickUps)
            {
                var tempDistance = Vector3.Distance(location, pickUp.transform.position);
            
                if (closestPickUp == null)
                {
                    closestPickUp = pickUp;
                    closestDistance = tempDistance;
                
                    continue;
                }

                if (tempDistance >= closestDistance)
                {
                    continue;
                }
            
                closestPickUp = pickUp;
                closestDistance = tempDistance;
            }

            return closestPickUp;
        }
        
        public virtual Vector3 FindSafeLocationForPlayer()
        {
            return wallsManager.RandomLocation();
        }

        protected virtual void ResetMovingWalls()
        {
            foreach (var movingWall in MovingWalls)
            {
                movingWall.SetLocation(wallsManager.RandomLocation());
            }
        }

        protected virtual void ResetPickUps()
        {
            foreach (var pickUp in PickUps)
            {
                pickUp.SetLocation(wallsManager.RandomLocation());
            }
        }

        protected virtual void ResetEnemies()
        {
            foreach (var enemy in Enemies)
            {
                enemy.SetLocation(wallsManager.RandomLocation());
            }
        }

        private void Awake()
        {
            PickUps = new List<PickUp>();
            Enemies = new List<Enemy>();
            MovingWalls = new List<MovingWall>();
        }

        private void Start()
        {
            playerManager = GetComponentInChildren<PlayerManager>();
            wallsManager = GetComponentInChildren<WallsManager>();
            _scoreManager = GetComponentInChildren<ScoreManager>();
            _trainingManager = GetComponent<TrainingManager>();
            
            UpdateCurrentPlayer();
        }

        private void UpdateCurrentPlayer()
        {
            var isAIPlayer = SceneSettings.useAI;

            FindCoinsAndEnemies(transform);
            ResetPlayGround();
            SetPlayersActive(isAIPlayer);
        }

        private void SetPlayersActive(bool isAIPlayer)
        {
            playerManager.aiPlayer.gameObject.SetActive(isAIPlayer);
            playerManager.manualPlayer.gameObject.SetActive(!isAIPlayer);
        }

        private void FindCoinsAndEnemies(Transform parent)
        {
            for (var i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
            
                if(!child.gameObject.activeSelf)
                    continue;
            
                if (child.CompareTag("PickUp") || child.CompareTag("SpecialPickUp"))
                    PickUps.Add(child.GetComponent<PickUp>());
            
                else if (child.CompareTag("Enemy"))
                    Enemies.Add(child.GetComponent<Enemy>());

                else if (child.CompareTag("MovingWall"))
                    MovingWalls.Add(child.GetComponent<MovingWall>());

                else
                    FindCoinsAndEnemies(child);
            }
        }

        public virtual bool OnPickUp(PickUp pickUp)
        {
            if (!_trainingManager.trainingMode)
            {
                AudioManager.Play(playerManager.pickUpAudio);
                
                if (pickUp.CompareTag("SpecialPickUp"))
                {
                    _isPp = !_isPp;
                    if(_isPp)
                        postProcessingEffector.ToggleEffect();
                    else
                        particlesEffector.StartParticles(pickUp.transform.position);
                }
            }
            
            SetPickUpLocation(pickUp);
            var hasWon = _scoreManager.Increase();

            if (hasWon && menuManager != null)
            {
                menuManager.OnWinGame();   
            }

            return hasWon;
        }

        public bool IsInTrainingMode()
        {
            return _trainingManager.trainingMode;
        }

        public virtual void OnPlayerExitBoundary()
        {
            menuManager.OnLoseGame();
        }

        protected virtual void SetPickUpLocation(PickUp pickUp)
        {
            pickUp.SetLocation(wallsManager.RandomLocation());
        }
    }
}
