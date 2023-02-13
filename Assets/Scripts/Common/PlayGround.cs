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
        public List<Enemy> Enemies { get; private set; }

        public virtual void ResetPlayGround()
        {
            scoreManager.Reset();
        
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

        protected virtual void ResetPickUps()
        {
            foreach (var pickUp in PickUps)
                pickUp.SetLocation(wallsManager.RandomLocation());
        }
    
        protected virtual void ResetEnemies()
        {
            foreach (var enemy in Enemies)
                enemy.SetLocation(wallsManager.RandomLocation());
        }

        private void Awake()
        {
            PickUps = new List<PickUp>();
            Enemies = new List<Enemy>();
        }

        private void Start()
        {
            FindCoinsAndEnemies(transform);
            ResetPlayGround();
            postProcessingEffector.EnterTrainingMode();
        }

        private void FindCoinsAndEnemies(Transform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
            
                if(!child.gameObject.activeSelf)
                    continue;
            
                if (child.CompareTag("PickUp") || child.CompareTag("Post Process Pickup"))
                    PickUps.Add(child.GetComponent<PickUp>());
            
                else if (child.CompareTag("Enemy"))
                    Enemies.Add(child.GetComponent<Enemy>());
            
                else
                    FindCoinsAndEnemies(child);
            }
        }

        public virtual bool OnPickUp(PickUp pickUp)
        {
            pickUp.SetLocation(wallsManager.RandomLocation());
            var hasWon = scoreManager.Increase();

            if(hasWon)
                menuManager.OnWinGame();
            
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
    }
}
