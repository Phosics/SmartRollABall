using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Common
{
    public class AudioManager : TrainLogicable
    {
        public Sound[] sounds;

        public string playOnStart = "Theme";

        private static AudioManager _instance;
        private static Sound[] _allSounds;

        public override void EnterTrainingMode()
        {
            base.EnterTrainingMode();
            gameObject.SetActive(false);
        }

        // Use this for initialization
        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            foreach (var s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }

            _allSounds = sounds;
        }

        private void Start()
        {
            Play(playOnStart);
        }

        public static void Play(string soundName)
        {
            GetSoundByName(soundName)?.source.Play();
        }

        public static void Pause(string soundName)
        {
            GetSoundByName(soundName)?.source.Pause();
        }
    
        public static void UnPause(string soundName)
        {
            GetSoundByName(soundName)?.source.UnPause();
        }

        [CanBeNull]
        private static Sound GetSoundByName(string soundName)
        {
            var sound = _allSounds.SingleOrDefault(sound => sound.name == soundName);
        
            if (sound == null)
                Debug.LogWarning("Error: Sound " + soundName + " not found");

            return sound;
        }
    }
}