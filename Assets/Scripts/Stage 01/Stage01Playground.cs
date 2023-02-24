using Common;
using UnityEngine;

public class Stage01Playground : PlayGround
{
    private static readonly Collider[] Colliders = new Collider[1];
    
    protected override void ResetEnemies()
    { 
        foreach (EnemyBall enemy in Enemies) 
            enemy.SetStartLocation(wallsManager);
    }

    protected override void ResetPickUps()
    {
        foreach (CubePickUp pickUp in PickUps)
            pickUp.SetStartLocation(wallsManager);
    }

    protected override void SetPickUpLocation(PickUp pickUp)
    {
        var possibleLocation = wallsManager.RandomLocation();

        Physics.OverlapBoxNonAlloc(possibleLocation, new Vector3(0.25f, 0.25f, 0.25f),
            Colliders, pickUp.transform.rotation);
        
        while (Colliders[0] != null)
        {
            Colliders[0] = null;
            Debug.Log("PickUp collided, setting new place");
            
            possibleLocation = wallsManager.RandomLocation();

            Physics.OverlapBoxNonAlloc(possibleLocation, new Vector3(0.25f, 0.25f, 0.25f),
                Colliders, pickUp.transform.rotation);
        }
        
        pickUp.SetLocation(possibleLocation);
    }
}
