using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class PlayGround : MonoBehaviour
    {
        public WallsManager wallsManager;
        public ScoreManager scoreManager;
        public PostProcessingEffector postProcessingEffector;
        public ParticlesEffector particlesEffector;
        public MenuManager menuManager;
        
        public List<PickUp> PickUps { get; private set; }
        public List<Enemy> Enemies { get; protected set; }
        public List<MovingWall> MovingWalls { get; protected set; }
        
        
        private PlayerController _player;

        public virtual void ResetPlayGround()
        {
            scoreManager.Reset();

            ResetMovingWalls();
            ResetPickUps();
            ResetEnemies();
            _player.ResetPlayer();
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
            _player = GetComponentInChildren<PlayerController>();
            FindCoinsAndEnemies(transform);
            ResetPlayGround();
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
            SetPickUpLocation(pickUp);
            var hasWon = scoreManager.Increase();

            if (hasWon && menuManager != null)
            {
                menuManager.OnWinGame();   
            }

            return hasWon;
        }

        private bool _isPp = true;
        public virtual bool OnSpecialPickUp(PickUp pickUp)
        {
            _isPp = !_isPp;
            if(_isPp)
                postProcessingEffector.ToggleEffect();
            else
                particlesEffector.StartParticles(pickUp.transform.position);
            
            return OnPickUp(pickUp);
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
