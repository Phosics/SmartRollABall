using System;
using UnityEngine;

namespace Common
{
    public class ParticlesEffector : TrainLogicable
    {
        protected ParticleSystem _particleSystem;

        private void Start()
        {
            OnStart();
        }

        protected void OnStart()
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
