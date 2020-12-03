using UnityEngine;
using Zenject;

namespace Script.Configs.Installers
{
    [CreateAssetMenu(fileName = "AudioConfigInstaller", menuName = "Installers/AudioConfigInstaller")]
    public class AudioConfigInstaller : ScriptableObjectInstaller<AudioConfigInstaller>
    {
        [SerializeField] private AudioConfig audioConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(audioConfig).IfNotBound();
        }
    }
}