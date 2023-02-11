using System.Collections;
using System.Collections.Generic;
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
        
        ResetPickUps();
        ResetEnemies();
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
    }

    private void FindCoinsAndEnemies(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            if (child.CompareTag("PickUp") || child.CompareTag("Post Process Pickup"))
                PickUps.Add(child.GetComponent<PickUp>());
            
            else if (child.CompareTag("Enemy"))
                Enemies.Add(child.GetComponent<Enemy>());
            
            else
                FindCoinsAndEnemies(child);
        }
    }

    public virtual void OnPickUp(PickUp pickUp)
    {
        pickUp.SetLocation(wallsManager.RandomLocation());
        scoreManager.Increase();
    }
}
