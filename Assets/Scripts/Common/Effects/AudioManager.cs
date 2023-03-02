using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Common.Effects
{
    public static class AudioManager
    {
        private static Sound[] _allSounds = Array.Empty<Sound>();

        public static void SetAudios(Sound[] audios)
        {
            _allSounds = audios;
        }
        
        public static void Play(string soundName, bool trainingMode)
        {
            GetSoundByName(soundName, trainingMode)?.source.Play();
        }

        public static void Pause(string soundName, bool trainingMode)
        {
            GetSoundByName(soundName, trainingMode)?.source.Pause();
        }
    
        public static void UnPause(string soundName, bool trainingMode)
        {
            GetSoundByName(soundName, trainingMode)?.source.UnPause();
        }

        [CanBeNull]
        private static Sound GetSoundByName(string soundName, bool trainingMode)
        {
            if (trainingMode)
            {
                return null;
            }
            
            var sound = _allSounds.SingleOrDefault(sound => sound.name == soundName);
        
            if (sound == null)
                Debug.LogWarning("Sound " + soundName + " not found");

            return sound;
        }
    }
}