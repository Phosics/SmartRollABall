using Common;
using Common.Objects;
using UnityEngine;

namespace Stage1 {
    public class Stage01Playground : PlayGround
    {
        private static readonly Collider[] Colliders = new Collider[1];

        public override Vector3 FindSafeLocationForPlayer()
        {
            // Using 0.6f so that it will not touch the ground
            var possibleLocation = wallsManager.RandomLocation(0.8f);

            Physics.OverlapSphereNonAlloc(possibleLocation, 0.5f, Colliders);

            while (Colliders[0] != null)
            {
                Colliders[0] = null;

                possibleLocation = wallsManager.RandomLocation(0.8f);

                Physics.OverlapSphereNonAlloc(possibleLocation, 0.5f, Colliders);
            }

            possibleLocation.y = 0.5f;
            return possibleLocation;
        }
        
        protected override void ResetEnemies()
        {
            foreach (var enemy in Enemies)
                ((EnemyBall)enemy).SetStartLocation(wallsManager);
        }

        protected override void ResetPickUps()
        {
            foreach (var pickUp in PickUps)
                ((CubePickUp)pickUp).SetStartLocation(wallsManager);
        }
        
        protected override void SetPickUpLocation(PickUp pickUp)
        {
            var possibleLocation = wallsManager.RandomLocation();

            Physics.OverlapBoxNonAlloc(possibleLocation, new Vector3(0.25f, 0.25f, 0.25f),
                Colliders, pickUp.transform.rotation);

            while (Colliders[0] != null)
            {
                Colliders[0] = null;

                possibleLocation = wallsManager.RandomLocation();

                Physics.OverlapBoxNonAlloc(possibleLocation, new Vector3(0.25f, 0.25f, 0.25f),
                    Colliders, pickUp.transform.rotation);
            }

            pickUp.SetLocation(possibleLocation);
        }
    }
}
