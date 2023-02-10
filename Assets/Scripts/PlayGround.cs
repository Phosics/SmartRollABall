using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PlayGround : MonoBehaviour
{
    public WallsManager wallsManager;
    public ScoreManager scoreManager;
    public List<PickUp> PickUps { get; private set; }
    public List<Enemy> Enemies { get; private set; }

    public virtual void ResetPlayGround()
    {
        scoreManager.Reset();
        
        foreach (var pickUp in PickUps)
            pickUp.SetStartLocation(wallsManager.RandomLocation());
        
        foreach (var enemy in Enemies)
            enemy.SetStartLocation(wallsManager.RandomLocation());
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
    }

    private void FindCoinsAndEnemies(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            if (child.CompareTag("PickUp"))
                PickUps.Add(child.GetComponent<PickUp>());
            
            else if (child.CompareTag("Enemy"))
                Enemies.Add(child.GetComponent<Enemy>());
            
            else
                FindCoinsAndEnemies(child);
        }
    }

    public virtual void OnPickUp(PickUp pickUp)
    {
        pickUp.SetStartLocation(wallsManager.RandomLocation());
        scoreManager.Increase();
    }
}
