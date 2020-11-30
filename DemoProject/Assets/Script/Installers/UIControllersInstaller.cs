using Script.GameControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIControllersInstaller : MonoInstaller
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text linesText;
    
    [SerializeField] private GameObject endGameUICanvas;
    [SerializeField] private Button replayButton;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EndGameUIController>().AsSingle().WithArguments(endGameUICanvas, replayButton);
        Container.BindInterfacesAndSelfTo<ScoreUIController>().AsSingle().WithArguments(scoreText, linesText, levelText).NonLazy();
    }
}