using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Objects;
using UnityEngine;

namespace Stage2
{
    public class Stage2PlayGround : PlayGround
    {
        public GameObject enemyPrefab;

        public override void ResetPlayGround()
        {
            if (Enemies.Count > 0)
            {
                foreach (var enemy in Enemies.Skip(1))
                {
                    if (enemy != null)
                    {
                        Destroy(enemy.gameObject);   
                    }
                }

                Enemies = new List<Enemy>() {Enemies.First()};
            }
            
            base.ResetPlayGround();
        }
    
        public override bool OnPickUp(PickUp pickUp)
        {
            var newEnemy = Instantiate(enemyPrefab);
            Enemies.Add(newEnemy.GetComponent<Enemy>());
        
            newEnemy.transform.parent = transform;
            newEnemy.transform.localPosition = new Vector3(0, 0, 0);
            newEnemy.transform.rotation = Quaternion.identity;
        
            return base.OnPickUp(pickUp);
        }
    }
}
