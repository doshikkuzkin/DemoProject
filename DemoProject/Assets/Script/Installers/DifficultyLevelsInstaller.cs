using Script.GameControllers;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DifficultyLevelsInstaller", menuName = "Installers/DifficultyLevelsInstaller")]
public class DifficultyLevelsInstaller : ScriptableObjectInstaller<DifficultyLevelsInstaller>
{
    [SerializeField] private ScoreController.DifficultyLevels difficultyLevels;
    public override void InstallBindings()
    {
        Container.BindInstance(difficultyLevels).IfNotBound();
    }
}