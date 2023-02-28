using System;
using Common;
using UnityEngine;

namespace Stage3
{
    public class StaticParticlesEffector : ParticlesEffector
    {
        public Vector3 staticPosition;

        public override void StartParticles(Vector3 location)
        {
            base.StartParticles(staticPosition);
        }
    }
}
