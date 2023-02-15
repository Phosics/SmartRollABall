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
    
        // private static bool _disabled = true;

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

        // public static void Disable()
        // {
        //     _disabled = true;
        //     foreach (var s in _allSounds)
        //         s.source.Stop();
        // }

        public static void Play(string soundName)
        {
            // if (_disabled)
            //     return;
        
            GetSoundByName(soundName)?.source.Play();
        }

        public static void Pause(string soundName)
        {
            // if (_disabled)
            //     return;
        
            GetSoundByName(soundName)?.source.Pause();
        }
    
        public static void UnPause(string soundName)
        {
            // if (_disabled)
            //     return;
        
            GetSoundByName(soundName)?.source.UnPause();
        }

        [CanBeNull]
        private static Sound GetSoundByName(string soundName)
        {
            var s = _allSounds.SingleOrDefault(sound => sound.name == soundName);
        
            if (s == null)
                Debug.LogWarning("Error: Sound " + soundName + " not found");

            return s;
        }
    }
}