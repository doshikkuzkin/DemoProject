using System.ComponentModel;
using Script.BlocksMovement;
using Script.GameControllers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Script.Installers
{
    public class BlocksControllersInstaller : MonoInstaller
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text linesText;
        
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Image previewImage;
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private GameObject[] ghostPrefabs;
        [SerializeField] private Board board;

        [SerializeField] private GameObject endGameUICanvas;
        [SerializeField] private Button replayButton;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BlocksSpawner>().AsSingle().WithArguments(spawnPoint, previewImage);
            Container.Bind<Board>().FromInstance(board).AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EndGameUIController>().AsSingle().WithArguments(endGameUICanvas, replayButton);
            
            Container.BindFactory<BlockFacade, BlockFacade.Factory>().FromSubContainerResolve().ByNewGameObjectMethod(SpawnBlock);
            
            
            Container.BindInterfacesAndSelfTo<BlocksMovementController>().AsSingle().WithArguments(spawnPoint);
            Container.BindInterfacesAndSelfTo<GhostBlocksMovementController>().AsSingle();

            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreUIController>().AsSingle().WithArguments(scoreText, linesText, levelText).NonLazy();
        }

        private void SpawnBlock(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<BlockFacade>().AsSingle();
            var prefabIndex = Random.Range(0, prefabs.Length);
            var prefab = prefabs[prefabIndex];
            var block = container.InstantiatePrefab(prefab);
            container.Bind<Block>().FromComponentOn(block).AsTransient();

            var ghostPrefab = container.InstantiatePrefab(ghostPrefabs[prefabIndex]);
            container.Bind<GhostBlock>().FromComponentOn(ghostPrefab).AsTransient();
        }
    }
}