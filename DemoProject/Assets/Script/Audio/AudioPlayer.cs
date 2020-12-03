using System;
using Script.Configs;
using UnityEngine;

namespace Script.Audio
{
    public class AudioPlayer : IAudioPlayer
    {
        private AudioConfig _audioConfig;
        private AudioSource _musicAudioSource;
        private AudioSource _soundsAudioSource;

        private AudioPlayer(AudioSource musicAudioSource, AudioSource soundsAudioSource, AudioConfig audioConfig)
        {
            _audioConfig = audioConfig;
            _musicAudioSource = musicAudioSource;
            _soundsAudioSource = soundsAudioSource;
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

        public void PlayBackgroundMusic()
        {
            _musicAudioSource.clip = _audioConfig.BackgroundMusic;
            _musicAudioSource.Play();
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