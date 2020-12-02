using Script.Installers;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "AudioConfigInstaller", menuName = "Installers/AudioConfigInstaller")]
public class AudioConfigInstaller : ScriptableObjectInstaller<AudioConfigInstaller>
{
    [SerializeField] private AudioConfig _audioConfig;
    public override void InstallBindings()
    {
        Container.BindInstance(_audioConfig).IfNotBound();
    }
}