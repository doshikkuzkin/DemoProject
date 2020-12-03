using Script.BlocksMovement;
using UnityEngine;
using Zenject;

namespace Script.Installers
{
    public class BlocksInstaller : MonoInstaller
    {
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private GameObject[] ghostPrefabs;
        
        public override void InstallBindings()
        {
            Container.BindFactory<BlockFacade, BlockFacade.Factory>().FromSubContainerResolve().ByNewGameObjectMethod(SpawnBlock);
        }

        private void SpawnBlock(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<BlockFacade>().AsSingle();
            var prefabIndex = Random.Range(0, prefabs.Length);
            var prefab = prefabs[prefabIndex];
            var block = container.InstantiatePrefabForComponent<Block>(prefab);
            container.Bind<Block>().FromInstance(block).AsSingle();

            var ghostPrefab = container.InstantiatePrefabForComponent<GhostBlock>(ghostPrefabs[prefabIndex]);
            container.Bind<GhostBlock>().FromInstance(ghostPrefab).AsSingle();
        }
    }
}