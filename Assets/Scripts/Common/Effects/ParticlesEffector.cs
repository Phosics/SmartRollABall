using UnityEngine;

namespace Common.Effects
{
    public class ParticlesEffector : MonoBehaviour
    {
        protected ParticleSystem sceneParticleSystem;

        protected void Start()
        {
            sceneParticleSystem = GetComponent<ParticleSystem>();
            sceneParticleSystem.Stop();
        }

        public virtual void StartParticles(Vector3 location)
        {
            if(sceneParticleSystem.isPlaying)
                return;

            transform.position = location;
            sceneParticleSystem.Play();
        }

        public void StopEffect()
        {
            if (!sceneParticleSystem.isPlaying)
            {
                return;
            }
            
            sceneParticleSystem.Stop();
            sceneParticleSystem.Clear();
        }
    }
}
