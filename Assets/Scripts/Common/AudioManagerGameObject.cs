using UnityEngine;

namespace Common
{
    public class AudioManagerGameObject : TrainLogicable
    {
        public Sound[] sounds;

        public string playOnStart = "Theme";

        public override void EnterTrainingMode()
        {
            base.EnterTrainingMode();
            gameObject.SetActive(false);
        }

        // Use this for initialization
        private void Awake()
        {
            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
            
            AudioManager.SetAudios(sounds);
        }

        private void Start()
        {
            AudioManager.Play(playOnStart);
        }
    }
}