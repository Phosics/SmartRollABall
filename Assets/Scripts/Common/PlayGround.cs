using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class PlayGround : MonoBehaviour
    {
        public WallsManager wallsManager;
        public ScoreManager scoreManager;
        public PostProcessingEffector postProcessingEffector;
        public MenuManager menuManager;
        
        public List<PickUp> PickUps { get; private set; }
        public List<Enemy> Enemies { get; protected set; }
        public List<MovingWall> MovingWalls { get; protected set; }

        public virtual void ResetPlayGround()
        {
            scoreManager.Reset();

            ResetMovingWalls();
            ResetPickUps();
            ResetEnemies();
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

        protected virtual void ResetMovingWalls()
        {
            foreach (var MovingWall in MovingWalls)
            {
                MovingWall.SetLocation(wallsManager.RandomLocation());
            }
        }

        protected virtual void ResetPickUps()
        {
            foreach (var pickUp in PickUps)
            {
                var potentialPosition = FindSafeLocation();
                pickUp.SetLocation(potentialPosition);
            }
        }

        protected virtual void ResetEnemies()
        {
            foreach (var enemy in Enemies)
            {
                var potentialPosition = FindSafeLocation();
                enemy.SetLocation(potentialPosition);
            }
        }

        public Vector3 FindSafeLocation(float height = 0.5f)
        {
            var possibleLocation = wallsManager.RandomLocation(height);

            var Colliders = Physics.OverlapBox(possibleLocation, new Vector3(0.25f, 0.25f, 0.25f));

            while (Colliders.Length > 1 || (Colliders.Length == 1 && Colliders[0].tag != "Ground" ))
            {
                Debug.LogWarning("PickUp collided with " + Colliders[0].tag + ", setting new place");

                possibleLocation = wallsManager.RandomLocation(height);

                Colliders = Physics.OverlapBox(possibleLocation, new Vector3(0.25f, 0.25f, 0.25f));
            }

            return possibleLocation;
        }

        private void Awake()
        {
            PickUps = new List<PickUp>();
            Enemies = new List<Enemy>();
            MovingWalls= new List<MovingWall>();
        }

        private void Start()
        {
            FindCoinsAndEnemies(transform);
            ResetPlayGround();
            //postProcessingEffector.EnterTrainingMode();
        }

        private void FindCoinsAndEnemies(Transform parent)
        {
            for (var i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
            
                if(!child.gameObject.activeSelf)
                    continue;
            
                if (child.CompareTag("PickUp") || child.CompareTag("Post Process Pickup"))
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

        public virtual bool OnSpecialPickUp(PickUp pickUp)
        {
            postProcessingEffector.ToggleEffect();
            return OnPickUp(pickUp);
        }

        public virtual void OnPlayerExitBoundary()
        {
            menuManager.OnLoseGame();
        }

        protected virtual void SetPickUpLocation(PickUp pickUp)
        {
            var potentialPosition = FindSafeLocation();
            pickUp.SetLocation(potentialPosition);
        }
    }
}
