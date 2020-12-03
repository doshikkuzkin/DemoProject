using UnityEngine;
using Zenject;

namespace Script.Configs.Installers
{
    [CreateAssetMenu(fileName = "DifficultyLevelsInstaller", menuName = "Installers/DifficultyLevelsInstaller")]
    public class DifficultyLevelsInstaller : ScriptableObjectInstaller<DifficultyLevelsInstaller>
    {
        [SerializeField] DifficultyLevelsConfig difficultyLevels;
        public override void InstallBindings()
        {
            Container.BindInstance(difficultyLevels).IfNotBound();
        }
    }
}