using Script.Audio;
using Script.BlocksMovement;
using Script.GameControllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameControllersInstaller : MonoInstaller
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Image previewImage;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundsAudioSource;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle().WithArguments(musicAudioSource, soundsAudioSource).NonLazy();
        Container.BindInterfacesAndSelfTo<BlocksSpawner>().AsSingle().WithArguments(spawnPoint, previewImage);
        Container.BindInterfacesAndSelfTo<BlockMovement>().AsSingle();
        Container.BindInterfacesAndSelfTo<GhostBlockMovement>().AsSingle();
        Container.BindInterfacesAndSelfTo<Board>().AsSingle().WithArguments(spawnPoint);

        Container.BindInterfacesAndSelfTo<ScoreModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreProcessor>().AsSingle();
    }
}