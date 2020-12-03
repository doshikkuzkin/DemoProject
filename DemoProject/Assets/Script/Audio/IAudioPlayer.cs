using Script.Configs;

namespace Script.Audio
{
    public interface IAudioPlayer
    {
        void PlaySound(SoundType soundType);
        void PlayBackgroundMusic();
    }
}