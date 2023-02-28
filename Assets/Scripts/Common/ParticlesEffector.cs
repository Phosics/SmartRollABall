using System;
using UnityEngine;

namespace Common
{
    public class ParticlesEffector : TrainLogicable
    {
        protected ParticleSystem _particleSystem;

        protected void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystem.Stop();
        }

        public virtual void StartParticles(Vector3 location)
        {
            if (trainingMode)
                return;

            if(_particleSystem.isPlaying)
                return;

            transform.position = location;
            _particleSystem.Play();
        }
    }
}
