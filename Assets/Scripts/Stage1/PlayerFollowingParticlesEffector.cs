using System;
using Common;
using UnityEngine;

namespace Stage1
{
    public class PlayerFollowingParticlesEffector : ParticlesEffector
    {
        public GameObject player;

        private void FixedUpdate()
        {
            if(_particleSystem.isPlaying)
                transform.position = player.transform.position;
        }
    }
}
