using Script.BlocksMovement;
using UnityEngine;
using Zenject;

namespace Script.Installers
{
    [CreateAssetMenu(fileName = "BoardSettingsInstaller", menuName = "Installers/BoardSettingsInstaller")]
    public class BoardSettingsInstaller : ScriptableObjectInstaller<BoardSettingsInstaller>
    {
        [SerializeField] private Board.BoardSettings boardSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(boardSettings).IfNotBound();
        }
    }
}