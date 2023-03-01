using System;
using UnityEngine;

namespace Common.Effects
{
    [Serializable]
    public class Sound
    {
        [Header("Audio Data")]
        public AudioClip clip;
        public string name;
    
        [Space(5)]
        [Header("Audio Parameters")]
        public bool loop;
        [Range(0f, 1f)]
        public float volume;

        [Range(.1f, 3f)]
        public float pitch;
    
        [HideInInspector]
        public AudioSource source;

    }
}
