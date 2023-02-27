using System;
using UnityEngine;

namespace Common
{
    public class PlayerFollowingParticlesEffector : ParticlesEffector
    {
        public GameObject _player;

        private void Start()
        {
            OnStart();
        }
        
        private void FixedUpdate()
        {
            if(_particleSystem.isPlaying)
                transform.position = _player.transform.position;
        }
    }
}
