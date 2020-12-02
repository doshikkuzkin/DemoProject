using System;
using UnityEngine;

namespace Script.Installers
{
    [Serializable]
    public class Sound
    {
        [SerializeField] private SoundType soundType;
        [SerializeField] private AudioClip clip;

        public SoundType SoundType => soundType;

        public AudioClip Clip => clip;
    }
}