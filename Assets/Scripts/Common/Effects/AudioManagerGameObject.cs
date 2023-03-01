using UnityEngine;

namespace Common.Effects
{
    public class AudioManagerGameObject : MonoBehaviour
    {
        public Sound[] sounds;

        public string playOnStart = "Theme";

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