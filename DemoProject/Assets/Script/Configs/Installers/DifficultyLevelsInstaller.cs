using Script.Installers;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DifficultyLevelsInstaller", menuName = "Installers/DifficultyLevelsInstaller")]
public class DifficultyLevelsInstaller : ScriptableObjectInstaller<DifficultyLevelsInstaller>
{
    [SerializeField] DifficultyLevelsConfig difficultyLevels;
    public override void InstallBindings()
    {
        Container.BindInstance(difficultyLevels).IfNotBound();
    }
}