using Script.BlocksMovement;
using Script.GameControllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameControllersInstaller : MonoInstaller
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Image previewImage;
    [SerializeField] private Board board;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<BlocksSpawner>().AsSingle().WithArguments(spawnPoint, previewImage);
        Container.BindInterfacesAndSelfTo<BlocksMovementController>().AsSingle();
        Container.BindInterfacesAndSelfTo<GhostBlocksMovementController>().AsSingle();
        Container.Bind<Board>().FromInstance(board).AsSingle();
        Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameLoopController>().AsSingle().NonLazy();
    }
}