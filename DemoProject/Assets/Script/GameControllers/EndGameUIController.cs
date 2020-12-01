using System;
using Script.GameControllersInterfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.GameControllers
{
    public class EndGameUIController : IUIWindow, IInitializable
    {
    private GameObject _endGameUI;
    private Button _replayButton;

    public event Action OnCloseButtonPressed;

    public EndGameUIController(GameObject endGameUI, Button replayButton)
    {
        _endGameUI = endGameUI;
        _replayButton = replayButton;
    }

    public void Initialize()
    {
        _replayButton.onClick.AddListener(Replay);
    }

    public void ShowWindow()
    {
        _endGameUI.SetActive(true);
    }

    public void HideWindow()
    {
        _endGameUI.SetActive(false);
    }

    private void Replay()
    {
        OnCloseButtonPressed?.Invoke();
    }
    
    }
}