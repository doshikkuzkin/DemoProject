using Script.BlocksMovement;
using UnityEngine;
using UnityEngine.UI;
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
            var block = container.InstantiatePrefab(prefab);
            container.Bind<Block>().FromComponentOn(block).AsTransient();

            var ghostPrefab = container.InstantiatePrefab(ghostPrefabs[prefabIndex]);
            container.Bind<GhostBlock>().FromComponentOn(ghostPrefab).AsTransient();
        }
    }
}