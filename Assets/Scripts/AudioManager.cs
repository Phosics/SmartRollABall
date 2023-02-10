using System.Collections;
using System;
using System.Linq;
using Unity.Audio;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        public static AudioManager instance;
        private static Sound[] allSounds;

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
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

            allSounds = sounds;
        }

        private void Start()
        {
            Play("Theme");
        }

        // Update is called once per frame
        public static void Play(string name)
        {
            var s = allSounds.SingleOrDefault(sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Error: Sound " + name + " not found");
                return;
            }
            
            s.source.Play();
        }

        public static void Pause(string name)
        {
            var s = allSounds.SingleOrDefault(sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Error: Sound " + name + " not found");
                return;
            }

            s.source.Pause();
        }
        
        public static void UnPause(string name)
        {
            var s = allSounds.SingleOrDefault(sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Error: Sound " + name + " not found");
                return;
            }

            s.source.UnPause();
        }
    }
}