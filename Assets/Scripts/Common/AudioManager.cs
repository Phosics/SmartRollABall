using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Common
{
    public class AudioManager
    {
        private static Sound[] _allSounds;

        public static void SetAudios(Sound[] audios)
        {
            _allSounds = audios;
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