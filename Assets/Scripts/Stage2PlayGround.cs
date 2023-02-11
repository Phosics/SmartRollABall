using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stage2PlayGround : PlayGround
{
    public GameObject enemyPrefab;

    public override void ResetPlayGround()
    {
        foreach (var enemy in Enemies.Skip(1))
        {
            if(enemy != null)
                Destroy(enemy.gameObject);
        }
        base.ResetPlayGround();
    }
    
    public override void OnPickUp(PickUp pickUp)
    {
        var newEnemy = Instantiate(enemyPrefab);
        Enemies.Add(newEnemy.GetComponent<Enemy>());
        
        newEnemy.transform.parent = transform;
        newEnemy.transform.localPosition = new Vector3(0, 0, 0);
        newEnemy.transform.rotation = Quaternion.identity;
        
        base.OnPickUp(pickUp);
    }
}
