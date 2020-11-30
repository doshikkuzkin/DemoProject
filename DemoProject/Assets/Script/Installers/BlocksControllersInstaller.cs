using System.ComponentModel;
using Script.BlocksMovement;
using Script.GameControllers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Script.Installers
{
    public class BlocksControllersInstaller : MonoInstaller
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Image previewImage;
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private GameObject[] ghostPrefabs;
        [SerializeField] private Board board;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BlocksSpawner>().AsSingle().WithArguments(spawnPoint, previewImage);
            Container.Bind<Board>().FromInstance(board).AsSingle();
            
            Container.BindFactory<BlockFacade, BlockFacade.Factory>().FromSubContainerResolve().ByNewGameObjectMethod(SpawnBlock);
        }

        private void SpawnBlock(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<GameLoopController>().AsSingle();
            container.Bind<BlockFacade>().AsSingle();
            container.BindInterfacesAndSelfTo<BlocksMovementController>().AsSingle().WithArguments(spawnPoint);
            container.BindInterfacesAndSelfTo<GhostBlocksMovementController>().AsSingle();
            var prefabIndex = Random.Range(0, prefabs.Length);
            var prefab = prefabs[prefabIndex];
            var block = container.InstantiatePrefab(prefab);
            container.Bind<Block>().FromComponentOn(block).AsTransient();

            var ghostPrefab = container.InstantiatePrefab(ghostPrefabs[prefabIndex]);
            container.Bind<GhostBlock>().FromComponentOn(ghostPrefab).AsTransient();
        }
    }
}