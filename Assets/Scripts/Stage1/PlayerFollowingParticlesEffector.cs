using System;
using Common;
using Common.Effects;
using Common.Player;
using UnityEditor;
using UnityEngine;

namespace Stage1
{
    public class PlayerFollowingParticlesEffector : ParticlesEffector
    {
        public PlayerManager playerManager;

        private void FixedUpdate()
        {
            if(sceneParticleSystem.isPlaying)
                transform.position = playerManager.GetPlayerTransform().position;
        }
    }
}
