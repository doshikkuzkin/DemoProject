using System;
using Script.Installers;
using UnityEngine;
using Zenject;

namespace Script.Audio
{
    public class AudioPlayer : IInitializable
    {
        public static AudioPlayer Instance;
        
        private AudioConfig _audioConfig;
        private AudioSource _musicAudioSource;
        private AudioSource _soundsAudioSource;

        private AudioPlayer(AudioSource musicAudioSource, AudioSource soundsAudioSource, AudioConfig audioConfig)
        {
            _audioConfig = audioConfig;
            _musicAudioSource = musicAudioSource;
            _soundsAudioSource = soundsAudioSource;
            
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void Initialize()
        {
            _musicAudioSource.clip = _audioConfig.BackgroundMusic;
            _musicAudioSource.Play();
        }

        public void PlaySound(SoundType soundType)
        {
            var clip = GetSound(soundType);
            if (clip == null)
            {
                throw new Exception($"Sound of type {soundType} is not assigned in AudioConfig");
            }
            _soundsAudioSource.PlayOneShot(clip);
        }

        private AudioClip GetSound(SoundType soundType)
        {
            AudioClip clip = null;
            foreach (var sound in _audioConfig.Sounds)
            {
                if (sound.SoundType == soundType)
                {
                    clip = sound.Clip;
                    break;
                }
            }
            return clip;
        }
    }
}