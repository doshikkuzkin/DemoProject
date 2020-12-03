using System;
using UnityEngine;

namespace Script.Configs
{
    [Serializable]
    public class AudioConfig
    {
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private Sound[] sounds;

        public Sound[] Sounds => sounds;

        public AudioClip BackgroundMusic => backgroundMusic;
    }
}