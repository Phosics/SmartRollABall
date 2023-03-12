using Common.Effects;
using Common.Player;

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
