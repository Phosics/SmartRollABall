using Common;
using UnityEngine;

public class Stage01Playground : PlayGround
{

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

}
