using UnityEngine;
using Zenject;

namespace Script.Installers
{
    [CreateAssetMenu(fileName = "BoardSettingsInstaller", menuName = "Installers/BoardSettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField] private BoardSettings boardSettings;
        [SerializeField] private BlocksSpeedSettings blocksSpeedSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(boardSettings).IfNotBound();
            Container.BindInstance(blocksSpeedSettings).IfNotBound();
        }
    }
}